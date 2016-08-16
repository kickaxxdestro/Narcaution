using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreditsHandler : MonoBehaviour {

    public GameObject finalText;
    public AlphaFader sidmLogo;
    public AlphaFader NYPLogo;

    public float scrollingSpeed = 10;

    public AlphaFader nextButton;
    public AlphaFader nextButtonText;

    private GameObject targetCredits;

    bool scrollCredits = true;

	// Update is called once per frame
	void Update () {
        if(scrollCredits)
        {
            finalText.transform.Translate(0f, Time.deltaTime * scrollingSpeed * 5, 0f);
            if (finalText.transform.position.y >= Screen.height/2)
            {
                scrollCredits = false;

                nextButton.GetComponent<Button>().interactable = true;
                nextButton.DoFadeIn();
                nextButtonText.DoFadeIn();

                PlayerPrefs.SetInt("ppSeenEndScreen", 1);
                PlayerPrefs.Save();
            }
        }
	}
}
