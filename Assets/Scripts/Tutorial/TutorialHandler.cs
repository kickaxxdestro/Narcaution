using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class TutorialSlideProperties : System.Object{
	public string description;
	public Vector3 localPos;
	public float rectTransformWidth;
	public float rectTransformHeight;
}

public class TutorialHandler : MonoBehaviour {

	public Image TutorialImage;
	public Text Description;

	public Button next;
	public Button back;

	public float TransitionSpeed = 1;

	public List<TutorialSlideProperties> TutorialSlides;

	public Sprite DefendModeImage;

	public List<TutorialSlideProperties> TutorialSlidesDefendMode;

	int index = -1;

	IEnumerator theCorutine = null;

	public void startTutorial () {

		Time.timeScale = 0;

		transform.GetChild (0).gameObject.SetActive (true);

		if (PlayerPrefs.GetInt ("ppPlayerGamemode", 0) == 1) 
		{
			TutorialSlides = TutorialSlidesDefendMode;
			TutorialImage.sprite = DefendModeImage;
		}

		if (TutorialSlides.Count != 0) 
		{
			index = 0;
			Description.text = TutorialSlides [index].description;
			TutorialImage.GetComponent<RectTransform>().localPosition = TutorialSlides [index].localPos;
			TutorialImage.GetComponent<RectTransform>().sizeDelta = new Vector2(TutorialSlides [index].rectTransformWidth, TutorialSlides [index].rectTransformHeight);
			//TutorialImage.GetComponent<RectTransform>().rect.height = TutorialSlides [index].rectTransformHeight;
		}
		back.interactable = false;
	}

	IEnumerator DoTransition(bool Next = true)
	{
		float normValue = 0;

		Vector3 tempPos = TutorialSlides [index].localPos;
		float tempWidth = TutorialSlides [index].rectTransformWidth;
		float tempheight = TutorialSlides [index].rectTransformHeight;

		if (Next)
			index++;
		else
			index--;

		back.interactable = true;
		next.GetComponentInChildren<Text>().text = "Next";

		if (index <= 0)
			back.interactable = false;
		else if (index >= TutorialSlides.Count - 1)
			next.GetComponentInChildren<Text>().text = "Continue";

		bool textChanged = false;

		while (normValue <= 1) 
		{
			normValue += Time.unscaledDeltaTime * TransitionSpeed;

			TutorialImage.GetComponent<RectTransform> ().localPosition = Vector3.Lerp (tempPos, TutorialSlides [index].localPos, normValue);
			Vector2 tempVec = new Vector2(Mathf.Lerp (tempWidth, TutorialSlides [index].rectTransformWidth, normValue), Mathf.Lerp (tempheight, TutorialSlides [index].rectTransformHeight, normValue));
			TutorialImage.GetComponent<RectTransform> ().sizeDelta = tempVec;

			if (!textChanged) {
				Color tempC = Description.color;
				tempC.a -= Time.unscaledDeltaTime * TransitionSpeed * 2;
				Description.color = tempC;
				if (tempC.a <= 0) {
					Description.text = TutorialSlides [index].description;
					textChanged = true;
				}
			} 
			else 
			{
				Color tempC = Description.color;
				tempC.a += Time.unscaledDeltaTime * TransitionSpeed * 2;
				Description.color = tempC;
			}

			yield return new WaitForEndOfFrame ();
		}

		StopCoroutine (theCorutine);
		theCorutine = null;
	}

	public void NextSlide()
	{
		if (theCorutine == null && index != -1 && index != TutorialSlides.Count - 1) 
		{
			theCorutine = DoTransition (true);
			StartCoroutine (theCorutine);
		}
		else if(theCorutine == null && index == TutorialSlides.Count - 1)
		{
			Time.timeScale = 1;

			gameObject.SetActive (false);

			if (PlayerPrefs.GetInt ("ppPlayerGamemode", 0) == 0) 
				PlayerPrefs.SetInt ("Tutorial1Done", 1);
			else if (PlayerPrefs.GetInt ("ppPlayerGamemode", 0) == 1) 
				PlayerPrefs.SetInt ("Tutorial2Done", 1);
		}
	}

	public void PrevSlide()
	{
		if (theCorutine == null && index != -1 && index > 0) 
		{
			theCorutine = DoTransition (false);
			StartCoroutine (theCorutine);
		}
	}
}
