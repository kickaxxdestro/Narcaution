﻿using UnityEngine;
using System.Collections;

public class SetPlayerInfo : MonoBehaviour {

	// Use this for initialization
	public void LoadGame () {
        if (PlayerPrefs.GetInt("ppPlayerTutorial") == 0)
        {
            PlayerPrefs.SetInt("ppPlayerGamemode", 0);
            PlayerPrefs.SetString("ppPlayerName", "Morphues");
            PlayerPrefs.SetInt("ppPlayerTutorial", 1);
            PlayerPrefs.Save();
            GameObject.Find("SceneHandler").GetComponent<SceneHandler>().LoadScene("tutorialScene");
        }
        else if (PlayerPrefs.GetInt("ppPlayerTutorial") == 1)
        {
            GameObject.Find("SceneHandler").GetComponent<SceneHandler>().LoadScene("levelSelect");
        }
	}
}
