using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
생성과 파괴 대체
언제 생성할 것인가?
	처음에 모두 생성.
	필요할때마다 생성.
언제 삭제할 것인가?
	삭제 하지 않기(씬 넘어가면 삭제)
	최대개수를 넘었다면 삭제 
 */
public class ObjectPool : MonoBehaviour
{
	public static ObjectPool instance;
    private void Awake()
    {
		instance = this;
	}

	public int initCount = 5;

	[System.Serializable]
    public class PoolItemInfo
    {
		public string name;
		public GameObject origin;
		public List<GameObject> items = new List<GameObject>();
		Transform parent;

        public PoolItemInfo(GameObject go, int initCount, Transform parent)
		{
            origin = go;
            name = go.name;
			this.parent = parent;

			for (int i = 0; i < initCount; i++)
            {
                CreateNewItem(go);
            }
        }

        private GameObject CreateNewItem(GameObject go, bool insertPool = true)
        {
			bool active = go.activeSelf;
			go.SetActive(false);
            var newGo = Object.Instantiate(go);
			go.SetActive(active);
			newGo.name = go.name;
			if (insertPool)
			{
				newGo.transform.parent = parent;
				items.Add(newGo);
			}
			newGo.SetActive(true);
			return newGo;
		}

        internal GameObject Pop()
        {
			if (items.Count == 0)
            {
				return CreateNewItem(origin, false);
			}

			var popItem = items[0];
			items.RemoveAt(0);

			popItem.transform.parent = null;
			popItem.SetActive(true);
			return popItem;
        }

        internal void Push(GameObject pushItem)
        {
			pushItem.SetActive(false);
			pushItem.transform.parent = parent;
			items.Add(pushItem);
        }
    }

	public List<PoolItemInfo> pool = new List<PoolItemInfo>();

	private void DestroyGo(GameObject go)
	{
		var find = pool.Find(x => x.origin.name == go.name.Replace("(Clone)", string.Empty));
		if (find == null)
		{
			find = new PoolItemInfo(go, 0, transform);
			pool.Add(find);
		}

		find.Push(go);
	}

    GameObject InstantiateGo(GameObject go)
	{
		var find = pool.Find(x => x.origin == go);
		if (find == null)
		{
			find = RegistPool(go);
		}

		return find.Pop();
	}

    private PoolItemInfo RegistPool(GameObject go)
    {
		PoolItemInfo poolItem = new PoolItemInfo(go, initCount, transform);
		pool.Add(poolItem);
		return poolItem;
	}
	#region Instantiate 래핑
	new public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Component
	{
		var newGo = Instantiate(original.gameObject);
		newGo.transform.SetPositionAndRotation(position, rotation);
		return newGo.GetComponent<T>();
	}
	public static GameObject Instantiate(GameObject go)
	{
		return instance.InstantiateGo(go);
	}
	#endregion Instantiate 래핑

	#region Destroy 래핑
	public static void Destroy(GameObject go)
	{
		instance.DestroyGo(go);
	}
	public static void Destroy(GameObject obj, float t)
	{
		instance.DestroyGo(obj, t);
	}

	private void DestroyGo(GameObject obj, float t)
	{
		StartCoroutine(DestroyGoCo(obj, t));
	}

	private IEnumerator DestroyGoCo(GameObject obj, float t)
	{
		yield return new WaitForSeconds(t);
		DestroyGo(obj);
	}
	#endregion Destroy 래핑

}
