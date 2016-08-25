using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelButton : MonoBehaviour {

    public int level;
    public bool doTouchCheck = false;
    public bool disabled = false;
    public bool offense = false;
    public bool defense = false;

	// Use this for initialization
	void Start () {

		GetComponent<AlphaFader>().fadeColor.a = 0f;
		GetComponent<SpriteRenderer>().color = GetComponent<AlphaFader>().fadeColor;

		GetComponentsInChildren<AlphaFader>()[1].fadeColor.a = 0f;
		GetComponentInChildren<Text>().color = GetComponentsInChildren<AlphaFader>()[1].fadeColor;

		GetComponent<Collider2D>().enabled = false;

		if (level == 21) 
		{
			if (CheckUnlocked (PlayerPrefs.GetInt ("ppCurrentLevel", 1))) 
			{
				GetComponent<AlphaFader>().fadeColor.a = 1f;
				GetComponent<SpriteRenderer>().color = GetComponent<AlphaFader>().fadeColor;

				GetComponentsInChildren<AlphaFader>()[1].fadeColor.a = 1f;
				GetComponentInChildren<Text>().color = GetComponentsInChildren<AlphaFader>()[1].fadeColor;

				doTouchCheck = true;
				GetComponent<Collider2D>().enabled = true;
				disabled = false;
			}
			else
			{
				doTouchCheck = false;
				GetComponent<Collider2D>().enabled = false;
				disabled = true;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
        if (doTouchCheck && !disabled)
        {
            TouchCheck touchcheck = GetComponent<TouchCheck>();

            if (touchcheck != null)
            {
                if (touchcheck.CheckTouchOnCollider())
                {
                    print("Touched");
                    PlayerPrefs.SetInt("ppSelectedLevel", level);
                    if(offense == true)
                    {
                        PlayerPrefs.SetInt("ppPlayerGamemode", 0);
                    }
                    if(defense == true)
                    {
                        PlayerPrefs.SetInt("ppPlayerGamemode", 1);
                    }
                    Debug.Log(PlayerPrefs.GetInt("ppPlayerGamemode"));
                    //PlayerPrefs.SetString("ppSelectedLevel", "Level" + level.GetComponent<LevelGeneratorScript>().levelID);
                    PlayerPrefs.Save();

                    GameObject.Find("ConfirmPanel").GetComponent<SliderItem>().DoLerpToCenter_FromRight();
					GameObject.Find("ConfirmPanel").GetComponent<enterLevel>().SetLevel(level);
					GameObject.Find ("ConfirmPanel").GetComponentInChildren<PowerUpSelector> ().CheckDisableBombAndMissile ();
					GameObject.Find("LevelRank").GetComponent<MedalInfoDisplayHandler> ().SetTargetScore (level);
                    GameObject.Find("ColourMaskHandler").GetComponent<ColourMaskController>().ActivateColourMask(ColourMaskController.COLOURMODE.COLOURMODE_TO_ALPHA_GREY, 1f);
					if(transform.parent.GetComponent<WorldButton>())
						transform.parent.GetComponent<WorldButton>().DisableChildButtons();

                    GetComponent<Animator>().Play(0);
                    AudioManager.audioManager.PlayDefaultButtonSound();

                    print("AnimPlayed");
                }
            }
        }
	}

    public void Disable()
    {
		GetComponent<AlphaFader> ().fadeColor = Color.gray;
		GetComponent<AlphaFader>().fadeColor.a = 0f;
		GetComponent<SpriteRenderer>().color = GetComponent<AlphaFader>().fadeColor;

		GetComponentInChildren<Text>().text = "LOCKED";

        disabled = true;
        GetComponent<Collider2D>().enabled = false;
    }

    public void DoOutTransition()
    {
        doTouchCheck = false;
        GetComponent<Collider2D>().enabled = false;
		GetComponent<AlphaFader>().DoFadeOut();
		GetComponentsInChildren<AlphaFader>()[1].DoFadeOut();
    }

    public void DoInTransition()
    {
        doTouchCheck = true;
        if (!disabled)
            GetComponent<Collider2D>().enabled = true;
		GetComponent<AlphaFader>().DoFadeIn();
		GetComponentsInChildren<AlphaFader>()[1].DoFadeIn();
    }

    //Check if this level is unlocked based on the player's current level
    public bool CheckUnlocked(int currentLevel)
    {
        return (level <= currentLevel);
    }
}
