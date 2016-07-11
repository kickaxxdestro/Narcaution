using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;

public class EnemyPatternBehaviour : MonoBehaviour {
	
	public GameObject enemyObject;
	public enum PatternTypes
	{
		singleType,
		repeatType,
		oneShotType
	};
	public PatternTypes type;
	
	public int amountToSpawn;
	public float timeInterval;
	
	public GameObject[] enemyList;
	public int amountSpawned;
	
	public Vector3 startOffset;
	
	float spawnTimer;
	bool finishSpawn = false;
	
	void ArrangeEnemy()
	{
		//applies starting position offset to the enemy's position
		for (int i = 0; i < enemyList.Length; ++i)
		{
			enemyList[i].transform.position += startOffset * i;
		}
	}
	
	void Start()
	{
		//initialise the enemy objects
		enemyList = new GameObject[amountToSpawn];
		for(int i = 0; i < amountToSpawn; ++i)
		{
			GameObject go = Instantiate(enemyObject) as GameObject;
			enemyList[i] = go;
			go.SetActive(false);
		}
		
		//ararnge the enemy pattern, refer to the function
		ArrangeEnemy();
		
		//set the timer
		spawnTimer = timeInterval;
	}
	
	// Update is called once per frame
	void Update () {
	
		spawnTimer -= Time.deltaTime;	
		if(spawnTimer <= 0.0f && amountSpawned < amountToSpawn && finishSpawn == false)
		{
			//if the type is repeat or single, spawn only one and wait for next interval
			if(type == PatternTypes.repeatType || type == PatternTypes.singleType)
			{
				for(int i = 0; i < enemyList.Length; ++i)
				{
                    if (enemyList[i] == null)
                        continue;
					if(enemyList[i].activeInHierarchy == false && enemyList[i].GetComponent<EnemyGeneralBehaviour>().spawned == false)
					{
						enemyList[i].SetActive(true);
						amountSpawned += 1;
						break;
					}
				}
				spawnTimer = timeInterval;
			}
			//if the type is one shot, spawn everything together
			else if (type == PatternTypes.oneShotType)
			{
				for(int i = 0; i < enemyList.Length; ++i)
				{
                    if (enemyList[i] == null)
                        continue;
					if(enemyList[i].activeInHierarchy == false && enemyList[i].GetComponent<EnemyGeneralBehaviour>().spawned == false)
					{
						enemyList[i].SetActive(true);
						amountSpawned += 1;
					}
				}
			}
		}
		else if (amountSpawned >= amountToSpawn)
		{
			finishSpawn = true;
			//this.enabled = false;
		}
	}
}
