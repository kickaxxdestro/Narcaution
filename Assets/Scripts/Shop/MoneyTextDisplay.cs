using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyTextDisplay : MonoBehaviour {

    void Awake()
    {
        UpdateText();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    //this.GetComponent<Text>().text = PlayerPrefs.GetInt("ppPlayerMoney", 0).ToString();
	}

    //Update text based on player's current money
    public void UpdateText()
    {
        this.GetComponent<Text>().text = PlayerPrefs.GetInt("ppPlayerMoney", 0).ToString();
    }
}
