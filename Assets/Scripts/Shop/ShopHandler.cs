using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShopHandler : MonoBehaviour
{

    /*
     Player preferences used:
     * ppShieldLevel
     * ppWepBlastLevel
     * ppWepCresLevel
     * ppWepLaserLevel
     * ppWepPulseLevel
     * ppWepBoomerangLevel
     * ppNumBoost
     * ppNumSentry
     * ppPlayerMoney
     * ppCurrentWeapon
     * ppCurrentWeaponLevel
     * ppCurrentSkin
     * ppSkin02Unlocked
     * ppSkin03Unlocked
     */

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

        ITEM_BARRIER_01,
        ITEM_BARRIER_02,
        ITEM_BARRIER_03,
        ITEM_BARRIER_04,
        ITEM_BARRIER_05,
    }

    public enum ITEM_TYPE
    {
        TYPE_POWERUP,
        TYPE_WEAPON,
        TYPE_BARRIER,
        TYPE_SKIN,
    }

    public Color sufficientRibbonColor;
    public Color insufficientRibbonColor;

    //Item prices
    public int priceSkin01 = 10;
    public int priceSkin02 = 10;
    public int priceSkin03 = 10;

    GameObject itemDisplay;
    GameObject currentDisplayLayout;

    public GameObject sentryPrefab;
    public GameObject bombsPrefab;
    public GameObject misslesPrefab;
    public GameObject multiplierPrefab;
    public GameObject[] weaponList;
    public GameObject[] barrierList;
    public GameObject[] skinList;

    public AudioClip purchaseAudio;
    public AudioClip insufficientAudio;
    public AudioClip equipAudio;

    private AudioSource audioSource;

    SHOP_ITEMS selectedItem;

    int playerMoney;

    void Awake()
    {
        itemDisplay = transform.FindChild("ItemDisplay").gameObject;
        if(itemDisplay == null)
        {
            print("ItemDisplay not found, assign as child of shop handler");
        }
        if(PlayerPrefs.GetInt("pp" + weaponList[0].GetComponent<Weapon>().name + "Level", 0) == 0)
        {
            PlayerPrefs.SetInt("pp" + weaponList[0].GetComponent<Weapon>().name + "Level", 1);
        }
        if (PlayerPrefs.GetInt("pp" + barrierList[0].GetComponent<Barrier>().name + "Level", 0) == 0)
        {
            PlayerPrefs.SetInt("pp" + barrierList[0].GetComponent<Barrier>().name + "Level", 1);
        }
        if (PlayerPrefs.GetInt("ppSkin" + skinList[0].GetComponent<GeneralItem>().itemID + "Unlocked", 0) == 0)
        {
            PlayerPrefs.SetInt("ppSkin" + skinList[0].GetComponent<GeneralItem>().itemID + "Unlocked", 1);
        }

        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
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

        //SetItemButtonValues();
        //SetItemButtonColor();
        //SetItemButtonQuantity();
        UpdateButtonLevelDisplay(SHOP_ITEMS.ITEM_WEAPON_BLAST);
        UpdateButtonLevelDisplay(SHOP_ITEMS.ITEM_WEAPON_CRES);
        UpdateButtonLevelDisplay(SHOP_ITEMS.ITEM_WEAPON_LASER);
        UpdateButtonLevelDisplay(SHOP_ITEMS.ITEM_WEAPON_PULSE);
        UpdateButtonLevelDisplay(SHOP_ITEMS.ITEM_WEAPON_BOOMERANG);
        UpdateButtonLevelDisplay(SHOP_ITEMS.ITEM_BARRIER_01);
        UpdateButtonLevelDisplay(SHOP_ITEMS.ITEM_BARRIER_02);
        UpdateButtonLevelDisplay(SHOP_ITEMS.ITEM_BARRIER_03);
        UpdateButtonLevelDisplay(SHOP_ITEMS.ITEM_BARRIER_04);
        UpdateButtonLevelDisplay(SHOP_ITEMS.ITEM_BARRIER_05);
        UpdateEquippedIconDisplay();
        UpdateSkinPurchased();
        //UpdateButtonUnlockedDisplay(SHOP_ITEMS.ITEM_PLAYER_SKIN_02);
        //UpdateButtonUnlockedDisplay(SHOP_ITEMS.ITEM_PLAYER_SKIN_03);

        if(PlayerPrefs.GetInt("ppCurrentLevel", 1) < 22)
        {
            GameObject[] objectList;
            if (PlayerPrefs.GetInt("ppPlayerGamemode", 0) == 0)
            {
                objectList = GameObject.FindGameObjectsWithTag("DefenseShopItem");
                foreach(GameObject go in objectList)
                {
                    go.SetActive(false);
                }
            }
            else
            {
                objectList = GameObject.FindGameObjectsWithTag("OffenseShopItem");
                foreach (GameObject go in objectList)
                {
                    go.SetActive(false);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Pass in string - Unity buttons do not take in enum
    public void SelectItem(string item)
    {
        SHOP_ITEMS parsed_item = (SHOP_ITEMS)System.Enum.Parse(typeof(SHOP_ITEMS), item);
        selectedItem = parsed_item;
        switch (selectedItem)
        {
            case SHOP_ITEMS.ITEM_MULTIPLIER:
                DisplayLayoutType(ITEM_TYPE.TYPE_POWERUP);
                SetItemStatsToLayout(multiplierPrefab, PlayerPrefs.GetInt("ppNumBoost", 0));
                break;
            case SHOP_ITEMS.ITEM_SENTRY:
                DisplayLayoutType(ITEM_TYPE.TYPE_POWERUP);
                SetItemStatsToLayout(sentryPrefab, PlayerPrefs.GetInt("ppNumSentry", 0));
                break;
            case SHOP_ITEMS.ITEM_BOMBS:
                DisplayLayoutType(ITEM_TYPE.TYPE_POWERUP);
                SetItemStatsToLayout(bombsPrefab, PlayerPrefs.GetInt("ppNumBombs", 0));
                break;
            case SHOP_ITEMS.ITEM_MISSLES:
                DisplayLayoutType(ITEM_TYPE.TYPE_POWERUP);
                SetItemStatsToLayout(misslesPrefab, PlayerPrefs.GetInt("ppNumMissles", 0));
                break;
            case SHOP_ITEMS.ITEM_WEAPON_BLAST:
            case SHOP_ITEMS.ITEM_WEAPON_CRES:
            case SHOP_ITEMS.ITEM_WEAPON_LASER:
            case SHOP_ITEMS.ITEM_WEAPON_PULSE:
            case SHOP_ITEMS.ITEM_WEAPON_BOOMERANG:
                DisplayLayoutType(ITEM_TYPE.TYPE_WEAPON);
                SetWeaponStatsToLayout(GetSelectedWeapon());
                break;
            case SHOP_ITEMS.ITEM_PLAYER_SKIN_01:
                DisplayLayoutType(ITEM_TYPE.TYPE_SKIN);
                SetSkinStatsToLayout(skinList[0]);
                break;
            case SHOP_ITEMS.ITEM_PLAYER_SKIN_02:
                DisplayLayoutType(ITEM_TYPE.TYPE_SKIN);
                SetSkinStatsToLayout(skinList[1]);
                break;
            case SHOP_ITEMS.ITEM_PLAYER_SKIN_03:
                DisplayLayoutType(ITEM_TYPE.TYPE_SKIN);
                SetSkinStatsToLayout(skinList[2]);
                break;
            case SHOP_ITEMS.ITEM_BARRIER_01:
            case SHOP_ITEMS.ITEM_BARRIER_02:
            case SHOP_ITEMS.ITEM_BARRIER_03: 
            case SHOP_ITEMS.ITEM_BARRIER_04: 
            case SHOP_ITEMS.ITEM_BARRIER_05: 
                DisplayLayoutType(ITEM_TYPE.TYPE_BARRIER);
                SetBarrierStatsToLayout(GetSelectedBarrier());
                break;
            default:
                break;
        }

        itemDisplay.GetComponent<SliderItem>().DoLerpToCenter_FromRight();
        itemDisplay.transform.FindChild("BackButton").GetComponent<Button>().interactable = true;
        GameObject.Find("ExitButton").GetComponent<Button>().interactable = false;
        GameObject.Find("ColourMaskHandler").GetComponent<ColourMaskController>().ActivateColourMask(ColourMaskController.COLOURMODE.COLOURMODE_TO_ALPHA_GREY, 1f);
    }

    void SetWeaponStatsToLayout(Weapon item)
    {
        //PlayerPrefs.GetInt("pp" + item.name + "Level", 0);

        itemDisplay.transform.FindChild("DescriptionText").GetComponent<Text>().text = item.weaponDescription;
        itemDisplay.transform.FindChild("Header").GetComponent<Text>().text = item.name;
        itemDisplay.transform.FindChild("BackgroundIcon").GetComponent<Image>().sprite = item.icon;

        UpdateWeaponDisplayLayout();
    }

    void SetBarrierStatsToLayout(Barrier item)
    {
        //int levelCheck = PlayerPrefs.GetInt("pp" + item.name + "Level", 0);

        itemDisplay.transform.FindChild("DescriptionText").GetComponent<Text>().text = item.barrierDescription;
        itemDisplay.transform.FindChild("Header").GetComponent<Text>().text = item.name;
        itemDisplay.transform.FindChild("BackgroundIcon").GetComponent<Image>().sprite = item.icon;

        UpdateBarrierDisplayLayout();
    }

    public void SetItemStatsToLayout(GameObject item, int quantity)
    {
        itemDisplay.transform.FindChild("Header").GetComponent<Text>().text = item.GetComponent<GeneralItem>().itemName;
        itemDisplay.transform.FindChild("BackgroundIcon").GetComponent<Image>().sprite = item.GetComponent<GeneralItem>().icon;
        itemDisplay.transform.FindChild("DescriptionText").GetComponent<Text>().text = item.GetComponent<GeneralItem>().description;

        UpdateItemDisplayLayout(item, quantity);
    }

    public void SetSkinStatsToLayout(GameObject item)
    {
        itemDisplay.transform.FindChild("Header").GetComponent<Text>().text = item.GetComponent<GeneralItem>().itemName;
        itemDisplay.transform.FindChild("BackgroundIcon").gameObject.SetActive(false);
        itemDisplay.transform.FindChild("DescriptionText").GetComponent<Text>().text = item.GetComponent<GeneralItem>().description;

        currentDisplayLayout.transform.FindChild("PurchaseButton").FindChild("Cost").GetComponent<Text>().text = item.GetComponent<GeneralItem>().cost.ToString();
        if (playerMoney < item.GetComponent<GeneralItem>().cost)
            currentDisplayLayout.transform.FindChild("PurchaseButton").FindChild("Cost").GetComponent<Text>().color = insufficientRibbonColor;
        else
            currentDisplayLayout.transform.FindChild("PurchaseButton").FindChild("Cost").GetComponent<Text>().color = sufficientRibbonColor;
        currentDisplayLayout.transform.FindChild("SkinDisplay").GetComponent<Image>().sprite = item.GetComponent<GeneralItem>().icon;

        UpdateSkinDisplayLayout(item);
    }

    public void EquipSelectedItem()
    { 
        switch (selectedItem)
        {
            case SHOP_ITEMS.ITEM_WEAPON_BLAST:
            case SHOP_ITEMS.ITEM_WEAPON_CRES:
            case SHOP_ITEMS.ITEM_WEAPON_LASER:
            case SHOP_ITEMS.ITEM_WEAPON_PULSE:
            case SHOP_ITEMS.ITEM_WEAPON_BOOMERANG:
                HandleWeaponEquip(GetSelectedWeapon());
                break;

            case SHOP_ITEMS.ITEM_PLAYER_SKIN_01:
                HandleSkinEquip(skinList[0]);
                break;
            case SHOP_ITEMS.ITEM_PLAYER_SKIN_02:
                HandleSkinEquip(skinList[1]);
                break;
            case SHOP_ITEMS.ITEM_PLAYER_SKIN_03:
                HandleSkinEquip(skinList[2]);
                break;
            case SHOP_ITEMS.ITEM_BARRIER_01:
            case SHOP_ITEMS.ITEM_BARRIER_02:
            case SHOP_ITEMS.ITEM_BARRIER_03:
            case SHOP_ITEMS.ITEM_BARRIER_04:
            case SHOP_ITEMS.ITEM_BARRIER_05:
                HandleBarrierEquip(GetSelectedBarrier());
                break;
            default:
                break;
        }
        audioSource.clip = equipAudio;
        audioSource.Play();
    }

    public void PurchaseSelectedItem()
    {
        bool purchaseSuccessful = false;
        switch (selectedItem)
        {
            case SHOP_ITEMS.ITEM_WEAPON_BLAST:
            case SHOP_ITEMS.ITEM_WEAPON_CRES:
            case SHOP_ITEMS.ITEM_WEAPON_LASER:
            case SHOP_ITEMS.ITEM_WEAPON_PULSE:
            case SHOP_ITEMS.ITEM_WEAPON_BOOMERANG:
                purchaseSuccessful = HandleWeaponPurchase(GetSelectedWeapon());
                UpdateButtonLevelDisplay(selectedItem);
                break;

            case SHOP_ITEMS.ITEM_BARRIER_01:
            case SHOP_ITEMS.ITEM_BARRIER_02:
            case SHOP_ITEMS.ITEM_BARRIER_03:
            case SHOP_ITEMS.ITEM_BARRIER_04:
            case SHOP_ITEMS.ITEM_BARRIER_05:
                purchaseSuccessful = HandleBarrierPurchase(GetSelectedBarrier());
                UpdateButtonLevelDisplay(selectedItem);
                break;
            case SHOP_ITEMS.ITEM_PLAYER_SKIN_02:
                purchaseSuccessful = HandleSkinPurchase(skinList[1]);
                break;
            case SHOP_ITEMS.ITEM_PLAYER_SKIN_03:
                purchaseSuccessful = HandleSkinPurchase(skinList[2]);
                break;
            case SHOP_ITEMS.ITEM_MULTIPLIER:
                purchaseSuccessful = HandleItemPurchase(multiplierPrefab, "ppNumBoost");
                break;
            case SHOP_ITEMS.ITEM_SENTRY:
                purchaseSuccessful = HandleItemPurchase(sentryPrefab, "ppNumSentry");
                break;
            case SHOP_ITEMS.ITEM_BOMBS:
                purchaseSuccessful = HandleItemPurchase(bombsPrefab, "ppNumBombs");
                break;
            case SHOP_ITEMS.ITEM_MISSLES:
                purchaseSuccessful = HandleItemPurchase(misslesPrefab, "ppNumMissles");
                break;
            default:
                break;
        }

        audioSource.clip = purchaseSuccessful ? purchaseAudio : insufficientAudio;
        audioSource.Play();
    }

    void HandleWeaponEquip(Weapon weapon)
    {
        if (PlayerPrefs.GetInt("pp" + weapon.name + "Level", 0) >= 1)
        {
            PlayerPrefs.SetInt("ppCurrentWeapon", weapon.ID);
            PlayerPrefs.SetInt("ppCurrentWeaponLevel", PlayerPrefs.GetInt("pp" + weapon.name + "Level", 0));
            PlayerPrefs.Save();
            currentDisplayLayout.transform.FindChild("EquipButton").gameObject.SetActive(false);
            currentDisplayLayout.transform.FindChild("EquippedButton").gameObject.SetActive(true);

            UpdateWeaponDisplayLayout();
            UpdateEquippedIconDisplay();
        }
    }

    bool HandleWeaponPurchase(Weapon weapon)
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
                UpdateWeaponDisplayLayout();
                UpdateButtonLevelDisplay(selectedItem);
                return true;
            }
        }
        return false;
    }

    void HandleBarrierEquip(Barrier barrier)
    {
        if (PlayerPrefs.GetInt("pp" + barrier.name + "Level", 0) >= 1)
        {
            PlayerPrefs.SetInt("ppCurrentBarrier", barrier.ID);
            PlayerPrefs.SetInt("ppCurrentBarrierLevel", PlayerPrefs.GetInt("pp" + barrier.name + "Level", 0));
            PlayerPrefs.Save();
            currentDisplayLayout.transform.FindChild("EquipButton").gameObject.SetActive(false);
            currentDisplayLayout.transform.FindChild("EquippedButton").gameObject.SetActive(true);

            UpdateBarrierDisplayLayout();
            UpdateEquippedIconDisplay();
        }
    }

    bool HandleBarrierPurchase(Barrier barrier)
    {
        int currentBarrierLevel = PlayerPrefs.GetInt("pp" + barrier.name + "Level", 0);
        if (currentBarrierLevel < 5)
        {
            if (DeductMoney(barrier.LevelXCost[currentBarrierLevel]))
            {
                PlayerPrefs.SetInt("pp" + barrier.name + "Level", currentBarrierLevel + 1);
                if (PlayerPrefs.GetInt("ppCurrentBarrier", 1) == barrier.ID)
                    PlayerPrefs.SetInt("ppCurrentBarrierLevel", currentBarrierLevel + 1);
                PlayerPrefs.Save();
                UpdateBarrierDisplayLayout();
                UpdateButtonLevelDisplay(selectedItem);
                return true;
            }
        }
        return false;
    }

    void HandleSkinEquip(GameObject skin)
    {
        PlayerPrefs.SetInt("ppCurrentSkin", skin.GetComponent<GeneralItem>().itemID);
        PlayerPrefs.Save();
        currentDisplayLayout.transform.FindChild("EquipButton").gameObject.SetActive(false);
        currentDisplayLayout.transform.FindChild("EquippedButton").gameObject.SetActive(true);

        UpdateEquippedIconDisplay();

    }

    bool HandleSkinPurchase(GameObject skin)
    {
        if(DeductMoney(skin.GetComponent<GeneralItem>().cost))
        {
            PlayerPrefs.SetInt("ppSkin" + skin.GetComponent<GeneralItem>().itemID + "Unlocked", 1);
            PlayerPrefs.Save();
            UpdateSkinDisplayLayout(skin);
            UpdateSkinPurchased();
            return true;
        }
        return false;
    }   

    bool HandleItemPurchase(GameObject item, string pref)
    {
        if(DeductMoney(item.GetComponent<GeneralItem>().cost))
        {
            PlayerPrefs.SetInt(pref, PlayerPrefs.GetInt(pref, 0) + 1);
            PlayerPrefs.Save();
            UpdateItemDisplayLayout(item, PlayerPrefs.GetInt(pref, 0));
            return true;
        }
        return false;
    }

    Weapon GetSelectedWeapon()
    {
        switch (selectedItem)
        {
            case SHOP_ITEMS.ITEM_WEAPON_BLAST:
                return weaponList[0].GetComponent<Weapon>();
            case SHOP_ITEMS.ITEM_WEAPON_CRES:
                return weaponList[1].GetComponent<Weapon>();
            case SHOP_ITEMS.ITEM_WEAPON_LASER:
                return weaponList[2].GetComponent<Weapon>();
            case SHOP_ITEMS.ITEM_WEAPON_PULSE:
                return weaponList[3].GetComponent<Weapon>();
            case SHOP_ITEMS.ITEM_WEAPON_BOOMERANG:
                return weaponList[4].GetComponent<Weapon>();
        }
        return null;
    }

    Barrier GetSelectedBarrier()
    {
        switch (selectedItem)
        {
            case SHOP_ITEMS.ITEM_BARRIER_01:
                return barrierList[0].GetComponent<Barrier>();
            case SHOP_ITEMS.ITEM_BARRIER_02:
                return barrierList[1].GetComponent<Barrier>();
            case SHOP_ITEMS.ITEM_BARRIER_03:
                return barrierList[2].GetComponent<Barrier>();
            case SHOP_ITEMS.ITEM_BARRIER_04:
                return barrierList[3].GetComponent<Barrier>();
            case SHOP_ITEMS.ITEM_BARRIER_05:
                return barrierList[4].GetComponent<Barrier>();
        }
        return null;
    }

    void UpdateItemDisplayLayout(GameObject item, int quantity)
    {
        currentDisplayLayout.transform.FindChild("PurchaseButton").FindChild("Cost").GetComponent<Text>().text = item.GetComponent<GeneralItem>().cost.ToString();
        if (playerMoney < item.GetComponent<GeneralItem>().cost)
            currentDisplayLayout.transform.FindChild("PurchaseButton").FindChild("Cost").GetComponent<Text>().color = insufficientRibbonColor;
        else
            currentDisplayLayout.transform.FindChild("PurchaseButton").FindChild("Cost").GetComponent<Text>().color = sufficientRibbonColor;

        currentDisplayLayout.transform.FindChild("Quantity").GetComponent<Text>().text = quantity.ToString();
    }

    void UpdateWeaponDisplayLayout()
    {
        Weapon selectedWeapon = GetSelectedWeapon();
        int levelCheck = PlayerPrefs.GetInt("pp" + selectedWeapon.name + "Level", 0);

        currentDisplayLayout.transform.FindChild("LevelDisplay").GetComponent<Text>().text = "Level " + levelCheck.ToString();

        if (levelCheck == 0)
        {
            currentDisplayLayout.transform.FindChild("PurchaseButton").gameObject.SetActive(true);

            if (playerMoney < selectedWeapon.LevelXCost[levelCheck])
                currentDisplayLayout.transform.FindChild("PurchaseButton").FindChild("Cost").GetComponent<Text>().color = insufficientRibbonColor;
            else
                currentDisplayLayout.transform.FindChild("PurchaseButton").FindChild("Cost").GetComponent<Text>().color = sufficientRibbonColor;

            currentDisplayLayout.transform.FindChild("PurchaseButton").FindChild("Cost").GetComponent<Text>().text = selectedWeapon.LevelXCost[levelCheck].ToString();
            currentDisplayLayout.transform.FindChild("UpgradeButton").gameObject.SetActive(false);
            currentDisplayLayout.transform.FindChild("MaxButton").gameObject.SetActive(false);
            currentDisplayLayout.transform.FindChild("EquipButton").gameObject.SetActive(false);
            currentDisplayLayout.transform.FindChild("EquippedButton").gameObject.SetActive(false);

            //Set weapon level to 1 to display level 1 stats
            levelCheck = 1;
        }
        else
        {
            currentDisplayLayout.transform.FindChild("PurchaseButton").gameObject.SetActive(false);
            if (levelCheck >= Weapon.maxWeaponLevel)
            {
                currentDisplayLayout.transform.FindChild("UpgradeButton").gameObject.SetActive(false);
                currentDisplayLayout.transform.FindChild("MaxButton").gameObject.SetActive(true);
            }
            else
            {
                currentDisplayLayout.transform.FindChild("UpgradeButton").gameObject.SetActive(true);
                currentDisplayLayout.transform.FindChild("UpgradeButton").FindChild("Cost").GetComponent<Text>().text = selectedWeapon.LevelXCost[levelCheck].ToString();
                if (playerMoney < selectedWeapon.LevelXCost[levelCheck])
                    currentDisplayLayout.transform.FindChild("UpgradeButton").FindChild("Cost").GetComponent<Text>().color = insufficientRibbonColor;
                else
                    currentDisplayLayout.transform.FindChild("UpgradeButton").FindChild("Cost").GetComponent<Text>().color = sufficientRibbonColor;
                currentDisplayLayout.transform.FindChild("MaxButton").gameObject.SetActive(false);
            }

            if (PlayerPrefs.GetInt("ppCurrentWeapon", 1) == selectedWeapon.ID)
            {
                currentDisplayLayout.transform.FindChild("EquipButton").gameObject.SetActive(false);
                currentDisplayLayout.transform.FindChild("EquippedButton").gameObject.SetActive(true);
            }
            else
            {
                currentDisplayLayout.transform.FindChild("EquipButton").gameObject.SetActive(true);
                currentDisplayLayout.transform.FindChild("EquippedButton").gameObject.SetActive(false);
            }
        }

        currentDisplayLayout.transform.FindChild("Stat_Damage").FindChild("stat").GetComponent<Text>().text = selectedWeapon.LevelXBulletDamage[levelCheck - 1].ToString();
        currentDisplayLayout.transform.FindChild("Stat_DPS").FindChild("stat").GetComponent<Text>().text = ((int)Weapon.CalculateDPS(selectedWeapon, levelCheck)).ToString();
        currentDisplayLayout.transform.FindChild("Stat_ProjectileNum").FindChild("stat").GetComponent<Text>().text = selectedWeapon.LevelXNumberOfProjectiles[levelCheck - 1].ToString();
        currentDisplayLayout.transform.FindChild("Stat_ROF").FindChild("stat").GetComponent<Text>().text = selectedWeapon.LevelXFiringSpeed[levelCheck - 1].ToString();
        currentDisplayLayout.transform.FindChild("Stat_ProjectileSpeed").FindChild("stat").GetComponent<Text>().text = selectedWeapon.projectileSpeed.ToString();
        currentDisplayLayout.transform.FindChild("Stat_FiringType").FindChild("stat").GetComponent<Text>().text = Weapon.GetFiringPattenString(selectedWeapon.firingPattern);
        currentDisplayLayout.transform.FindChild("Stat_ProjectileMovement").FindChild("stat").GetComponent<Text>().text = Weapon.GetProjectileMovementString(selectedWeapon.projectileMovement);
    }

    void UpdateBarrierDisplayLayout()
    {
        Barrier selectedBarrier = GetSelectedBarrier();
        int levelCheck = PlayerPrefs.GetInt("pp" + selectedBarrier.name + "Level", 0);

        currentDisplayLayout.transform.FindChild("LevelDisplay").GetComponent<Text>().text = "Level " + levelCheck.ToString();

        if (levelCheck == 0)
        {
            currentDisplayLayout.transform.FindChild("PurchaseButton").gameObject.SetActive(true);
            if (playerMoney < selectedBarrier.LevelXCost[levelCheck])
                currentDisplayLayout.transform.FindChild("PurchaseButton").FindChild("Cost").GetComponent<Text>().color = insufficientRibbonColor;
            else
                currentDisplayLayout.transform.FindChild("PurchaseButton").FindChild("Cost").GetComponent<Text>().color = sufficientRibbonColor;

            currentDisplayLayout.transform.FindChild("PurchaseButton").FindChild("Cost").GetComponent<Text>().text = selectedBarrier.LevelXCost[levelCheck].ToString();
            currentDisplayLayout.transform.FindChild("UpgradeButton").gameObject.SetActive(false);
            currentDisplayLayout.transform.FindChild("MaxButton").gameObject.SetActive(false);
            currentDisplayLayout.transform.FindChild("EquipButton").gameObject.SetActive(false);
            currentDisplayLayout.transform.FindChild("EquippedButton").gameObject.SetActive(false);

            //Set weapon level to 1 to display level 1 stats
            levelCheck = 1;
        }
        else
        {
            currentDisplayLayout.transform.FindChild("PurchaseButton").gameObject.SetActive(false);
            if (levelCheck >= Barrier.maxBarrierLevel)
            {
                currentDisplayLayout.transform.FindChild("UpgradeButton").gameObject.SetActive(false);
                currentDisplayLayout.transform.FindChild("MaxButton").gameObject.SetActive(true);
            }
            else
            {
                currentDisplayLayout.transform.FindChild("UpgradeButton").gameObject.SetActive(true);
                currentDisplayLayout.transform.FindChild("UpgradeButton").FindChild("Cost").GetComponent<Text>().text = selectedBarrier.LevelXCost[levelCheck].ToString();
                if (playerMoney < selectedBarrier.LevelXCost[levelCheck])
                    currentDisplayLayout.transform.FindChild("UpgradeButton").FindChild("Cost").GetComponent<Text>().color = insufficientRibbonColor;
                else
                    currentDisplayLayout.transform.FindChild("UpgradeButton").FindChild("Cost").GetComponent<Text>().color = sufficientRibbonColor;
                currentDisplayLayout.transform.FindChild("MaxButton").gameObject.SetActive(false);
            }

            if (PlayerPrefs.GetInt("ppCurrentBarrier", 1) == selectedBarrier.ID)
            {
                currentDisplayLayout.transform.FindChild("EquipButton").gameObject.SetActive(false);
                currentDisplayLayout.transform.FindChild("EquippedButton").gameObject.SetActive(true);
            }
            else
            {
                currentDisplayLayout.transform.FindChild("EquipButton").gameObject.SetActive(true);
                currentDisplayLayout.transform.FindChild("EquippedButton").gameObject.SetActive(false);
            }
        }

        currentDisplayLayout.transform.FindChild("Stat_Durability").FindChild("stat").GetComponent<Text>().text = selectedBarrier.LevelXDurability[levelCheck - 1].ToString();
        currentDisplayLayout.transform.FindChild("Stat_Regeneration").FindChild("stat").GetComponent<Text>().text = selectedBarrier.LevelXRegeneration[levelCheck - 1].ToString();
        currentDisplayLayout.transform.FindChild("Stat_Size").FindChild("stat").GetComponent<Text>().text = selectedBarrier.size.ToString();
        currentDisplayLayout.transform.FindChild("Stat_ReflectedDamage").FindChild("stat").GetComponent<Text>().text = selectedBarrier.LevelXReflectedDamage[levelCheck - 1].ToString();
        currentDisplayLayout.transform.FindChild("Stat_ReflectedSpeed").FindChild("stat").GetComponent<Text>().text = "X" + selectedBarrier.reflectedSpeed.ToString();
        currentDisplayLayout.transform.FindChild("Stat_MinimumActivationHealth").FindChild("stat").GetComponent<Text>().text = selectedBarrier.minimumActivationHealth.ToString();
        currentDisplayLayout.transform.FindChild("Stat_Ability").FindChild("stat").GetComponent<Text>().text = "oopss";

    }

    void UpdateSkinDisplayLayout(GameObject skin)
    {
        if (PlayerPrefs.GetInt("ppSkin" + skin.GetComponent<GeneralItem>().itemID + "Unlocked", 0) == 1 || skin.GetComponent<GeneralItem>().itemID == 1)
        {
            currentDisplayLayout.transform.FindChild("PurchaseButton").gameObject.SetActive(false);
            if (PlayerPrefs.GetInt("ppCurrentSkin", 1) == skin.GetComponent<GeneralItem>().itemID)
            {
                currentDisplayLayout.transform.FindChild("EquipButton").gameObject.SetActive(false);
                currentDisplayLayout.transform.FindChild("EquippedButton").gameObject.SetActive(true);
            }
            else 
            {
                currentDisplayLayout.transform.FindChild("EquipButton").gameObject.SetActive(true);
                currentDisplayLayout.transform.FindChild("EquippedButton").gameObject.SetActive(false);
            }
        }
        else
        {
            currentDisplayLayout.transform.FindChild("PurchaseButton").gameObject.SetActive(true);
            currentDisplayLayout.transform.FindChild("PurchaseButton").FindChild("Cost").GetComponent<Text>().text = skin.GetComponent<GeneralItem>().cost.ToString();
            if (playerMoney < skin.GetComponent<GeneralItem>().cost)
                currentDisplayLayout.transform.FindChild("PurchaseButton").FindChild("Cost").GetComponent<Text>().color = insufficientRibbonColor;
            else
                currentDisplayLayout.transform.FindChild("PurchaseButton").FindChild("Cost").GetComponent<Text>().color = sufficientRibbonColor;

            currentDisplayLayout.transform.FindChild("EquipButton").gameObject.SetActive(false);
            currentDisplayLayout.transform.FindChild("EquippedButton").gameObject.SetActive(false);
        }
    }

    void UpdateSkinPurchased()
    {
        foreach(GameObject skin in skinList)
        {
             if (PlayerPrefs.GetInt("ppSkin" + skin.GetComponent<GeneralItem>().itemID + "Unlocked", 0) == 1)
             {
                 GameObject.Find("ItemSkin" + skin.GetComponent<GeneralItem>().itemID).transform.FindChild("UnlockedIcon").gameObject.SetActive(true);
             }
             else
                 GameObject.Find("ItemSkin" + skin.GetComponent<GeneralItem>().itemID).transform.FindChild("UnlockedIcon").gameObject.SetActive(false);

        }
    }

    GameObject DisplayLayoutType(ITEM_TYPE type)
    {
        itemDisplay.transform.FindChild("PowerupLayout").gameObject.SetActive(false);
        itemDisplay.transform.FindChild("WeaponLayout").gameObject.SetActive(false);
        itemDisplay.transform.FindChild("BarrierLayout").gameObject.SetActive(false);
        itemDisplay.transform.FindChild("SkinLayout").gameObject.SetActive(false);

        switch(type)
        {
            case ITEM_TYPE.TYPE_POWERUP:
                itemDisplay.transform.FindChild("PowerupLayout").gameObject.SetActive(true);
                currentDisplayLayout = itemDisplay.transform.FindChild("PowerupLayout").gameObject;
                break;
            case ITEM_TYPE.TYPE_WEAPON:
                itemDisplay.transform.FindChild("WeaponLayout").gameObject.SetActive(true);
                currentDisplayLayout = itemDisplay.transform.FindChild("WeaponLayout").gameObject;
                break;
            case ITEM_TYPE.TYPE_BARRIER:
                itemDisplay.transform.FindChild("BarrierLayout").gameObject.SetActive(true);
                currentDisplayLayout = itemDisplay.transform.FindChild("BarrierLayout").gameObject;
                break;
            case ITEM_TYPE.TYPE_SKIN:
                itemDisplay.transform.FindChild("SkinLayout").gameObject.SetActive(true);
                currentDisplayLayout = itemDisplay.transform.FindChild("SkinLayout").gameObject;
                break;
        }
        return currentDisplayLayout;
    }

    void UpdateButtonLevelDisplay(SHOP_ITEMS item)
    {
        switch (item)
        {
            case SHOP_ITEMS.ITEM_WEAPON_BLAST:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemBlastButton").GetComponent<ItemLevelDisplay>().UpdateItemLevelDisplay(PlayerPrefs.GetInt("pp" + weaponList[0].GetComponent<Weapon>().name + "Level", 1));
                break;
            case SHOP_ITEMS.ITEM_WEAPON_CRES:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemCresButton").GetComponent<ItemLevelDisplay>().UpdateItemLevelDisplay(PlayerPrefs.GetInt("pp" + weaponList[1].GetComponent<Weapon>().name + "Level", 0));
                break;
            case SHOP_ITEMS.ITEM_WEAPON_LASER:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemLaserButton").GetComponent<ItemLevelDisplay>().UpdateItemLevelDisplay(PlayerPrefs.GetInt("pp" + weaponList[2].GetComponent<Weapon>().name + "Level", 0));
                break;
            case SHOP_ITEMS.ITEM_WEAPON_PULSE:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemPulseButton").GetComponent<ItemLevelDisplay>().UpdateItemLevelDisplay(PlayerPrefs.GetInt("pp" + weaponList[3].GetComponent<Weapon>().name + "Level", 0));
                break;
            case SHOP_ITEMS.ITEM_WEAPON_BOOMERANG:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemBoomerangButton").GetComponent<ItemLevelDisplay>().UpdateItemLevelDisplay(PlayerPrefs.GetInt("pp" + weaponList[4].GetComponent<Weapon>().name + "Level", 0));
                break;
            case SHOP_ITEMS.ITEM_BARRIER_01:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemBarrier1").GetComponent<ItemLevelDisplay>().UpdateItemLevelDisplay(PlayerPrefs.GetInt("pp" + barrierList[0].GetComponent<Barrier>().name + "Level", 1));
                break;
            case SHOP_ITEMS.ITEM_BARRIER_02:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemBarrier2").GetComponent<ItemLevelDisplay>().UpdateItemLevelDisplay(PlayerPrefs.GetInt("pp" + barrierList[1].GetComponent<Barrier>().name + "Level", 0));
                break;
            case SHOP_ITEMS.ITEM_BARRIER_03:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemBarrier3").GetComponent<ItemLevelDisplay>().UpdateItemLevelDisplay(PlayerPrefs.GetInt("pp" + barrierList[2].GetComponent<Barrier>().name + "Level", 0));
                break;
            case SHOP_ITEMS.ITEM_BARRIER_04:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemBarrier4").GetComponent<ItemLevelDisplay>().UpdateItemLevelDisplay(PlayerPrefs.GetInt("pp" + barrierList[3].GetComponent<Barrier>().name + "Level", 0));
                break;
            case SHOP_ITEMS.ITEM_BARRIER_05:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemBarrier5").GetComponent<ItemLevelDisplay>().UpdateItemLevelDisplay(PlayerPrefs.GetInt("pp" + barrierList[4].GetComponent<Barrier>().name + "Level", 0));
                break;
            default:
                break;
        }

    }

    void UpdateButtonUnlockedDisplay(SHOP_ITEMS item)
    {
        switch (item)
        {
            case SHOP_ITEMS.ITEM_PLAYER_SKIN_02:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemSkin2").GetComponent<ItemUnlockedDisplay>().UpdateItemUnlockedDisplay(PlayerPrefs.GetInt("ppSkin02Unlocked") == 1);
                break;
            case SHOP_ITEMS.ITEM_PLAYER_SKIN_03:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemSkin3").GetComponent<ItemUnlockedDisplay>().UpdateItemUnlockedDisplay(PlayerPrefs.GetInt("ppSkin03Unlocked") == 1);
                break;
        }
    }

    void UpdateEquippedIconDisplay()
    {
        transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemBlastButton").FindChild("EquipIcon").gameObject.SetActive(false);
        transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemCresButton").FindChild("EquipIcon").gameObject.SetActive(false);
        transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemLaserButton").FindChild("EquipIcon").gameObject.SetActive(false);
        transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemPulseButton").FindChild("EquipIcon").gameObject.SetActive(false);
        transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemBoomerangButton").FindChild("EquipIcon").gameObject.SetActive(false);

        switch (PlayerPrefs.GetInt("ppCurrentWeapon", 1))
        {
            case 1:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemBlastButton").FindChild("EquipIcon").gameObject.SetActive(true);
                break;
            case 2:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemCresButton").FindChild("EquipIcon").gameObject.SetActive(true);
                break;
            case 3:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemLaserButton").FindChild("EquipIcon").gameObject.SetActive(true);
                break;
            case 4:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemPulseButton").FindChild("EquipIcon").gameObject.SetActive(true);
                break;
            case 5:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemBoomerangButton").FindChild("EquipIcon").gameObject.SetActive(true);
                break;
        }

        transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemBarrier1").FindChild("EquipIcon").gameObject.SetActive(false);
        transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemBarrier2").FindChild("EquipIcon").gameObject.SetActive(false);
        transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemBarrier3").FindChild("EquipIcon").gameObject.SetActive(false);
        transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemBarrier4").FindChild("EquipIcon").gameObject.SetActive(false);
        transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemBarrier5").FindChild("EquipIcon").gameObject.SetActive(false);

        switch (PlayerPrefs.GetInt("ppCurrentBarrier", 1))
        {
            case 1:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemBarrier1").FindChild("EquipIcon").gameObject.SetActive(true);
                break;
            case 2:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemBarrier2").FindChild("EquipIcon").gameObject.SetActive(true);
                break;
            case 3:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemBarrier3").FindChild("EquipIcon").gameObject.SetActive(true);
                break;
            case 4:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemBarrier4").FindChild("EquipIcon").gameObject.SetActive(true);
                break;
            case 5:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemBarrier5").FindChild("EquipIcon").gameObject.SetActive(true);
                break;
        }


        transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemSkin1").FindChild("EquipIcon").gameObject.SetActive(false);
        transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemSkin2").FindChild("EquipIcon").gameObject.SetActive(false);
        transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemSkin3").FindChild("EquipIcon").gameObject.SetActive(false);

        switch (PlayerPrefs.GetInt("ppCurrentSkin", 1))
        {
            case 1:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemSkin1").FindChild("EquipIcon").gameObject.SetActive(true);
                break;
            case 2:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemSkin2").FindChild("EquipIcon").gameObject.SetActive(true);
                break;
            case 3:
                transform.FindChild("ItemPanel").FindChild("Grid").FindChild("ItemSkin3").FindChild("EquipIcon").gameObject.SetActive(true);
                break;
        }

    }

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
}
