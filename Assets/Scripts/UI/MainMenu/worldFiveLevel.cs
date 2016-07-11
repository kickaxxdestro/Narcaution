using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class worldFiveLevel : MonoBehaviour {
	
	public static int levelSeventeenClear = 0;
	public static int levelEighteenClear = 0;
	public static int levelNineteenClear = 0;
	public static int levelBoss1Clear = 0;
	public static int levelBoss2Clear = 0;
	
	Transform levelSeventeen;
	Transform levelEighteen;
	Transform levelNineteen;
	Transform boss1;
	Transform boss2;
	Transform boss3;
	
	Transform Connector1718;
	Transform ConnectorSecretBossBoss;
	
	Transform Line1718;
	Transform Line1819;
	Transform Line19SecretBoss;
	Transform LineSecretBossBoss;
	
	// Use this for initialization
	void Start () {
		
		levelSeventeen = transform.FindChild("Level17");
		levelEighteen = transform.FindChild("Level18");
		levelNineteen = transform.FindChild("Level19");	
		boss1 = transform.FindChild("SecretBoss");
		boss2 = transform.FindChild("Boss");
		boss3 = transform.FindChild("FinalBoss");
		
		Connector1718 = transform.FindChild("(17-18)Connector");
		ConnectorSecretBossBoss = transform.FindChild("(SecretBoss-Boss)Connector");
		Line1718 = transform.FindChild("(17-18)Line0");
		Line1819 = transform.FindChild("(18-19)Line0");
		Line19SecretBoss = transform.FindChild("(19-SecretBoss)Line0");
		LineSecretBossBoss = transform.FindChild("(SecretBoss-Boss)Line0");
		
		levelSeventeenClear = PlayerPrefs.GetInt("ppWorld5Lv1");
		levelEighteenClear = PlayerPrefs.GetInt("ppWorld5Lv2");
		levelNineteenClear = PlayerPrefs.GetInt("ppWorld5Lv3");
		levelBoss1Clear = PlayerPrefs.GetInt("ppWorld5Boss1");
		levelBoss2Clear = PlayerPrefs.GetInt("ppWorld5Boss2");
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(levelSeventeenClear == 1)
		{
			Line1718.GetComponent<Button>().interactable = true;	
			Connector1718.GetComponent<Button>().interactable = true;
			levelEighteen.GetComponent<Button>().interactable = true;
		
		}
		if(levelEighteenClear == 1)
		{
			Line1819.GetComponent<Button>().interactable = true;
			levelNineteen.GetComponent<Button>().interactable = true;
		}
		if(levelNineteenClear == 1)
		{
			Line19SecretBoss.GetComponent<Button>().interactable = true;
			boss1.GetComponent<Button>().interactable = true;
		}	
		if(levelBoss1Clear == 1)
		{
			LineSecretBossBoss.GetComponent<Button>().interactable = true;
			ConnectorSecretBossBoss.GetComponent<Button>().interactable = true;
			boss2.GetComponent<Button>().interactable = true;
		}	
		if(levelBoss2Clear == 1)
		{
			boss3.GetComponent<Button>().interactable = true;
		}	
	}
}