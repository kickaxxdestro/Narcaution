using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PageSelector : MonoBehaviour {

    public Color selectedColor;
    public Vector3 selectedScale;
    public bool startSelected = false;
    Color deselectedColor;
    Vector3 deselectedScale;

	// Use this for initialization
	void Start () {
        deselectedColor = GetComponent<Image>().color;
        deselectedScale = transform.localScale;
        if (startSelected)
            Select();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Select()
    {
        GetComponent<Image>().color = selectedColor;
        transform.localScale = selectedScale;
    }

    public void Deselect()
    {
        GetComponent<Image>().color = deselectedColor;
        transform.localScale = deselectedScale;
    }
}
