using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopHandlerDeluxe : MonoBehaviour
{

    //public Color sufficientRibbonColor;
    //public Color insufficientRibbonColor;

    ////Item prices
    //public int priceSkin01;
    //public int priceSkin02;
    //public int priceSkin03;

    //GameObject itemDisplay;
    //GameObject currentDisplayLayout;

    public GameObject multiplierPrefab;
    public GameObject sentryPrefab;
    public GameObject bombsPrefab;
    public GameObject missilesPrefab;
    public GameObject[] weaponList;
    public GameObject[] skinList;

    int playerMoney;
    int levelCheck;
    int skinCheck;

    GameObject targetName;
    GameObject targetItem;
    GameObject targetPrefab;
    GameObject selectedSkin;
    Weapon selectedWeapon;

    ChangeShopTab changeButton;

    public AudioClip purchaseAudio;
    public AudioClip insufficientAudio;
    public AudioClip equipAudio;
    private AudioSource audioSource;

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

        Display();
   
        GameObject.Find("ValueDisplay").GetComponent<MoneyTextDisplay>().UpdateText();

        changeButton = gameObject.transform.GetChild(0).GetComponent<ChangeShopTab>();

        if (PlayerPrefs.GetInt("pp" + weaponList[0].GetComponent<Weapon>().name + "Level", 0) == 0)
        {
            PlayerPrefs.SetInt("pp" + weaponList[0].GetComponent<Weapon>().name + "Level", 1);
            Debug.Log("Upgraded!");
        }
    }

    public void setTarget(GameObject button) //Select the tab you want to interact with
    {
        targetName = button.transform.parent.gameObject;
        Debug.Log(targetName.name);
        if (targetName.tag == "Shop_Item_Weapon" && targetName.name != "Header_Weapons")
        {
            selectedWeapon = setWeaponComponent(targetName);
            levelCheck = PlayerPrefs.GetInt("pp" + selectedWeapon.name + "Level", 0);
            Debug.Log(levelCheck);  
            changeButtonsForWeapon(levelCheck);
        }
        else if (targetName.tag == "Shop_Item_Skin" && targetName.name != "Header_Skins")
        {
            selectedSkin = setSkinComponent(targetName);
            skinCheck = PlayerPrefs.GetInt("ppSkin" + selectedSkin.GetComponent<GeneralItem>().itemID + "Unlocked");
            changeButtonsForSkins(skinCheck);
        }
    }

    public void changeButtonsForWeapon(int currentLevelCheck)
    {
        if (currentLevelCheck == 0)
        {
            changeButton.Upgrade.SetActive(false);
            changeButton.Purchase.SetActive(true);
            changeButton.Equip.SetActive(false);
        }
        else if (currentLevelCheck > 0)
        {
            changeButton.Upgrade.SetActive(true);
            changeButton.Purchase.SetActive(false);
            changeButton.Equip.SetActive(true);
        }
    }

    public void changeButtonsForSkins(int skinUnlockCheck)
    {
        if (skinUnlockCheck == 0)
        {
            changeButton.Upgrade.SetActive(false);
            changeButton.Purchase.SetActive(true);
            changeButton.Equip.SetActive(false);
        }
        else if (skinUnlockCheck == 1)
        {
            changeButton.Upgrade.SetActive(false);
            changeButton.Purchase.SetActive(false);
            changeButton.Equip.SetActive(true);
        }
    }

    //Anything to do with displaying the layout and the descriptions

    void Display()
    {
        for (int i = 0; i < this.transform.FindChild("Grid").childCount; i++)
        {
            if (this.transform.FindChild("Grid").GetChild(i).tag == "Shop_Item_Weapon" && this.transform.FindChild("Grid").GetChild(i).name != "Header_Weapons") //Weapons
            {
                selectedWeapon = setWeaponComponent(this.transform.FindChild("Grid").GetChild(i));
                levelCheck = PlayerPrefs.GetInt("pp" + selectedWeapon.name + "Level", 0);

                if (levelCheck > 0)
                {


                    this.transform.FindChild("Grid").GetChild(i).FindChild("Extra_Description").FindChild("Damage").GetComponent<Text>().text = "Damage: " + selectedWeapon.LevelXBulletDamage[levelCheck - 1].ToString();

                    this.transform.FindChild("Grid").GetChild(i).FindChild("Extra_Description").FindChild("AtkSpeed").GetComponent<Text>().text = "AtkSpeed: " + selectedWeapon.LevelXFiringSpeed[levelCheck - 1].ToString();

                    for (int j = 0; j < levelCheck; j++)
                    {
                        this.transform.FindChild("Grid").GetChild(i).FindChild("Extra_Description").FindChild("Level").GetChild(j).gameObject.SetActive(true);
                    }
                }
                else if (levelCheck == 0)
                {
                    this.transform.FindChild("Grid").GetChild(i).FindChild("Extra_Description").FindChild("AtkSpeed").GetComponent<Text>().text = "AtkSpeed: " + selectedWeapon.LevelXFiringSpeed[levelCheck].ToString();

                    this.transform.FindChild("Grid").GetChild(i).FindChild("Description_Button").FindChild("Price Value").GetComponent<Text>().text = selectedWeapon.LevelXCost[levelCheck].ToString();
                }
                this.transform.FindChild("Grid").GetChild(i).FindChild("Description_Button").FindChild("Price Value").GetComponent<Text>().text = selectedWeapon.LevelXCost[levelCheck].ToString();

                this.transform.FindChild("Grid").GetChild(i).FindChild("Extra_Description").FindChild("Type").GetComponent<Text>().text = "Type: " + Weapon.GetFiringPattenString(selectedWeapon.firingPattern);
            }
        }
    }

    GameObject setSkinComponent(GameObject skin)
    {
        if (skin.name == "Item_Skins_Grallion")
        {
            return skinList[0];
        }
        else if (skin.name == "Item_Skins_Tenrho")
        {
            return skinList[1];
        }
        else if (skin.name == "Item_Skins_Buster")
        {
            return skinList[2];
        }
        else
        {
            return null;
        }
    }
    Weapon setWeaponComponent(Transform weapon)
    {
        if (weapon.name == "Item_Weapons_DrugBlaster")
        {
            return weaponList[0].GetComponent<Weapon>();
        }
        else if (weapon.name == "Item_Weapons_Purifier")
        {
            return weaponList[1].GetComponent<Weapon>();
        }
        else if (weapon.name == "Item_Weapons_Cleanser")
        {
            return weaponList[2].GetComponent<Weapon>();
        }
        else if (weapon.name == "Item_Weapons_Repulsor")
        {
            return weaponList[3].GetComponent<Weapon>();
        }
        else if (weapon.name == "Item_Weapons_Vindicator")
        {
            return weaponList[4].GetComponent<Weapon>();
        }
        else
        {
            return null;
        }
    }
    Weapon setWeaponComponent(GameObject weapon)
    {
        if (weapon.name == "Item_Weapons_DrugBlaster")
        {
            return weaponList[0].GetComponent<Weapon>();
        }
        else if (weapon.name == "Item_Weapons_Purifier")
        {
            return weaponList[1].GetComponent<Weapon>();
        }
        else if (weapon.name == "Item_Weapons_Cleanser")
        {
            return weaponList[2].GetComponent<Weapon>();
        }
        else if (weapon.name == "Item_Weapons_Repulsor")
        {
            return weaponList[3].GetComponent<Weapon>();
        }
        else if (weapon.name == "Item_Weapons_Vindicator")
        {
            return weaponList[4].GetComponent<Weapon>();
        }
        else
        {
            return null;
        }
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
            audioSource.clip = purchaseAudio;
            audioSource.Play();
            return true;

        }
        else
        {
            audioSource.clip = insufficientAudio;
            audioSource.Play();
        }
        return false;
    }

    public void Purchase()
    {
        if (targetName.tag == "Shop_Item_PowerUp") //Powerups
        {
            string getItemName = "ppNum";
            if (targetName.name == "Item_PowerUps_ScoreMultiplier")
            {
                getItemName += "Boost";
                HandleItemPurchase(multiplierPrefab, getItemName);
            }
            else if (targetName.name == "Item_PowerUps_Sentry")
            {
                getItemName += "Sentry";
                HandleItemPurchase(sentryPrefab, getItemName);
            }
            else if (targetName.name == "Item_PowerUps_Bomb")
            {
                getItemName += "Bomb";
                HandleItemPurchase(missilesPrefab, getItemName);
            }
            else if (targetName.name == "Item_PowerUps_HomingMissiles")
            {
                getItemName += "Missles";
                HandleItemPurchase(bombsPrefab, getItemName);
            }
        }
        else if (targetName.tag == "Shop_Item_Weapon") //Weapons
        {
            if (targetName.name == "Item_Weapons_DrugBlaster")
            {
                HandleWeaponPurchase(weaponList[0].GetComponent<Weapon>());
            }
            else if (targetName.name == "Item_Weapons_Purifier")
            {
                HandleWeaponPurchase(weaponList[1].GetComponent<Weapon>());
            }
            else if (targetName.name == "Item_Weapons_Cleanser")
            {
                HandleWeaponPurchase(weaponList[2].GetComponent<Weapon>());
            }
            else if (targetName.name == "Item_Weapons_Repulser")
            {
                HandleWeaponPurchase(weaponList[3].GetComponent<Weapon>());
            }
            else if (targetName.name == "Item_Weapons_Vindicator")
            {
                HandleWeaponPurchase(weaponList[4].GetComponent<Weapon>());
            }
        }
        else if (targetName.tag == "Shop_Item_Skin") //Skins
        {
            if (targetName.name == "Item_Skins_Tenrho")
            {
                HandleSkinPurchase(skinList[1]);
            }
            else if (targetName.name == "Item_Skins_Buster")
            {
                HandleSkinPurchase(skinList[2]);
            }
        }
    }

    void HandleItemPurchase(GameObject item, string pref)
    {
        if (DeductMoney(item.GetComponent<GeneralItem>().cost))
        {
            PlayerPrefs.SetInt(pref, PlayerPrefs.GetInt(pref, 0) + 1);
            PlayerPrefs.Save();
        }
    }

    void HandleWeaponPurchase(Weapon weapon)
    {
        int currentWeaponLevel = PlayerPrefs.GetInt("pp" + weapon.name + "Level", 0);
        if (currentWeaponLevel < 5)
        {
            if (DeductMoney(weapon.LevelXCost[currentWeaponLevel]))
            {
                PlayerPrefs.SetInt("pp" + weapon.name + "Level", currentWeaponLevel + 1);
                if (PlayerPrefs.GetInt("ppCurrentWeapon", 1) == weapon.ID)
                    PlayerPrefs.SetInt("ppCurrentWeaponLevel", currentWeaponLevel + 1);
                PlayerPrefs.Save();
                changeButtonsForWeapon(PlayerPrefs.GetInt("pp" + weapon.name + "Level", 0));
                Display();
            }
        }
    }

    void HandleSkinPurchase(GameObject skin)
    {
        if (DeductMoney(skin.GetComponent<GeneralItem>().cost))
        {
            PlayerPrefs.SetInt("ppSkin" + skin.GetComponent<GeneralItem>().itemID + "Unlocked", 1);
            PlayerPrefs.Save();
            changeButtonsForSkins(PlayerPrefs.GetInt(skin.GetComponent<GeneralItem>().itemID + "Unlocked"));
        }
    }

    //Anything to do with equiping items

    public void Equip()
    {
        if (targetName.tag == "Shop_Item_Weapon") //Weapons
        {
            if (PlayerPrefs.GetInt("pp" + targetName.GetComponent<Weapon>().name + "Level", 0) >= 1)
            {
                PlayerPrefs.SetInt("ppCurrentWeapon", targetName.GetComponent<Weapon>().ID);
                PlayerPrefs.SetInt("ppCurrentWeaponLevel", PlayerPrefs.GetInt("pp" + targetName.GetComponent<Weapon>().name + "Level", 0));
                PlayerPrefs.Save();
                Display();
            }
        }
        else if (targetName.tag == "Shop_Item_Skin") //Skins
        {
            selectedSkin = setSkinComponent(targetName);
            PlayerPrefs.SetInt("ppCurrentSkin", selectedSkin.GetComponent<GeneralItem>().itemID);
            PlayerPrefs.Save();
            Display();
        }

        audioSource.clip = equipAudio;
        audioSource.Play();
    }
}