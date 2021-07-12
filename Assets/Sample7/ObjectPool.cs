using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class PoolItemInfo
    {
        public string name;
        public HashSet<GameObject> items = new HashSet<GameObject>(); //HashSet은 중복을 허용하지 않는다.
        public Transform parent;

        public PoolItemInfo(GameObject original, int initCount, Transform transform)
        {
            name = original.name;
            this.parent = transform;

            for (int i = 0; i < initCount; i++)
            {
                var newItem = Object.Instantiate(original);
                newItem.name = original.name;

                InsertPoolItem(newItem);
            }
        }

        private void InsertPoolItem(GameObject newItem)
        {
            if (items.Contains(newItem))
                return;

            newItem.SetActive(false);
            newItem.transform.parent = parent;

            //if (items.Find(x => x == newItem) == null)
            //    items.Add(newItem);

            //GameObject find = items.Find(x => x == newItem);
            //if (find == null)
            items.Add(newItem);
        }

        public void Push(GameObject newItem)
        {
            InsertPoolItem(newItem);
        }

        public GameObject Pop(GameObject original)
        {
            GameObject result = null;
            if(items.Count == 0)
            {
                //result 생고 생성.
                result = Object.Instantiate(original);
                result.name = original.name;
                //result.SetActive(true);
                return result;
            }
            result = items.First();
            items.Remove(result);
            result.transform.parent = null;
            result.SetActive(true);

            result.transform.SetPositionAndRotation(
                original.transform.position
                , original.transform.rotation);

            return result;
        }
    }

    public List<PoolItemInfo> poolList = new List<PoolItemInfo>();
    static ObjectPool m_instance;
    static ObjectPool Instance
    {
        get
        {
            if (m_instance == null)
            {
                GameObject go = new GameObject(nameof(ObjectPool), typeof(ObjectPool));
                m_instance = go.GetComponent<ObjectPool>();

                DontDestroyOnLoad(go);
            }

            return m_instance;
        }
    }

    private void Awake()
    {
        m_instance = this;
	}

    new public static T Instantiate<T>(T go) where T : Component
    {
        return Instance.InstantiateGo(go.gameObject).GetComponent<T>();
    }
    public static GameObject Instantiate(GameObject go)
    {
        return Instance.InstantiateGo(go);
    }

    new public static void Destroy(Object obj, float t)
    {
        Instance.DestroyGo(obj, t);
    }
    GameObject InstantiateGo(GameObject original)
    {
        PoolItemInfo find = poolList.Find(x => x.name == original.name);

        //PoolItemInfo find = null;//
        //for (int i = 0; i < pool.Count; i++)
        //{
        //    if (pool[i].name == original.name)
        //    {
        //        find = pool[i];
        //        break;
        //    }
        //}


        if (find == null)
        {
            find = RegistPool(original);
        }

        return find.Pop(original);
    }
    public int initCount = 5;
    private PoolItemInfo RegistPool(GameObject original)
    {
        PoolItemInfo newPoolItem = new PoolItemInfo(original, initCount, transform);
        poolList.Add(newPoolItem);
        return newPoolItem;
    }

    private void DestroyGo(Object obj, float t)
    {
        StartCoroutine(DestroyGoCo((GameObject)obj, t));
    }
    private IEnumerator DestroyGoCo(GameObject original, float t)
    {
        yield return new WaitForSeconds(t);

        PoolItemInfo find = poolList.Find(x => x.name == original.name);
        if (find != null)
            find.Push(original);

        //find?.Push(original);


        //poolList.Find(x => x.name == original.name)?.Push(original);
    }
}
