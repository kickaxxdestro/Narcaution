using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NewGalleryScript : MonoBehaviour {

	public static int levelOneClear = 0;		//Crack
	public static int levelThreeClear = 0;		//Heroin
	public static int levelFourClear = 0;		//Ice
	public static int levelFiveClear = 0;		//BZP
	public static int levelSixClear = 0;		//Ketamine
	public static int levelEightClear = 0;		//Cannabis
	public static int levelTenClear = 0;		//Nimetazepam
	public static int levelTwelveClear = 0;		//Inhalant

	public static int levelThirteenClear = 0;	//Buprenorphine
	public static int levelSixteenClear = 0;	//Ecstacy

	public static int levelSeventeenClear = 0;	//M
	public static int levelTwentyClear = 0;		//LSD
	public static int levelTwentyOneClear = 0;	//NPS

	
	public int DrugNumber; //in order of appearance
	public GameObject myPrev;
	public GameObject myNext;



	// Use this for initialization
	void Start () {
	
		levelOneClear = PlayerPrefs.GetInt("ppWorld1Lv1");
		levelThreeClear = PlayerPrefs.GetInt("ppWorld1Lv2");
		levelFourClear = PlayerPrefs.GetInt("ppWorld1Boss");

		levelFiveClear = PlayerPrefs.GetInt("ppWorld2Lv1");
		levelSixClear = PlayerPrefs.GetInt("ppWorld2Lv2");
		levelEightClear = PlayerPrefs.GetInt("ppWorld2Boss");

		levelTenClear = PlayerPrefs.GetInt("ppWorld3Lv2");
		levelTwelveClear = PlayerPrefs.GetInt("ppWorld3Lv4");

		levelThirteenClear = PlayerPrefs.GetInt("ppWorld4Lv1");
		levelSixteenClear = PlayerPrefs.GetInt("ppWorld4Boss");

		levelSeventeenClear = PlayerPrefs.GetInt("ppWorld5Lv1");
		levelTwentyClear = PlayerPrefs.GetInt("ppWorld5Boss1");
		levelTwentyOneClear = PlayerPrefs.GetInt("ppWorld5Boss2");

	}

	// Update is called once per frame
	void Update () {

		switch (DrugNumber) {

		case 1:
			GetComponent<Button>().interactable = false;
			if(levelOneClear == 1)
			{
				GetComponent<Button>().interactable = true;
			}
			break;

		case 2:
			GetComponent<Button>().interactable = false;
			if(levelThreeClear == 1)
			{
				GetComponent<Button>().interactable = true;
			}
			break;

		case 3:
			GetComponent<Button>().interactable = false;
			if(levelFourClear == 1)
			{
				GetComponent<Button>().interactable = true;
			}
			break;

		case 4:
			GetComponent<Button>().interactable = false;
			if(levelFiveClear == 1)
			{
				GetComponent<Button>().interactable = true;
			}
			break;

		case 5:
			GetComponent<Button>().interactable = false;
			if(levelSixClear == 1)
			{
				GetComponent<Button>().interactable = true;
			}
			break;
			
		case 6:
			GetComponent<Button>().interactable = false;
			if(levelEightClear == 1)
			{
				GetComponent<Button>().interactable = true;
			}
			break;
			
		case 7:
			GetComponent<Button>().interactable = false;
			if(levelTenClear == 1)
			{
				GetComponent<Button>().interactable = true;
			}
			break;
			
		case 8:
			GetComponent<Button>().interactable = false;
			if(levelTwelveClear == 1)
			{
				GetComponent<Button>().interactable = true;
			}
			break;

		case 9:
			GetComponent<Button>().interactable = false;
			if(levelThirteenClear == 1)
			{
				GetComponent<Button>().interactable = true;
			}
			break;
		
		case 10:
			GetComponent<Button>().interactable = false;
			if(levelSixteenClear == 1)
			{
				GetComponent<Button>().interactable = true;
			}
			break;
		
		case 11:
			GetComponent<Button>().interactable = false;
			if(levelSeventeenClear == 1)
			{
				GetComponent<Button>().interactable = true;
			}
			break;
		
		case 12:
			GetComponent<Button>().interactable = false;
			if(levelTwentyClear == 1)
			{
				GetComponent<Button>().interactable = true;
			}
			break;

		case 13:
			GetComponent<Button>().interactable = false;
			if(levelTwentyOneClear == 1)
			{
				GetComponent<Button>().interactable = true;
			}
			break;
	}

}
}