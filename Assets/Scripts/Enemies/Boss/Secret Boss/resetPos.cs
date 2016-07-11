using UnityEngine;
using System.Collections;

public class resetPos : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
		if (name == "minionL-R")
			transform.position = new Vector3 (-4, 0.5f, 0);
		if (name == "minionR-L")
			transform.position = new Vector3 (4, -1, 0);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
