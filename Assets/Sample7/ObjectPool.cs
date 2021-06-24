using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class PoolItemInfo
    {
        public string name;
        public List<GameObject> items = new List<GameObject>();
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
            newItem.SetActive(false);
            newItem.transform.parent = parent;
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
                result.SetActive(true);
                return result;
            }
            result = items[0];
            items.Remove(result);
            result.transform.parent = null;
            result.SetActive(true);

            result.transform.SetPositionAndRotation(
                original.transform.position
                , original.transform.rotation);

            return result;
        }
    }

    public List<PoolItemInfo> pool = new List<PoolItemInfo>();
    static ObjectPool instance;
    private void Awake()
    {
        instance = this;
	}

	public static GameObject Instantiate(GameObject go)
	{
		return instance.InstantiateGo(go);
	}

    new public static void Destroy(Object obj, float t)
    {
        instance.DestroyGo(obj, t);
    }
    private GameObject InstantiateGo(GameObject original)
    {
        PoolItemInfo find = pool.Find(x => x.name == original.name);
        if(find == null)
        {
            find = RegistPool(original);
        }

        return find.Pop(original);
    }
    public int initCount = 5;
    private PoolItemInfo RegistPool(GameObject original)
    {
        PoolItemInfo newPoolItem = new PoolItemInfo(original, initCount, transform);
        pool.Add(newPoolItem);
        return newPoolItem;
    }

    private void DestroyGo(Object obj, float t)
    {
        StartCoroutine(DestroyGoCo((GameObject)obj, t));
    }
    private IEnumerator DestroyGoCo(GameObject original, float t)
    {
        yield return new WaitForSeconds(t);

        PoolItemInfo find = pool.Find(x => x.name == original.name);
        if (find != null)
            find.Push(original);
    }
}
