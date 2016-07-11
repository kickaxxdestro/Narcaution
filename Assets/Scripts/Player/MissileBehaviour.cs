using UnityEngine;
using System.Collections;

public class MissileBehaviour : MonoBehaviour {

	public GameObject target;
	public Transform targetPos;
	private float torque = 5f;
	Rigidbody2D myRigidbody;
	public Vector2 force;
	public bool hasForce;

	GameObject player;
	GameObject bulletParticlePooler;

	public float bulletDamage = 0.65f;

	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		bulletParticlePooler = GameObject.Find("bulletParticlePooler");
		target = null;
	}

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D> ();
		myRigidbody.velocity = Vector2.up* 20f;
//		target.GetComponent<Rigidbody2D> ().velocity = -Vector2.up * 5f;
//
////		target.transform.position = Vector2.MoveTowards(target.transform.position, targetPos, Time.deltaTime);
//
//		//Get where the target will be after 1s
//		Vector3 expectedPos = target.transform.position + (Vector3)(target.GetComponent<Rigidbody2D> ().velocity * 1f);
//		
//
////		Vector2 dist = target.transform.position - transform.position;
//		Vector2 dist = expectedPos - transform.position;
//		force = 2f * dist - 2f * (Vector2.up * 20f);
	}

	// Update is called once per frame
	void Update () {
		if (target == null)
			return;

		if (!hasForce) {
//			target.transform.position = Vector2.MoveTowards(target.transform.position, targetPos.position, Time.deltaTime);
			
			//Get where the target will be after 1s
			Vector3 expectedPos = target.transform.position + (Vector3)(target.GetComponent<Rigidbody2D> ().velocity * 1f);
			
			
			//		Vector2 dist = target.transform.position - transform.position;
			Vector2 dist = expectedPos - transform.position;
			force = 2f * dist - 2f * (Vector2.up * 20f);
			hasForce = true;
		}


		if(transform.position.y > SystemVariables.current.CameraBoundsY)
		{
			gameObject.SetActive(false);
			if(target != null){
				target.GetComponent<EnemyGeneralBehaviour>().targetedBullet = null;
				target = null;
			}
		}
	}

	void FixedUpdate() {
		if (target == null || myRigidbody == null)
			return;

		myRigidbody.AddForce (force,ForceMode2D.Force);
	}

	void Reset()
	{
		gameObject.SetActive (false);
		hasForce = false;
		force = Vector2.zero;
		myRigidbody.velocity = Vector2.up* 20f;
	}

	void OnTriggerEnter2D(Collider2D other) {
//		Reset ();

		if(other.CompareTag("Enemy") == true || other.CompareTag("Minion") == true)
		{
			//get a particle object from the object pooler
			GameObject go = bulletParticlePooler.GetComponent<ObjectPooler>().GetPooledObject();
			go.transform.position = transform.position;
			go.SetActive(true);
			other.GetComponent<EnemyGeneralBehaviour>().hpCount -= bulletDamage;

			if(target.GetComponent<EnemyGeneralBehaviour>().targetedBullet != null){
				target.GetComponent<EnemyGeneralBehaviour>().targetedBullet = null;
			}
			else{
			}
			if(target != null){
				target = null;
			}
			else{
			}

			gameObject.SetActive(false);
			GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().comboCount += 1;
			hasForce = false;
			force = Vector2.zero;
			myRigidbody.velocity = Vector2.up* 20f;
			
		}
	}

}
