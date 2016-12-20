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

	float timmer = 0;
	void Update()
	{
		if (DisplayAchievementUnlockPanel.GetComponent<VerticalLayoutGroup> ().padding.top < 0) 
		{
			timmer += Time.unscaledDeltaTime * 200;
			if (timmer >= 1) 
			{
				timmer = 0;
				DisplayAchievementUnlockPanel.GetComponent<VerticalLayoutGroup> ().padding.top += 5;
				LayoutRebuilder.MarkLayoutForRebuild(DisplayAchievementUnlockPanel.GetComponent<RectTransform>());
			}
		}
	}

	void GenerateAllAchievements()
	{
		allAchievements = new List<AchievementInfo> ();
		allAchievements.Add(new AchievementInfo("That's Cold", AchievementInfo.Category.C_General, "Clear W-1 stages", 0, 1, false));
		allAchievements.Add(new AchievementInfo("Blaze It", AchievementInfo.Category.C_General, "Clear W-2 stages", 0, 1, false));
		allAchievements.Add(new AchievementInfo("Fresh Air", AchievementInfo.Category.C_General, "Clear W-3 stages", 0, 1, false));
		allAchievements.Add(new AchievementInfo("High No More", AchievementInfo.Category.C_General, "Clear W-4 stages", 0, 1, false));
		allAchievements.Add(new AchievementInfo("Acid Trip", AchievementInfo.Category.C_General, "Clear W-5 stages", 0, 1, false));
		allAchievements.Add(new AchievementInfo("It's Over", AchievementInfo.Category.C_General, "Clear Final stage", 0, 1, false));
		allAchievements.Add(new AchievementInfo("Medal Lover", AchievementInfo.Category.C_General, "Obtain 21 medals in total", 0, 21, false));
		allAchievements.Add(new AchievementInfo("Medal Collector", AchievementInfo.Category.C_General, "Obtain 42 medals in total", 0, 42, false));
		allAchievements.Add(new AchievementInfo("Medal Maniac", AchievementInfo.Category.C_General, "Obtain all medals in total", 0, 63, false));
		allAchievements.Add(new AchievementInfo("That's New", AchievementInfo.Category.C_General, "Buy a new weapon", 0, 1, false));
		allAchievements.Add(new AchievementInfo("Weapon Hoarder", AchievementInfo.Category.C_General, "Buy all Weapon", 0, 4, false));
		allAchievements.Add(new AchievementInfo("Over Powered", AchievementInfo.Category.C_General, "Fully upgrade one weapon", 0, 1, false));
		allAchievements.Add(new AchievementInfo("Nerf Pls", AchievementInfo.Category.C_General, "Fully upgrade all weapon", 0, 5, false));
		allAchievements.Add(new AchievementInfo("Need a new look", AchievementInfo.Category.C_General, "Buy a new skin", 0, 1, false));
		allAchievements.Add(new AchievementInfo("Fashionista", AchievementInfo.Category.C_General, "Get all the Skins", 0, 3, false));

		allAchievements.Add(new AchievementInfo("Drug Stoper", AchievementInfo.Category.C_Battle, "Defeat 50 enemies in total", 0, 50, false));
		allAchievements.Add(new AchievementInfo("Drug Hunter", AchievementInfo.Category.C_Battle, "Defeat 250 enemies in total", 0, 250, false));
		allAchievements.Add(new AchievementInfo("Drug Buster", AchievementInfo.Category.C_Battle, "Defeat 500 enemies in total", 0, 500, false));
		allAchievements.Add(new AchievementInfo("Drug Destroyer", AchievementInfo.Category.C_Battle, "Defeat 1000 enemies in total", 0, 1000, false));
		allAchievements.Add(new AchievementInfo("Have It Back", AchievementInfo.Category.C_Battle, "Defeat 20 enemies by deflecting", 0, 20, false));
		allAchievements.Add(new AchievementInfo("Taste of your own medicine", AchievementInfo.Category.C_Battle, "Defeat 50 enemies by deflecting", 0, 50, false));
		allAchievements.Add(new AchievementInfo("Deflector", AchievementInfo.Category.C_Battle, "Defeat 100 enemies by deflecting", 0, 100, false));
		allAchievements.Add(new AchievementInfo("Savior's Fall", AchievementInfo.Category.C_Battle, "Die once", 0, 1, false));
		allAchievements.Add(new AchievementInfo("Stubborn Hero", AchievementInfo.Category.C_Battle, "Die 15 times", 0, 15, false));

		allAchievements.Add(new AchievementInfo("Pacifist", AchievementInfo.Category.C_Special, "Clear stage without defeating any enemies", 0, 1, false));
		allAchievements.Add(new AchievementInfo("Know your enemy", AchievementInfo.Category.C_Special, "View infomation of 10 different drugs in the library", 0, 10, false));
		allAchievements.Add(new AchievementInfo("Sharer", AchievementInfo.Category.C_Special, "Share game on facebook", 0, 1, false));
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
						go.GetComponent<AchievementTab> ().SetInfo(achievement.name);
						go.transform.SetAsFirstSibling ();
						DisplayAchievementUnlockPanel.GetComponent<VerticalLayoutGroup> ().padding.top -= 140;
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
						go.GetComponent<AchievementTab> ().SetInfo(achievement.name);
						go.transform.SetAsFirstSibling ();
						DisplayAchievementUnlockPanel.GetComponent<VerticalLayoutGroup> ().padding.top -= 140;
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
