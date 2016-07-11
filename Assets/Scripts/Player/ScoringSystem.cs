using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoringSystem : MonoBehaviour {

	public GameObject score ;
	public GameObject scoreShadow ;

	public int currentScore = 0;

	public int lifeLeft = 3;
	public float highestCombo = 0.0f;
	public float gradeRating = 0.0f;
	public string grade;
	string title ;
	
	public int enemiesKilled = 0;
	int totalEnemies = 0;
	
	void Start()
	{
		//tutorial level
		if(GameObject.FindGameObjectWithTag("Level") == null)
		{
			totalEnemies = 0;
		}
		else
		{
			//any normal levels
			totalEnemies = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelGeneratorScript>().totalEnemies;
		}
		score = GameObject.Find("scoring");
		scoreShadow = GameObject.Find("scoringShadow");
	}

	//function to calculate the grade
	public void gradeCal ()
	{
		//gradeRating = (highestCombo * 0.1) + (lifeLeft );
		gradeRating = (((float)enemiesKilled * 100) / (float)totalEnemies) * ((float)lifeLeft/3);
		//grade sorting
		if(gradeRating >= 96.0f )
		{
			grade = "S" ;	
		}
		else if(gradeRating >= 85.0f && gradeRating < 96.0f)
		{
			grade = "A" ;	
		}
		else if(gradeRating >= 75.0f && gradeRating < 85.0f)
		{
			grade = "B" ;
		}
		else if(gradeRating >= 65.0f && gradeRating < 75.0f)
		{
			grade = "C";
		}
		else if(gradeRating >= 55.0f && gradeRating < 65.0f)
		{
			grade = "D";
		}
		else if(gradeRating >= 50.0f && gradeRating < 55.0f)
		{
			grade = "E";
		}
		else if(gradeRating < 50.0f)
		{
			grade = "F";
		}
	}
	// Update is called once per frame
	void Update () {
		if(score != null && Application.loadedLevelName != "tutorialScene")
		{
//			score.GetComponent<Text>().text = currentScore.ToString() + " " + "\npts" ;	
			score.GetComponent<Text>().text = currentScore.ToString() + " ";
			scoreShadow.GetComponent<Text>().text = currentScore.ToString() + " ";
		}
		if(GetComponent<PlayerController>().topComboCount > highestCombo)
		{
			highestCombo = GetComponent<PlayerController>().topComboCount ;
			PlayerPrefs.SetFloat("ppHighestCombo" , highestCombo);
			PlayerPrefs.Save ();
		}
		
		lifeLeft = GetComponent<PlayerController>().emotionPoint;
	
	}
}
