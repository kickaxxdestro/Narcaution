using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeShopTab : MonoBehaviour {

    public GameObject Upgrade;
    public GameObject Purchase;
    public GameObject Equip;

    void activateChildObjects()
    {
        for(int i = 0; i < this.transform.childCount; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

	public void ChangeTabPowerUp () 
    {
        Upgrade.SetActive(false);
        Purchase.SetActive(true);
        Equip.SetActive(false);

        activateChildObjects();

        GameObject[] objectList2;
        objectList2 = GameObject.FindGameObjectsWithTag("Shop_Item_Weapon");
        foreach (GameObject go in objectList2)
        {
            go.SetActive(false);
        }

        GameObject[] objectList3;
        objectList3 = GameObject.FindGameObjectsWithTag("Shop_Item_Skin");
        foreach (GameObject go in objectList3)
        {
            go.SetActive(false);
        }
	}

    public void ChangeTabWeapons()
    {
        Upgrade.SetActive(true);
        Purchase.SetActive(false);
        Equip.SetActive(true);

        activateChildObjects();

        GameObject[] objectList;
        objectList = GameObject.FindGameObjectsWithTag("Shop_Item_PowerUp");
        foreach (GameObject go in objectList)
        {
            go.SetActive(false);
        }

        GameObject[] objectList3;
        objectList3 = GameObject.FindGameObjectsWithTag("Shop_Item_Skin");
        foreach (GameObject go in objectList3)
        {
            go.SetActive(false);
        }
    }

    public void ChangeTabSkins()
    {
        Upgrade.SetActive(false);
        Purchase.SetActive(true);
        Equip.SetActive(true);

        activateChildObjects();

        GameObject[] objectList;
        objectList = GameObject.FindGameObjectsWithTag("Shop_Item_PowerUp");
        foreach (GameObject go in objectList)
        {
            go.SetActive(false);
        }

        GameObject[] objectList2;
        objectList2 = GameObject.FindGameObjectsWithTag("Shop_Item_Weapon");
        foreach (GameObject go in objectList2)
        {
            go.SetActive(false);
        }
    }
}
