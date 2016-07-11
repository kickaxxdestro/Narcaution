using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PausedMenu : MonoBehaviour {
	
	public bool paused;
	public bool retMain;
	public bool restart;
	
	GameObject pausePanel;
	GameObject comfirmationPanel;
    GameObject gameOverPanel;

	public GameObject PauseButton;
//	public Sprite World1Pause;
//	public Sprite World1PausePressed;
//
//	public Sprite World2Pause;
//	public Sprite World2PausePressed;
//
//	public Sprite World3Pause;
//	public Sprite World3PausePressed;

	// Use this for initialization
	void Start () {
	
		restart = false;
		retMain = false;
		paused = false;
		pausePanel = GameObject.Find("pausepanel");
		pausePanel.SetActive(false);
		
		comfirmationPanel = GameObject.Find("ConfirmationMenu");
		comfirmationPanel.SetActive(false);

        gameOverPanel = GameObject.Find("GameOverPanel");
        gameOverPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

//		switch(PlayerPrefs.GetString("ppSelectedLevel"))
//		{
//		case "Level1":
//		case "Level2":
//		case "Level3":
//		case "Level4":
//			PauseButton.GetComponent<Image>().sprite = World1Pause;
//			SpriteState tempSS = PauseButton.GetComponent<Button>().spriteState;
//			tempSS.pressedSprite = World1PausePressed;
//			PauseButton.GetComponent<Button>().spriteState = tempSS;
//			break;
//			
//		case "Level5":
//		case "Level6":
//		case "Level7":
//		case "Level8":
//			PauseButton.GetComponent<Image>().sprite = World2Pause;
//			SpriteState tempSS2 = PauseButton.GetComponent<Button>().spriteState;
//			tempSS2.pressedSprite = World2PausePressed;
//			PauseButton.GetComponent<Button>().spriteState = tempSS2;
//			break;
//			
//		case "Level9":
//		case "Level10":
//		case "Level11":
//		case "Level12":
//			PauseButton.GetComponent<Image>().sprite = World3Pause;
//			SpriteState tempSS3 = PauseButton.GetComponent<Button>().spriteState;
//			tempSS3.pressedSprite = World3PausePressed;
//			PauseButton.GetComponent<Button>().spriteState = tempSS3;
//			break;
//		}

	}
	
	public void pauseGame ()
	{
		paused = true;
		pausePanel.SetActive(true);
		Time.timeScale = 0;
	}
	
	public void unPause ()
	{
		paused = false;
		pausePanel.SetActive(false);
		Time.timeScale =1;
	}
	
	public void BackToMainMenu ()
	{
		retMain = true;
		pausePanel.SetActive(false);
		comfirmationPanel.SetActive(true);
        gameOverPanel.SetActive(false);
	}
	
	public void RestartLevel()
	{
		restart = true;
		pausePanel.SetActive(false);
		comfirmationPanel.SetActive(true);
        gameOverPanel.SetActive(false);
		
	}
	public void Confirmed ()
	{
		paused = false;
		Time.timeScale =1;
		if(retMain == true)
		{
            GameObject.Find("SceneHandler").GetComponent<SceneHandler>().LoadScene("levelSelect");
		}
		else if( restart == true)
		{
			Application.LoadLevel(Application.loadedLevel);
            AudioManager.audioManager.UpdateBGM();
		}
	}
	public void Decline ()
	{
		pausePanel.SetActive(true);
		comfirmationPanel.SetActive(false);
		restart = false;
		retMain = false;
	}

    public void ImmediateRestart()
    {
        GameObject.Find("SceneHandler").GetComponent<SceneHandler>().LoadScene("gameScene");
    }

    public void ImmediateBackToMainMenu()
    {
        Application.LoadLevel("mainMenu");
    }
}
