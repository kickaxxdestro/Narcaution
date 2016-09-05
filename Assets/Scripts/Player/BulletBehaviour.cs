using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour {

	public float bulletSpeed = 25.0f;
	
	public float bulletDamage = 0.5f;

	GameObject bulletParticlePooler;
	
	GameObject player;

	public enum BulletType{
		bulletNormal,
		bulletHoming,
		bulletBomb,
		bulletBeam,
        bulletBoomerang
	};

	public BulletType bulletType;

	public GameObject homingTarget;

	public float beamDurationValue;
	public float beamDuration;

    //For boomerang projectile
    bool movementDirection = true;

	public GameObject Explosion;
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		bulletParticlePooler = GameObject.Find("bulletParticlePooler");
		homingTarget = null;
		beamDuration = beamDurationValue;
	}

	void FixedUpdate()
	{
        switch (bulletType)
        {
            case BulletType.bulletNormal:
                {
                    // ?whyyyyyyyyyyy
                    GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
                    break;
                }
            case BulletType.bulletHoming:
                {
                    //			GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");

                    //			foreach (GameObject go in enemies) 
                    //			{
                    //				homingTarget = null;
                    //				if(go.activeInHierarchy && !go.GetComponent<EnemyGeneralBehaviour>().isTargeted)
                    //				{
                    //					homingTarget = go;
                    //					go.GetComponent<EnemyGeneralBehaviour>().isTargeted = true;
                    //					Debug.Log("TARGET: "+homingTarget);
                    //					break;
                    //				}
                    //				else if(!go.activeInHierarchy)
                    //				{
                    //					homingTarget = null;
                    //				}
                    //			}
                    //homingTarget = null;

                    if (homingTarget == null || !homingTarget.activeInHierarchy)
                    {
                        GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;

                    }
                    else 
                    {
                        Vector3 vectorToTarget = homingTarget.transform.position - transform.position;
                        float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90f;
                        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 12.5f);

                        //				Vector2 relativePos = homingTarget.transform.position - transform.position;
                        //				Quaternion rotation = Quaternion.LookRotation(relativePos);
                        //				Quaternion current = transform.localRotation;
                        //
                        //				transform.localRotation = Quaternion.Slerp(current,rotation, Time.deltaTime);

                        //				Vector2 dir = homingTarget.transform.position - transform.position;
                        //				float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg - 90f;
                        //				transform.localRotation = Quaternion.AngleAxis(angle,Vector3.forward);
                        GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed * 0.75f;
                    }

                    break;
                }
            case BulletType.bulletBomb:
                {
                    GetComponent<Rigidbody2D>().velocity = transform.up * (bulletSpeed * 0.75f);
                    break;
                }

            case BulletType.bulletBeam:
                {
                    transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 5.5f);
                    beamDuration -= Time.fixedDeltaTime;

                    if (beamDuration <= 0f)
                    {
                        gameObject.SetActive(false);
                        beamDuration = beamDurationValue;
                    }
                    break;
                }
            //case BulletType.bulletBoomerang:
            //    {
            //        if(movementDirection)
            //            GetComponent<Rigidbody2D>().velocity = transform.up * bulletSpeed;
            //        else
            //        break;
            //    }
            default:
                break;
        }

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if ((bulletType == BulletType.bulletNormal || bulletType == BulletType.bulletHoming) && (other.CompareTag ("Enemy") == true || other.CompareTag ("Minion") == true)) 
		{
			//get a particle object from the object pooler
			GameObject go = bulletParticlePooler.GetComponent<ObjectPooler> ().GetPooledObject ();
			go.transform.position = transform.position;
			go.SetActive (true);
			other.GetComponent<EnemyGeneralBehaviour> ().hpCount -= bulletDamage;
			if (bulletType == BulletType.bulletHoming) {
				if (homingTarget.GetComponent<EnemyGeneralBehaviour> ().targetedBullet != null) {
					homingTarget.GetComponent<EnemyGeneralBehaviour> ().targetedBullet = null;
				} else {
				}
				if (homingTarget != null) {
					homingTarget = null;
				} else {
				}
			}
			gameObject.SetActive (false);
			//GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().comboCount += 1;
			//Debug.Log ("Bullet/Homing Hit!");

		} 
		else if (bulletType == BulletType.bulletBomb  && (other.CompareTag ("Enemy") == true || other.CompareTag ("Minion") == true)) 
		{
			GameObject go = Instantiate (Explosion, transform.position, Quaternion.identity) as GameObject;
			gameObject.SetActive (false);
			//GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().comboCount += 1;
            //print("hit2");
		}
		else if (other.CompareTag("TutorialEnemy") == true)
        {
			GameObject go = bulletParticlePooler.GetComponent<ObjectPooler>().GetPooledObject();
			go.transform.position = transform.position;
			go.SetActive(true);
			gameObject.SetActive(false);
			//GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().comboCount += 1;
	    }
    }

	void OnTriggerStay2D(Collider2D other)
	{
        if (bulletType == BulletType.bulletBeam && (other.CompareTag("Enemy") == true || other.CompareTag("Minion") == true))
        {
			GameObject go = bulletParticlePooler.GetComponent<ObjectPooler> ().GetPooledObject ();
			go.transform.position = other.transform.position;
			go.SetActive (true);
			other.GetComponent<EnemyGeneralBehaviour> ().hpCount -= bulletDamage;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().comboCount += 1;

		}
	}

	// Update is called once per frame
	void Update ()
	{	
		
		if(transform.position.y > SystemVariables.current.CameraBoundsY)
		{
			if(bulletType!= BulletType.bulletBeam){
				gameObject.SetActive(false);
				if(homingTarget != null){
					homingTarget.GetComponent<EnemyGeneralBehaviour>().targetedBullet = null;
					homingTarget = null;
				}
			}
		}

	}
}
