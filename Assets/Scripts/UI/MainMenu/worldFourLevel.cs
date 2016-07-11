using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class worldFourLevel : MonoBehaviour {
	
	public static int levelThirteenClear = 0;
	public static int levelFourteenClear = 0;
	public static int levelFifteenClear = 0;
	public static int levelBossClear = 0;
	
	Transform levelThirteen;
	Transform levelFourteen;
	Transform levelFifteen;
	Transform boss;
	
	Transform connector;
	Transform BotConnector;
	Transform MidConnector;

	Transform SlashLine;
	Transform LLine;
	Transform RevLLine;
	
	// Use this for initialization
	void Start () {
		
		levelThirteen = transform.FindChild("Level13");
		levelFourteen = transform.FindChild("Level14");
		levelFifteen = transform.FindChild("Level15");	
		boss = transform.FindChild("Boss");

		connector = transform.FindChild("Connector");
		BotConnector = transform.FindChild("BotConnector");
		MidConnector = transform.FindChild("MidConnector");
		SlashLine = transform.FindChild("SlashLine");
		LLine = transform.FindChild("LLine");
		RevLLine = transform.FindChild("RevLLine");
		
		levelThirteenClear = PlayerPrefs.GetInt("ppWorld4Lv1");
		levelFourteenClear = PlayerPrefs.GetInt("ppWorld4Lv2");
		levelFifteenClear = PlayerPrefs.GetInt("ppWorld4Lv3");
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(levelThirteenClear == 1)
		{
			LLine.GetComponent<Button>().interactable = true;	
			BotConnector.GetComponent<Button>().interactable = true;
			levelFourteen.GetComponent<Button>().interactable = true;
		
		}
		if(levelFourteenClear == 1)
		{
			SlashLine.GetComponent<Button>().interactable = true;
			MidConnector.GetComponent<Button>().interactable = true;
			levelFifteen.GetComponent<Button>().interactable = true;
		}
		if(levelFifteenClear == 1)
		{
			RevLLine.GetComponent<Button>().interactable = true;
			connector.GetComponent<Button>().interactable = true;
			boss.GetComponent<Button>().interactable = true;
		}	
	}
}