using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SideMenuExpansion : MonoBehaviour {

    public GameObject[] Button;
    public float moveSpeed;

    bool Clicked = false;
    bool Expand = false;
    bool Contract = false;

    float ExpandLimit;
    float OriginalLimit;

    int buttonLimit;

    void Start() 
    {
        buttonLimit = Button.Length; //Set max buttons in array
        OriginalLimit = Button[0].transform.position.y; //Set the original position of the buttons
        ExpandLimit = (Button[0].transform.position.y + (Button[0].GetComponent<Image>().rectTransform.rect.height) / 456 * Screen.height); //Set the max limit the position of the buttons can translate to.
    }

    public void ExpandOrContractSideMenu()  //Check if the side menu has popped out or not
    {
        if(Clicked == false)
        {
            Expand = true;
        }
        else
        {
            Contract = true;
        }
	}

    void Update() 
    {
        if (Expand == true) //Expand the side menu
        {
            if (Button[buttonLimit-1].transform.position.y <= ExpandLimit)
            {
                for (int i = 0; i < buttonLimit; i++)
                {
                    Button[i].transform.position = new Vector3(Button[i].transform.position.x, Button[i].transform.position.y + (moveSpeed * Time.deltaTime * (i + 1)), Button[i].transform.position.z);
                }
            }
            else
            {
                Expand = false;
                Clicked = true;
            }
        }

        if (Contract == true) //Contract the side menu
        {
            if (Button[buttonLimit - 1].transform.position.y >= OriginalLimit)
            {
                for (int i = 0; i < buttonLimit; i++)
                {
                    Button[i].transform.position = new Vector3(Button[i].transform.position.x, Button[i].transform.position.y - (moveSpeed * Time.deltaTime * (i + 1)), Button[i].transform.position.z);
                }
            }
            else
            {
                Contract = false;
                Clicked = false;
            }           
        }
    }
}
