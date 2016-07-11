using UnityEngine;
using System.Collections;

public class ItemLevelDisplay : MonoBehaviour {

    void Awake()
    {
        transform.FindChild("UpgradeLevel1").gameObject.SetActive(false);
        transform.FindChild("UpgradeLevel2").gameObject.SetActive(false);
        transform.FindChild("UpgradeLevel3").gameObject.SetActive(false);
        transform.FindChild("UpgradeLevel4").gameObject.SetActive(false);
        transform.FindChild("UpgradeLevel5").gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateItemLevelDisplay(int level)
    {
        Reset();
        if (level == 0)
            return;
        else if (level <= 1)
            transform.FindChild("UpgradeLevel1").gameObject.SetActive(true);
        else if (level <= 2)
        {
            transform.FindChild("UpgradeLevel1").gameObject.SetActive(true);
            transform.FindChild("UpgradeLevel2").gameObject.SetActive(true);
        }
        else if (level <= 3)
        {
            transform.FindChild("UpgradeLevel1").gameObject.SetActive(true);
            transform.FindChild("UpgradeLevel2").gameObject.SetActive(true);
            transform.FindChild("UpgradeLevel3").gameObject.SetActive(true);
        }
        else if (level <= 4)
        {
            transform.FindChild("UpgradeLevel1").gameObject.SetActive(true);
            transform.FindChild("UpgradeLevel2").gameObject.SetActive(true);
            transform.FindChild("UpgradeLevel3").gameObject.SetActive(true);
            transform.FindChild("UpgradeLevel4").gameObject.SetActive(true);
        }
        else if (level <= 5)
        {
            transform.FindChild("UpgradeLevel1").gameObject.SetActive(true);
            transform.FindChild("UpgradeLevel2").gameObject.SetActive(true);
            transform.FindChild("UpgradeLevel3").gameObject.SetActive(true);
            transform.FindChild("UpgradeLevel4").gameObject.SetActive(true);
            transform.FindChild("UpgradeLevel5").gameObject.SetActive(true);
        }
    }

    void Reset()
    {
        transform.FindChild("UpgradeLevel1").gameObject.SetActive(false);
        transform.FindChild("UpgradeLevel2").gameObject.SetActive(false);
        transform.FindChild("UpgradeLevel3").gameObject.SetActive(false);
        transform.FindChild("UpgradeLevel4").gameObject.SetActive(false);
        transform.FindChild("UpgradeLevel5").gameObject.SetActive(false);
    }
}
