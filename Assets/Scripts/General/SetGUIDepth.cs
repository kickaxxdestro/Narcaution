using UnityEngine;
using System.Collections;

public class SetGUIDepth : MonoBehaviour {

    public int guiDepth = 1;

	// Use this for initialization
	void Start () {
        GUI.depth = guiDepth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
