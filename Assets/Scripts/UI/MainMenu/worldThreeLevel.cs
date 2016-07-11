using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class worldThreeLevel : MonoBehaviour {
	
	public static int levelNineClear = 0;
	public static int levelTenClear = 0;
	public static int levelElevenClear = 0;
	public static int levelBossClear = 0;
	
	Transform levelNine;
	Transform levelTen;
	Transform levelEleven;
	Transform boss;
	
	Transform connector;
	Transform BotConnector;
	Transform MidConnector;

	Transform SlashLine;
	Transform LLine;
	Transform RevLLine;
	
	// Use this for initialization
	void Start () {
		
		levelNine = transform.FindChild("Level9");
		levelTen = transform.FindChild("Level10");
		levelEleven = transform.FindChild("Level11");	
		boss = transform.FindChild("Boss");

		connector = transform.FindChild("Connector");
		BotConnector = transform.FindChild("BotConnector");
		MidConnector = transform.FindChild("MidConnector");
		SlashLine = transform.FindChild("SlashLine");
		LLine = transform.FindChild("LLine");
		RevLLine = transform.FindChild("RevLLine");
		
		levelNineClear = PlayerPrefs.GetInt("ppWorld3Lv1");
		levelTenClear = PlayerPrefs.GetInt("ppWorld3Lv2");
		levelElevenClear = PlayerPrefs.GetInt("ppWorld3Lv3");
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(levelNineClear == 1)
		{
			LLine.GetComponent<Button>().interactable = true;	
			BotConnector.GetComponent<Button>().interactable = true;
			levelTen.GetComponent<Button>().interactable = true;
		
		}
		if(levelTenClear == 1)
		{
			SlashLine.GetComponent<Button>().interactable = true;
			MidConnector.GetComponent<Button>().interactable = true;
			levelEleven.GetComponent<Button>().interactable = true;
		}
		if(levelElevenClear == 1)
		{
			RevLLine.GetComponent<Button>().interactable = true;
			connector.GetComponent<Button>().interactable = true;
			boss.GetComponent<Button>().interactable = true;
		}	
	}
}