using UnityEngine;
using System.Collections;

public class ControlWorldConnector : MonoBehaviour {

    public GameObject Level;

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
