using UnityEngine;
using System.Collections;

public class BackgroundHandler : MonoBehaviour {

    //List of background lists
    public GameObject[] backgroundList;

    GameObject currentBackground;

	// Use this for initialization
	void Start () {
        int selectedLevel = PlayerPrefs.GetInt("ppSelectedLevel", 1);

        GameObject background = null;

        DisableAllBackgrounds();

        if (selectedLevel <= 4)
        {
            foreach (Transform child in backgroundList[0].transform)
            {
                child.gameObject.SetActive(true);
                if (child.gameObject.name == "Background")
                    background = child.gameObject;
            }
            print("first set");
        }
        else if (selectedLevel <= 8)
        {
            foreach (Transform child in backgroundList[1].transform)
            {
                child.gameObject.SetActive(true);
                if (child.gameObject.name == "Background")
                    background = child.gameObject;
            }
            print("second set");
        }
        else if (selectedLevel <= 12)
        {
            foreach (Transform child in backgroundList[2].transform)
            {
                child.gameObject.SetActive(true);
                if (child.gameObject.name == "Background")
                    background = child.gameObject;
            }
        }
        else if (selectedLevel <= 16)
        {
            foreach (Transform child in backgroundList[3].transform)
            {
                child.gameObject.SetActive(true);
                if (child.gameObject.name == "Background")
                    background = child.gameObject;
            }
        }
        else
        {
            foreach (Transform child in backgroundList[4].transform)
            {
                child.gameObject.SetActive(true);
                if (child.gameObject.name == "Background")
                    background = child.gameObject;
            }
        }

        if (background == null)
        {
            print("No background found");
            return;
        }

        //I have no idea why this part needs to be here for the shaking to work
        /**/
        background.GetComponent<ShakeTexture>().enabled = false;
        background.GetComponent<ShakeTexture>().enabled = true;
        /**/

        currentBackground = background;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void DisableAllBackgrounds()
    {
        for (int i = 0; i < backgroundList.Length; ++i)
            foreach(Transform child in backgroundList[i].transform)
                child.gameObject.SetActive(false);

    }

    public void ActivateCurrentBackgroundShake(float duration)
    {
        currentBackground.GetComponent<ShakeTexture>().DoShake(duration);
    }
}
