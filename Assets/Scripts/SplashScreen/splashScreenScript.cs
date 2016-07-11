using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class splashScreenScript : MonoBehaviour {

	public float timeToFadeOut = 2.0f;
	public float timeToNextScene = 2.0f;
	public Image GameLogo;
	public Image SIDMLogo;
	public Image NYPLogo;
    public Image CNBLogo;
    public Text loadingText;
    public GameObject loadingScreen;

	Color GameLogoColor;
	Color SIDMLogoColor;
	Color NYPLogoColor;

    bool nextScene = false;

	// Use this for initialization
	void Start () {
#if UNITY_STANDALONE_WIN
        Screen.fullScreen = false;
        Screen.SetResolution(540, 960, false);

        GameLogoColor = GameLogo.color;
        SIDMLogoColor = SIDMLogo.color;
        NYPLogoColor = NYPLogo.color;
        print("Splash");

        //GameObject go1 = Instantiate(loadingScreen) as GameObject;
        //go1.name = "LoadingScreen";
        return;
#endif

		Input.multiTouchEnabled = false;
		Screen.SetResolution(720, (int)(720 * (1 / Camera.main.aspect)), true, 60);


        GameLogoColor = GameLogo.color;
		SIDMLogoColor = SIDMLogo.color;
		NYPLogoColor = NYPLogo.color;
        print("Splash");

        GameObject go = Instantiate(loadingScreen) as GameObject;
        go.name = "LoadingScreen";
	}
	
	// Update is called once per frame
	void Update () {
        if(nextScene)
            SceneManager.LoadScene("mainMenuSliding");
		if (timeToFadeOut > 0) {
			timeToFadeOut -= 1f * Time.deltaTime;
		}
		else if (timeToFadeOut <= 0) {
			GameLogoColor.a -= 0.5f * Time.deltaTime;
			GameLogo.color = GameLogoColor;
		}

		if (GameLogoColor.a <= 0f) {
			SIDMLogoColor.a += 1f * Time.deltaTime;
			SIDMLogo.color = SIDMLogoColor;
            CNBLogo.color = SIDMLogoColor;

			NYPLogoColor.a += 1f * Time.deltaTime;
			NYPLogo.color = NYPLogoColor;
			if (SIDMLogoColor.a >= 1f) {
				timeToNextScene -= 1f * Time.deltaTime;
			}
			
			if(timeToNextScene <= 0)
			{
                if (PlayerPrefs.GetInt("ppFirstPlay", 0) == 0)
                    GameObject.Find("SceneHandler").GetComponent<SceneHandler>().LoadScene("mainMenuSliding");
                else
                    GameObject.Find("SceneHandler").GetComponent<SceneHandler>().LoadScene("mainMenuSliding");
			}
		}


	}
}