using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class minigameHandler : MonoBehaviour {

    public int baseMinRibbons = 1;
    public int baseMaxRibbons = 5;
    public GameObject[] chestList;
    public GameObject openChest;
    Vector3 centerPosition;  //position to lerp selected chest to
    public float chestLerpSpeed = 2f;
    public float openChestOffsetY;
    public GameObject ribbonDisplay;
    public GameObject buttonPanel;
    public GameObject header;

    GameObject selectedChest;
    bool lerpChestToCenter = false;
    bool checkFading = true;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start () {
        Color transparent = Color.white;
        transparent.a = 0f;

        openChest.GetComponent<Image>().color = transparent;
        header.GetComponent<Image>().color = transparent;
        header.GetComponent<AlphaFader>().DoFadeIn(1f);

        foreach (GameObject chest in chestList)
        {
            chest.GetComponent<Image>().color = transparent;
            chest.GetComponent<Button>().interactable = false;
        }

        centerPosition.y = Screen.height * 0.7f;

        openChest.transform.position = new Vector3(openChest.transform.position.x, centerPosition.y + openChestOffsetY, openChest.transform.position.z);

        StartCoroutine("FadeInChests");
	}
	
	// Update is called once per frame
	void Update () {
        if(checkFading)
        {
            bool fadeDone = true;
            foreach (GameObject chest in chestList)
            {
                if (!chest.GetComponent<AlphaFader>().fadingDone)
                    fadeDone = false;
            }
            if(fadeDone)
            {
                foreach (GameObject chest in chestList)
                    chest.GetComponent<Button>().interactable = true;
                checkFading = false;
            }
        }

        if (lerpChestToCenter)
        {
            if (Mathf.Abs(selectedChest.transform.position.y - centerPosition.y) > 0.5f)
                selectedChest.transform.position = new Vector3(selectedChest.transform.position.x, Mathf.Lerp(selectedChest.transform.position.y, centerPosition.y, Time.deltaTime * chestLerpSpeed), selectedChest.transform.position.z);
            else
            {
                selectedChest.transform.position = new Vector3(selectedChest.transform.position.x, centerPosition.y, selectedChest.transform.position.z);
                openChest.transform.position = new Vector3(selectedChest.transform.position.x, centerPosition.y + openChestOffsetY, selectedChest.transform.position.z);

                lerpChestToCenter = false;
                StartCoroutine("OpenChest");
            }
        }
	}

    IEnumerator OpenChest()
    {
        yield return new WaitForSeconds(1f);

        selectedChest.GetComponent<AlphaFader>().DoFadeOut(0.5f);
        openChest.GetComponent<AlphaFader>().DoFadeIn(0.5f);

        AssignRibbons();

        ribbonDisplay.GetComponent<SliderItem>().DoLerpToCenter_FromRight();

        if (PlayerPrefs.GetInt("ppSelectedLevel", 0) == 22)
        {
            buttonPanel.transform.FindChild("Default").gameObject.SetActive(false);
            buttonPanel.transform.FindChild("EndButton").gameObject.SetActive(true);
        }

        buttonPanel.GetComponent<SliderItem>().DoLerpToCenter_FromLeft();

        openChest.transform.FindChild("ChestItem").GetComponent<Image>().color = Color.white;
        openChest.transform.FindChild("ChestItem").GetComponent<Animator>().enabled = true;

        audioSource.Play();

        StopCoroutine("OpenChest");
    }

    IEnumerator FadeInChests()
    {
        yield return new WaitForSeconds(0.5f);

        foreach (GameObject chest in chestList)
        {
            chest.GetComponent<AlphaFader>().DoFadeIn();

            yield return new WaitForSeconds(0.3f);
        }

        StopCoroutine("FadeInChests");
    }

    public void SelectChest(GameObject selectedChest)
    {
        this.selectedChest = selectedChest;
        foreach (GameObject chest in chestList)
        {
            if (chest == selectedChest)
                continue;

            chest.GetComponent<AlphaFader>().DoFadeOut(0.5f);
            chest.GetComponent<Button>().interactable = false;
        }
        lerpChestToCenter = true;
        header.GetComponent<AlphaFader>().DoFadeOut(0.5f);
    }

    void AssignRibbons()
    {
        int ribbonsFound;
        print("Levelnum" + PlayerPrefs.GetInt("ppSelectedLevel", 0));
        ribbonsFound = Random.Range(baseMinRibbons * PlayerPrefs.GetInt("ppSelectedLevel", 0), baseMaxRibbons * PlayerPrefs.GetInt("ppSelectedLevel", 0));
        GameObject.Find("RibbonsFound").GetComponent<Text>().text = ribbonsFound.ToString();
        GameObject.Find("RibbonsCurrent").GetComponent<Text>().text = (PlayerPrefs.GetInt("ppPlayerMoney", 0) + ribbonsFound).ToString();

        PlayerPrefs.SetInt("ppPlayerMoney", PlayerPrefs.GetInt("ppPlayerMoney", 0) + ribbonsFound);
        PlayerPrefs.Save();
    }

    void AssignRandomPowerUp()
    {
        int randomPowerUpFound;
        randomPowerUpFound = Random.Range(1, 4);
        if(randomPowerUpFound == 1)
        {
        
            PlayerPrefs.SetInt("ppNumBoost", PlayerPrefs.GetInt("ppNumBoost", 0) + 1);
            PlayerPrefs.Save();
        }
        else if(randomPowerUpFound == 2)
        {
            PlayerPrefs.SetInt("ppNumSentry", PlayerPrefs.GetInt("ppNumSentry", 0) + 1);
            PlayerPrefs.Save();
        }
        else if (randomPowerUpFound == 3)
        {
            PlayerPrefs.SetInt("ppNumBombs", PlayerPrefs.GetInt("ppNumBombs", 0) + 1);
            PlayerPrefs.Save();
        }
        else if (randomPowerUpFound == 4)
        {
            PlayerPrefs.SetInt("ppNumMissles", PlayerPrefs.GetInt("ppNumMissles", 0) + 1);
            PlayerPrefs.Save();
        }
    }

    public void DoLastLevelTransition()
    {
        if(PlayerPrefs.GetInt("ppSeenEndScreen", 0) == 0)
        {
            PlayerPrefs.SetInt("ppSelectedCutscene", 22);
            PlayerPrefs.SetInt("ppCutsceneNext", 4);
            PlayerPrefs.Save();
            GameObject.Find("SceneHandler").GetComponent<SceneHandler>().LoadScene("cutsceneScreen");
        }
        else
        {
            GameObject.Find("SceneHandler").GetComponent<SceneHandler>().LoadScene("levelSelect");
        }
    }
}
