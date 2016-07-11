using UnityEngine;
using System.Collections;

public class justEggs : MonoBehaviour {

    public GameObject lenny;
    public GameObject huh;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool CheckEggs(string input)
    {
        if(input == "lenny")
        {
            GameObject go = Instantiate(lenny) as GameObject;
            go.transform.SetParent(GameObject.Find("Canvas").transform, false);
            return true;
        }
        else if(input == "kappa")
        {
            GameObject go = Instantiate(huh) as GameObject;
            go.transform.SetParent(GameObject.Find("Canvas").transform, false);
        }
        else if(input == "pussydestroyer")
        {
            PlayerPrefs.SetString("ppPlayerName", "kittendecomposer");
            PlayerPrefs.Save();
        }
        else if (input == "morpheus")
        {
            PlayerPrefs.SetString("ppPlayerName", "NotMorpheus");
            PlayerPrefs.Save();
        }
        else if (input == "phobeter")
        {
            PlayerPrefs.SetString("ppPlayerName", "Nope");
            PlayerPrefs.Save();
        }

        return false;
    }
}
