using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MedalInfoDisplayHandler : MonoBehaviour {

	public enum DisplayType
	{
		T_ClearLevel,
		T_FullHealth,
		T_TargetScore
	};

	int targetScore = 0;

	public GameObject InfoPanel;

	void Start()
	{
		InfoPanel.SetActive (false);
	}

	public void SetTargetScore(LevelGeneratorScript level)
	{
		targetScore = level.scoreBonusAmt;
	}

	public void EnableDisplayInfo(int T)
	{
		if(!InfoPanel.activeInHierarchy)
		{
			InfoPanel.SetActive (true);
			switch ((DisplayType)T) 
			{
			case DisplayType.T_ClearLevel:
				InfoPanel.GetComponentInChildren<Text> ().text = "Complete this level to unlock";
				break;
			case DisplayType.T_FullHealth:
				InfoPanel.GetComponentInChildren<Text> ().text = "No damage taken to unlock";
				break;
			case DisplayType.T_TargetScore:
				InfoPanel.GetComponentInChildren<Text> ().text = "Achieve " + targetScore + " points to unlock";
				break;
			}
		}
	}

	public void DisableDisplayInfo()
	{
		InfoPanel.SetActive (false);
	}

}
