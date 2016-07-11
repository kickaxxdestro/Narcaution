using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {

	public GameObject bullet;			//Instantiate method
	public float interval, moveTimer, delayInterval, speed;
	public float repeatInterval, repeatIntervalCount;	//default interval
	public bool shoot, move;
	public int column, columnBullets, coneAngle, repeatCount;
	public bool repeatInf;

	//GameObject roundBulletPooler;		//pooling method
	Transform player;					//for player

	float [] angles;
	float intervalCount;
	int j;
	bool done;

	public patternValues.bulletPattern pattern;

	// Use this for initialization
	void Start () {
		if (pattern == patternValues.bulletPattern.guided || pattern == patternValues.bulletPattern.downward) {
			player = GameObject.FindGameObjectWithTag ("Player").transform;
			column = 1;	//stop from spawning multiple
		}
		angles = new float[column];
		//intervalCount = interval;
	}

	void Awake () {
		//roundBulletPooler = GameObject.Find("RoundBulletPooler");
		//player = GameObject.FindGameObjectWithTag("Player");
	}

	void shootFront () {
		//float angle = ((180 - coneAngle) / 2);
		float startAngle = (360 - coneAngle) - ((180 - coneAngle) / 2) - 90f;	//starting angle for cone (anti clockwise)
		float angleSpacing = coneAngle / column;	//spacing between each bullet
		float offset = angleSpacing / 2;	//offset cone to middle

		for (int i = 0; i < column; i++) {
			GameObject go = Instantiate (bullet) as GameObject;
			go.GetComponent<bulletMove>().speed = speed;

			//GameObject go = roundBulletPooler.GetComponent<ObjectPooler>().GetPooledObject();
			go.transform.position = transform.position;
		
			//go.transform.eulerAngles = new Vector3 (0, 0, startAngle + (angleSpacing * i) + offset);	
			go.transform.rotation = Quaternion.AngleAxis (startAngle + (angleSpacing * i) + offset, Vector3.forward);
            go.GetComponent<Rigidbody2D>().velocity = go.transform.up * 5f;
			//go.SetActive (true);
		}
        //print("front");
	}

	void shootAround () {
		float angleSpacing = 360 / column;

		for (int i = 0; i < column; i++) {
			angles[i] = angleSpacing * i;

			GameObject go = Instantiate (bullet) as GameObject;
			go.GetComponent<bulletMove>().speed = speed;
			//GameObject go = roundBulletPooler.GetComponent<ObjectPooler>().GetPooledObject();

			go.transform.position = transform.position;
			go.transform.rotation = Quaternion.AngleAxis (angleSpacing * i, Vector3.forward);	
			go.GetComponent<bulletMove>().source = transform.position;
            go.GetComponent<Rigidbody2D>().velocity = go.transform.up * 5f;
			//go.SetActive (true);
		}
	}

	void shootGuiding () {
		Vector3 playerPos = player.position;

		for (int i = 0; i < column; i++) {
			GameObject go = Instantiate (bullet) as GameObject;
			go.GetComponent<bulletMove>().speed = speed;
			//GameObject go = roundBulletPooler.GetComponent<ObjectPooler>().GetPooledObject();
		
			go.transform.position = transform.position;

			Vector3 difference = playerPos - transform.position;
			float rotationZ = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
			go.transform.rotation = Quaternion.Euler (0.0f, 0.0f, rotationZ - 90f);

            go.GetComponent<Rigidbody2D>().velocity = go.transform.up * 5f;
           // print("guiding");
		}
		//go.transform.rotation = Quaternion.AngleAxis (angleSpacing * i, Vector3.forward);	
		//go.GetComponent<bulletMove>().source = transform.position;
	}

	void shootDown () {
		GameObject go = Instantiate (bullet) as GameObject;
		go.GetComponent<bulletMove>().speed = speed;

		go.transform.position = transform.position;
		go.transform.rotation = Quaternion.Euler (0.0f, 0.0f, 180);

        go.GetComponent<Rigidbody2D>().velocity = go.transform.up * 5f;
	}

	void shootHelix () {
		GameObject go = Instantiate (bullet) as GameObject;
		//GameObject go = roundBulletPooler.GetComponent<ObjectPooler>().GetPooledObject();
		go.GetComponent<sineCurve> ().enabled = true;		//enable sine movement script
		sineCurve temp = go.GetComponent<sineCurve> ();
        go.GetComponent<Rigidbody2D>().velocity = go.transform.up * 5f;
		//go.transform.position = transform.position;
		temp.curveDir = sineCurve.dir.right;
		temp.ampDir = sineCurve.dir.down;
		temp.move = true;

		GameObject go1 = Instantiate (bullet) as GameObject;
		go1.GetComponent<sineCurve> ().enabled = true;		//enable sine movement script
		sineCurve temp1 = go1.GetComponent<sineCurve> ();
        go.GetComponent<Rigidbody2D>().velocity = go.transform.up * 5f;
		//go1.transform.position = transform.position;
		temp1.curveDir = sineCurve.dir.right;
		temp1.ampDir = sineCurve.dir.up;
		temp1.move = true;
  
		//go.SetActive (true);
		//go1.SetActive (true);
	}

	void spawnPattern () {
		//GameObject go = roundBulletPooler.GetComponent<ObjectPooler>().GetPooledObject();
		//go.transform.position = transform.position;

		//if (player != null) {
			switch (pattern) {
			case patternValues.bulletPattern.circle:
				shootAround ();
				break;
			case patternValues.bulletPattern.cone:
				shootFront ();
				break;
			case patternValues.bulletPattern.helix:
				shootHelix ();
				break;
			case patternValues.bulletPattern.downward :
				shootDown();
				break;
			case patternValues.bulletPattern.guided:
				shootGuiding();
				break;
			}
		//}


	}

	public bool doneShooting () {
		return done;
	}

	// Update is called once per frame
	void Update () {

		if (move) {
			//enable spawner sprite if spawner is Cluster
			this.GetComponent<SpriteRenderer>().enabled = true;	
		}

		if (delayInterval > 0) {
			delayInterval -= Time.deltaTime;
		} 
		else {
			if (moveTimer > 0) {
				moveTimer -= Time.deltaTime;
				transform.position -= new Vector3 (0, Time.deltaTime, 0);
			} else {
				//stop moving, start spawning bullet, disable Cluster sprite
				this.GetComponent<SpriteRenderer> ().enabled = false;
				shoot = true;
				move = false;
			}
		}

		//Shooting
		if (shoot) {
			if(repeatCount >= 0|| repeatInf) {				//repeats (repeatCount) number of times
				if(repeatIntervalCount > 0)
					repeatIntervalCount -= Time.deltaTime;

				else {
					if (j < columnBullets) {		// for each columnBullet
						if (intervalCount > 0)
							intervalCount -= Time.deltaTime;
						else {
							spawnPattern ();
							intervalCount = interval;
							j++;
						}
					} 
					else {
						repeatCount --;				//decrease repeatCount
						j = 0;						//reset columnBulletCount for each repetition
						repeatIntervalCount = repeatInterval;
					}
				}
			}

			else {
				done = true;
				//Destroy (this.gameObject);	//Destroy spawner after all bullets shot
				//this.gameObject.SetActive(false);
			}	
		}
		//Shooting
	}
}

