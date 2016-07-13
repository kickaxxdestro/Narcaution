using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopHandlerDeluxe : MonoBehaviour
{
    public enum SHOP_ITEMS
    {
        ITEM_MULTIPLIER,
        ITEM_SENTRY,
        ITEM_BOMBS,
        ITEM_MISSLES,

        ITEM_WEAPON_BLAST,
        ITEM_WEAPON_CRES,
        ITEM_WEAPON_LASER,
        ITEM_WEAPON_PULSE,
        ITEM_WEAPON_BOOMERANG,

        ITEM_PLAYER_SKIN_01,
        ITEM_PLAYER_SKIN_02,
        ITEM_PLAYER_SKIN_03,
    }

    public enum ITEM_TYPE
    {
        TYPE_POWERUP,
        TYPE_WEAPON,
        TYPE_SKIN,
    }

    //public Color sufficientRibbonColor;
    //public Color insufficientRibbonColor;

    ////Item prices
    //public int priceSkin01;
    //public int priceSkin02;
    //public int priceSkin03;

    //GameObject itemDisplay;
    //GameObject currentDisplayLayout;

    //public GameObject sentryPrefab;
    //public GameObject bombsPrefab;
    //public GameObject misslesPrefab;
    //public GameObject multiplierPrefab;
    //public GameObject[] weaponList;
    //public GameObject[] skinList;

    //public AudioClip purchaseAudio;
    //public AudioClip insufficientAudio;
    //public AudioClip equipAudio;

    private AudioSource audioSource;

    int playerMoney;

    GameObject targetItem;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        //Get player money
        try
        {
            playerMoney = PlayerPrefs.GetInt("ppPlayerMoney", 0);
        }
        catch (System.Exception err)
        {
            Debug.Log("Got: " + err);
        }
        GameObject.Find("ValueDisplay").GetComponent<MoneyTextDisplay>().UpdateText();
    }

    public void setTarget(GameObject button) //Select the tab you want to interact with
    {
        targetItem = button.transform.parent.gameObject;
    }


    //Anything to do with displaying the layout and the descriptions

    void Display()
    {

    }

    //Anything to do with spending money to purchase or upgrade items

    bool DeductMoney(int price)
    {
        if (playerMoney >= price)
        {
            playerMoney -= price;
            PlayerPrefs.SetInt("ppPlayerMoney", playerMoney);
            PlayerPrefs.Save();
            GameObject.Find("ValueDisplay").GetComponent<MoneyTextDisplay>().UpdateText();
            //SetItemButtonColor();
            return true;
        }
        return false;
    }

    public void Purchase()
    {
        if (targetItem.tag == "Shop_Item_PowerUp") //Powerups
        {
            string getItemName = "ppNum";
            if (targetItem.name == "Item_PowerUp_Multiplier")
            {
                getItemName += "Boost";
            }
            else if (targetItem.name == "Item_PowerUp_Sentry")
            {
                getItemName += "Sentry";
            }
            else if (targetItem.name == "Item_PowerUp_Bomb")
            {
                getItemName += "Bomb";
            }
            else if (targetItem.name == "Item_PowerUp_Missles")
            {
                getItemName += "Missles";
            }
            if (DeductMoney(targetItem.GetComponent<GeneralItem>().cost))
            {
                PlayerPrefs.SetInt(getItemName, PlayerPrefs.GetInt(getItemName, 0) + 1);
                PlayerPrefs.Save();
            }
        }
        else if (targetItem.tag == "Shop_Item_Weapon") //Weapons
        {
            int currentWeaponLevel = PlayerPrefs.GetInt("pp" + targetItem.name + "Level", 0);
            if (currentWeaponLevel < 5)
            {
                if (DeductMoney(targetItem.GetComponent<Weapon>().LevelXCost[currentWeaponLevel]))
                {
                    PlayerPrefs.SetInt("pp" + targetItem.name + "Level", currentWeaponLevel + 1);
                    if (PlayerPrefs.GetInt("ppCurrentWeapon", 1) == targetItem.GetComponent<Weapon>().ID)
                        PlayerPrefs.SetInt("ppCurrentWeaponLevel", currentWeaponLevel + 1);
                    PlayerPrefs.Save();
                }
            }
        }
        else if (targetItem.tag == "Shop_Item_Skin") //Skins
        {
            if (DeductMoney(targetItem.GetComponent<GeneralItem>().cost))
            {
                PlayerPrefs.SetInt("ppSkin" + targetItem.GetComponent<GeneralItem>().itemID + "Unlocked", 1);
                PlayerPrefs.Save();
            }
        }
    }

    //Anything to do with equiping items

    public void Equip()
    {
        if (targetItem.tag == "Shop_Item_Weapon") //Weapons
        {
            if (PlayerPrefs.GetInt("pp" + targetItem.GetComponent<Weapon>().name + "Level", 0) >= 1)
            {
                PlayerPrefs.SetInt("ppCurrentWeapon", targetItem.GetComponent<Weapon>().ID);
                PlayerPrefs.SetInt("ppCurrentWeaponLevel", PlayerPrefs.GetInt("pp" + targetItem.GetComponent<Weapon>().name + "Level", 0));
                PlayerPrefs.Save();
            }
        }
        else if (targetItem.tag == "Shop_Item_Skin") //Skins
        {
            PlayerPrefs.SetInt("ppCurrentSkin", targetItem.GetComponent<GeneralItem>().itemID);
            PlayerPrefs.Save();
        }
    }
}