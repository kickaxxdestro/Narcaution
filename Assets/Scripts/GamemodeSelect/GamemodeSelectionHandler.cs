using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamemodeSelectionHandler : MonoBehaviour {

    public GameObject Inputfield;
    public GameObject playerDisplay;
    public GameObject nameButton;
    public GameObject offensePanel;
    public GameObject defensePanel;
    public GameObject modeHeader;
    public GameObject modeWarningText;
    public GameObject confirmPanelOffenseDisplay;
    public GameObject confirmPanelDefenseDisplay;
    bool sceneStart = true;
    bool doChangeScene = false;
    float changeSceneTimer = 0f;

	// Use this for initialization
	void Start () {
        //Fade in objects
        modeHeader.GetComponent<AlphaFader>().DoFadeIn();
        StartCoroutine("FadeInDisplayItems");
	}
	
	// Update is called once per frame
	void Update () {
	    if(doChangeScene)
        {
            changeSceneTimer -= Time.deltaTime;
            if (changeSceneTimer <= 0f)
                GameObject.Find("SceneHandler").GetComponent<SceneHandler>().LoadScene("cutsceneScreen");
        }
	}

    //Offense button is pressed
    public void SelectOffense()
    {
        PlayerPrefs.SetInt("ppPlayerGamemode", 0);
        PlayerPrefs.Save();
        confirmPanelOffenseDisplay.SetActive(true);
        confirmPanelDefenseDisplay.SetActive(false);
        GameObject.Find("ColourMaskHandler").GetComponent<ColourMaskController>().ActivateColourMask(ColourMaskController.COLOURMODE.COLOURMODE_TO_ALPHA_GREY, 1f);
    }

    //Defense button is pressed
        public void SelectDefense()
        {
        PlayerPrefs.SetInt("ppPlayerGamemode", 1);
        PlayerPrefs.Save();
        confirmPanelOffenseDisplay.SetActive(false);
        confirmPanelDefenseDisplay.SetActive(true);
        GameObject.Find("ColourMaskHandler").GetComponent<ColourMaskController>().ActivateColourMask(ColourMaskController.COLOURMODE.COLOURMODE_TO_ALPHA_GREY, 1f);

    }

    public void ConfirmSelection()
    {

    }

    public void ConfirmName()
    {
        string targetText = Inputfield.transform.FindChild("Text").GetComponent<Text>().text;
        if (targetText != "")
        {
            PlayerPrefs.SetString("ppPlayerName", targetText);
            PlayerPrefs.SetInt("ppFirstPlay", 1);
            GetComponent<justEggs>().CheckEggs(targetText);
            GameObject.Find("NameHeader").GetComponent<AlphaFader>().DoFadeOut();
            Inputfield.GetComponent<InputField>().interactable = false;
            Inputfield.GetComponent<SliderItem>().DoLerpToLeft();
            nameButton.GetComponent<SliderItem>().DoLerpToDown();
            playerDisplay.GetComponent<SliderItem>().DoLerpToLeft();
            //Change scene after changeSceneTimer delay
            doChangeScene = true;
            changeSceneTimer = 1f;
            PlayerPrefs.SetInt("ppCutsceneNext", 3);
            PlayerPrefs.SetInt("ppSelectedCutscene", 0);
            PlayerPrefs.Save();
        }
    }

    IEnumerator FadeInDisplayItems()
    {
        yield return new WaitForSeconds(1.8f);
        foreach(Transform child in offensePanel.transform)
        {
            child.GetComponent<AlphaFader>().DoFadeIn();
            if (child.GetComponent<Button>() != null)
                child.GetComponent<Button>().interactable = true;
        }

        yield return new WaitForSeconds(0.2f);
        foreach (Transform child in defensePanel.transform)
        {
            child.GetComponent<AlphaFader>().DoFadeIn();
            if (child.GetComponent<Button>() != null)
                child.GetComponent<Button>().interactable = true;
        }

        yield return new WaitForSeconds(0.2f);
        modeWarningText.GetComponent<AlphaFader>().DoFadeIn();

        StopCoroutine("FadeInDisplayItems");
    }
}
