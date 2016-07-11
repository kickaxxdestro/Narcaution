using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class LevelLoader : MonoBehaviour {
	
	public GameObject levelEndMenu;
    public Button pauseButton;
	Text menuScore;
	public Transform grade;

    public GameObject[] rankIndicator;
    public Sprite rankIconCompleteActive;
    public Sprite rankIconHealthActive;
    public Sprite rankIconScoreActive;
	
	//leaderboard
	[System.NonSerialized]
	public int worldOneHighScore;
	public int worldTwoHighScore;
	public int worldThreeHighScore;
	public int worldFourHighScore;
	public int worldFiveHighScore;
	
	//public GameObject[] iceBG;
	//public GameObject[] weedBG;
	//public GameObject[] inhBG;
	//public GameObject[] ecstacyBG;
	//public GameObject[] lsdBG;
	
	//world 1
	public GameObject level1_1;
	public GameObject level1_2;
	public GameObject level1_3;
	public GameObject level1_4;
	//world 2
	public GameObject level2_1;
	public GameObject level2_2;
	public GameObject level2_3;
	public GameObject level2_4;
	//world 3
	public GameObject level3_1;
	public GameObject level3_2;
	public GameObject level3_3;
	public GameObject level3_4;
	//world 4
	public GameObject level4_1;
	public GameObject level4_2;
	public GameObject level4_3;
	public GameObject level4_4;
	//world 5
	public GameObject level5_1;
	public GameObject level5_2;
	public GameObject level5_3;
	public GameObject level5_4;
	public GameObject level5_5;
	public GameObject level5_6;
	
	public Sprite gradeS;
	public Sprite gradeA;
	public Sprite gradeB;
	public Sprite gradeC;
	public Sprite gradeD;
	public Sprite gradeE;
	public Sprite gradeF;
	public Sprite dash;

    public Text levelDisplay;
	
	GameObject[] enemyObjects;

	float timer;
	[System.NonSerialized]
	public GameObject lastSpawned;
	public int loadedLevel = 0;

    bool scoringDone = false;
	
	// Use this for initialization
	void Awake () {
		timer = 2f;

		worldOneHighScore = PlayerPrefs.GetInt("ppLevel1HighScore") + PlayerPrefs.GetInt("ppLevel2HighScore") + PlayerPrefs.GetInt("ppLevel3HighScore") + PlayerPrefs.GetInt("ppLevel4HighScore");
		worldTwoHighScore = PlayerPrefs.GetInt("ppLevel5HighScore") + PlayerPrefs.GetInt("ppLevel6HighScore") + PlayerPrefs.GetInt("ppLevel7HighScore") + PlayerPrefs.GetInt("ppLevel8HighScore");
		worldThreeHighScore = PlayerPrefs.GetInt("ppLevel9HighScore") + PlayerPrefs.GetInt("ppLevel10HighScore") + PlayerPrefs.GetInt("ppLevel11HighScore") + PlayerPrefs.GetInt("ppLevel12HighScore");
		worldFourHighScore = PlayerPrefs.GetInt("ppLevel13HighScore") + PlayerPrefs.GetInt("ppLevel14HighScore") + PlayerPrefs.GetInt("ppLevel15HighScore") + PlayerPrefs.GetInt("ppLevel16HighScore");
		worldFiveHighScore = PlayerPrefs.GetInt("ppLevel17HighScore") + PlayerPrefs.GetInt("ppLevel18HighScore") + PlayerPrefs.GetInt("ppLevel19HighScore") + PlayerPrefs.GetInt("ppLevel20HighScore") + PlayerPrefs.GetInt("ppLevel21HighScore") + PlayerPrefs.GetInt("ppLevel22HighScore");
		
		menuScore = levelEndMenu.transform.FindChild("totalScore").FindChild("Text").GetComponent<Text>();
		//grade = levelEndMenu.transform.FindChild("GradeBg").FindChild("grade");
        loadedLevel = PlayerPrefs.GetInt("ppSelectedLevel", 1);

        if(levelDisplay)
        {
            if (loadedLevel == 21)
                levelDisplay.text = "5-5";
            else
                levelDisplay.text = (((loadedLevel - 1) / 4) + 1) + "-" + (loadedLevel - (((loadedLevel - 1) / 4) * 4)).ToString();
        }

        switch (loadedLevel)
		{
		case 1:
			lastSpawned = Instantiate(level1_1) as GameObject;
			break;
		case 2:
			lastSpawned = Instantiate(level1_2) as GameObject;
			break;
		case 3:
			lastSpawned = Instantiate(level1_3) as GameObject;
			break;
		case 4:
			lastSpawned = Instantiate(level1_4) as GameObject;
			break;
		case 5:
			lastSpawned = Instantiate(level2_1) as GameObject;
			break;
		case 6:
			lastSpawned = Instantiate(level2_2) as GameObject;
			break;
		case 7:
			lastSpawned = Instantiate(level2_3) as GameObject;
			break;			
		case 8:
			lastSpawned = Instantiate(level2_4) as GameObject;
			break;			
		case 9:
			lastSpawned = Instantiate(level3_1) as GameObject;
			break;			
		case 10:
			lastSpawned = Instantiate(level3_2) as GameObject;
			break;			
		case 11:
			lastSpawned = Instantiate(level3_3) as GameObject;
			break;			
		case 12:
			lastSpawned = Instantiate(level3_4) as GameObject;
			break;			
		case 13:
			lastSpawned = Instantiate(level4_1) as GameObject;
			break;			
		case 14:
			lastSpawned = Instantiate(level4_2) as GameObject;
			break;			
		case 15:
			lastSpawned = Instantiate(level4_3) as GameObject;
			break;			
		case 16:
			lastSpawned = Instantiate(level4_4) as GameObject;
			break;			
		case 17:
			lastSpawned = Instantiate(level5_1) as GameObject;
			break;			
		case 18:
			lastSpawned = Instantiate(level5_2) as GameObject;
			break;			
		case 19:
			lastSpawned = Instantiate(level5_3) as GameObject;
			break;			
		case 20:
			lastSpawned = Instantiate(level5_4) as GameObject;
			break;			
		case 21:
			lastSpawned = Instantiate(level5_5) as GameObject;
			break;
		}
	}

	void Update()
	{
		if(GameObject.FindGameObjectWithTag("Player") != null)
		{
			if(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().levelEnded == true)
			{
                if(!scoringDone)
                    DoScoring();
                ////Calculate Grade rating and grade
                //GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeCal();
				
                //levelEndMenu.SetActive(true);
                //switch(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().grade)
                //{
                //case "S":
                //    grade.GetComponent<Image>().sprite = gradeS;
                //    break;
                //case "A":
                //    grade.GetComponent<Image>().sprite = gradeA;
                //    break;
                //case "B":
                //    grade.GetComponent<Image>().sprite = gradeB;
                //    break;
                //case "C":
                //    grade.GetComponent<Image>().sprite = gradeC;
                //    break;
                //case "D":
                //    grade.GetComponent<Image>().sprite = gradeD;
                //    break;
                //case "E":
                //    grade.GetComponent<Image>().sprite = gradeE;
                //    break;
                //case "F":
                //    grade.GetComponent<Image>().sprite = gradeF;
                //    break;
                //default:
                //    grade.GetComponent<Image>().sprite = dash;
                //    break;
                //}
                //menuScore.text = GameObject.Find("scoring").GetComponent<Text>().text;
				
                //switch(loadedLevel)
                //{
                //    //WORLD 1
                //case 1:
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore >PlayerPrefs.GetInt("ppLevel1HighScore"))
                //    {
                //        PlayerPrefs.SetInt("ppLevel1HighScore", GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore);
                //        PlayerPrefs.SetInt("ppWorld1Lv1", 1);
                //    }
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating > PlayerPrefs.GetFloat("ppLevel1HighestRating", 0.0f))
                //    {
                //        PlayerPrefs.SetFloat("ppLevel1HighestRating" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating);
                //        PlayerPrefs.SetString("ppLevel1HighestGrade" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().grade);
                //    }
                //    break;
					
                //case 2:
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore >PlayerPrefs.GetInt("ppLevel2HighScore"))
                //    {
                //        PlayerPrefs.SetInt("ppLevel2HighScore", GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore);
                //        PlayerPrefs.SetInt("ppWorld1Lv2", 1);
                //    }
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating > PlayerPrefs.GetFloat("ppLevel2HighestRating"))
                //    {
                //        PlayerPrefs.SetFloat("ppLevel2HighestRating" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating);
                //        PlayerPrefs.SetString("ppLevel2HighestGrade" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().grade);
                //    }
                //    break;
					
                //case 3:
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore >PlayerPrefs.GetInt("ppLevel3HighScore"))
                //    {
                //        PlayerPrefs.SetInt("ppLevel3HighScore", GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore);
                //        PlayerPrefs.SetInt("ppWorld1Lv3", 1);
                //    }
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating > PlayerPrefs.GetFloat("ppLevel3HighestRating"))
                //    {
                //        PlayerPrefs.SetFloat("ppLevel3HighestRating" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating);
                //        PlayerPrefs.SetString("ppLevel3HighestGrade" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().grade);
                //    }
                //    break;
					
                //case 4:
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore >PlayerPrefs.GetInt("ppLevel4HighScore"))
                //    {
                //        PlayerPrefs.SetInt("ppLevel4HighScore", GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore);
                //        PlayerPrefs.SetInt("ppWorld1Boss", 1);
						
                //        //googleplay achievement unlock
                //        //meltedIce achievment 
                //        Social.ReportProgress("CgkI__bt5ooSEAIQBQ", 100.0f, (bool success) =>{
                //            if(success)
                //            {
                //                if(PlayerPrefs.GetInt("ppMeltedIce") == 0)
                //                {
                //                    PlayerPrefs.SetInt("ppMeltedIce" , 1);
									
                //                }
                //            }
							
                //        });
						
                //        if(PlayerPrefs.GetInt("ppMeltedIce") == 1)
                //        {
                //            // increment achievement
                //            PlayGamesPlatform.Instance.IncrementAchievement(
                //                "CgkI__bt5ooSEAIQEA", 5, (bool success) => {
                //            });
                //            PlayerPrefs.SetInt("ppMeltedIce" , 2);
                //        }	
                //    }
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating > PlayerPrefs.GetFloat("ppLevel4HighestRating"))
                //    {
                //        PlayerPrefs.SetFloat("ppLevel4HighestRating" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating);
                //        PlayerPrefs.SetString("ppLevel4HighestGrade" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().grade);
                //    }
                //    break;
					
                //    //WORLD 2
                //case 5:
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore >PlayerPrefs.GetInt("ppLevel5HighScore"))
                //    {
                //        PlayerPrefs.SetInt("ppLevel5HighScore", GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore);
                //        PlayerPrefs.SetInt("ppWorld2Lv1", 1);
                //    }
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating > PlayerPrefs.GetFloat("ppLevel5HighestRating"))
                //    {
                //        PlayerPrefs.SetFloat("ppLevel5HighestRating" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating);
                //        PlayerPrefs.SetString("ppLevel5HighestGrade" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().grade);
                //    }
                //    break;
					
                //case 6:
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore >PlayerPrefs.GetInt("ppLevel6HighScore"))
                //    {
                //        PlayerPrefs.SetInt("ppLevel6HighScore", GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore);
                //        PlayerPrefs.SetInt("ppWorld2Lv2", 1);
                //    }
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating > PlayerPrefs.GetFloat("ppLevel6HighestRating"))
                //    {
                //        PlayerPrefs.SetFloat("ppLevel6HighestRating" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating);
                //        PlayerPrefs.SetString("ppLevel6HighestGrade" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().grade);
                //    }
                //    break;
					
                //case 7:
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore >PlayerPrefs.GetInt("ppLevel7HighScore"))
                //    {
                //        PlayerPrefs.SetInt("ppLevel7HighScore", GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore);
                //        PlayerPrefs.SetInt("ppWorld2Lv3", 1);
                //    }
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating > PlayerPrefs.GetFloat("ppLevel7HighestRating"))
                //    {
                //        PlayerPrefs.SetFloat("ppLevel7HighestRating" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating);
                //        PlayerPrefs.SetString("ppLevel7HighestGrade" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().grade);
                //    }
                //    break;
					
                //case 8:
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore >PlayerPrefs.GetInt("ppLevel8HighScore"))
                //    {
                //        PlayerPrefs.SetInt("ppLevel8HighScore", GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore);
                //        PlayerPrefs.SetInt("ppWorld2Boss", 1);
                //        //googleplay achievement unlock
                //        //Burned achievment 
                //        Social.ReportProgress("CgkI__bt5ooSEAIQCQ", 100.0f, (bool success) =>{ 
                //            if(success)
                //            {
                //                if(PlayerPrefs.GetInt("ppBurned") == 0)
                //                {
                //                    PlayerPrefs.SetInt("ppBurned" , 1);
                //                }
                //            }
							
                //        });
						
                //        if(PlayerPrefs.GetInt("ppBurned") == 1)
                //        {
                //            // increment achievement
                //            PlayGamesPlatform.Instance.IncrementAchievement(
                //                "CgkI__bt5ooSEAIQEA", 5, (bool success) => {
                //            });
                //            PlayerPrefs.SetInt("ppBurned" , 2);
                //        }	
						
						
                //        if(PlayerPrefs.GetInt("ppWorld1Boss") == 1 && PlayerPrefs.GetInt("ppWorld2Boss") == 1)
                //        {
                //            //googleplay achievement unlock
                //            //welcomebackmyfriend achievment 
                //            Social.ReportProgress("CgkI__bt5ooSEAIQDQ", 100.0f, (bool success) =>{
                //                if(success)
                //                {
                //                    if(PlayerPrefs.GetInt("ppWelcomBackMyFriends") == 0)
                //                    {
                //                        PlayerPrefs.SetInt("ppWelcomBackMyFriends" , 1);
                //                    }
                //                }
								
                //            });
                //            if(PlayerPrefs.GetInt("ppWelcomBackMyFriends") == 1)
                //            {
                //                // increment achievement
                //                PlayGamesPlatform.Instance.IncrementAchievement(
                //                    "CgkI__bt5ooSEAIQEA", 5, (bool success) => {
                //                });
                //                PlayerPrefs.SetInt("ppWelcomBackMyFriends" , 2);
                //            }	
							
                //        }
                //    }
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating > PlayerPrefs.GetFloat("ppLevel8HighestRating"))
                //    {
                //        PlayerPrefs.SetFloat("ppLevel8HighestRating" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating);
                //        PlayerPrefs.SetString("ppLevel8HighestGrade" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().grade);
                //    }
					
                //    break;
					
                //    //WORLD 3
                //case 9:
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore >PlayerPrefs.GetInt("ppLevel9HighScore"))
                //    {
                //        PlayerPrefs.SetInt("ppLevel9HighScore", GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore);
                //        PlayerPrefs.SetInt("ppWorld3Lv1", 1);
                //    }
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating > PlayerPrefs.GetFloat("ppLevel9HighestRating", 0.0f))
                //    {
                //        PlayerPrefs.SetFloat("ppLevel9HighestRating" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating);
                //        PlayerPrefs.SetString("ppLevel9HighestGrade" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().grade);
                //    }
                //    break;
					
                //case 10:
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore >PlayerPrefs.GetInt("ppLevel10HighScore"))
                //    {
                //        PlayerPrefs.SetInt("ppLevel10HighScore", GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore);
                //        PlayerPrefs.SetInt("ppWorld3Lv2", 1);
                //    }
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating > PlayerPrefs.GetFloat("ppLevel10HighestRating"))
                //    {
                //        PlayerPrefs.SetFloat("ppLevel10HighestRating" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating);
                //        PlayerPrefs.SetString("ppLevel10HighestGrade" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().grade);
                //    }
                //    break;
					
                //case 11:
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore >PlayerPrefs.GetInt("ppLevel11HighScore"))
                //    {
                //        PlayerPrefs.SetInt("ppLevel11HighScore", GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore);
                //        PlayerPrefs.SetInt("ppWorld3Lv3", 1);
                //    }
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating > PlayerPrefs.GetFloat("ppLevel11HighestRating"))
                //    {
                //        PlayerPrefs.SetFloat("ppLevel11HighestRating" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating);
                //        PlayerPrefs.SetString("ppLevel11HighestGrade" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().grade);
                //    }
                //    break;
					
                //case 12:
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore >PlayerPrefs.GetInt("ppLevel12HighScore"))
                //    {
                //        PlayerPrefs.SetInt("ppLevel12HighScore", GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore);
                //        PlayerPrefs.SetInt("ppWorld3Boss", 1);

                //    }
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating > PlayerPrefs.GetFloat("ppLevel12HighestRating"))
                //    {
                //        PlayerPrefs.SetFloat("ppLevel12HighestRating" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating);
                //        PlayerPrefs.SetString("ppLevel12HighestGrade" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().grade);
                //    }
                //    break;
					
                //    //World 4
                //case 13:
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore >PlayerPrefs.GetInt("ppLevel13HighScore"))
                //    {
                //        PlayerPrefs.SetInt("ppLevel13HighScore", GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore);
                //        PlayerPrefs.SetInt("ppWorld4Lv1", 1);
                //    }
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating > PlayerPrefs.GetFloat("ppLevel13HighestRating", 0.0f))
                //    {
                //        PlayerPrefs.SetFloat("ppLevel13HighestRating" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating);
                //        PlayerPrefs.SetString("ppLevel13HighestGrade" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().grade);
                //    }
                //    break;
					
                //case 14:
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore >PlayerPrefs.GetInt("ppLevel14HighScore"))
                //    {
                //        PlayerPrefs.SetInt("ppLevel14HighScore", GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore);
                //        PlayerPrefs.SetInt("ppWorld4Lv2", 1);
                //    }
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating > PlayerPrefs.GetFloat("ppLevel14HighestRating"))
                //    {
                //        PlayerPrefs.SetFloat("ppLevel14HighestRating" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating);
                //        PlayerPrefs.SetString("ppLevel14HighestGrade" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().grade);
                //    }
                //    break;
					
                //case 15:
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore >PlayerPrefs.GetInt("ppLevel15HighScore"))
                //    {
                //        PlayerPrefs.SetInt("ppLevel15HighScore", GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore);
                //        PlayerPrefs.SetInt("ppWorld4Lv3", 1);
                //    }
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating > PlayerPrefs.GetFloat("ppLevel15HighestRating"))
                //    {
                //        PlayerPrefs.SetFloat("ppLevel15HighestRating" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating);
                //        PlayerPrefs.SetString("ppLevel15HighestGrade" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().grade);
                //    }
                //    break;
					
                //case 16:
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore >PlayerPrefs.GetInt("ppLevel16HighScore"))
                //    {
                //        PlayerPrefs.SetInt("ppLevel16HighScore", GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore);
                //        PlayerPrefs.SetInt("ppWorld4Boss", 1);
                //    }
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating > PlayerPrefs.GetFloat("ppLevel16HighestRating"))
                //    {
                //        PlayerPrefs.SetFloat("ppLevel16HighestRating" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating);
                //        PlayerPrefs.SetString("ppLevel16HighestGrade" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().grade);
                //    }
                //    break;
					
                //    //world 5
                //case 17:
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore >PlayerPrefs.GetInt("ppLevel17HighScore"))
                //    {
                //        PlayerPrefs.SetInt("ppLevel17HighScore", GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore);
                //        PlayerPrefs.SetInt("ppWorld5Lv1", 1);
                //    }
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating > PlayerPrefs.GetFloat("ppLevel17HighestRating", 0.0f))
                //    {
                //        PlayerPrefs.SetFloat("ppLevel17HighestRating" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating);
                //        PlayerPrefs.SetString("ppLevel17HighestGrade" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().grade);
                //    }
                //    break;
					
                //case 18:
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore >PlayerPrefs.GetInt("ppLevel18HighScore"))
                //    {
                //        PlayerPrefs.SetInt("ppLevel18HighScore", GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore);
                //        PlayerPrefs.SetInt("ppWorld5Lv2", 1);
                //    }
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating > PlayerPrefs.GetFloat("ppLevel18HighestRating"))
                //    {
                //        PlayerPrefs.SetFloat("ppLevel18HighestRating" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating);
                //        PlayerPrefs.SetString("ppLevel18HighestGrade" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().grade);
                //    }
                //    break;
					
                //case 19:
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore >PlayerPrefs.GetInt("ppLevel19HighScore"))
                //    {
                //        PlayerPrefs.SetInt("ppLevel19HighScore", GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore);
                //        PlayerPrefs.SetInt("ppWorld5Lv3", 1);
                //    }
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating > PlayerPrefs.GetFloat("ppLevel19HighestRating"))
                //    {
                //        PlayerPrefs.SetFloat("ppLevel19HighestRating" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating);
                //        PlayerPrefs.SetString("ppLevel19HighestGrade" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().grade);
                //    }
                //    break;
					
                //case 20:
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore >PlayerPrefs.GetInt("ppLevel20HighScore"))
                //    {
                //        PlayerPrefs.SetInt("ppLevel20HighScore", GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore);
                //        PlayerPrefs.SetInt("ppWorld5Boss1", 1);

                //    }
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating > PlayerPrefs.GetFloat("ppLevel20HighestRating"))
                //    {
                //        PlayerPrefs.SetFloat("ppLevel20HighestRating" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating);
                //        PlayerPrefs.SetString("ppLevel20HighestGrade" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().grade);
                //    }
                //    break;

                //case 21:
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore >PlayerPrefs.GetInt("ppLevel21HighScore"))
                //    {
                //        PlayerPrefs.SetInt("ppLevel21HighScore", GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore);
                //        PlayerPrefs.SetInt("ppWorld5Boss2", 1);
                //    }
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating > PlayerPrefs.GetFloat("ppLevel21HighestRating"))
                //    {
                //        PlayerPrefs.SetFloat("ppLevel21HighestRating" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating);
                //        PlayerPrefs.SetString("ppLevel21HighestGrade" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().grade);
                //    }
                //    break;

                //case 22:
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore >PlayerPrefs.GetInt("ppLevel22HighScore"))
                //    {
                //        PlayerPrefs.SetInt("ppLevel22HighScore", GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().currentScore);
                //        PlayerPrefs.SetInt("ppWorld5Boss3", 1);

                //    }
                //    if(GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating > PlayerPrefs.GetFloat("ppLevel22HighestRating"))
                //    {
                //        PlayerPrefs.SetFloat("ppLevel22HighestRating" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().gradeRating);
                //        PlayerPrefs.SetString("ppLevel22HighestGrade" , GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystem>().grade);
                //    }
                //    break;
                //}							
			}
			
		}
	}

    void DoScoring()
    {
        print("Calculating score");
        scoringDone = true;
        levelEndMenu.SetActive(true);
        pauseButton.interactable = false;
        
        if (PlayerPrefs.GetInt("ppBoostEquipped", 0) == 1)
        {
            levelEndMenu.transform.FindChild("MultiplierIcon").gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystemStar>().currentScore *= 2;       
            PlayerPrefs.SetInt("ppNumBoost", PlayerPrefs.GetInt("ppNumBoost", 0) - 1);
            if (PlayerPrefs.GetInt("ppNumBoost", 0) <= 0)
                PlayerPrefs.SetInt("ppBoostEquipped", 0);
        }
        else
        {
            levelEndMenu.transform.FindChild("MultiplierIcon").gameObject.SetActive(false);
        }
        int finalScore = GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystemStar>().currentScore;

        ScoringSystemStar.SCORING_TYPES tempRank = GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystemStar>().CalculateRank();

        rankIndicator[0].GetComponent<Image>().sprite = rankIconCompleteActive;
        switch (tempRank)
        {
            case ScoringSystemStar.SCORING_TYPES.END_TRUE_LIFE_TRUE_SCORE_TRUE:
                print("case 1 ");
                rankIndicator[1].GetComponent<Image>().sprite = rankIconHealthActive;
                rankIndicator[2].GetComponent<Image>().sprite = rankIconScoreActive;
                break;
            case ScoringSystemStar.SCORING_TYPES.END_TRUE_LIFE_TRUE_SCORE_FALSE:
                print("case 2");
                rankIndicator[1].GetComponent<Image>().sprite = rankIconHealthActive;
                break;
            case ScoringSystemStar.SCORING_TYPES.END_TRUE_LIFE_FALSE_SCORE_TRUE:
                print("case 3");
                rankIndicator[2].GetComponent<Image>().sprite = rankIconScoreActive;
                break;
            case ScoringSystemStar.SCORING_TYPES.END_TRUE_LIFE_FALSE_SCORE_FALSE:
                print("case 4");
                break;
            default:
                print("default");
                break;
        }
        menuScore.text = finalScore.ToString();
        print("Done score");

        //Level data player pref namings
        //Highscore     : ppTopScoreLevelX
        //Rank          : ppTopRankLevelX
        //Current level : ppCurrentLevel

        //Save current level data
        //Set current level to next
        if (loadedLevel >= PlayerPrefs.GetInt("ppCurrentLevel", 1))
            PlayerPrefs.SetInt("ppCurrentLevel", loadedLevel + 1);

        if (PlayerPrefs.GetInt("ppSelectedLevel", 0) < 22)
            PlayerPrefs.SetInt("ppSelectedLevel", PlayerPrefs.GetInt("ppSelectedLevel", 0) + 1);

        print("Loaded level: " + loadedLevel);
        print("CurrentLevel: " + PlayerPrefs.GetInt("ppCurrentLevel", 1));

        //Highest score
        if (finalScore > PlayerPrefs.GetInt("ppTopScoreLevel" + loadedLevel.ToString()))
            PlayerPrefs.SetInt("ppTopScoreLevel" + loadedLevel.ToString(), finalScore);

        //Highest rank
        PlayerPrefs.SetInt("ppTopRankLevel" + loadedLevel, 
            (int)ScoringSystemStar.CombineRank((ScoringSystemStar.SCORING_TYPES)PlayerPrefs.GetInt("ppTopRankLevel" + loadedLevel, (int)ScoringSystemStar.SCORING_TYPES.END_FALSE_LIFE_FALSE_SCORE_FALSE), tempRank
            ));

        //google leaderboard world 1
        worldOneHighScore = PlayerPrefs.GetInt("ppLevel1HighScore") + PlayerPrefs.GetInt("ppLevel2HighScore") + PlayerPrefs.GetInt("ppLevel3HighScore") + PlayerPrefs.GetInt("ppLevel4HighScore");
        Social.ReportScore(worldOneHighScore, "CgkI__bt5ooSEAIQDg", (bool success) =>
        {
        });

        //google leaderboard world 2
        worldTwoHighScore = PlayerPrefs.GetInt("ppLevel5HighScore") + PlayerPrefs.GetInt("ppLevel6HighScore") + PlayerPrefs.GetInt("ppLevel7HighScore") + PlayerPrefs.GetInt("ppLevel8HighScore");
        Social.ReportScore(worldTwoHighScore, "CgkI__bt5ooSEAIQDw", (bool success) =>
        {
        });

        //google leaderboard world 3
        worldThreeHighScore = PlayerPrefs.GetInt("ppLevel9HighScore") + PlayerPrefs.GetInt("ppLevel10HighScore") + PlayerPrefs.GetInt("ppLevel11HighScore") + PlayerPrefs.GetInt("ppLevel12HighScore");
        Social.ReportScore(worldThreeHighScore, "CgkI__bt5ooSEAIQGA", (bool success) =>
        {
        });

        //google leaderboard world 4
        worldFourHighScore = PlayerPrefs.GetInt("ppLevel13HighScore") + PlayerPrefs.GetInt("ppLevel14HighScore") + PlayerPrefs.GetInt("ppLevel15HighScore") + PlayerPrefs.GetInt("ppLevel16HighScore");
        Social.ReportScore(worldFourHighScore, "CgkI__bt5ooSEAIQGQ", (bool success) =>
        {
        });

        //google leaderboard world 5
        worldFiveHighScore = PlayerPrefs.GetInt("ppLevel17HighScore") + PlayerPrefs.GetInt("ppLevel18HighScore") + PlayerPrefs.GetInt("ppLevel19HighScore") + PlayerPrefs.GetInt("ppLevel20HighScore") + PlayerPrefs.GetInt("ppLevel21HighScore") + PlayerPrefs.GetInt("ppLevel22HighScore");
        Social.ReportScore(worldFiveHighScore, "CgkI__bt5ooSEAIQGg", (bool success) =>
        {
        });
    }

    public void GoToNextLevel()
    {
        ////Extract level number from current level string
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}