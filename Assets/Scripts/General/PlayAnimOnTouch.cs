using UnityEngine;
using System.Collections;

public class PlayAnimOnTouch : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        TouchCheck touchcheck = GetComponent<TouchCheck>();

        if (touchcheck != null)
        {
            if (touchcheck.CheckTouchOnCollider())
            {

                GetComponent<Animator>().Play("LaserAnim");
                print("AninPlayed");
            }
        }
	}
}
