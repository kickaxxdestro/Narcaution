using UnityEngine;
using System.Collections;

public class cloneBehaviour : MonoBehaviour {
	
	float speed = 6;
	float targetDist;
	float delayTimer = 0.5f;
	
	Vector3 lerpPos;
	
	bool lerping, boolCheck, destroyCheck;
	public bool startAttack;
	
	EnemyGeneralBehaviour cloneRef;
	
	public int dir;
	
	public enum direction {
		down,
		left,
		right
	}
	
	public enum state {
		idle,
		spawn,
		shuffle,
		destroy,
		retractSemi,
		retractFull,
		changePos,
		delay,
		attack
	}
	
	public direction cloneDir;
	public state cloneState;
	
	// Use this for initialization
	void Start () {
		//cloneDir = direction.down;
		//cloneState = state.idle;
		
		cloneRef = GetComponent<EnemyGeneralBehaviour> ();
	}
	
	IEnumerator ChangeAIStateDelay(state newState, float time)
	{
		yield return new WaitForSeconds(time);
		cloneState = newState;
		StopCoroutine("ChangeAIStateDelay");
	}
	
	void aiState () {
		switch (cloneState) {
		case state.idle :
			boolCheck = false;
			speed = 6;
			break;
			
		case state.spawn :
			if(!boolCheck) {
				//Vector3 lerpPos = new Vector3 (newClone.transform.position.x, 5, 0); 
				lerp (new Vector3 (transform.position.x, 5, 0));
				boolCheck = true;
			}
			
			if (targetDist > 0.01)
				transform.position = Vector3.Lerp (transform.position, lerpPos, (speed / 2) * Time.deltaTime);
			else {
				transform.position = lerpPos;
				StartCoroutine(ChangeAIStateDelay(state.idle, 0.0f));
			}
			break;
			
		case state.shuffle :
			speed = 7;
			
			if (targetDist > 0.001)
				transform.position = Vector3.Lerp (transform.position, lerpPos, (speed / 2) * Time.deltaTime);
			else {
				transform.position = new Vector3 (Mathf.Round(lerpPos.x), Mathf.Round(lerpPos.y), 0);
				StartCoroutine(ChangeAIStateDelay(state.idle, 0.0f));
			}
			break;
			
		case state.destroy :
			if(!destroyCheck) {
				if(cloneDir == direction.down)
					lerp (new Vector3 (transform.position.x, 9, 0));
				
				if(cloneDir == direction.left)
					lerp (new Vector3 (-7, transform.position.y, 0));
				
				if(cloneDir == direction.right)
					lerp (new Vector3 (7, transform.position.y, 0));
				
				destroyCheck = true;
			}


			if(cloneDir == direction.down) {
				if (transform.position.y < 8.9)
					transform.position = Vector3.Lerp (transform.position, lerpPos, (speed / 2) * Time.deltaTime);
				else 
					Destroy (this.gameObject);
			}
			else {
				if (targetDist > 0.01)
					transform.position = Vector3.Lerp (transform.position, lerpPos, (speed / 2) * Time.deltaTime);
				else 
					Destroy (this.gameObject);
			}

			break;
			
		case state.retractSemi :
			if(!boolCheck) {
				lerp (new Vector3 (transform.position.x, 6, 0));
				boolCheck = true;
			}
			
			if (targetDist > 0.01)
				transform.position = Vector3.Lerp (transform.position, lerpPos, (speed / 2) * Time.deltaTime);
			else {
				//transform.position = lerpPos;
				transform.position = new Vector3 (Mathf.Round(lerpPos.x), Mathf.Round(lerpPos.y), 0);
				StartCoroutine(ChangeAIStateDelay(state.idle, 0.0f));
			}
			break;
			
		case state.retractFull :
			startAttack = true;

			cloneRef.hpCount += 1000;

			if(!boolCheck) {
				lerp (new Vector3 (transform.position.x, 9, 0));
				cloneDir = (direction) dir;
				
				boolCheck = true;
			}
			
			if (transform.position.y < 8.9)
				transform.position = Vector3.Lerp (transform.position, lerpPos, (speed / 2) * Time.deltaTime);
			else {
				transform.position = lerpPos;
				
				boolCheck = false;
				StartCoroutine(ChangeAIStateDelay(state.changePos, 0.0f));
			}
			break;
			
		case state.changePos :
			if(cloneDir == direction.left) {
				transform.position = new Vector3 (-7, -4, 0);
				transform.rotation = Quaternion.AngleAxis (90, Vector3.forward);
			}
			
			else if (cloneDir == direction.right) {
				transform.position = new Vector3 (7, -2, 0);
				transform.rotation = Quaternion.AngleAxis (-90, Vector3.forward);
			}
			
			boolCheck = false;
			StartCoroutine(ChangeAIStateDelay(state.delay, 1.0f));
			break;
			
		case state.delay :
			if(!boolCheck) {
				if(cloneDir == direction.left) 
					lerp (new Vector3 (-6, transform.position.y, 0));
				
				else if(cloneDir == direction.right) 
					lerp (new Vector3 (6, transform.position.y, 0));
				
				boolCheck = true;
			}
			
			if (targetDist > 0.01)
				transform.position = Vector3.Lerp (transform.position, lerpPos, (speed / 2) * Time.deltaTime);
			else {
				//transform.position = lerpPos;
				
				if(delayTimer > 0)
					delayTimer -= Time.deltaTime;
				else {
					boolCheck = false;
					StartCoroutine(ChangeAIStateDelay(state.attack, 0.0f));
				}
			}
			break;
			
		case state.attack :
			if(!boolCheck) {
				lerp (new Vector3 (0, transform.position.y, 0));
				boolCheck = true;
			}
			
			if (targetDist > 0.01)
				transform.position = Vector3.Lerp (transform.position, lerpPos, (speed / 2) * Time.deltaTime);
			else {
				//transform.position = lerpPos;
				//boolCheck = false;
				
				StartCoroutine(ChangeAIStateDelay(state.destroy, 0.8f));
			}
			break;
		}
	}
	
	public void lerp (Vector3 pos) {
		//lerping = true;
		lerpPos = pos;
	}
	
	// Update is called once per frame
	void Update () {
		targetDist = 0;
		//targetDist = Vector3.Distance (transform.position, lerpPos);
		targetDist = (transform.position - lerpPos).sqrMagnitude;
		
		aiState ();
		
		if (cloneRef.hpCount < 5 && !startAttack) {
			//GetComponent<Collider2D>().enabled = false;
			cloneState = state.retractFull;
			startAttack = true;
		}
		
	}
}
