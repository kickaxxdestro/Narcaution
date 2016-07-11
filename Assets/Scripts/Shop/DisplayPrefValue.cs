using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayPrefValue : MonoBehaviour {

    public string playerPref;

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = PlayerPrefs.GetInt(playerPref, 0).ToString();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void UpdateValue()
    {
        GetComponent<Text>().text = PlayerPrefs.GetInt(playerPref, 0).ToString();
    }
}
