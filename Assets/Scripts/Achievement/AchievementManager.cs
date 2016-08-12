using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour {

	public AchievementTab achievementTab;

	public GameObject DisplayAchievementUnlockPanel;

	List<AchievementInfo> allAchievements;
	public List<AchievementInfo> getAchievementList()
	{
		return allAchievements;
	}

	string dataPath;

	static AchievementManager _Instance;

	public static AchievementManager instance()
	{
		return _Instance;
	}

	void Awake ()
	{
		if (_Instance == null)
			_Instance = this;
		else
			Destroy (this.gameObject);

		DontDestroyOnLoad (gameObject);

		dataPath = Path.Combine (Application.persistentDataPath, ("achievement.json"));

		if (!File.Exists (dataPath))
		{
			GenerateAllAchievements ();

			File.Create (dataPath).Close ();

			SaveAchievementToJSON ();
		} 
		else 
		{
			JSONObject tempJ = JSONObject.Create (File.ReadAllText (dataPath));

			allAchievements = new List<AchievementInfo> ();

			foreach (string achievementName in tempJ.keys) 
			{
				if (achievementName != "Rank" && achievementName != "Points") 
				{
					string name = "";
					name = achievementName;

					int category = 0;
					tempJ.GetField (achievementName).GetField (ref category, "Category");

					string description = "";
					tempJ.GetField (achievementName).GetField (ref description, "Description");

					int currentProgress = 0;
					tempJ.GetField (achievementName).GetField (ref currentProgress, "CurentProgress");

					int progressNeeded = 0;
					tempJ.GetField (achievementName).GetField (ref progressNeeded, "ProgressNeeded");

					bool unlocked = false;
					tempJ.GetField (achievementName).GetField (ref unlocked, "Unocked");

					allAchievements.Add (new AchievementInfo (name, (AchievementInfo.Category)category, description, currentProgress, progressNeeded, unlocked));
				}
			}
		}
	}

	void GenerateAllAchievements()
	{
		allAchievements = new List<AchievementInfo> ();
		allAchievements.Add(new AchievementInfo("Comming Soon!", AchievementInfo.Category.C_General, "", 0, 1, false));
		allAchievements.Add(new AchievementInfo("Comming Soon!!", AchievementInfo.Category.C_Special, "", 0, 1, false));
		allAchievements.Add(new AchievementInfo("Drug Stoper", AchievementInfo.Category.C_Battle, "Defeat 50 enemies in total", 0, 50, false));
		allAchievements.Add(new AchievementInfo("Drug Hunter", AchievementInfo.Category.C_Battle, "Defeat 250 enemies in total", 0, 250, false));
		allAchievements.Add(new AchievementInfo("Drug Buster", AchievementInfo.Category.C_Battle, "Defeat 500 enemies in total", 0, 500, false));
		allAchievements.Add(new AchievementInfo("Drug Destroyer", AchievementInfo.Category.C_Battle, "Defeat 1000 enemies in total", 0, 1000, false));
		allAchievements.Add(new AchievementInfo("More Comming Soon!", AchievementInfo.Category.C_Battle, "", 0, 1, false));
	}

	void SaveAchievementToJSON()
	{
		JSONObject tempJ = new JSONObject (JSONObject.Type.OBJECT);

		foreach (AchievementInfo achievement in allAchievements) {
			tempJ.AddField (achievement.name, CreateAchievementJSON (achievement.description, achievement.category, achievement.currentProgress, achievement.progressNeeded, achievement.unlocked));
		}

		File.WriteAllText (dataPath, tempJ.ToString ());
	}

	JSONObject CreateAchievementJSON(string description, AchievementInfo.Category category, int currentProgress, int progressNeeded, bool unlocked)
	{
		JSONObject j = new JSONObject (JSONObject.Type.OBJECT);
		j.AddField ("Description", description);
		j.AddField ("Category", (int)category);
		j.AddField ("CurentProgress", currentProgress);
		j.AddField ("ProgressNeeded", progressNeeded);
		j.AddField ("Unocked", unlocked);

		return j;
	}

	public void IncreaseAchievementProgress(string achievementName)
	{
		instance().StartCoroutine(instance().IncreaseAchievementProgressCoroutine (achievementName));
	}

	IEnumerator IncreaseAchievementProgressCoroutine (string achievementName)
	{
		foreach (AchievementInfo achievement in allAchievements) 
		{
			if (achievementName == achievement.name) 
			{
				if (!achievement.unlocked) 
				{
					achievement.currentProgress ++;
					if (achievement.currentProgress >= achievement.progressNeeded) 
					{
						achievement.currentProgress = achievement.progressNeeded;
						achievement.unlocked = true;

						GameObject go = Instantiate (achievementTab).gameObject;
						go.transform.SetParent (DisplayAchievementUnlockPanel.transform);
						go.transform.localScale = Vector3.one;
						go.GetComponentInChildren<AchievementTab> ().SetInfo(achievement.name);
					}
					SaveAchievementToJSON ();
				}
				break;
			}
		}
		yield return null;
	}

	public void SetAchievementProgress(string achievementName, int num)
	{
		instance().StartCoroutine(instance().IncreaseAchievementProgressCoroutine (achievementName, num));
	}

	IEnumerator IncreaseAchievementProgressCoroutine (string achievementName, int num)
	{
		foreach (AchievementInfo achievement in allAchievements) 
		{
			if (achievementName == achievement.name) 
			{
				if (!achievement.unlocked) 
				{
					achievement.currentProgress = num;
					if (achievement.currentProgress >= achievement.progressNeeded) 
					{
						achievement.currentProgress = achievement.progressNeeded;
						achievement.unlocked = true;

						GameObject go = Instantiate (achievementTab).gameObject;
						go.transform.SetParent (DisplayAchievementUnlockPanel.transform);
						go.transform.localScale = Vector3.one;
						go.GetComponentInChildren<AchievementTab> ().SetInfo(achievement.name);
					}
					SaveAchievementToJSON ();
				}
				break;
			}
		}
		yield return null;
	}

	public void reset()
	{
		if (File.Exists (dataPath))
			File.Delete (dataPath);
		_Instance = null;
		Awake ();
	}
}
