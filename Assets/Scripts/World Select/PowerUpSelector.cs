using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerUpSelector : MonoBehaviour {

	public GameObject sentryInUseIcon;
	public GameObject boostInUseIcon;
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

		equipAudioSource = GetComponent<AudioSource>();
	}

	public void ToggleSentryUse()
	{
		if (PlayerPrefs.GetInt("ppNumSentry", 0) <= 0)
			return;

		sentryInUse = !sentryInUse;
		sentryInUseIcon.SetActive(sentryInUse);
		if(sentryInUse)
			PlayerPrefs.SetInt("ppSentryEquipped", 1);
		else
			PlayerPrefs.SetInt("ppSentryEquipped", 0);

		PlayerPrefs.Save();

		equipAudioSource.Play();
	}

	public void ToggleBombUse()
	{
		if (PlayerPrefs.GetInt("ppNumBombs", 0) <= 0)
			return;

		bombInUse = !bombInUse;
		bombInUseIcon.SetActive(bombInUse);
		if (bombInUse)
			PlayerPrefs.SetInt("ppBombEquipped", 1);
		else
			PlayerPrefs.SetInt("ppBombEquipped", 0);

		PlayerPrefs.Save();
		equipAudioSource.Play();

	}

	public void ToggleMissleUse()
	{
		if (PlayerPrefs.GetInt("ppNumMissles", 0) <= 0)
			return;

		missleInUse = !missleInUse;
		missleInUseIcon.SetActive(missleInUse);
		if (missleInUse)
			PlayerPrefs.SetInt("ppMissleEquipped", 1);
		else
			PlayerPrefs.SetInt("ppMissleEquipped", 0);

		PlayerPrefs.Save();
		equipAudioSource.Play();

	}

	public void ToggleBoostUse()
	{
		if (PlayerPrefs.GetInt("ppNumBoost", 0) <= 0)
			return;

		boostInUse = !boostInUse;
		boostInUseIcon.SetActive(boostInUse);
		if (boostInUse)
			PlayerPrefs.SetInt("ppBoostEquipped", 1);
		else
			PlayerPrefs.SetInt("ppBoostEquipped", 0);

		PlayerPrefs.Save();
		equipAudioSource.Play();

	}

	public void StartGameplay()
	{
		//Add stuff to check before starting gameplay here
		GameObject.Find("CutsceneChecker").GetComponent<CutsceneChecker>().DoPreproomCutsceneCheck();
	}
}
