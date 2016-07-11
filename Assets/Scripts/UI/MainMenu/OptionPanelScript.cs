using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionPanelScript : MonoBehaviour {
	
	public Slider sfxSlider;
	public Slider bgmSlider;
	
	void Start()
	{
		sfxSlider.value = PlayerPrefs.GetFloat("ppSFXVolume", 1.0f);
		bgmSlider.value = PlayerPrefs.GetFloat("ppBGMVolume", 1.0f);
	}
	
	public void ClearAllData ()
	{
		PlayerPrefs.DeleteAll();
	}
	
	public void OnSFXChange()
	{
		PlayerPrefs.SetFloat("ppSFXVolume", sfxSlider.value);
		PlayerPrefs.Save();
        AudioManager.audioManager.UpdateVolume();
	}
	public void onBGMChange()
	{
		PlayerPrefs.SetFloat("ppBGMVolume", bgmSlider.value);
		PlayerPrefs.Save();
        AudioManager.audioManager.UpdateBGM();
	}
}