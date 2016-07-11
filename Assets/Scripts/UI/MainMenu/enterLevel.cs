using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class enterLevel : MonoBehaviour {
	
	public GameObject levelIndicator;
    public GameObject descriptionText;
    public GameObject[] rankIndicator;
    public GameObject highscoreIndicator;
    public Text targetScore;

    public Sprite rankIconCompleteInactive;
    public Sprite rankIconHealthInactive;
    public Sprite rankIconScoreInactive;
    public Sprite rankIconCompleteActive;
    public Sprite rankIconHealthActive;
    public Sprite rankIconScoreActive;

	void Start ()
	{
	}

	// Update is called once per frame
	void Update () {		
	}
	
	public void SetLevel(LevelGeneratorScript level)
	{
        if (level.levelID == 21)
            levelIndicator.GetComponent<Text>().text = "5-5";
        else
            levelIndicator.GetComponent<Text>().text = (((level.levelID - 1) / 4) + 1) + "-" + (level.levelID - (((level.levelID - 1) / 4) * 4));
        descriptionText.GetComponent<Text>().text = level.description;
        highscoreIndicator.GetComponent<Text>().text = PlayerPrefs.GetInt("ppTopScoreLevel" + level.levelID.ToString()).ToString();
        targetScore.text = level.scoreBonusAmt.ToString();

        ScoringSystemStar.SCORING_TYPES tempRank = (ScoringSystemStar.SCORING_TYPES)PlayerPrefs.GetInt("ppTopRankLevel" + level.levelID, (int)ScoringSystemStar.SCORING_TYPES.END_FALSE_LIFE_FALSE_SCORE_FALSE);
        switch (tempRank)
        {
            case ScoringSystemStar.SCORING_TYPES.END_TRUE_LIFE_TRUE_SCORE_TRUE:
                rankIndicator[0].GetComponent<Image>().sprite = rankIconCompleteActive;
                rankIndicator[1].GetComponent<Image>().sprite = rankIconHealthActive;
                rankIndicator[2].GetComponent<Image>().sprite = rankIconScoreActive;
                //print("case 1 ");
                break;
            case ScoringSystemStar.SCORING_TYPES.END_TRUE_LIFE_TRUE_SCORE_FALSE:
                rankIndicator[0].GetComponent<Image>().sprite = rankIconCompleteActive;
                rankIndicator[1].GetComponent<Image>().sprite = rankIconHealthActive;
                rankIndicator[2].GetComponent<Image>().sprite = rankIconScoreInactive;
                //print("case 2");
                break;
            case ScoringSystemStar.SCORING_TYPES.END_TRUE_LIFE_FALSE_SCORE_TRUE:
                rankIndicator[0].GetComponent<Image>().sprite = rankIconCompleteActive;
                rankIndicator[1].GetComponent<Image>().sprite = rankIconHealthInactive;
                rankIndicator[2].GetComponent<Image>().sprite = rankIconScoreActive;
                //print("case 3");
                break;
            case ScoringSystemStar.SCORING_TYPES.END_TRUE_LIFE_FALSE_SCORE_FALSE:
                rankIndicator[0].GetComponent<Image>().sprite = rankIconCompleteActive;
                rankIndicator[1].GetComponent<Image>().sprite = rankIconHealthInactive;
                rankIndicator[2].GetComponent<Image>().sprite = rankIconScoreInactive;
                //print("case 4");
                break;
            case ScoringSystemStar.SCORING_TYPES.END_FALSE_LIFE_FALSE_SCORE_FALSE:
                rankIndicator[0].GetComponent<Image>().sprite = rankIconCompleteInactive;
                rankIndicator[1].GetComponent<Image>().sprite = rankIconHealthInactive;
                rankIndicator[2].GetComponent<Image>().sprite = rankIconScoreInactive;
                break;
            default:
                rankIndicator[0].GetComponent<Image>().sprite = rankIconCompleteInactive;
                rankIndicator[1].GetComponent<Image>().sprite = rankIconHealthInactive;
                rankIndicator[2].GetComponent<Image>().sprite = rankIconScoreInactive;
                //print("default");
                break;
        }
    }

}
