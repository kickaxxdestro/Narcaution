using UnityEngine;
using System.Collections;

public class ObjectStick : MonoBehaviour {

    public GameObject stickToObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = stickToObject.transform.position;
	}
}
