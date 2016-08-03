using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AchievementSystem : MonoBehaviour {

    public GameObject achievementPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CreateAchievement(string name, string category, string description, Image rewardType, int rewardNo)
    {
        GameObject achievement = (GameObject)Instantiate(achievementPrefab);
        SetAchievementInfo(category, achievement, name, description, rewardType, rewardNo);
    }

    public void SetAchievementInfo(string category, GameObject achievement, string name, string description, Image rewardType, int rewardNo)
    {
        achievement.transform.SetParent(GameObject.Find(category).transform);
        achievement.transform.GetChild(0).GetComponent<Text>().text = name;
        achievement.transform.GetChild(1).GetComponent<Text>().text = description;
        //achievement.transform.GetChild(2) = rewardType;
        //achievement.transform.GetChild(3).GetComponent<Text>().text = rewardNo.ToString();
    }
}
