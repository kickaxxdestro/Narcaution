using UnityEngine;
using System.Collections;

public class LibraryIfSeenInfo : MonoBehaviour {

	public void CheckSeen(string EntryName)
	{
		if (!PlayerPrefs.HasKey (EntryName)) 
		{
			PlayerPrefs.SetInt (EntryName, 1);
			AchievementManager.instance ().IncreaseAchievementProgress ("Know your enemy");
		}
	}
}
