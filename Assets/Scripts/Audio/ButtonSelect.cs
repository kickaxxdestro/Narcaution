using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonSelect : MonoBehaviour {

    void Awake()
    {
        Button myButton = GetComponent<Button>();
        myButton.onClick.AddListener(() => { buttonClick(); });  

    }

	void buttonClick()
    {
        AudioManager.audioManager.PlayDefaultButtonSound();
    }
}
