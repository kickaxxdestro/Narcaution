using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

//Checks to see if there is a cutscene to display based on selected and current level

public class CutsceneChecker : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DoPreproomCutsceneCheck()
    {
        if (CheckForCutscene())
            GameObject.Find("SceneHandler").GetComponent<SceneHandler>().LoadScene("cutsceneScreen");
        else
            GameObject.Find("SceneHandler").GetComponent<SceneHandler>().LoadScene("gameScene");
    }

    public bool CheckForCutscene()
    {
        int selectedLvl = PlayerPrefs.GetInt("ppSelectedLevel", 1);
        int currentLvl = PlayerPrefs.GetInt("ppCurrentLevel", 1);

        if (selectedLvl != currentLvl)
            return false;

        switch (selectedLvl)
        {
            case 1:
                PlayerPrefs.SetInt("ppSelectedCutscene", 1);
                break;
            case 5:
                PlayerPrefs.SetInt("ppSelectedCutscene", 5);
                break;
            case 6:
                PlayerPrefs.SetInt("ppSelectedCutscene", 6);
                break;
            case 9:
                PlayerPrefs.SetInt("ppSelectedCutscene", 9);
                break;
            case 14:
                PlayerPrefs.SetInt("ppSelectedCutscene", 14);
                break;
            case 17:
                PlayerPrefs.SetInt("ppSelectedCutscene", 17);
                break;
            case 21:
                PlayerPrefs.SetInt("ppSelectedCutscene", 21);
                break;
            default:
                return false;
        }
        PlayerPrefs.SetInt("ppCutsceneNext", 0);
        PlayerPrefs.Save();
        return true;
    }

    public void PlayCutsceneFromGallery(int cutsceneID)
    {
        PlayerPrefs.SetInt("ppCutsceneNext", 1);
        PlayerPrefs.SetInt("ppSelectedCutscene", cutsceneID);
        PlayerPrefs.Save();

        GameObject.Find("SceneHandler").GetComponent<SceneHandler>().LoadScene("cutsceneScreen");
    }
}