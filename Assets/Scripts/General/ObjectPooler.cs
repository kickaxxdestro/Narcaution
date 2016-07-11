using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {
	
	public GameObject pooledObject;
	public int pooledAmount = 20;
	public bool willGrow = true;
	
	public GameObject[] pooledObjects;
	//public List<GameObject> pooledObjects;
	
	void Awake()
	{
		pooledObjects = new GameObject[pooledAmount];
		//pooledObjects = new GameObject[pooledAmount];
		
		//setting the said amount of pooled objects into the pooledObject List
		for(int i = 0; i < pooledAmount; ++i)
		{
            GameObject go = Instantiate(pooledObject) as GameObject;
			go.SetActive(false);
			pooledObjects[i] = go;
		}
	}
	
//	// Use this for initialization
//	void Start ()
//	{
//		pooledObjects = new GameObject[pooledAmount];
//		//pooledObjects = new GameObject[pooledAmount];
//		
//		//setting the said amount of pooled objects into the pooledObject List
//		for(int i = 0; i < pooledAmount; ++i)
//		{
//			GameObject go = Instantiate(pooledObject) as GameObject;
//			go.SetActive(false);
//			pooledObjects[i] = go;
//		}
//	}
	
	public GameObject GetPooledObject()
	{
		//return the first pooledObject from the List that is not active
		for(int i = 0; i < pooledObjects.Length; ++i)
		{
			if(pooledObjects[i].activeInHierarchy == false)
			{
				return pooledObjects[i];
			}
		}
		
		//if there are no objects to be that is active, it will dynamically expand the size of the List.
		if(willGrow == true)
		{
			//resize the array then add the new object at the end
			pooledAmount += 1;
			System.Array.Resize(ref pooledObjects, pooledAmount);
            GameObject go = Instantiate(pooledObject) as GameObject;
			go.SetActive(false);
			pooledObjects[pooledAmount - 1] = go;

			return go;
		}
		
		return null;
	}
	
}
