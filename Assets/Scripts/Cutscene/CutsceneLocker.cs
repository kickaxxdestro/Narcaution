using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CutsceneLocker : MonoBehaviour {

    public Button cutsceneDay1EndButton;
    public Button cutsceneDay2Button;
    public Button cutsceneDay3Button;
    public Button cutsceneDay4Button;
    public Button cutsceneDay5Button;
    public Button cutsceneDay6Button;
    public Button cutsceneDayEndingButton;

    // Use this for initialization
    void Start()
    {
        Color lockedColor = Color.white;
        lockedColor.r = 0.5f;
        lockedColor.g = 0.5f;
        lockedColor.b = 0.5f;

        int currentLvl = PlayerPrefs.GetInt("ppCurrentLevel", 1);
        if (currentLvl < 5)
        {
            cutsceneDay1EndButton.interactable = false;
            cutsceneDay2Button.interactable = false;
            cutsceneDay3Button.interactable = false;
            cutsceneDay4Button.interactable = false;
            cutsceneDay5Button.interactable = false;
            cutsceneDay6Button.interactable = false;
            cutsceneDayEndingButton.interactable = false;

            cutsceneDay1EndButton.GetComponent<Image>().color = lockedColor;
            cutsceneDay2Button.GetComponent<Image>().color = lockedColor;
            cutsceneDay3Button.GetComponent<Image>().color = lockedColor;
            cutsceneDay4Button.GetComponent<Image>().color = lockedColor;
            cutsceneDay5Button.GetComponent<Image>().color = lockedColor;
            cutsceneDay6Button.GetComponent<Image>().color = lockedColor;
            cutsceneDayEndingButton.GetComponent<Image>().color = lockedColor;
        }
        else if(currentLvl < 6)
        {
            cutsceneDay2Button.interactable = false;
            cutsceneDay3Button.interactable = false;
            cutsceneDay4Button.interactable = false;
            cutsceneDay5Button.interactable = false;
            cutsceneDay6Button.interactable = false;
            cutsceneDayEndingButton.interactable = false;

            cutsceneDay3Button.GetComponent<Image>().color = lockedColor;
            cutsceneDay4Button.GetComponent<Image>().color = lockedColor;
            cutsceneDay5Button.GetComponent<Image>().color = lockedColor;
            cutsceneDay6Button.GetComponent<Image>().color = lockedColor;
            cutsceneDayEndingButton.GetComponent<Image>().color = lockedColor;
        }
        else if(currentLvl < 9)
        {
            cutsceneDay3Button.interactable = false;
            cutsceneDay4Button.interactable = false;
            cutsceneDay5Button.interactable = false;
            cutsceneDay6Button.interactable = false;
            cutsceneDayEndingButton.interactable = false;

            cutsceneDay3Button.GetComponent<Image>().color = lockedColor;
            cutsceneDay4Button.GetComponent<Image>().color = lockedColor;
            cutsceneDay5Button.GetComponent<Image>().color = lockedColor;
            cutsceneDay6Button.GetComponent<Image>().color = lockedColor;
            cutsceneDayEndingButton.GetComponent<Image>().color = lockedColor;
        }
        else if(currentLvl < 14)
        {
            cutsceneDay4Button.interactable = false;
            cutsceneDay5Button.interactable = false;
            cutsceneDay6Button.interactable = false;
            cutsceneDayEndingButton.interactable = false;

            cutsceneDay4Button.GetComponent<Image>().color = lockedColor;
            cutsceneDay5Button.GetComponent<Image>().color = lockedColor;
            cutsceneDay6Button.GetComponent<Image>().color = lockedColor;
            cutsceneDayEndingButton.GetComponent<Image>().color = lockedColor;
        }
        else if(currentLvl < 17)
        {
            cutsceneDay5Button.interactable = false;
            cutsceneDay6Button.interactable = false;
            cutsceneDayEndingButton.interactable = false;

            cutsceneDay5Button.GetComponent<Image>().color = lockedColor;
            cutsceneDay6Button.GetComponent<Image>().color = lockedColor;
            cutsceneDayEndingButton.GetComponent<Image>().color = lockedColor;
        }
        else if (currentLvl < 21)
        {
            cutsceneDay6Button.interactable = false;
            cutsceneDayEndingButton.interactable = false;

            cutsceneDay6Button.GetComponent<Image>().color = lockedColor;
            cutsceneDayEndingButton.GetComponent<Image>().color = lockedColor;
        }
        else if(currentLvl == 22 && PlayerPrefs.GetInt("ppSeenEndScreen") != 1)
        {
            cutsceneDayEndingButton.interactable = false;
            cutsceneDayEndingButton.GetComponent<Image>().color = lockedColor;
        }

        Destroy(this.gameObject);        
    }
}
