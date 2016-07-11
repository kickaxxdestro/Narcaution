using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ECSBossBehaviour : MonoBehaviour {
	
	int shuffleCycleCount = 3;
	int maxCloneCount = 2, currCloneCount;
	int minionWave = 0, maxWave;
	
	float hpDifference;
	float cloneWaitTime = 10.0f, bossVulTime = 2.0f;	//how long the clones wait before attacking
	float targetDist;
	float getCurrHp, hpThreshhold = 10;
	
	public float speed;
	
	bool minionsActive;
	bool retractAll = false;
	bool getClonePos;
	bool retracted;
	bool minionSetAttack;
	bool attacked;
	
	List<GameObject> minionList = new List<GameObject> ();
	public List<GameObject> cloneList = new List<GameObject> ();
	
	public GameObject[] minionPattern;
	public GameObject clonePrefab;
	
	Vector3 retractPos, initPos, minionInitPos;
	Vector3 cloneInitPos;

	public enum state {
		state_idle,
		state_spawnMinion,
		//state_waitMinion,
		state_spawnClone,
		state_shuffle,
		state_waitClone,
		state_attack
	}
	
	public state ecsState;
	EnemyGeneralBehaviour ecsRef;
	patternList ecsAttackControl;
	Animator ecsController;

	//couroutine change AI state with a delay time
	IEnumerator ChangeAIStateDelay(state newState, float time)
	{
		yield return new WaitForSeconds(time);
		ecsState = newState;
		StopCoroutine("ChangeAIStateDelay");
	}
	
	
	// Use this for initialization
	void Start () {
		//minionPrefab = new GameObject[3];
		maxWave = 3;
		speed = 3;

		//for(int i=0; i< minionPattern.Length; i++) {
		//	minionPattern[i].transform.position = new Vector3 (0, 2, 1);
		//}
		minionInitPos = new Vector3 (0, 2, 0);
		retractPos = new Vector3 (transform.position.x, 6, 0);
		initPos = transform.position;

		ecsState = state.state_idle;
		ecsRef = this.GetComponent<EnemyGeneralBehaviour> ();
		ecsController = GetComponent<Animator>();
		ecsAttackControl = GetComponent <patternList> ();

		GetComponent<bossHealthbar> ().setBossHp ();
	}

	void aiState () {
		switch (ecsState) {
		case state.state_idle :
			minionSetAttack = false;
			attacked = false;
			retracted = false;

			cloneWaitTime = 10.0f;
			minionWave = 0;
			currCloneCount = 0;
			shuffleCycleCount = 3;

			StartCoroutine(ChangeAIStateDelay(state.state_spawnMinion, bossVulTime));

			break;
			
		case state.state_spawnMinion :
			if(minionWave < maxWave && !minionsActive) {

				ecsController.SetBool("isSpawning", true);

				for(int i=0; i< minionPattern.Length; i++) {
					minionPattern[i].transform.position = new Vector3 (0, 2, 0);
				}

				switch (minionWave) {
				case 2:
					minionPattern[0].SetActive(true);
					minionPattern[1].SetActive(true);
					minionPattern[2].SetActive(true);
					minionPattern[3].SetActive(true);
					minionPattern[4].SetActive(true);
					break;
				case 1:
					minionPattern[0].SetActive(true);
					minionPattern[1].SetActive(true);
					minionPattern[2].SetActive(true);
					break;
				case 0: 
					minionPattern[1].SetActive(true);
					minionPattern[0].SetActive(true);
					break;
				}

				for(int i=0; i < minionPattern.Length; i++) {
					if(minionPattern[i].activeInHierarchy)
						minionList.Add(minionPattern[i]);
				}

				minionWave++;
				minionsActive = true;
			}

			if(minionsActive) {
				ecsController.SetBool("isSpawning", false);
			}

			for(int i=0; i< minionList.Count; i++) {
				if(!minionList[i].activeInHierarchy) {
					//minionList[i].transform.position = minionInitPos;
					minionList.Remove(minionList[i]);
				}
			}

			if(minionList.Count == 0 && minionsActive) {
				//resetPos();
				minionsActive = false;
			}

			if(minionWave == 3 && !minionsActive) {
				StartCoroutine(ChangeAIStateDelay(state.state_spawnClone, 2.0f));
			}

			//if(minionList.Count <= 0)
				//minionsActive = false;
			
			break;
			
		case state.state_spawnClone :
			if(currCloneCount < maxCloneCount) {

				ecsController.SetBool("isAttacking", true);

				GameObject newClone = Instantiate(clonePrefab) as GameObject;
				
				newClone.transform.position = new Vector3 (-2 + (4f * currCloneCount), 9,0);
				//Vector3 lerpPos = new Vector3 (newClone.transform.position.x, 5, 0); 
				
				//newClone.GetComponent<cloneBehaviour> ().lerp(lerpPos);
				//newClone.GetComponent<cloneBehaviour> ().cloneState = cloneBehaviour.state.spawn;
				newClone.GetComponent<cloneBehaviour> ().dir = currCloneCount + 1;
				
				cloneList.Add (newClone);
				
				currCloneCount++;
			}
			else {
				retractAll = true;
				ecsController.SetBool("isAttacking", false);
				StartCoroutine(ChangeAIStateDelay(state.state_shuffle, 2.0f));
			}
			break;
			
		case state.state_shuffle :
			if(retractAll) {
				for(int i=0; i< cloneList.Count; i++) {
					cloneList[i].GetComponent<cloneBehaviour> ().cloneState = cloneBehaviour.state.retractSemi;
				}
				retractAll = false;
			}
			
			targetDist = (transform.position - retractPos).sqrMagnitude;
			
			if(targetDist > 0.01 && !retracted) {
				transform.position = Vector3.Lerp (transform.position, retractPos, (speed/2) * Time.deltaTime);
			}
			else {
				retracted = true;
				
				if(shuffleCycleCount > 0) {
					int rand = UnityEngine.Random.Range(0, 2);
					
					if(!getClonePos) {
						cloneInitPos = cloneList[rand].transform.position;
						cloneList[rand].GetComponent<cloneBehaviour> ().lerp (transform.position);
						cloneList[rand].GetComponent<cloneBehaviour> ().cloneState = cloneBehaviour.state.shuffle;
						
						speed = 7;
						
						getClonePos = true;
					}
					
					//float cloneDist = (transform.position - cloneInitPos).sqrMagnitude;
					float cloneDist = (transform.position - cloneInitPos).sqrMagnitude;

					if(cloneDist > 0.01) 
						transform.position = Vector3.Lerp (transform.position, cloneInitPos, (speed/2) * Time.deltaTime);
					else {
						transform.position = new Vector3 (Mathf.Round(cloneInitPos.x), Mathf.Round(cloneInitPos.y), 0);
						
						getClonePos = false;
						shuffleCycleCount --;
					}
				}
				else {
					speed = 3;
					getCurrHp = ecsRef.hpCount;
					StartCoroutine(ChangeAIStateDelay(state.state_waitClone, 0.0f));
				}
			}
			
			break;
			
		case state.state_waitClone :
			if(cloneWaitTime > 0) {
				cloneWaitTime -= Time.deltaTime;
			}
			else if (cloneWaitTime <= 0){
				if(ecsRef.hpCount > (getCurrHp - hpThreshhold) || cloneList.Count != 0) {		
					for (int i=0; i < cloneList.Count; i++) {
							cloneList[i].GetComponent<cloneBehaviour> ().cloneState = cloneBehaviour.state.retractFull;
					}

					StartCoroutine(ChangeAIStateDelay(state.state_attack, 0.0f));
				}
			}

			if(ecsRef.hpCount < (getCurrHp - hpThreshhold) || cloneList.Count == 0) {
				for (int i=0; i < cloneList.Count; i++) {
					if(!cloneList[i].GetComponent<cloneBehaviour>().startAttack)
						cloneList[i].GetComponent<cloneBehaviour> ().cloneState = cloneBehaviour.state.destroy;
				}

				float dist1 = (transform.position - initPos).sqrMagnitude;
				
				if (dist1 > 0.01) {
					transform.position = Vector3.Lerp (transform.position, initPos, (speed / 2) * Time.deltaTime);
				} 
				else {
					transform.position = new Vector3 (Mathf.Round(initPos.x), Mathf.Round(initPos.y), 0);
					
					bossVulTime = 7.0f;
					StartCoroutine(ChangeAIStateDelay(state.state_idle, 0.0f));
				}

			}

			break;
			
		case state.state_attack :
			if(!minionSetAttack) {
				for (int i=0; i < cloneList.Count; i++) {
					cloneList[i].GetComponent<cloneBehaviour> ().cloneState = cloneBehaviour.state.retractFull;
				}
				
				minionSetAttack = true;
			}
			
			float dist = (transform.position - initPos).sqrMagnitude;

			if (dist > 0.01) {
				transform.position = Vector3.Lerp (transform.position, initPos, (speed / 2) * Time.deltaTime);
			} 
			else {
				transform.position = new Vector3 (Mathf.Round(initPos.x), Mathf.Round(initPos.y), 0);

				if(!attacked) {
					ecsAttackControl.attack(0);
					attacked = true;
				}

				if(ecsAttackControl.SpawnersDone) {
					if(cloneList.Count == 0) {
						bossVulTime = 2.0f;
						StartCoroutine(ChangeAIStateDelay(state.state_idle, 0.0f));
					}
				}

			}
			
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		print (ecsState);
	
		for (int i=0; i<cloneList.Count; i++) {
			if(cloneList[i] == null)
				cloneList.Remove(cloneList[i]);
		}

		aiState ();
	}
}
