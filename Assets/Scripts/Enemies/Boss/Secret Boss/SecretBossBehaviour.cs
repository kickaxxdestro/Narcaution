using UnityEngine;
using System.Collections;

public class SecretBossBehaviour : MonoBehaviour {
	
	enum state {
		state_idle,
		state_spawnminion,
		state_random,
		state_iceAttack,
		state_cannabisAttack,
		state_inhAttack,
		state_ecsAttack,
		state_lsdAttack,
		state_defaultAttack,
		//level 5-4
		state_homingShot
	}
	
	state bossState;
	
	public int cycle, cycleCount, hpThreshhold = 100, rand;
	bool attacked, moveBack, random, cycleIncrement;
	float speed;
	
	//Cannabis Attack
	int chargeCount;
	float trackTimer = 3.0f;
	bool trackPlayer;
	
	Vector3 initialPos;
	
	//default minion 
	GameObject minionParent;
	//ice obj (spawns 3 ice chunks and shoots them downwards, each firing circular shots)
	GameObject iceChunkParent;
	//ecs obj
	GameObject ecsCloneParent;
	//lsd obj
	GameObject lsdTurretParent;
	//Attack Animations
	GameObject iceAnim, homingAnim, defaultAnim;
	
	LevelLoader getLevel;
	patternList attackControl;
	EnemyGeneralBehaviour bossGeneral;
	
	Transform playerObj;
	PlayerController getPlayer;
	Animator secretbossController;
	
	void Awake () {
		cycle = 1;
	}
	
	// Use this for initialization
	void Start () {
		speed = 4;
		
		initialPos = transform.position;
		
		playerObj = GameObject.Find("player").transform;
		
		bossGeneral = GetComponent<EnemyGeneralBehaviour> ();
		attackControl = GetComponent<patternList> ();
		getPlayer = playerObj.GetComponent<PlayerController> ();
		GetComponent<bossHealthbar> ().setBossHp ();
		secretbossController = this.GetComponent<Animator> ();
		
		getLevel = GameObject.Find("LevelLoader").GetComponent<LevelLoader> ();
		
		bossState = state.state_idle;
		
		foreach (Transform child in transform) {
			switch (child.name) {
			case "minionParent" :
				minionParent = child.gameObject;
				break;
			case "iceChunkParent" :
				iceChunkParent = child.gameObject;
				break;
			case "lsdTurretParent" :
				lsdTurretParent = child.gameObject;
				break;
			case "ecsCloneParent" :
				ecsCloneParent = child.gameObject;
				break;
			case "iceAnim" :
				iceAnim = child.gameObject;
				break;
			case "aoeAnim" :
				homingAnim = child.gameObject;
				break;
			case "defaultAnim" :
				defaultAnim = child.gameObject;
				break;
			}
		}
		
	}
	
	void OnEnable () {
		
	}
	
	//couroutine change AI state with a delay time
	IEnumerator ChangeAIStateDelay(state newState, float time)
	{
		yield return new WaitForSeconds(time);
		bossState = newState;
		StopCoroutine("ChangeAIStateDelay");
	}
	
	void attackCheck (int attack) {
		switch (attack) {
		case 1 :
			StartCoroutine(ChangeAIStateDelay(state.state_iceAttack, 1.0f));
			break;
		case 2 :
			StartCoroutine(ChangeAIStateDelay(state.state_cannabisAttack, 1.0f));
			break;
		case 3 :
			StartCoroutine(ChangeAIStateDelay(state.state_ecsAttack, 1.0f));
			break;
		case 4 :
			StartCoroutine(ChangeAIStateDelay(state.state_lsdAttack, 1.0f));
			break;
		}
	}
	
