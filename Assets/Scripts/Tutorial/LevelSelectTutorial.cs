using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSelectTutorial : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if(!PlayerPrefs.HasKey("ppLevelSelectTutorial"))
        {
            PlayerPrefs.SetInt("ppLevelSelectTutorial", 0);
            PlayerPrefs.SetInt("ppPlayerGamemode", 0);
            gameObject.GetComponent<TutorialHandler>().startTutorial();
        }
	}
}
