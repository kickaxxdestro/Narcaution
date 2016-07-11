using UnityEngine;
using System.Collections;

public class Rotate2DObject : MonoBehaviour {

    public bool rotateDirection = true;
    public float rotationSpeed = 60;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (rotateDirection)
            this.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        else
            this.transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
	}
}
