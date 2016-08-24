using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class patternList : MonoBehaviour {

	public List <patternValues> ListOfPatterns = new List<patternValues> ();

	public List<spawner> activeSpawners = new List<spawner> ();
	List <patternValues> temp = new List<patternValues> ();

	float intervalCount;
	float speed = 2.0f, defaultSpeed;
	bool startAttack, instantiatedAll;
	public bool SpawnersDone, repeatInfinite;	//all spawners done shooting
	int count;

	public GameObject spawnerPrefab;
	//public GameObject spawnerPooler;

	void Awake () {
		//spawnerPooler = GameObject.Find("SpawnerPooler");

	}

	void OnEnable () {
	}

	// Use this for initialization
	void Start () {
		//attack (0);
		if (this.name == "LSD_Boss")
			speed = 5.0f;
		//if(this.name == "LSDMinionL" || this.name == "LSDMinionR" || this.name == "LSDMinionM")
		if(this.tag == "Minion")
			speed = 4.0f;

		defaultSpeed = speed;
	}
	
	public void attack (int patternNum) {
		temp.Clear ();
		SpawnersDone = false;
		instantiatedAll = false;

		//if (ListOfPatterns.Count > 1) {
			for (int i=0; i < ListOfPatterns.Count; i++) {
				if (ListOfPatterns [i].attackSet == patternNum) {
					temp.Add (ListOfPatterns [i]);
				}
			}
		//}
		//skips checking if there is only one pattern
		//else
		//	temp = ListOfPatterns;

		startAttack = true;
	}

	void setValues (spawner spawnRef, patternValues patternRef) {
		spawnRef.pattern = patternRef.pattern;
		spawnRef.column = patternRef.column;
		spawnRef.columnBullets = patternRef.columnBullets;
		spawnRef.interval = patternRef.bulletInterval;
		spawnRef.coneAngle = patternRef.coneAngle;
		spawnRef.move = patternRef.moveSpawner;
		spawnRef.moveTimer = patternRef.spawnerMoveInterval;
		spawnRef.repeatCount = patternRef.repeat;
		spawnRef.delayInterval = patternRef.delay;
		spawnRef.repeatInterval = patternRef.repeatInterval;
		spawnRef.repeatInf = patternRef.repeatInfinite;
		spawnRef.speed = speed;

		//spawnRef.shoot = patternRef.startShooting;
	}

	void checkTemp () {

		if (count < temp.Count) {
			if (intervalCount > 0)
				intervalCount -= Time.deltaTime;
			else {
				GameObject newSpawner = Instantiate (spawnerPrefab) as GameObject;	//instantiate spawner
				activeSpawners.Add (newSpawner.GetComponent<spawner> ());
				//activeSpawners[count] = newSpawner.GetComponent<spawner> ();

				newSpawner.transform.position = this.transform.position;
				//GameObject newSpawner = spawnerPooler.GetComponent<ObjectPooler>().GetPooledObject();	//pool spawner
				//newSpawner.SetActive(true);
				setValues (newSpawner.GetComponent<spawner> (), temp [count]);			//set spawner values
				newSpawner.transform.SetParent (this.transform, true);
				count++;

				if (count < temp.Count)
					intervalCount = temp [count].patternInterval;
			}
		} 
		else {
			instantiatedAll = true;

			count = 0;
			startAttack = false;	
		}
		//stop func when finished iterating

		/*for (int i = 0; i < temp.Count; i++) {
			if (intervalCount > 0)
				intervxalCount -= Time.deltaTime;
			else {
				//GameObject newSpawner = Instantiate (spawnerPrefab) as GameObject;	//instantiate spawner
				GameObject newSpawner = spawnerPooler.GetComponent<ObjectPooler>().GetPooledObject();	//pool spawner
				newSpawner.SetActive(true);
				setValues (newSpawner.GetComponent<spawner>(), temp[count]);			//set spawner values
				//newSpawner.transform.SetParent(this.transform, true);
				//count++;
			}
			intervalCount = temp[i].patternInterval;
		}

		startAttack = false;*/
	}

	public void forceDestroy () {
		for (int i=0; i<activeSpawners.Count; i++) {
			Destroy (activeSpawners[i].gameObject);	//Destroy spawner after all bullets shot
			activeSpawners.RemoveAt(i);
		}
	}

	// Update is called once per frame
	void Update () {
		//print (intervalCount.ToString ());
		SpawnersDone = false;

		/*for (int i=0; i<activeSpawners.Count; i++) {
			if(!activeSpawners[i].GetComponent<spawner>().doneShooting()) {
				SpawnersDone = false;
				break;
			}
			else {
				Destroy (activeSpawners[i].gameObject);	//Destroy spawner after all bullets shot
				activeSpawners.Remove(activeSpawners[i]);
				SpawnersDone = true;
			}
		}*/
		for (int i=0; i<activeSpawners.Count; i++) {
			if(activeSpawners[i].GetComponent<spawner>().doneShooting()) {
				Destroy (activeSpawners[i].gameObject);	//Destroy spawner after all bullets shot
				activeSpawners.Remove(activeSpawners[i]);
			}
		}

		if (activeSpawners.Count == 0 && instantiatedAll) {
			SpawnersDone = true;
		}

		if (startAttack) {
			checkTemp ();
		}
			//StartCoroutine (checkTemp());
	}
}
