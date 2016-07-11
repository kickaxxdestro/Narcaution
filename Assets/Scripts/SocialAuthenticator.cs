using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class SocialAuthenticator : MonoBehaviour {
	
	void Start(){

#if UNITY_ANDROID
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
			// enables saving game progress
			//.EnableSavedGames()
			.Build();
		PlayGamesPlatform.InitializeInstance(config);
		//PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();

		((PlayGamesLocalUser)Social.localUser).GetStats((rc , stats) =>
		                                                {
			if(rc <= 0){
				Debug.Log("It has been " + stats.DaysSinceLastPlayed + " days");
			}
		});
#endif

#if UNITY_IOS
		UnityEngine.SocialPlatforms.GameCenter.GameCenterPlatform.ShowDefaultAchievementCompletionBanner (true);
#endif

		Social.localUser.Authenticate((bool success) => {
			//handle success or failure of signing in
			if (success)
			{
				Debug.Log("Signin Success");
				if(success)
				{
                    //if(PlayerPrefs.GetInt("ppFirstPlay") == 0)
                    //{
                    //    PlayerPrefs.SetInt("ppFirstPlay" , 1);
                    //}
				}
			}
			else
			{
				Debug.Log("Signin Failed");
			}
		});
		

		
	}
}