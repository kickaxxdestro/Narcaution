using UnityEngine;
using System.Collections;

public class bulletMove : MonoBehaviour {

	public float speed;
	float angleCount, defSpeed, defRot;

	public Vector3 source;
	public bool rotate = true;
	bool visible;
    [HideInInspector]
    public bool reflected = false;
    public float reflectDamage = 500f;
    GameObject bulletParticlePooler;

	// Use this for initialization
	void Start () {
		defSpeed = speed;
		defRot = transform.rotation.z;
        bulletParticlePooler = GameObject.Find("bulletParticlePooler");
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        if(reflected)
        {
            if ((other.CompareTag("Enemy") == true || other.CompareTag("Minion") == true))
            {
                GameObject go = bulletParticlePooler.GetComponent<ObjectPooler>().GetPooledObject();
                go.transform.position = transform.position;
                go.SetActive(true);
                other.GetComponent<EnemyGeneralBehaviour>().hpCount -= reflectDamage; 
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().comboCount += 1;
                Destroy(this.gameObject);
            }

        }
		if(other.CompareTag("Player") == true)
		{
            //GetComponent<SpriteRenderer>().color = Color.red;
            //speed = defSpeed;
            gameObject.SetActive(false);
            //GetComponent<Rigidbody2D>().velocity = -GetComponent<Rigidbody2D>().velocity;
		}
	}

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Sentry")
        {
			GetComponent<SpriteRenderer>().color = Color.cyan;
            reflected = true;
            float rotationZ = Mathf.Atan2(GetComponent<Rigidbody2D>().velocity.y, GetComponent<Rigidbody2D>().velocity.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, rotationZ - 90f);	
        }
    }

	//Make it such that bullet despawns when outside cam bounds instead of cam bound y
	void OnBecameInvisible () {
		//speed = defSpeed;
		//gameObject.SetActive(false);
		Destroy (this.gameObject);
	}

	// Update is called once per frame
	void Update () {
		//transform.Rotate (new Vector3 (0,0,1));

		if (rotate) {
			transform.RotateAround (source, Vector3.forward, -1);
		}
		
		//transform.position += transform.right * speed *Time.deltaTime;

		//if(transform.position.y < -SystemVariables.current.CameraBoundsY)
		//if(!GetComponent<Renderer> ().isVisible)
		//{
		//	speed = defSpeed;
		//	gameObject.SetActive(false);
		//}
	}
}
