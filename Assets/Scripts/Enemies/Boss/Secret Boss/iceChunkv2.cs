using UnityEngine;
using System.Collections;

public class iceChunkv2 : MonoBehaviour {

	float speed = 5;
	bool attacked;
	patternList attackControl;

	void Start () {
		attackControl = GetComponent<patternList> ();
		transform.position = new Vector3 (transform.position.x, SystemVariables.current.CameraBoundsY);
		
		if (!attacked) {
			attackControl.attack(0);
			attacked = true;
		}
	}

	void OnEnable () {
		attackControl = GetComponent<patternList> ();

		if (!attacked) {
			attackControl.attack(0);
			attacked = true;
		}
	}

	void OnDisable () {
		attacked = false;
		transform.position = new Vector3 (transform.position.x, SystemVariables.current.CameraBoundsY);
	}

	// Update is called once per frame
	void Update () {
		transform.position -= new Vector3 (0, speed* Time.deltaTime, 0);
		transform.Rotate (Vector3.forward* 400* Time.deltaTime);

		if (transform.position.y < -SystemVariables.current.CameraBoundsY)
			this.gameObject.SetActive (false);
	}
}