	void aiState () {
		switch (bossState) {
		case state.state_idle :
			cycleCount = cycle;
			attacked = false;
			cycleIncrement = false;
			chargeCount = 0;
			
			StartCoroutine(ChangeAIStateDelay(state.state_spawnminion, 1.0f));
			break;
			
		case state.state_spawnminion :
			if(!attacked) {
				attackControl.attack(0);
				minionParent.SetActive(true);
				attacked = true;
			}
			
			if(!minionParent.activeInHierarchy) {
				attackControl.forceDestroy();
				StartCoroutine(ChangeAIStateDelay(state.state_random, 0.0f));
			}
			break;
			
		case state.state_random :
			attacked = false;
			chargeCount = 0;
			
            //if(bossGeneral.hpCount < (bossGeneral.hp - hpThreshhold) && getLevel.loadedLevel == 20) {
            //    setInvul(false);
            //    GetComponent<SpriteRenderer>().color = Color.red;
            //    bossState = state.state_homingShot;
            //}
			
            //else 
            {
				if(!random && cycleCount == 0) {
					StartCoroutine(ChangeAIStateDelay(state.state_defaultAttack, 0.0f));
				}
				
				else if(!random && cycleCount > 0) {
					cycleCount -= 1;
					rand = Random.Range(1,4);
					//rand = 4;
					
					switch (rand) {
					case 1 :
						StartCoroutine(ChangeAIStateDelay(state.state_iceAttack, 1.0f));
						break;
					case 2 :
						StartCoroutine(ChangeAIStateDelay(state.state_cannabisAttack, 1.0f));
						break;
					case 3 :
						StartCoroutine(ChangeAIStateDelay(state.state_ecsAttack, 1.0f));
						break;
					case 4 :
						StartCoroutine(ChangeAIStateDelay(state.state_lsdAttack, 1.0f));
						break;
					}
					
					random = true;
				}
			}
			break;
			
		case state.state_iceAttack :
			if(!attacked) {
				iceAnim.SetActive(true);
				secretbossController.SetBool("isAttacking", true);
				
				iceChunkParent.SetActive(true);
				
				attacked = true;
			}
			
			if(!iceChunkParent.activeInHierarchy) {
				iceAnim.SetActive(false);
				secretbossController.SetBool("isAttacking", false);
				random = false;
				
				StartCoroutine(ChangeAIStateDelay(state.state_random, 0.0f));
			}
			break;
			
		case state.state_cannabisAttack :
			if(chargeCount < 3) {
				if(trackTimer > 0) {
					trackTimer -= Time.deltaTime;
					secretbossController.SetBool("preparingCharge", true);
					secretbossController.SetBool("charge", false);
					transform.position = Vector3.Lerp(transform.position, new Vector3(playerObj.position.x, transform.position.y), (speed/2) *Time.deltaTime);
				}
				else {
					if(!moveBack) {
						if(!attacked) {
							attackControl.attack(1);
							attacked = true;
						}
						if(Vector3.Distance(transform.position, new Vector3(transform.position.x, -SystemVariables.current.CameraBoundsY)) > 0.1f)
						{
							transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -SystemVariables.current.CameraBoundsY), (speed/2) *Time.deltaTime);
							secretbossController.SetBool("preparingCharge", false);
							secretbossController.SetBool("charge", true);
						}
						else
						{
							moveBack = true;
							secretbossController.SetBool("preparingCharge", false);
							secretbossController.SetBool("charge", false);
						}
					}
					else if(moveBack)
					{
						if(Vector3.Distance(transform.position, new Vector3(transform.position.x, initialPos.y)) > 0.1f)
						{
							transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, initialPos.y), (speed/2) *Time.deltaTime);
						}
						else
						{
							chargeCount += 1;
							attacked = false;
							moveBack = false;
							trackTimer = 3.0f;
						}
					}
				}
			}
			else {
				secretbossController.SetBool("preparingCharge", false);
				if(Vector3.Distance(transform.position, initialPos) > 0.01f)
				{
					transform.position = Vector3.Lerp(transform.position, initialPos, (speed/2) *Time.deltaTime );
				}
				else {
					transform.position = initialPos;
					random = false;
					StartCoroutine(ChangeAIStateDelay(state.state_random, 0.0f));
					//StartCoroutine(ChangeAIStateDelay(state.state_idle, 0.0f));
				}
			}
			break;
			
		case state.state_ecsAttack :
			if(!attacked) {
				secretbossController.SetBool("isAttacking", true);
				ecsCloneParent.SetActive(true);
				attackControl.attack (2);
				attacked = true;
			}
			
			if(!ecsCloneParent.activeInHierarchy && attackControl.SpawnersDone) {
				secretbossController.SetBool("isAttacking", false);
				random = false;
				
				StartCoroutine(ChangeAIStateDelay(state.state_random, 0.0f));
			}
			break;
			
		case state.state_lsdAttack :
			if(!attacked) {
				lsdTurretParent.SetActive(true);
				attacked = true;
			}
			
			if(!lsdTurretParent.activeInHierarchy) {
				random = false;
				StartCoroutine(ChangeAIStateDelay(state.state_random, 0.0f));
			}
			break;
			
		case state.state_defaultAttack :
			if(!attacked) {
				secretbossController.SetBool("isAttacking", true);
				defaultAnim.SetActive(true);
				
				attacked = true;
			}
			if(!defaultAnim.activeInHierarchy) {
				secretbossController.SetBool("isAttacking", false);
				if(!cycleIncrement) {
					cycle += 1;
					cycleIncrement = true;
				}
				StartCoroutine(ChangeAIStateDelay(state.state_idle, 0.0f));
			}
			break;
			
		case state.state_homingShot :
			if(!attacked) {
				homingAnim.SetActive(true);
				attacked = true;
			}
			if(getPlayer.emotionPoint == 1) {
				//fly away and hp = 0
				homingAnim.SetActive(false);
				if(Vector3.Distance(transform.position, new Vector3 (transform.position.x, 10, 0)) > 0.01f)
				{
					transform.position = Vector3.Lerp(transform.position, new Vector3 (transform.position.x, 10, 0), (speed/2) *Time.deltaTime);
				}
				else {
					bossGeneral.hpCount = 0;
				}
			}
			break;
		}
	}
	
	void setInvul (bool invul) {
		if (invul) {
			GetComponent<Collider2D>().enabled = false;
			GetComponent<SpriteRenderer>().color = Color.red;
		}
		
		if (!invul) {
			GetComponent<Collider2D>().enabled = true;
			GetComponent<SpriteRenderer>().color = Color.white;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//print (bossState);
		//print (cycleCount.ToString ());
		
        //if(bossGeneral.hpCount < (bossGeneral.hp - hpThreshhold) && getLevel.loadedLevel == 20 && bossState != state.state_homingShot) {
        //    setInvul (true);
        //}
		
		//print (trackTimer.ToString());
		aiState ();
	}
}
