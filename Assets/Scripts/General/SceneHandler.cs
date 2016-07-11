using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void LoadScene(string sceneName)
    {
        GameObject loadingScreen = GameObject.Find("LoadingScreen");

        if (loadingScreen != null)
        {
            loadingScreen.GetComponent<LoadingScreenHandler>().sceneToLoad = sceneName;
            loadingScreen.GetComponent<LoadingScreenHandler>().DoTransitionIn();
        }
        else
        {   
            SceneManager.LoadScene(sceneName);
            AudioManager.audioManager.UpdateBGM();
        }
    }
}
