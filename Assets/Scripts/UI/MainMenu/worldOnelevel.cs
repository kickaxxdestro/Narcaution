using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class worldOnelevel : MonoBehaviour {

	public static int levelOneClear = 0;
	public static int levelTwoClear = 0;
	public static int levelThreeClear = 0;
	public static int levelBossClear = 0;
	
	Transform levelOne;
	Transform levelTwo;
	Transform levelThree;
	Transform boss;

	//Dot connectors
	Transform Connector1;
	Transform Connector2;
	Transform Connector3;

	//Line connectors
	Transform Line1_0;

	Transform Line2_0;
	
	Transform Line3_0;
	
	// Use this for initialization
	void Start () {
		
		levelOne = transform.FindChild("Level1");
		levelTwo = transform.FindChild("Level2");
		levelThree = transform.FindChild("Level3");	
		boss = transform.FindChild("Boss");

		//Connectors & lines
		Connector1 = transform.FindChild("(1-2)Connector");
		Connector2 = transform.FindChild("(2-3)Connector");
		Connector3 = transform.FindChild("(3-Boss)Connector");

		Line1_0 = transform.FindChild("(1-2)Line0");
		Line2_0 = transform.FindChild("(2-3)Line0");
		Line3_0 = transform.FindChild("(3-Boss)Line0");

		levelOneClear = PlayerPrefs.GetInt("ppWorld1Lv1");
		levelTwoClear = PlayerPrefs.GetInt("ppWorld1Lv2");
		levelThreeClear = PlayerPrefs.GetInt("ppWorld1Lv3");
		levelBossClear = PlayerPrefs.GetInt("ppWorld1Boss");
	}
	
	// Update is called once per frame
	void Update () {
		if(levelOneClear == 1)
		{
			Connector1.GetComponent<Button>().interactable = true;	
			Line1_0.GetComponent<Button>().interactable = true;

			//Level 2 button
			levelTwo.GetComponent<Button>().interactable = true;
		
		}
		if(levelTwoClear == 1)
		{
			Connector2.GetComponent<Button>().interactable = true;
			Line2_0.GetComponent<Button>().interactable = true;

			//Level 3 button
			levelThree.GetComponent<Button>().interactable = true;
		}
		/*if(levelThreeClear == 1)
		{
			RevSevenConnector.GetComponent<Button>().interactable = true;
		}*/
		if(levelThreeClear  == 1 && levelTwoClear == 1)
		{
			Connector3.GetComponent<Button>().interactable = true;	
			Line3_0.GetComponent<Button>().interactable = true;

			boss.GetComponent<Button>().interactable = true;
		}
		
	}

}