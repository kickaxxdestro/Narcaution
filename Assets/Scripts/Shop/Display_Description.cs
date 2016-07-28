using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Display_Description : MonoBehaviour
{
    bool Displayed = false;
    string lastUsed = "";
    bool appear = false;
    bool disappear = false;
    public bool Add_ExtraDesciption;
    public GameObject Extra_Description;

    public void ModifyDescription()
    {
        // ModifyDescription() will cause a description to appear when the item it is related to has it's button been pressed. 
        // It will cause the description to disappear if the description has already been displayed and the button the item has is pressed.
        // If an item is pressed while another item's description is displayed, the latter's description will hide while the former's description gets displayed.
        if (Add_ExtraDesciption == true)
        {
            if (Displayed == false) //If the tab clicked has not displayed it's description
            {
                for (int i = 0; i < transform.parent.childCount; i++)  //Close any other tabs that are displaying it's description
                {
                    transform.parent.GetChild(i).gameObject.GetComponent<Display_Description>().disappear = true;
                }
                if (GetComponent<LayoutElement>().preferredHeight < 300) //Expand the current tab clicked
                {
                    appear = true;
                }
            }
            else if (Displayed == true) //If the tab clicked has displayed it's description
            {
                if (GetComponent<LayoutElement>().preferredHeight > 200) //Close the current tab clicked
                {
                    disappear = true;
                }
            }
        }
    }

    void Update()
    {
        if (Add_ExtraDesciption == true)
        {
            if (appear == true) //Expands the tab by shifting the position of the tab's description
            {
                GetComponent<LayoutElement>().preferredHeight += 300f * Time.deltaTime;
                Extra_Description.transform.position = new Vector3(Extra_Description.transform.position.x, Extra_Description.transform.position.y - 100f * Time.deltaTime, Extra_Description.transform.position.z);

                if (GetComponent<LayoutElement>().preferredHeight >= 300)
                {
                    GetComponent<LayoutElement>().preferredHeight = 300;
                    Extra_Description.GetComponent<RectTransform>().anchoredPosition = new Vector3(102f, -145.5f, 0);
                    appear = false; 
                    Displayed = true;
                }
            }


            if (disappear == true) //Closes the tab by shifting the position of the tab's description
            {
                GetComponent<LayoutElement>().preferredHeight -= 300f * Time.deltaTime;
                Extra_Description.transform.position = new Vector3(Extra_Description.transform.position.x, Extra_Description.transform.position.y + 100f * Time.deltaTime, Extra_Description.transform.position.z);
                if (GetComponent<LayoutElement>().preferredHeight <= 200f)
                {
                    GetComponent<LayoutElement>().preferredHeight = 200f;
                    Extra_Description.GetComponent<RectTransform>().anchoredPosition = new Vector3(102f, -70f, 0);
                    disappear = false;
                    Displayed = false;
                    lastUsed = "";
                }
            }
        }
    }
}
