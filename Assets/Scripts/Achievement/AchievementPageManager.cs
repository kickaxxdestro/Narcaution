using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchievementPageManager : MonoBehaviour {
	public GameObject AchievementButton;

	public GameObject GeneralTab;
	public GameObject BattleTab;
	public GameObject SpecialTab;

	// Use this for initialization
	void Start () {
		foreach (AchievementInfo achievement in AchievementManager.instance().getAchievementList()) 
		{
			GameObject temp = Instantiate (AchievementButton).gameObject;
			temp.transform.Find ("Name").gameObject.GetComponent<Text> ().text = achievement.name;
			temp.transform.Find ("Description").gameObject.GetComponent<Text> ().text = achievement.description;
			temp.transform.Find ("Progress").gameObject.GetComponent<Text> ().text = achievement.currentProgress + " / " + achievement.progressNeeded;
			temp.transform.Find ("Filled").gameObject.GetComponent<Image> ().fillAmount = (float)achievement.currentProgress / (float)achievement.progressNeeded;
			//if(achievement.unlocked)
			//	temp.transform.Find ("Unlocked").gameObject.SetActive(true);

			//temp.GetComponent<Button>().onClick.AddListener(() => OnButtonPress(temp.GetComponent<Button>()));

			switch (achievement.category) 
			{
			case AchievementInfo.Category.C_General:
				temp.transform.SetParent (GeneralTab.transform);
				break;
			case AchievementInfo.Category.C_Battle:
				temp.transform.SetParent (BattleTab.transform);
				break;
			case AchievementInfo.Category.C_Special:
				temp.transform.SetParent (SpecialTab.transform);
				break;
			}

			temp.transform.localScale = Vector3.one;
		}
	}
}
