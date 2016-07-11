using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bulletPattern : MonoBehaviour {

	int radiusCount, count;
	float x, y, timer = 2.0f;
	public GameObject testPrefab;
	bool test;

	List <GameObject> activeBullet = new List<GameObject>();

	// Use this for initialization
	void Start () {
	
	}

	// straight bullet via angle, 360 deg is all around
	//public void pattern (Vector3 origin, float angle, int lines, int bulletCount) {
	//}

	// spiral
	public void pattern (Vector3 origin, int lines, int bulletCount, bool clockwise) {

		for (int i=0; i<bulletCount; i++) {
			timer -= Time.deltaTime;

			if(timer < 0) {

				count = i;
				if (count != 0) {
					x = activeBullet [count - 1].transform.position.x;
					y = activeBullet [count - 1].transform.position.y;

					activeBullet [count - 1].transform.position = new Vector3 (x - (count), y + (count), 0);
					count--;
				}
				GameObject go = Instantiate (testPrefab) as GameObject;
				go.transform.position = transform.position;

				activeBullet.Add (go);
				//GameObject go = roundBulletPooler.GetComponent<ObjectPooler>().GetPooledObject();
				//activeBullet.Add(go);
				//go.SetActive(true);

				timer = 2.0f;
			}
		}
	
		//test = true;
	}

	// Update is called once per frame
	void Update () {
		if (!test) {
			pattern(transform.position, 2, 10, false);
			//test = true;
		}
	}
}
