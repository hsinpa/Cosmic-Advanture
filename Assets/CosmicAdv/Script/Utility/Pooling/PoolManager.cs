using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {

	private Dictionary<int, Queue<GameObject>>poolDictionary = new Dictionary<int, Queue<GameObject>>();

	static PoolManager _instance;

	public static PoolManager instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType<PoolManager>();
			}
			return _instance;
		}
	}

	public void CreatePool(GameObject prefab, int poolkey, int p_poolsize) {

		if (!poolDictionary.ContainsKey(poolkey)) {
			poolDictionary.Add(poolkey, new Queue<GameObject>());
			for (int i = 0; i < p_poolsize; i++) {
                if (poolDictionary[poolkey].Count > p_poolsize) break;

				GameObject newObject = Instantiate(prefab) as GameObject;
				newObject.name = newObject.name +"-"+i;
				newObject.transform.SetParent(this.transform);
				newObject.SetActive(false);
				poolDictionary[poolkey].Enqueue(newObject);
			}
		}
	}

	public GameObject ReuseObject(int poolKey) {
		if (poolDictionary.ContainsKey(poolKey)) {
			GameObject objectToReuse = poolDictionary[poolKey].Dequeue();
			
			poolDictionary[poolKey].Enqueue( objectToReuse );
			return objectToReuse;
		}
		return null;
	}

	public void Destroy(GameObject p_gameobject) {
		p_gameobject.transform.position = Vector3.zero;
		p_gameobject.transform.SetParent(this.transform);
		p_gameobject.SetActive(false);
	}
	
}
