using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class minigameHandler : MonoBehaviour {

	public Color NormalGlow;
	public Color RareGlow;
	public Color EpicGlow;
	public Color LedgendaryGlow;

	public GameObject BoostPrefab;
	public GameObject SentryPrefab;
	public GameObject BombPrefab;
	public GameObject MissiletPrefab;
	public GameObject CreditsPrefab;

	enum LootGrade
	{
		L_NORMAL,
		L_RARE,
		L_EPIC,
		L_LEGENDARY
	};

	LootGrade ChestGrade;

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
	bool lerpOpenChestToCenter = false;
    bool checkFading = true;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
		audioSource.volume = PlayerPrefs.GetFloat("ppSFXVolume", 1.0f);
    }

	// Use this for initialization
	void Start () {
        Color transparent = Color.white;
        transparent.a = 0f;

		openChest.GetComponent<CanvasGroup>().alpha = 0;
		header.GetComponent<Text>().color = transparent;
        header.GetComponent<AlphaFader>().DoFadeIn(1f);

        foreach (GameObject chest in chestList)
        {
            chest.GetComponent<CanvasGroup>().alpha = 0;
            chest.GetComponent<Button>().interactable = false;
        }

        centerPosition.y = Screen.height * 0.5f;

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
				if (selectedChest != openChest) 
				{
					openChest.transform.position = new Vector3 (selectedChest.transform.position.x, centerPosition.y + openChestOffsetY, selectedChest.transform.position.z);
					StartCoroutine("OpenChest");
				}

                lerpChestToCenter = false;
            }
        }
	}

    IEnumerator OpenChest()
    {
        yield return new WaitForSeconds(1f);

        selectedChest.GetComponent<AlphaFader>().DoFadeOut(0.3f);
		openChest.GetComponent<AlphaFader>().DoFadeIn(0.3f);

		if(ChestGrade == LootGrade.L_NORMAL)
			header.GetComponent<Text> ().text = "GRADE C";
		else if(ChestGrade == LootGrade.L_RARE)
			header.GetComponent<Text> ().text = "GRADE B";
		else if(ChestGrade == LootGrade.L_EPIC)
			header.GetComponent<Text> ().text = "GRADE A";
		else if(ChestGrade == LootGrade.L_LEGENDARY)
			header.GetComponent<Text> ().text = "GRADE S";
		
		header.GetComponent<AlphaFader>().DoFadeIn(0.3f);

		openChest.GetComponent<Animator> ().Play ("OpenChest");

		AssignReward();

		audioSource.Play();

		yield return new WaitForSeconds(0.8f);
		foreach(Transform t in openChest.transform.FindChild ("ObtainedItems"))
		{
			t.gameObject.SetActive(true);
			yield return new WaitForSeconds(1f);
		}
		yield return new WaitForSeconds(0.5f);

		//header.GetComponent<AlphaFader>().DoFadeOut(0.3f);

		centerPosition.y = Screen.height * 0.7f;
		selectedChest = openChest;
		lerpChestToCenter = true;
							


        ribbonDisplay.GetComponent<SliderItem>().DoLerpToCenter_FromRight();

        if (PlayerPrefs.GetInt("ppSelectedLevel", 0) >= 20)
        {
            buttonPanel.transform.FindChild("Default").gameObject.SetActive(false);
            buttonPanel.transform.FindChild("EndButton").gameObject.SetActive(true);
        }

        buttonPanel.GetComponent<SliderItem>().DoLerpToCenter_FromLeft();

        //openChest.transform.FindChild("ChestItem").GetComponent<Image>().color = Color.white;
        //openChest.transform.FindChild("ChestItem").GetComponent<Animator>().enabled = true;


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
			chest.GetComponent<Button>().interactable = false;

            if (chest == selectedChest)
			{
				selectedChest.GetComponent<Animator> ().Play ("ChestShake");
				continue;
			}
            chest.GetComponent<AlphaFader>().DoFadeOut(0.3f);
        }
        lerpChestToCenter = true;
        header.GetComponent<AlphaFader>().DoFadeOut(0.3f);

		AssignGrade ();
    }

	void AssignGrade()
	{
		int RNG = Random.Range (1, 101);

		if (RNG < 15) 
		{
			ChestGrade = LootGrade.L_LEGENDARY;
			selectedChest.transform.FindChild ("RarityGlow").gameObject.GetComponent<Image>().color = LedgendaryGlow;
			openChest.transform.FindChild ("RarityGlow").gameObject.GetComponent<Image>().color = LedgendaryGlow;
		}
		else if (RNG < 30) 
		{
			ChestGrade = LootGrade.L_EPIC;
			selectedChest.transform.FindChild ("RarityGlow").gameObject.GetComponent<Image>().color = EpicGlow;
			openChest.transform.FindChild ("RarityGlow").gameObject.GetComponent<Image>().color = EpicGlow;
		}
		else if (RNG < 65) 
		{
			ChestGrade = LootGrade.L_RARE;
			selectedChest.transform.FindChild ("RarityGlow").gameObject.GetComponent<Image>().color = RareGlow;
			openChest.transform.FindChild ("RarityGlow").gameObject.GetComponent<Image>().color = RareGlow;
		}
		else
		{
			ChestGrade = LootGrade.L_NORMAL;
			selectedChest.transform.FindChild ("RarityGlow").gameObject.GetComponent<Image>().color = NormalGlow;
			openChest.transform.FindChild ("RarityGlow").gameObject.GetComponent<Image>().color = NormalGlow;
		}
	}

	void AssignReward()
	{
		int ribbonsFound = 0;

		GameObject a = Instantiate (CreditsPrefab);
		a.transform.SetParent (openChest.transform.FindChild ("ObtainedItems"));
		a.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
		a.transform.localScale = Vector3.one;
		a.SetActive (false);

		GameObject b = Instantiate (CreditsPrefab);
		b.GetComponent<Animator> ().enabled = false;
		b.transform.SetParent (ribbonDisplay.transform.FindChild ("CreditsFound"));
		b.transform.localScale = Vector3.one;

		switch (ChestGrade) 
		{
		case LootGrade.L_NORMAL:
			ribbonsFound = Random.Range(2 * PlayerPrefs.GetInt("ppSelectedLevel", 0), 4 * PlayerPrefs.GetInt("ppSelectedLevel", 0));
			break;

		case LootGrade.L_RARE:
			ribbonsFound = Random.Range (5 * PlayerPrefs.GetInt ("ppSelectedLevel", 0), 7 * PlayerPrefs.GetInt ("ppSelectedLevel", 0));
			GeneratePowerUp (1);
			break;

		case LootGrade.L_EPIC:
			ribbonsFound = Random.Range (8 * PlayerPrefs.GetInt ("ppSelectedLevel", 0), 11 * PlayerPrefs.GetInt ("ppSelectedLevel", 0));
			GeneratePowerUp (2);
			break;

		case LootGrade.L_LEGENDARY:
			ribbonsFound = Random.Range (11 * PlayerPrefs.GetInt ("ppSelectedLevel", 0), 15 * PlayerPrefs.GetInt ("ppSelectedLevel", 0));
			GeneratePowerUp (3);
			break;
		}

		a.GetComponent<Text>().text = "+" + ribbonsFound.ToString() + "  \nCredits";
		b.GetComponent<Text>().text = "+" + ribbonsFound.ToString() + "  \nCredits";
		//GameObject.Find("RibbonsFound").GetComponent<Text>().text = ribbonsFound.ToString();
		//GameObject.Find("RibbonsCurrent").GetComponent<Text>().text = (PlayerPrefs.GetInt("ppPlayerMoney", 0) + ribbonsFound).ToString();

		PlayerPrefs.SetInt("ppPlayerMoney", PlayerPrefs.GetInt("ppPlayerMoney", 0) + ribbonsFound);
		PlayerPrefs.Save();
	}

    void AssignRibbons()
    {
        int ribbonsFound;
        print("Levelnum" + PlayerPrefs.GetInt("ppSelectedLevel", 0));
        ribbonsFound = Random.Range(1 * PlayerPrefs.GetInt("ppSelectedLevel", 0), 5 * PlayerPrefs.GetInt("ppSelectedLevel", 0));
        GameObject.Find("RibbonsFound").GetComponent<Text>().text = ribbonsFound.ToString();
        GameObject.Find("RibbonsCurrent").GetComponent<Text>().text = (PlayerPrefs.GetInt("ppPlayerMoney", 0) + ribbonsFound).ToString();

        PlayerPrefs.SetInt("ppPlayerMoney", PlayerPrefs.GetInt("ppPlayerMoney", 0) + ribbonsFound);
        PlayerPrefs.Save();
    }

	void GeneratePowerUp( int numOfTime )
    {
		for(int i = 0; i < numOfTime; ++i)
		{
	        int randomPowerUpFound;
	        randomPowerUpFound = Random.Range(1, 5);

			GameObject a = new GameObject();
			Destroy (a);

	        if(randomPowerUpFound == 1)
	        {
	            PlayerPrefs.SetInt("ppNumBoost", PlayerPrefs.GetInt("ppNumBoost", 0) + 1);
				a = Instantiate (BoostPrefab);
	            PlayerPrefs.Save();
	        }
	        else if(randomPowerUpFound == 2)
	        {
				PlayerPrefs.SetInt("ppNumSentry", PlayerPrefs.GetInt("ppNumSentry", 0) + 1);
				a = Instantiate (SentryPrefab);
	            PlayerPrefs.Save();
	        }
	        else if (randomPowerUpFound == 3)
	        {
				PlayerPrefs.SetInt("ppNumBombs", PlayerPrefs.GetInt("ppNumBombs", 0) + 1);
				a = Instantiate (BombPrefab);
	            PlayerPrefs.Save();
	        }
	        else if (randomPowerUpFound == 4)
	        {
				PlayerPrefs.SetInt("ppNumMissles", PlayerPrefs.GetInt("ppNumMissles", 0) + 1);
				a = Instantiate (MissiletPrefab);
	            PlayerPrefs.Save();
	        }

			a.transform.SetParent (openChest.transform.FindChild ("ObtainedItems"));
			a.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
			a.transform.localScale = Vector3.one;

			GameObject b = Instantiate (a);
			b.GetComponent<Animator> ().enabled = false;
			b.transform.SetParent (ribbonDisplay.transform.FindChild ("PupFound"));
			b.transform.localScale = Vector3.one;

			a.SetActive (false);
		}
    }

    public void DoLastLevelTransition()
    {
		if(PlayerPrefs.GetInt("ppSeenEndScreen", 0) == 0 && PlayerPrefs.GetInt("ppSelectedLevel", 0) >= 21)
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
