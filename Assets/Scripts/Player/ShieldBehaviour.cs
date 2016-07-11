using UnityEngine;
using System.Collections;

public class ShieldBehaviour : MonoBehaviour {
	//float currentRotation = 60.0f;
	bool noneActive;

	// Use this for initialization
	void Start () {
	}

	public bool removeShield () {
		bool destroyed = false;

		foreach (Transform child in transform) {
			if(child.gameObject.activeInHierarchy && !destroyed) {
				child.gameObject.SetActive(false);
				noneActive = true;
				destroyed = true;
			}

			if(destroyed) {
				if(!child.gameObject.activeInHierarchy)
					noneActive = true;
				else {
					noneActive = false;
					return noneActive;
				}
			}
		}

		return noneActive; 
	}

	// Update is called once per frame
	void Update () {

		//transform.Rotate(0,0,currentRotation * Time.deltaTime);
	}
}
