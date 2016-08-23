using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerUpSelector : MonoBehaviour {

	public GameObject boostIcon;
	public GameObject sentryIcon;
	public GameObject bombIcon;
	public GameObject missleIcon;

	public GameObject boostInUseIcon;
	public GameObject sentryInUseIcon;
	public GameObject bombInUseIcon;
	public GameObject missleInUseIcon;


	bool sentryInUse = false;
	bool boostInUse = false;
	bool bombInUse = false;
	bool missleInUse = false;

	private AudioSource equipAudioSource;

	void Awake()
	{

		if (PlayerPrefs.GetInt("ppNumBoost", 0) <= 0)
			PlayerPrefs.SetInt("ppBoostEquipped", 0);
		if (PlayerPrefs.GetInt("ppNumSentry", 0) <= 0)
			PlayerPrefs.SetInt("ppSentryEquipped", 0);
		if (PlayerPrefs.GetInt("ppNumBombs", 0) <= 0)
			PlayerPrefs.SetInt("ppBombEquipped", 0);
		if (PlayerPrefs.GetInt("ppNumMissles", 0) <= 0)
			PlayerPrefs.SetInt("ppMissleEquipped", 0);

		PlayerPrefs.Save();

		sentryInUse = PlayerPrefs.GetInt("ppSentryEquipped", 0) == 1 ? true : false;
		boostInUse = PlayerPrefs.GetInt("ppBoostEquipped", 0) == 1 ? true : false;
		bombInUse = PlayerPrefs.GetInt("ppBombEquipped", 0) == 1 ? true : false;
		missleInUse = PlayerPrefs.GetInt("ppMissleEquipped", 0) == 1 ? true : false;

		sentryInUseIcon.SetActive(sentryInUse);
		boostInUseIcon.SetActive(boostInUse);
		bombInUseIcon.SetActive(bombInUse);
		missleInUseIcon.SetActive(missleInUse);

		if (sentryInUse)
			sentryIcon.GetComponent<Image> ().color = Color.green;
		else
			sentryIcon.GetComponent<Image> ().color = Color.white;

		if (boostInUse)
			boostIcon.GetComponent<Image> ().color = Color.green;
		else
			boostIcon.GetComponent<Image> ().color = Color.white;

		if (bombInUse)
			bombIcon.GetComponent<Image> ().color = Color.green;
		else
			bombIcon.GetComponent<Image> ().color = Color.white;

		if (missleInUse)
			missleIcon.GetComponent<Image> ().color = Color.green;
		else
			missleIcon.GetComponent<Image> ().color = Color.white;
		
		equipAudioSource = GetComponent<AudioSource>();
	}

	public void CheckDisableBombAndMissile ()
	{
		if (PlayerPrefs.GetInt ("ppPlayerGamemode", 0) == 1) 
		{
			bombIcon.GetComponent<Image> ().color = Color.gray;
			bombIcon.GetComponentInParent<Button> ().interactable = false;
			missleIcon.GetComponent<Image> ().color = Color.gray;
			missleIcon.GetComponentInParent<Button> ().interactable = false;
		}
		else
		{
			bombIcon.GetComponent<Image> ().color = Color.white;
			bombIcon.GetComponentInParent<Button> ().interactable = true;
			missleIcon.GetComponent<Image> ().color = Color.white;
			missleIcon.GetComponentInParent<Button> ().interactable = true;
		}
	}

	public void ToggleSentryUse()
	{
		if (PlayerPrefs.GetInt("ppNumSentry", 0) <= 0)
			return;

		sentryInUse = !sentryInUse;
		sentryInUseIcon.SetActive(sentryInUse);
		if (sentryInUse) {
			PlayerPrefs.SetInt ("ppSentryEquipped", 1);
			sentryIcon.GetComponent<Image> ().color = Color.green;
		} else {
			sentryIcon.GetComponent<Image> ().color = Color.white;
			PlayerPrefs.SetInt ("ppSentryEquipped", 0);
		}

		PlayerPrefs.Save();

		equipAudioSource.volume = PlayerPrefs.GetFloat("ppSFXVolume", 1.0f);
		equipAudioSource.Play();
	}

	public void ToggleBombUse()
	{
		if (PlayerPrefs.GetInt("ppNumBombs", 0) <= 0)
			return;

		bombInUse = !bombInUse;
		bombInUseIcon.SetActive(bombInUse);
		if (bombInUse) {
			PlayerPrefs.SetInt ("ppBombEquipped", 1);
			bombIcon.GetComponent<Image> ().color = Color.green;
		} else {
			bombIcon.GetComponent<Image> ().color = Color.white;
			PlayerPrefs.SetInt ("ppBombEquipped", 0);
		}

		PlayerPrefs.Save();

		equipAudioSource.volume = PlayerPrefs.GetFloat("ppSFXVolume", 1.0f);
		equipAudioSource.Play();

	}

	public void ToggleMissleUse()
	{
		if (PlayerPrefs.GetInt("ppNumMissles", 0) <= 0)
			return;

		missleInUse = !missleInUse;
		missleInUseIcon.SetActive(missleInUse);
		if (missleInUse) {
			PlayerPrefs.SetInt ("ppMissleEquipped", 1);
			missleIcon.GetComponent<Image> ().color = Color.green;
		} else {
			missleIcon.GetComponent<Image> ().color = Color.white;
			PlayerPrefs.SetInt ("ppMissleEquipped", 0);
		}

		PlayerPrefs.Save();

		equipAudioSource.volume = PlayerPrefs.GetFloat("ppSFXVolume", 1.0f);
		equipAudioSource.Play();

	}

	public void ToggleBoostUse()
	{
		if (PlayerPrefs.GetInt("ppNumBoost", 0) <= 0)
			return;

		boostInUse = !boostInUse;
		boostInUseIcon.SetActive(boostInUse);
		if (boostInUse) {
			PlayerPrefs.SetInt ("ppBoostEquipped", 1);
			boostIcon.GetComponent<Image> ().color = Color.green;
		} else {
			boostIcon.GetComponent<Image> ().color = Color.white;
			PlayerPrefs.SetInt ("ppBoostEquipped", 0);
		}

		PlayerPrefs.Save();

		equipAudioSource.volume = PlayerPrefs.GetFloat("ppSFXVolume", 1.0f);
		equipAudioSource.Play();

	}

	public void StartGameplay()
	{
		//Add stuff to check before starting gameplay here
		GameObject.Find("CutsceneChecker").GetComponent<CutsceneChecker>().DoPreproomCutsceneCheck();
	}
}
