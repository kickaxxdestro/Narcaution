using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class enterLevel : MonoBehaviour {
	
	public GameObject levelIndicator;
    public GameObject descriptionText;
    public GameObject[] rankIndicator;
    public GameObject highscoreIndicator;
    public Text targetScore;

	public Color medalLockedColor;
	public Color medalUnlockedColor;

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
			rankIndicator[0].GetComponent<Image>().color = medalUnlockedColor;
			rankIndicator[1].GetComponent<Image>().color = medalUnlockedColor;
			rankIndicator[2].GetComponent<Image>().color = medalUnlockedColor;
                //print("case 1 ");
                break;
            case ScoringSystemStar.SCORING_TYPES.END_TRUE_LIFE_TRUE_SCORE_FALSE:
			rankIndicator[0].GetComponent<Image>().color = medalUnlockedColor;
			rankIndicator[1].GetComponent<Image>().color = medalUnlockedColor;
			rankIndicator[2].GetComponent<Image>().color = medalLockedColor;
                //print("case 2");
                break;
            case ScoringSystemStar.SCORING_TYPES.END_TRUE_LIFE_FALSE_SCORE_TRUE:
			rankIndicator[0].GetComponent<Image>().color = medalUnlockedColor;
			rankIndicator[1].GetComponent<Image>().color = medalLockedColor;
			rankIndicator[2].GetComponent<Image>().color = medalUnlockedColor;
                //print("case 3");
                break;
            case ScoringSystemStar.SCORING_TYPES.END_TRUE_LIFE_FALSE_SCORE_FALSE:
			rankIndicator[0].GetComponent<Image>().color = medalUnlockedColor;
			rankIndicator[1].GetComponent<Image>().color = medalLockedColor;
			rankIndicator[2].GetComponent<Image>().color = medalLockedColor;
                //print("case 4");
                break;
            case ScoringSystemStar.SCORING_TYPES.END_FALSE_LIFE_FALSE_SCORE_FALSE:
			rankIndicator[0].GetComponent<Image>().color = medalLockedColor;
			rankIndicator[1].GetComponent<Image>().color = medalLockedColor;
			rankIndicator[2].GetComponent<Image>().color = medalLockedColor;
                break;
            default:
			rankIndicator[0].GetComponent<Image>().color = medalLockedColor;
			rankIndicator[1].GetComponent<Image>().color = medalLockedColor;
			rankIndicator[2].GetComponent<Image>().color = medalLockedColor;
                //print("default");
                break;
        }
    }

}
