using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class INHBossBehaviour : MonoBehaviour {

	enum state {
		state_idle,
		state_spawnMinion,
		state_move,
		state_attacking,
		state_doneAttacking
	}

	state inhState;
	//List<GameObject> conjoinedMinions = new List<GameObject> ();

	GameObject minion;
	//public GameObject LeftMinion, RightMinion;
	public Transform LeftMinion, RightMinion;

	int dir = 1;
	public float initialSpeed;
	float speed;
	int speedMultiplier = 1;

	Animator inhController;
	bool minionSpawned, attacked;
	patternList inhPatternList;
	float targetDist;
	EnemyGeneralBehaviour inhHealth;

	Vector3 defaultPosition;

	//couroutine change AI state with a delay time
	IEnumerator ChangeAIStateDelay(state newState, float time)
	{
		yield return new WaitForSeconds(time);
		inhState = newState;
		StopCoroutine("ChangeAIStateDelay");
	}

	void OnEnable () {
		defaultPosition = this.transform.root.position;
	}

	void Start () {
		inhState = state.state_idle;

		/*foreach (Transform child in transform) {
			if(child.name == "(L)ConjoinedMinion")
				LeftMinion = child;
		
			if(child.name == "(R)ConjoinedMinion")
				RightMinion = child;
		}*/

		LeftMinion = transform.FindChild ("(L)ConjoinedMinion");
		RightMinion = transform.FindChild ("(R)ConjoinedMinion");

		inhController = GetComponent<Animator>();
		inhPatternList = GetComponent<patternList> ();
		inhHealth = this.GetComponent<EnemyGeneralBehaviour> ();

		GetComponent<bossHealthbar> ().setBossHp ();
	}

	void SetInvul(bool invul)
	{
		if(invul)
		{
			GetComponent<Collider2D>().enabled = false;
			GetComponent<SpriteRenderer>().color = Color.grey;
		}
		else if (!invul)
		{
			GetComponent<Collider2D>().enabled = true;
			GetComponent<SpriteRenderer>().color = Color.white;
		}
	}

	void aiState () {
		switch (inhState) {
		case state.state_idle :
			inhController.SetBool("spawningMinion", true);

			//reset bool
			minionSpawned = false;
			attacked = false;

			StartCoroutine(ChangeAIStateDelay(state.state_spawnMinion, 2.0f));
			break;

		case state.state_spawnMinion :
			//set active (if dead inactive)
			if(!minionSpawned) {
				//reset position to boss
				LeftMinion.transform.position = new Vector3 (this.transform.position.x - 0.5f, this.transform.position.y - 1, 0);
				RightMinion.transform.position = new Vector3 (this.transform.position.x + 0.5f, this.transform.position.y - 1, 0);

				LeftMinion.gameObject.SetActive(true);
				RightMinion.gameObject.SetActive(true); 
				minionSpawned = true;
				SetInvul(true);
			}

			//if all minions active
			StartCoroutine(ChangeAIStateDelay(state.state_move, 3.0f));
			break;

		case state.state_move :
			inhController.SetBool("spawningMinion", false);
			inhController.SetBool("isMoving", true);

			//if all minions dead (inactive)
			if(!LeftMinion.gameObject.activeInHierarchy && !RightMinion.gameObject.activeInHierarchy) {
				targetDist = (defaultPosition - transform.position).sqrMagnitude;
				
				if(targetDist > 0.1) {
					transform.position = Vector3.Lerp(transform.position, defaultPosition, (speed/2) * Time.deltaTime);
				}

				else if(targetDist <= 0.1){
					inhController.SetBool("isShooting", true);
					inhController.SetBool("isMoving", false);
					SetInvul(false);

					StartCoroutine(ChangeAIStateDelay(state.state_attacking, 1.0f));
				}
			}

			else {
				//constant moving back and forth
				transform.position += new Vector3(speed, 0, 0) * dir * Time.deltaTime;
				//changes the direction when it reaches the boundaries
				if(transform.position.x <= -SystemVariables.current.CameraBoundsX + (transform.localScale.x * 0.5f) || transform.position.x >= SystemVariables.current.CameraBoundsX - (transform.localScale.x * 0.5f))
				{
					dir = -dir;
				}
			}
			break;

		case state.state_attacking :
			if(!attacked) {

				inhPatternList.attack(0);
				attacked = true;
			}

			if(inhPatternList.SpawnersDone)
			{
				inhController.SetBool("isShooting", false);
		
				StartCoroutine(ChangeAIStateDelay(state.state_idle, 2.0f));
			}
			break;
		}
	}

	void Update() {

		speed = initialSpeed * speedMultiplier;

		if (inhHealth.hpCount <= 0) {
			this.transform.parent.GetComponent<EnemyGeneralBehaviour>().hpCount = 0;
		}
		aiState ();
	}
}
