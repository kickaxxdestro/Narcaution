using UnityEngine;
using System.Collections;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GooglePlay : MonoBehaviour {
	/*
	void Start(){
		
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
			// enables saving game progress
			//.EnableSavedGames()
			.Build();
		PlayGamesPlatform.InitializeInstance(config);
		//PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();
		
		
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
		
		((PlayGamesLocalUser)Social.localUser).GetStats((rc , stats) =>
		                                                {
			if(rc <= 0){
				Debug.Log("It has been " + stats.DaysSinceLastPlayed + " days");
			}
		});
		
	}*/
}