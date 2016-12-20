using UnityEngine;
using System.Collections;

public class LoadPrepRoomInfo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<enterLevel> ().SetLevel (PlayerPrefs.GetInt ("ppSelectedLevel", 1));
		gameObject.GetComponentInChildren<PowerUpSelector> ().CheckDisableBombAndMissile ();
		gameObject.GetComponentInChildren<MedalInfoDisplayHandler> ().SetTargetScore (PlayerPrefs.GetInt ("ppSelectedLevel", 1));
	}
}
