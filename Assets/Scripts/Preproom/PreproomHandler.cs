using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PreproomHandler : MonoBehaviour {

    public GameObject skinList;
    public GameObject sentryInUseIcon;
    public GameObject boostInUseIcon;
    public GameObject bombInUseIcon;
    public GameObject missleInUseIcon;
    public Button startButton;
    bool sentryInUse = false;
    bool boostInUse = false;
    bool bombInUse = false;
    bool missleInUse = false;
    int currentSkin = 1;
    Color disableColor;

    private AudioSource equipAudioSource;

    void Awake()
    {
        disableColor = Color.white;
        disableColor.a = 0.5f;
        GameObject selectedSkin;
        switch(PlayerPrefs.GetInt("ppCurrentSkin", 1))
        {
            case 1:
                selectedSkin = skinList.transform.FindChild("Skin1").gameObject;
                break;
            case 2:
                selectedSkin = skinList.transform.FindChild("Skin2").gameObject;
                break;
            case 3:
                selectedSkin = skinList.transform.FindChild("Skin3").gameObject;
                break;
            default:
                selectedSkin = skinList.transform.FindChild("Skin1").gameObject;
                break;
        }
        selectedSkin.GetComponent<SliderItem>().showAtStart = true;
        selectedSkin.GetComponent<SliderItem>().pageSelector.GetComponent<PageSelector>().startSelected = true;
        skinList.GetComponent<GeneralSwipeScript>().current = selectedSkin;

        if (PlayerPrefs.GetInt("ppNumBoost", 0) <= 0)
            PlayerPrefs.SetInt("ppBoostEquipped", 0);
        if (PlayerPrefs.GetInt("ppNumSentry", 0) <= 0)
            PlayerPrefs.SetInt("ppSentryEquipped", 0);
        if (PlayerPrefs.GetInt("ppNumBombs", 0) <= 0)
            PlayerPrefs.SetInt("ppBombEquipped", 0);
        if (PlayerPrefs.GetInt("ppNumMissles", 0) <= 0)
            PlayerPrefs.SetInt("ppMissleEquipped", 0);

        if (PlayerPrefs.GetInt("ppSkin1Unlocked", 0) == 0)
            PlayerPrefs.GetInt("ppSkin1Unlocked", 1);

        PlayerPrefs.Save();

        sentryInUse = PlayerPrefs.GetInt("ppSentryEquipped", 0) == 1 ? true : false;
        boostInUse = PlayerPrefs.GetInt("ppBoostEquipped", 0) == 1 ? true : false;
        bombInUse = PlayerPrefs.GetInt("ppBombEquipped", 0) == 1 ? true : false;
        missleInUse = PlayerPrefs.GetInt("ppMissleEquipped", 0) == 1 ? true : false;

        sentryInUseIcon.SetActive(sentryInUse);
        boostInUseIcon.SetActive(boostInUse);
        bombInUseIcon.SetActive(bombInUse);
        missleInUseIcon.SetActive(missleInUse);

        currentSkin = PlayerPrefs.GetInt("ppCurrentSkin", 1);

        for(int i = 1; i <= 3; ++i)
        {
            if (PlayerPrefs.GetInt("ppSkin" + i + "Unlocked", 0) == 1)
                GameObject.Find("Skin" + i).transform.FindChild("LockedIcon").gameObject.SetActive(false);
            else
            {
                GameObject.Find("Skin" + i).transform.FindChild("LockedIcon").gameObject.SetActive(true);
                Color tempCol = Color.white;
                tempCol.a = 0.5f;
                GameObject.Find("Skin" + i).GetComponent<Image>().color = tempCol;
            }
        }

        equipAudioSource = GetComponent<AudioSource>();
    }

    public void SelectRightSkin()
    {
        currentSkin = (currentSkin >= 3) ? 1 : currentSkin + 1;
        print(currentSkin);

        if (PlayerPrefs.GetInt("ppSkin" + currentSkin + "Unlocked", 0) == 1)
        {
            PlayerPrefs.SetInt("ppCurrentSkin", currentSkin);
            PlayerPrefs.Save();
            startButton.interactable = true;
            startButton.GetComponent<Image>().color = Color.white;
        }
        else
        {
            startButton.interactable = false;
            startButton.GetComponent<Image>().color = disableColor;
        }
    }

    public void SelectLeftSkin()
    {
        currentSkin = (currentSkin <= 1) ? 3 : currentSkin - 1;
        if (PlayerPrefs.GetInt("ppSkin" + currentSkin + "Unlocked", 0) == 1)
        {
            PlayerPrefs.SetInt("ppCurrentSkin", currentSkin);
            PlayerPrefs.Save();
            startButton.interactable = true;
            startButton.GetComponent<Image>().color = Color.white;
        }
        else
        {
            startButton.interactable = false;
            startButton.GetComponent<Image>().color = disableColor;
        }
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
