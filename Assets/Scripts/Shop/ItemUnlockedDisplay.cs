using UnityEngine;
using System.Collections;

public class ItemUnlockedDisplay : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateItemUnlockedDisplay(bool b)
    {
        if(b)
        {
            transform.FindChild("UnlockedIcon").gameObject.SetActive(true);
            transform.FindChild("Cost").gameObject.SetActive(false);
        }
        else
        {
            transform.FindChild("UnlockedIcon").gameObject.SetActive(false);
            transform.FindChild("Cost").gameObject.SetActive(true);
        }
    }
}
