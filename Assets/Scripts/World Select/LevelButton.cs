using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelButton : MonoBehaviour {

    public GameObject level;
    public bool doTouchCheck = false;
    public bool disabled = false;
    public bool offense = false;
    public bool defense = false;

	// Use this for initialization
	void Start () {
        GetComponent<AlphaFader>().fadeColor.a = 0f;
        GetComponent<SpriteRenderer>().color = GetComponent<AlphaFader>().fadeColor;
        GetComponent<Collider2D>().enabled = false;
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
                    PlayerPrefs.SetInt("ppSelectedLevel", level.GetComponent<LevelGeneratorScript>().levelID);
                    if(offense == true)
                    {
                        PlayerPrefs.SetInt("ppPlayerGamemode", 0);
                    }
                    if(defense == true)
                    {
                        PlayerPrefs.SetInt("ppPlayerGamemode", 1);
                    }
                    //PlayerPrefs.SetString("ppSelectedLevel", "Level" + level.GetComponent<LevelGeneratorScript>().levelID);
                    PlayerPrefs.Save();

                    GameObject.Find("ConfirmPanel").GetComponent<SliderItem>().DoLerpToCenter_FromRight();
                    GameObject.Find("ConfirmPanel").GetComponent<enterLevel>().SetLevel(level.GetComponent<LevelGeneratorScript>());
                    GameObject.Find("ColourMaskHandler").GetComponent<ColourMaskController>().ActivateColourMask(ColourMaskController.COLOURMODE.COLOURMODE_TO_ALPHA_GREY, 1f);
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
        if(this.transform.parent.name == "World1")
        {
            GetComponent<SpriteRenderer>().sprite = GameObject.Find("WorldList").GetComponent<WorldListManager>().disabledLevelButtonSpriteWorld1;
        }
        else if (this.transform.parent.name == "World2")
        {
            GetComponent<SpriteRenderer>().sprite = GameObject.Find("WorldList").GetComponent<WorldListManager>().disabledLevelButtonSpriteWorld2;
        }
        else if (this.transform.parent.name == "World3")
        {
            GetComponent<SpriteRenderer>().sprite = GameObject.Find("WorldList").GetComponent<WorldListManager>().disabledLevelButtonSpriteWorld3;
        }
        else if (this.transform.parent.name == "World4")
        {
            GetComponent<SpriteRenderer>().sprite = GameObject.Find("WorldList").GetComponent<WorldListManager>().disabledLevelButtonSpriteWorld4;
        }
        else if (this.transform.parent.name == "World5")
        {
            GetComponent<SpriteRenderer>().sprite = GameObject.Find("WorldList").GetComponent<WorldListManager>().disabledLevelButtonSpriteWorld5;
        }
        disabled = true;
        GetComponent<Collider2D>().enabled = false;

    }

    public void DoOutTransition()
    {
        doTouchCheck = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<AlphaFader>().DoFadeOut();
    }

    public void DoInTransition()
    {
        doTouchCheck = true;
        if (!disabled)
            GetComponent<Collider2D>().enabled = true;
        GetComponent<AlphaFader>().DoFadeIn();
    }

    //Check if this level is unlocked based on the player's current level
    public bool CheckUnlocked(int currentLevel)
    {
        return (level.GetComponent<LevelGeneratorScript>().levelID <= currentLevel);
    }
}
