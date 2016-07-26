using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeShopTab : MonoBehaviour {

    public Image Tab;
    public Sprite Tab_PowerUps;
    public Sprite Tab_Weapons;
    public Sprite Tab_Skins;
    public GameObject Upgrade;
    public GameObject Purchase;
    public GameObject Equip;
    public GameObject OriginalEquip;
    public GameObject PowerUpMainTarget;
    public GameObject WeaponMainTarget;
    public GameObject SkinMainTarget;

    ShopHandlerDeluxe SHDX;

    void Start()
    {
        SHDX = gameObject.transform.parent.GetComponent<ShopHandlerDeluxe>();
    }

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
        Tab.sprite = Tab_PowerUps;

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

        SHDX.setTarget(PowerUpMainTarget);
	}

    public void ChangeTabWeapons()
    {
        Upgrade.SetActive(true);
        Purchase.SetActive(false);
        Equip.SetActive(true);

        RevertEquipPosition();
        activateChildObjects();
        Tab.sprite = Tab_Weapons;

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

        SHDX.setTarget(WeaponMainTarget);
    }

    public void ChangeTabSkins()
    {
        Upgrade.SetActive(false);
        Purchase.SetActive(false);
        Equip.SetActive(true);

        ChangeEquipPosition();
        activateChildObjects();
        Tab.sprite = Tab_Skins;

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

        SHDX.setTarget(SkinMainTarget);
    }

    public void ChangeEquipPosition()
    {
        Equip.transform.position = Purchase.transform.position;
    }

    public void RevertEquipPosition()
    {
        Equip.transform.position = OriginalEquip.transform.position;
    }
}
