using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CutsceneLocker : MonoBehaviour {

    public Button cutsceneDay2Button;
    public Button cutsceneDay3Button;
    public Button cutsceneDay4Button;
    public Button cutsceneDay5Button;
    public Button cutsceneDayDButton;
    public Button cutsceneDayD2Button;
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
            cutsceneDay2Button.interactable = false;
            cutsceneDay3Button.interactable = false;
            cutsceneDay4Button.interactable = false;
            cutsceneDay5Button.interactable = false;
            cutsceneDayDButton.interactable = false;
            cutsceneDayD2Button.interactable = false;
            

            cutsceneDay2Button.GetComponent<Image>().color = lockedColor;
            cutsceneDay3Button.GetComponent<Image>().color = lockedColor;
            cutsceneDay4Button.GetComponent<Image>().color = lockedColor;
            cutsceneDay5Button.GetComponent<Image>().color = lockedColor;
            cutsceneDayDButton.GetComponent<Image>().color = lockedColor;
            cutsceneDayD2Button.GetComponent<Image>().color = lockedColor;
        }
        else if(currentLvl < 6)
        {
            cutsceneDay3Button.interactable = false;
            cutsceneDay4Button.interactable = false;
            cutsceneDay5Button.interactable = false;
            cutsceneDayDButton.interactable = false;
            cutsceneDayD2Button.interactable = false;

            cutsceneDay3Button.GetComponent<Image>().color = lockedColor;
            cutsceneDay4Button.GetComponent<Image>().color = lockedColor;
            cutsceneDay5Button.GetComponent<Image>().color = lockedColor;
            cutsceneDayDButton.GetComponent<Image>().color = lockedColor;
            cutsceneDayD2Button.GetComponent<Image>().color = lockedColor;
        }
        else if(currentLvl < 9)
        {
            cutsceneDay4Button.interactable = false;
            cutsceneDay5Button.interactable = false;
            cutsceneDayDButton.interactable = false;
            cutsceneDayD2Button.interactable = false;

            cutsceneDay4Button.GetComponent<Image>().color = lockedColor;
            cutsceneDay5Button.GetComponent<Image>().color = lockedColor;
            cutsceneDayDButton.GetComponent<Image>().color = lockedColor;
            cutsceneDayD2Button.GetComponent<Image>().color = lockedColor;
        }
        else if(currentLvl < 14)
        {
            cutsceneDay5Button.interactable = false;
            cutsceneDayDButton.interactable = false;
            cutsceneDayD2Button.interactable = false;

            cutsceneDay5Button.GetComponent<Image>().color = lockedColor;
            cutsceneDayDButton.GetComponent<Image>().color = lockedColor;
            cutsceneDayD2Button.GetComponent<Image>().color = lockedColor;
        }
        else if(currentLvl < 21)
        {
            cutsceneDayDButton.interactable = false;
            cutsceneDayD2Button.interactable = false;

            cutsceneDayDButton.GetComponent<Image>().color = lockedColor;
            cutsceneDayD2Button.GetComponent<Image>().color = lockedColor;
        }
        else if (currentLvl < 22)
        {
            cutsceneDayD2Button.interactable = false;

            cutsceneDayD2Button.GetComponent<Image>().color = lockedColor;
        }

        Destroy(this.gameObject);        
    }
}
