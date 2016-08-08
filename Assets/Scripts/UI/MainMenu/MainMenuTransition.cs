using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class MainMenuTransition : MonoBehaviour 
{

	// Twerkable value		   
	float scrollingSpeed = 3;	 		 		 // Panel moving to left/right/center speed
	float distance = 500; 				 		 // How far away are they from ur screen
	Vector2 default_res = new Vector2 (720, 1280); // Better dont touch if not aspect ratio will gg
	
	// Variables For Checking
	private bool LerpToLeft; 		  	 // True: UI slowly moving to LEFT
	private bool LerpToRight;			 // True: UI slowly moving to RIGHT
	private bool LerpToCenter_FromLeft;  // True: UI slowly moving to CENTER, from left
	private bool LerpToCenter_FromRight; // True: UI slowly moving to CENTER, from right
	private int conditionDone; 			 // if(conditionDone==2) Moving panel have reached where they want to go
	
	// Variables to keep track of the 3 target position [LEFT/RIGHT/CENTER]  
	public float centerPositionX; // I make it public so you can see the start, center, end position
	public float leftPositionX;
	public float rightPositionX;
	
	public bool showAtStart;
	public bool hideExitButton ;
	
	//level Progression
	public static int levelProg = 0; 
	
	GameObject logoObject;
	GameObject BgmPooler;
	Transform backButton ;
		 	
	void Start() //[THIS ONE CANNOT PUT AT AWAKE ah!!!!]
	{
		
		float refResoX = 1280 * Camera.main.aspect;
		default_res = new Vector2(refResoX, 1280);
		Vector2 refAspect = new Vector2(refResoX, refResoX * (1 / Camera.main.aspect));
		
		backButton = transform.FindChild("Back");
		logoObject = GameObject.Find("Logo");
		
		// Only need adjust 1 Aspect ratio which is distance
		//distance = distance * (Screen.width/default_res.x);
		distance = distance / 720 / 1280 * refAspect.x * refAspect.y / Camera.main.aspect;
		if(distance <= 0)
		{
			distance = 0.001f;
		}
		// Record: Target Position of "CENTER"      
		centerPositionX = this.transform.position.x;
		
		// Record: Target Position of "LEFT"
		leftPositionX = centerPositionX + (Vector3.left * distance).x;
		
		// Record: Target Position of "RIGHT"
		rightPositionX = centerPositionX + (Vector3.right * distance).x;
		// Every Panel from scroll to the "RIGHT" from the start
		if(showAtStart == false)
			this.transform.position = new Vector3(rightPositionX, this.transform.position.y, this.transform.position.z);
			
		Time.timeScale = 1.0f;
	}
	
	void Update () 
	{
		//Debug.Log (levelProg);
		if(LerpToLeft)
		{
			//Lerping
			if(Mathf.Abs(this.transform.position.x - leftPositionX) > 0.5f)
			{
				this.transform.position = new Vector3 (Mathf.Lerp (this.transform.position.x, leftPositionX , Time.deltaTime * scrollingSpeed), this.transform.position.y, this.transform.position.z);
			}
			//Lerp Finished Or any other condition
			else if(Mathf.Abs(this.transform.position.x - leftPositionX) < 0.5f)
			{
				this.transform.position = new Vector3(leftPositionX, this.transform.position.y, this.transform.position.z);
				conditionDone++;
			}	
		}
		if(LerpToCenter_FromLeft || LerpToCenter_FromRight)
		{
			//Lerping
			if(Mathf.Abs(this.transform.position.x - centerPositionX) > 0.5f)
			{
				this.transform.position = new Vector3 (Mathf.Lerp (this.transform.position.x, centerPositionX , Time.deltaTime * scrollingSpeed), this.transform.position.y, this.transform.position.z);
			}
			//Lerp Finished Or any other condition
			else if(Mathf.Abs(this.transform.position.x - centerPositionX) < 0.5f)
			{
				this.transform.position = new Vector3(centerPositionX, this.transform.position.y, this.transform.position.z);
				conditionDone++;
			}	
		}
		if(LerpToRight)
		{
			//Lerping
			if(Mathf.Abs(this.transform.position.x - rightPositionX) > 0.5f)
			{
				this.transform.position = new Vector3 (Mathf.Lerp (this.transform.position.x, rightPositionX , Time.deltaTime * scrollingSpeed), this.transform.position.y, this.transform.position.z);
			}
			//Lerp Finished Or any other condition
			else if(Mathf.Abs(this.transform.position.x - rightPositionX) < 0.5f)
			{
				this.transform.position = new Vector3(rightPositionX, this.transform.position.y, this.transform.position.z);
				conditionDone++;
			}	
		}
		
		if(conditionDone >= 2)
		{
			LerpToLeft = LerpToCenter_FromLeft = LerpToCenter_FromRight = LerpToRight = false;
			conditionDone = 0;
		}
	}
	
	public void ThisPanel_LerpToLeft()
	{
		this.transform.position = new Vector3 (centerPositionX, this.transform.position.y, this.transform.position.z);
		LerpToRight = false;	
		LerpToLeft = true;	
		LerpToCenter_FromLeft = false;
		LerpToCenter_FromRight = false;
		conditionDone = 0;
	}
	
	public void ThisPanel_LerpToCenter_FromLeft()
	{
		this.transform.position = new Vector3 (leftPositionX, this.transform.position.y, this.transform.position.z);
		LerpToRight = false;	
		LerpToLeft = false;	
		LerpToCenter_FromLeft = true;
		LerpToCenter_FromRight = false;
		conditionDone = 0;
	}
	public void ThisPanel_LerpToCenter_FromRight()
	{
		this.transform.position = new Vector3 (rightPositionX, this.transform.position.y, this.transform.position.z);
		LerpToRight = false;	
		LerpToLeft = false;	
		LerpToCenter_FromLeft = false;
		LerpToCenter_FromRight = true;
		
		conditionDone = 0;
	}
	
	public void ThisPanel_LerpToRight()
	{
		this.transform.position = new Vector3 (centerPositionX, this.transform.position.y, this.transform.position.z);
		LerpToRight = true;	
		LerpToLeft = false;	
		LerpToCenter_FromLeft = false;
		LerpToCenter_FromRight = false;
		conditionDone = 0;
	}
	public void exitGame()
	{
		Application.Quit();
	}
	
	public void offBackButton  ()
	{
		backButton.GetComponent<Button>().interactable = false;
	}
	
	public void onBackButton  ()
	{
		backButton.GetComponent<Button>().interactable = true;
	}

	public void GoToTutorial ()
	{
		Application.LoadLevel("tutorialScene");
	}

	public void GoToWorldOne ()
	{
		Application.LoadLevel("worldOne");
	}
	
	public void GoToWorldTwo ()
	{
		Application.LoadLevel("worldTwo");
	}

	public void GoToWorldThree ()
	{
		Application.LoadLevel("worldThree");
	}

	public void GoToWorldFour ()
	{
		Application.LoadLevel("worldFour");
	}

	public void GoToWorldFive ()
	{
		Application.LoadLevel("worldFive");
	}
	
	public void GoToWorldSelect ()
	{
		if(PlayerPrefs.GetInt("ppFirstPlay", 0) == 0)
		{
			Application.LoadLevel("tutorialScene");
		}
		else if(PlayerPrefs.GetInt("ppFirstPlay") >= 1)
		{
			Application.LoadLevel("worldSelect");
		}
	}
	
	public void BackToWorldSelect ()
	{
		Application.LoadLevel("WorldSelect");
	}
	
	public void BackToMainMenu ()
	{
		Application.LoadLevel("mainMenu");
	}
	
	public void GameOverBack ()
	{
		if(PlayerPrefs.GetString("ppSelectedLevel") == "Level1" || PlayerPrefs.GetString("ppSelectedLevel") == "Level2" || PlayerPrefs.GetString("ppSelectedLevel") == "Level3"  || PlayerPrefs.GetString("ppSelectedLevel") == "Level4")
		{
			Application.LoadLevel("worldOne");
			Time.timeScale =1 ;
		}
		else if(PlayerPrefs.GetString("ppSelectedLevel") == "Level5" || PlayerPrefs.GetString("ppSelectedLevel") == "Level6" || PlayerPrefs.GetString("ppSelectedLevel") == "Level7"  || PlayerPrefs.GetString("ppSelectedLevel") == "Level8")
		{
			Application.LoadLevel("worldTwo");
			Time.timeScale =1 ;
		}
		else if(PlayerPrefs.GetString("ppSelectedLevel") == "Level9" || PlayerPrefs.GetString("ppSelectedLevel") == "Level10" || PlayerPrefs.GetString("ppSelectedLevel") == "Level11"  || PlayerPrefs.GetString("ppSelectedLevel") == "Level12")
		{
			Application.LoadLevel("worldThree");
			Time.timeScale =1 ;
		}
		else if(PlayerPrefs.GetString("ppSelectedLevel") == "Level13" || PlayerPrefs.GetString("ppSelectedLevel") == "Level14" || PlayerPrefs.GetString("ppSelectedLevel") == "Level15"  || PlayerPrefs.GetString("ppSelectedLevel") == "Level16")
		{
			Application.LoadLevel("worldFour");
			Time.timeScale =1 ;
		}
		else if(PlayerPrefs.GetString("ppSelectedLevel") == "Level17" || PlayerPrefs.GetString("ppSelectedLevel") == "Level18" || PlayerPrefs.GetString("ppSelectedLevel") == "Level19"  || PlayerPrefs.GetString("ppSelectedLevel") == "Level20" || PlayerPrefs.GetString("ppSelectedLevel") == "Level21") 
		{
			Application.LoadLevel("worldFive");
			Time.timeScale =1 ;
		}
	}
	
	public void GoToGallery ()
	{
		//googleplay achievement unlock
		//Knowledge Is Power
		Social.ReportProgress("CgkI__bt5ooSEAIQEg", 100.0f, (bool success) =>{
			if(success)
			{
				if(PlayerPrefs.GetInt("ppKnowledgeIsPower") == 0)
				{
					PlayerPrefs.SetInt("ppKnowledgeIsPower" , 1);
				}
			}
			
		});
		
		if(PlayerPrefs.GetInt("ppKnowledgeIsPower") == 1)
		{
			// increment achievement
			//PlayGamesPlatform.Instance.IncrementAchievement(
			//	"CgkI__bt5ooSEAIQEg", 5, (bool success) => {
			//});
			PlayerPrefs.SetInt("ppKnowledgeIsPower" , 2);
		}
		Application.LoadLevel("gallery");

	}

	public void GameOverRestart()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
	
}

