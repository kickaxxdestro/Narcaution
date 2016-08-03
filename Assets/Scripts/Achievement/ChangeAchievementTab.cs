using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeAchievementTab : MonoBehaviour {

    public Image Tab;
    public Sprite Tab_General;
    public Sprite Tab_Battle;
    public Sprite Tab_Special;

    void activateChildObjects()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void ChangeTabGeneral()
    {
        activateChildObjects();
        Tab.sprite = Tab_General;

        GameObject[] objectList2;
        objectList2 = GameObject.FindGameObjectsWithTag("Achievement_Battle");
        foreach (GameObject go in objectList2)
        {
            go.SetActive(false);
        }

        GameObject[] objectList3;
        objectList3 = GameObject.FindGameObjectsWithTag("Achievement_Special");
        foreach (GameObject go in objectList3)
        {
            go.SetActive(false);
        }
    }

    public void ChangeTabBattle()
    {
        activateChildObjects();
        Tab.sprite = Tab_Battle;

        GameObject[] objectList;
        objectList = GameObject.FindGameObjectsWithTag("Achievement_General");
        foreach (GameObject go in objectList)
        {
            go.SetActive(false);
        }

        GameObject[] objectList3;
        objectList3 = GameObject.FindGameObjectsWithTag("Achievement_Special");
        foreach (GameObject go in objectList3)
        {
            go.SetActive(false);
        }

    }

    public void ChangeTabSpecial()
    {
        activateChildObjects();
        Tab.sprite = Tab_Special;

        GameObject[] objectList;
        objectList = GameObject.FindGameObjectsWithTag("Achievement_General");
        foreach (GameObject go in objectList)
        {
            go.SetActive(false);
        }

        GameObject[] objectList2;
        objectList2 = GameObject.FindGameObjectsWithTag("Achievement_Battle");
        foreach (GameObject go in objectList2)
        {
            go.SetActive(false);
        }
    }
}
