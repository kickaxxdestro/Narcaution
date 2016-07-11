using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class optionGoogle : MonoBehaviour {

	public void showAchievement ()
	{
		Social.ShowAchievementsUI();
	}
	public void showLeaderBoard()
	{
		Social.ShowLeaderboardUI();
	}
}
