using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProfileInfoLoader : MonoBehaviour {

	public Text levelDisplay;
	public Text creditsDisplay;

	// Use this for initialization
	void Start () {
		int currentLevel = PlayerPrefs.GetInt("ppCurrentLevel", 1);

		if (currentLevel <= 4) 
		{
			levelDisplay.text = ("W 1-" + currentLevel.ToString ());
		}
		else if (currentLevel <= 8) 
		{
			levelDisplay.text = ("W 2-" + (currentLevel - 4).ToString ());
		}
		else if (currentLevel <= 12) 
		{
			levelDisplay.text = ("W 3-" + (currentLevel - 8).ToString ());
		}
		else if (currentLevel <= 16) 
		{
			levelDisplay.text = ("W 4-" + (currentLevel - 12).ToString ());
		}
		else if (currentLevel <= 20) 
		{
			levelDisplay.text = ("W 5-" + (currentLevel - 16).ToString ());
		}
		else
			levelDisplay.text = ("W 5-5");

		creditsDisplay.text = "RM " + PlayerPrefs.GetInt("ppPlayerMoney", 0).ToString();
	}
}