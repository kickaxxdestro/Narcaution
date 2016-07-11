using UnityEngine;
using System.Collections;

public class ScaleOverTime : MonoBehaviour {

    public float scalingSpeed = 1f;
    public bool limitSize = false;
    public float maxScale = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale == 0)
            return;
        if (limitSize)
        {
            if (this.transform.localScale.x >= maxScale)
                return;
        }
        this.transform.localScale += new Vector3(scalingSpeed * Time.deltaTime, scalingSpeed * Time.deltaTime);
	}
}
