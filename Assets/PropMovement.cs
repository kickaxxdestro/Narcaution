using UnityEngine;
using System.Collections;

public class PropMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector2(0 ,Time.deltaTime * -1.2f));

		if (transform.position.y <= -6.5f) {
			gameObject.SetActive(false);
		}
	}
}
