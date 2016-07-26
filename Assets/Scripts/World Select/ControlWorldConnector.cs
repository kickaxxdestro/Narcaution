using UnityEngine;
using System.Collections;

public class ControlWorldConnector : MonoBehaviour {

    public GameObject Level;

	void Start()
	{
		GetComponent<AlphaFader>().fadeColor.a = 0f;
		GetComponent<SpriteRenderer>().color = GetComponent<AlphaFader>().fadeColor;
	}

    public void DoInTransition()
    {
        if (Level.GetComponent<LevelButton>().disabled == false)
        {
            //GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<AlphaFader>().DoFadeIn();
        }
    }

    public void DoOutTransition()
    {
        GetComponent<AlphaFader>().DoFadeOut();
       // GetComponent<SpriteRenderer>().enabled = false;
    }
}
