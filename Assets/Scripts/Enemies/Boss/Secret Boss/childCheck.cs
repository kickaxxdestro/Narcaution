using UnityEngine;
using System.Collections;

public class childCheck : MonoBehaviour {

	bool noneActive;

	void Start () {
	}

	// Use this for initialization
	void OnEnable () {
		noneActive = false;

		foreach (Transform child in transform) {
			child.gameObject.SetActive(true);
		}
	}

	// Update is called once per frame
	void Update () {
		foreach (Transform child in transform) {
			if(child.gameObject.activeInHierarchy) {
				noneActive = false;
				break;
			}
			else 
				noneActive = true;
		}

		if (noneActive)
			//Destroy(this.gameObject);
			this.gameObject.SetActive (false);
	}
}
