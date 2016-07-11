using UnityEngine;
using System.Collections;

public class attackActivate : MonoBehaviour {
	
	Animator currAnimator;
	patternList attackController;
	
	void OnEnable () {
		attackController = GetComponent<patternList> ();
		attackController.attack (0);
		
		//reposition
		transform.position = transform.parent.position + new Vector3 (0, 1.7f, 0);
		//currAnimator.SetBool ("start", true);
	}
	
	void OnDisable () {
		attackController.forceDestroy ();
	}
	
	// Update is called once per frame
	void Update () {
		if (attackController.SpawnersDone) {
			this.gameObject.SetActive(false);
		}
		
	}
}
