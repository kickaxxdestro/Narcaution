using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

//Scoring system which assigns a score of 1 to 3 stars
//1 : Complete level
//2 : End with full health
//3 : Get above specified level score

//Ranking is stored as a single integer to save space

public class ScoringSystemStar : MonoBehaviour {

    public GameObject scoreText;

    public int currentScore = 0;

    public int enemiesKilled = 0;
    int totalEnemies = 0;
    public int lifeLeft = 3;
    public float highestCombo = 0.0f;

    //Types of all possible ranking outcomes
    public enum SCORING_TYPES
    {
        END_FALSE_LIFE_FALSE_SCORE_FALSE,
        END_TRUE_LIFE_FALSE_SCORE_FALSE,
        END_TRUE_LIFE_TRUE_SCORE_FALSE,
        END_TRUE_LIFE_FALSE_SCORE_TRUE,
        END_TRUE_LIFE_TRUE_SCORE_TRUE,
    }

	// Use this for initialization
	void Start () {
        totalEnemies = (GameObject.FindGameObjectWithTag("Level") == null) ? 0 : GameObject.FindGameObjectWithTag("Level").GetComponent<LevelGeneratorScript>().totalEnemies;

	}
	
	// Update is called once per frame
	void Update () {

        if (scoreText != null && SceneManager.GetActiveScene().name != "tutorialScene")
        {
            scoreText.GetComponent<Text>().text = currentScore.ToString();
        }
        if (GetComponent<PlayerController>().topComboCount > highestCombo)
        {
            highestCombo = GetComponent<PlayerController>().topComboCount;
            PlayerPrefs.SetFloat("ppHighestCombo", highestCombo);
            PlayerPrefs.Save();
        }
	}

    public SCORING_TYPES CalculateRank()
    {
        int scoreBonusAmt = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelGeneratorScript>().scoreBonusAmt;
        
        if (GetComponent<PlayerController>().emotionPoint >= 7 && currentScore >= scoreBonusAmt)
            return SCORING_TYPES.END_TRUE_LIFE_TRUE_SCORE_TRUE;
        else if (GetComponent<PlayerController>().emotionPoint >= 7 && currentScore < scoreBonusAmt)
            return SCORING_TYPES.END_TRUE_LIFE_TRUE_SCORE_FALSE;
        else if (GetComponent<PlayerController>().emotionPoint < 7 && currentScore >= scoreBonusAmt)
            return SCORING_TYPES.END_TRUE_LIFE_FALSE_SCORE_TRUE;

        return SCORING_TYPES.END_TRUE_LIFE_FALSE_SCORE_FALSE;
    }

    //Takes 2 exisiting score types and combines them
    public static SCORING_TYPES CombineRank(SCORING_TYPES score1, SCORING_TYPES score2)
    {
        if (score1 == score2)
            return score1;

        if (score1 == SCORING_TYPES.END_FALSE_LIFE_FALSE_SCORE_FALSE)
            return score2;

        if (score2 == SCORING_TYPES.END_FALSE_LIFE_FALSE_SCORE_FALSE)
            return score1;

        if (score1 == SCORING_TYPES.END_TRUE_LIFE_TRUE_SCORE_TRUE)
            return score1;

        if (score2 == SCORING_TYPES.END_TRUE_LIFE_TRUE_SCORE_TRUE)
            return score2;

        if (score1 == SCORING_TYPES.END_TRUE_LIFE_FALSE_SCORE_FALSE)
            return score2;

        if (score2 == SCORING_TYPES.END_TRUE_LIFE_FALSE_SCORE_FALSE)
            return score1;

        if ((score1 == SCORING_TYPES.END_TRUE_LIFE_TRUE_SCORE_FALSE && score2 == SCORING_TYPES.END_TRUE_LIFE_FALSE_SCORE_TRUE) ||
            (score2 == SCORING_TYPES.END_TRUE_LIFE_TRUE_SCORE_FALSE && score1 == SCORING_TYPES.END_TRUE_LIFE_FALSE_SCORE_TRUE))
            return SCORING_TYPES.END_TRUE_LIFE_TRUE_SCORE_TRUE;

        return SCORING_TYPES.END_FALSE_LIFE_FALSE_SCORE_FALSE;
    }
}
