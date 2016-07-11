using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageSwap : MonoBehaviour {

    public Sprite EnabledImage;
    public Sprite DisabledImage;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetToEnabledImage()
    {
        GetComponent<Image>().sprite = EnabledImage;
    }

    public void SetToDisabledImage()
    {
        GetComponent<Image>().sprite = DisabledImage;
    }
}
