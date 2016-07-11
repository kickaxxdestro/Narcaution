using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Sfx : MonoBehaviour {

	AudioSource src;
	
	void Awake ()
	{
		src = GetComponent<AudioSource>();		
		src.volume = PlayerPrefs.GetFloat("ppSFXVolume", 1.0f);
	}
	
    void OnEnable()
    {
        src.volume = PlayerPrefs.GetFloat("ppSFXVolume", 1.0f);
        
    }
	
	void Update ()
	{
		if(src.isPlaying == false)
		{
			gameObject.SetActive(false);
		}
		
	}
}