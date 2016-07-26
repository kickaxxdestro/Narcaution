using UnityEngine;
using System.Collections;

public class WorldButton : MonoBehaviour {

    public GameObject[] levelList;
    public GameObject[] connectorList;
    bool doTouchCheck = true;
    bool disabled = false;

    GameObject worldListManager;
    GameObject displaySprite;

	GameObject ButtonToZoomInto;

    void Awake()
    {
        displaySprite = transform.FindChild("DisplaySprite").gameObject;
    }

	// Use this for initialization
	void Start () {
        worldListManager = GameObject.Find("WorldList");
        int currentLevel = PlayerPrefs.GetInt("ppCurrentLevel", 1);

		int highestNum = 0;

        foreach (GameObject levelItem in levelList)
        {
			if (!levelItem.GetComponent<LevelButton> ().CheckUnlocked (currentLevel)) 
			{
				levelItem.GetComponent<LevelButton> ().Disable ();
			} 
			else if (levelItem.GetComponent<LevelButton> ().level.GetComponent<LevelGeneratorScript>().levelID > highestNum)
			{
				ButtonToZoomInto = levelItem;
			}
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    void LateUpdate()
    {

        if (doTouchCheck && !disabled)
        {
            TouchCheck touchcheck = GetComponent<TouchCheck>();

            if (touchcheck != null)
            {
                if (touchcheck.CheckTouchOnCollider())
                {
                    //print("Hit");
					worldListManager.GetComponent<WorldListManager>().DoSelectedTransitions(gameObject);
					GameObject.Find("Main Camera").GetComponent<CameraControl2D>().MoveToObject(ButtonToZoomInto);
                    foreach (GameObject connector in connectorList)
                    {
                        connector.GetComponent<ControlWorldConnector>().DoInTransition();
                    }
                    foreach (GameObject levelItem in levelList)
                    {
                        levelItem.GetComponent<LevelButton>().DoInTransition();
                    }
                    AudioManager.audioManager.PlayDefaultButtonSound();
                }
            }
        }
    }

    public void Disable()
    {
        GetComponent<SpriteRenderer>().color = Color.gray;
        displaySprite.GetComponent<SpriteRenderer>().color = Color.gray;
        disabled = true;
        GetComponent<AlphaFader>().fadeColor = Color.gray;
        displaySprite.GetComponent<AlphaFader>().fadeColor = Color.gray;
        GetComponent<Collider2D>().enabled = false;
    }

    public void DoFadeOutDisplay()
    {
		gameObject.GetComponent<AlphaFader>().DoFadeIn();
        displaySprite.GetComponent<AlphaFader>().DoFadeOut();
        GetComponent<Collider2D>().enabled = false;
        doTouchCheck = false;
    }

    public void DoOutTransition()
    {
        doTouchCheck = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<AlphaFader>().DoFadeOut();
        displaySprite.GetComponent<AlphaFader>().DoFadeOut();
    }

    public void DoInTransition()
    {
        doTouchCheck = true;
        if(!disabled)
            GetComponent<Collider2D>().enabled = true;
		GetComponent<AlphaFader>().DoFadeIn();
		gameObject.GetComponent<AlphaFader>().DoFadeOut();
        displaySprite.GetComponent<AlphaFader>().DoFadeIn();
        foreach (GameObject connector in connectorList)
        {
            connector.GetComponent<ControlWorldConnector>().DoOutTransition();
        }
        foreach (GameObject levelItem in levelList)
        {
            levelItem.GetComponent<LevelButton>().DoOutTransition();
        } 
    }

    public void DisableChildButtons()
    {
        foreach(GameObject levelItem in levelList)
        {
                levelItem.GetComponent<LevelButton>().doTouchCheck = false;
        }
    }

    public void EnableChildButtons()
    {
        foreach (GameObject levelItem in levelList)
        {
            if (!levelItem.GetComponent<LevelButton>().disabled)
            levelItem.GetComponent<LevelButton>().doTouchCheck = true;
        }
    }
}
