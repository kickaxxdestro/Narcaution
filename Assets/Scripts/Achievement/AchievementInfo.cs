using UnityEngine;
using System.Collections;

public class AchievementInfo {

	public enum Category
	{
		C_General,
		C_Battle,
		C_Special
	};

	public string name;
	public Category category;
	public string description;
	public int currentProgress;
	public int progressNeeded;
	public bool unlocked;

	public AchievementInfo (string name, Category category, string description, int currentProgress, int progressNeeded, bool unlocked)
	{
		this.name = name;
		this.category = category;
		this.description = description;
		this.currentProgress = currentProgress;
		this.progressNeeded = progressNeeded;
		this.unlocked = unlocked;
	}
}
