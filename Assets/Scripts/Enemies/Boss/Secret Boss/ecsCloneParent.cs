using UnityEngine;
using System.Collections;

public class ecsCloneParent : MonoBehaviour {

	public GameObject cloneL, cloneR;
	bool spawned;

	// Use this for initialization
	void Start () {
	}

	void OnEnable () {
		GameObject go = Instantiate (cloneL) as GameObject;
		GameObject go1 = Instantiate (cloneR) as GameObject;

		go.transform.position = new Vector3 (0, 50, 0);
		go1.transform.position = new Vector3 (0, 50, 0);

		go.GetComponent<cloneBehaviour> ().cloneState = cloneBehaviour.state.changePos; 
		go1.GetComponent<cloneBehaviour> ().cloneState = cloneBehaviour.state.changePos; 

		go.transform.parent = transform;
		go1.transform.parent = transform;

		spawned = true;
	}

	void OnDisable () {
		spawned = false;
	}

	// Update is called once per frame
	void Update () {
		if (spawned) {
			if (transform.childCount <= 0) 
				gameObject.SetActive (false);
		}
	}
}
