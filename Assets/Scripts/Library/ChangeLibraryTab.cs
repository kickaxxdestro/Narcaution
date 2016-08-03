using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeLibraryTab : MonoBehaviour {

    public Image Tab;
    public Sprite Tab_Minion;
    public Sprite Tab_Boss;
    public Sprite Tab_Entry;

    void activateChildObjects()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void ChangeTabMinion()
    {
        activateChildObjects();
        Tab.sprite = Tab_Minion;

        GameObject[] objectList2;
        objectList2 = GameObject.FindGameObjectsWithTag("Library_Boss");
        foreach (GameObject go in objectList2)
        {
            go.SetActive(false);
        }

        GameObject[] objectList3;
        objectList3 = GameObject.FindGameObjectsWithTag("Library_Entry");
        foreach (GameObject go in objectList3)
        {
            go.SetActive(false);
        }
    }

    public void ChangeTabBoss()
    {
        activateChildObjects();
        Tab.sprite = Tab_Boss;

        GameObject[] objectList;
        objectList = GameObject.FindGameObjectsWithTag("Library_Minion");
        foreach (GameObject go in objectList)
        {
            go.SetActive(false);
        }

        GameObject[] objectList3;
        objectList3 = GameObject.FindGameObjectsWithTag("Library_Entry");
        foreach (GameObject go in objectList3)
        {
            go.SetActive(false);
        }

    }

    public void ChangeTabEntry()
    {
        activateChildObjects();
        Tab.sprite = Tab_Entry;

        GameObject[] objectList;
        objectList = GameObject.FindGameObjectsWithTag("Library_Minion");
        foreach (GameObject go in objectList)
        {
            go.SetActive(false);
        }

        GameObject[] objectList2;
        objectList2 = GameObject.FindGameObjectsWithTag("Library_Boss");
        foreach (GameObject go in objectList2)
        {
            go.SetActive(false);
        }
    }
}
