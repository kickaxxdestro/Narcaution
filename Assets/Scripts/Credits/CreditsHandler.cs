using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreditsHandler : MonoBehaviour {

    public GameObject creditsOffense;
    public GameObject creditsDefense;
    public GameObject finalText;
    public AlphaFader sidmLogo;
    public AlphaFader NYPLogo;

    public float scrollingSpeed = 10f;

    public AlphaFader nextButton;

    private GameObject targetCredits;
    private float stopPos;

    bool scrollCredits = true;



    void Awake()
    {
        if (PlayerPrefs.GetInt("ppPlayerGamemode", 0) == 0)
        {
            creditsDefense.SetActive(false);
            targetCredits = creditsOffense;
        }
        else
        {
            creditsOffense.SetActive(false);
            targetCredits = creditsDefense;
        }

        stopPos = 1950;
        print(stopPos);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(scrollCredits)
        {
            targetCredits.transform.Translate(0f, scrollingSpeed * Time.deltaTime, 0f);
            if (targetCredits.transform.position.y >= stopPos)
            {
                print(targetCredits.transform.position.y);
                scrollCredits = false;

                sidmLogo.DoFadeIn();
                NYPLogo.DoFadeIn();
                finalText.GetComponent<SliderItem>().DoLerpToCenter_FromRight();

                StartCoroutine("ShowButton");
            }
        }
	}

    IEnumerator ShowButton()
    {
        yield return new WaitForSeconds(2f);

        nextButton.GetComponent<Button>().interactable = true;
        nextButton.DoFadeIn();

        PlayerPrefs.SetInt("ppSeenEndScreen", 1);
        PlayerPrefs.Save();

        StopCoroutine("ShowButton");
    }
}
