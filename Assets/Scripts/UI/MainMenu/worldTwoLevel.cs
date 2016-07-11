using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class worldTwoLevel : MonoBehaviour {
	
	public static int levelFiveClear = 0;
	public static int levelSixClear = 0;
	public static int levelSevenClear = 0;
	public static int levelBossClear = 0;
	
	Transform levelFive;
	Transform levelSix;
	Transform levelSeven;
	Transform boss;
	
	//Dot connectors
	Transform Connector1;
	Transform Connector2;
	Transform Connector3;
	
	//Line connectors
	Transform Line1_0;
	Transform Line1_1;
	
	Transform Line2_0;
	
	Transform Line3_0;
	
	// Use this for initialization
	void Start () {
		
		levelFive = transform.FindChild("Level5");
		levelSix = transform.FindChild("Level6");
		levelSeven = transform.FindChild("Level7");	
		boss = transform.FindChild("Boss");
		
		//Connectors & lines
		Connector1 = transform.FindChild("(5-6)Connector");
		Connector2 = transform.FindChild("(6-7)Connector");
		Connector3 = transform.FindChild("(7-Boss)Connector");
		
		Line1_0 = transform.FindChild("(5-6)Line0");
		Line2_0 = transform.FindChild("(6-7)Line0");
		Line3_0 = transform.FindChild("(7-Boss)Line0");
		
		levelFiveClear = PlayerPrefs.GetInt("ppWorld2Lv1");
		levelSixClear = PlayerPrefs.GetInt("ppWorld2Lv2");
		levelSevenClear = PlayerPrefs.GetInt("ppWorld2Lv3");
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(levelFiveClear == 1)
		{
			Line1_0.GetComponent<Button>().interactable = true;	
			Connector1.GetComponent<Button>().interactable = true;

			levelSix.GetComponent<Button>().interactable = true;
		
		}
		if(levelSixClear == 1)
		{
			Line2_0.GetComponent<Button>().interactable = true;
			Connector2.GetComponent<Button>().interactable = true;

			levelSeven.GetComponent<Button>().interactable = true;
		}
		/*if(levelSevenClear == 1)
		{
			RevSevenConnector.GetComponent<Button>().interactable = true;
		}*/
		if(/*levelSixClear  == 1 &&*/ levelSevenClear == 1)
		{
			Line3_0.GetComponent<Button>().interactable = true;	
			Connector3.GetComponent<Button>().interactable = true;

			boss.GetComponent<Button>().interactable = true;
		}	
	}
}