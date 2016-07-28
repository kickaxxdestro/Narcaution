using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipmentLoader : MonoBehaviour {

    public GameObject[] playerSkins;
    public GameObject[] weapons;
    public GameObject[] barriers;

    GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null)
        {
            print("Player not found");
            Destroy(gameObject);
            return;
        }

        //Load current skin
        foreach (GameObject go in playerSkins)
        {
            if (go.GetComponent<GeneralItem>().itemID == PlayerPrefs.GetInt("ppCurrentSkin", 1))
            {
                player.GetComponent<Animator>().runtimeAnimatorController = go.GetComponent<Animator>().runtimeAnimatorController;
            }
        }

        //Defend mode
        if (PlayerPrefs.GetInt("ppPlayerGamemode", 0) == 1)
        {
            GameObject currentBarrier = null;
            foreach (GameObject go in barriers)
            {
                if (go.GetComponent<Barrier>().ID == PlayerPrefs.GetInt("ppCurrentBarrier", 1))
                {
                    currentBarrier = Instantiate(go) as GameObject;
                    player.GetComponent<PlayerController>().barrier = currentBarrier.GetComponent<Barrier>();
                    currentBarrier.transform.SetParent(player.transform, false);
                    //currentBarrier.GetComponent<Barrier>().barrierDisplay = GameObject.Find("BarrierDisplay").GetComponent<Image>();
                    //currentBarrier.GetComponent<Barrier>().barrierMinDisplay = GameObject.Find("BarrierMinDisplay").GetComponent<Image>();
                    //currentBarrier.GetComponent<Barrier>().barrierButton = GameObject.Find("BarrierButton");
                    break;
                }
            }
        }
        //Attack mode
        else
        {
            GameObject currentWeapon = null;
            foreach(GameObject go in weapons)
            {
                if (go.GetComponent<Weapon>().ID == PlayerPrefs.GetInt("ppCurrentWeapon", 1))
                {
                    currentWeapon = Instantiate(go) as GameObject;
                    player.GetComponent<PlayerController>().weapon = currentWeapon.GetComponent<Weapon>();
                    currentWeapon.transform.SetParent(player.transform, false);
                    break;
                }
            }

            if(currentWeapon == null)
                print("Weapon not found");

            Destroy(GameObject.Find("BarrierPanel"));
        }

        //Destroy after loading skins
        Destroy(gameObject);
	}
}
