using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;

[System.Serializable]
public class PatternList : System.Object{
	public GameObject enemyPattern;
	public float waitTime;
}

public class LevelGeneratorScript : MonoBehaviour {

	public GameObject dialogueSystem;
    public int levelID;
    public string description = "no description";
	GameObject dial;

	public PatternList[] patternLists;

	float timer;

	[System.NonSerialized]
	public int spawnCounter;

	//[System.NonSerialized]
	public int patternAmount;

	//[System.NonSerialized]
	public int totalEnemies = 0;

	public bool spawnWave;
	public bool endDial;

    public int scoreBonusAmt = 0;

    bool levelEnded = false;

	void Awake()
	{
		if(dialogueSystem != null)
		{
			dial = Instantiate(dialogueSystem) as GameObject;
		}

		//dial = Instantiate(dialogueSystem) as GameObject;

		for(int i = 0; i < patternLists.Length; ++i)
		{
			GameObject go = Instantiate(patternLists[i].enemyPattern);
			patternLists[i].enemyPattern = go;
			go.SetActive(false);
		}

		for(int i = 0; i < patternLists.Length; ++i)
		{
			for(int j = 0; j < patternLists[i].enemyPattern.GetComponents<EnemyPatternBehaviour>().Length; ++j)
			{
				totalEnemies += patternLists[i].enemyPattern.GetComponents<EnemyPatternBehaviour>()[j].amountToSpawn;
			}
		}
	}

	void Start()
	{
		dial.transform.FindChild("DialogueSystem").GetComponent<DialogueSystemScript>().ActivateDialogue(true);
		spawnWave = false;
		spawnCounter = 0;
		timer = patternLists[spawnCounter].waitTime;
	}

	// Update is called once per frame
	void Update ()
	{
        if (levelEnded)
            return;
		//print (totalEnemies.ToString());

		if (totalEnemies <= 0) {
			//print ("shit");
		}

		//debug for timer
		Debug.Log(totalEnemies);

		if(totalEnemies <= 0)
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			player.GetComponent<CircleCollider2D>().enabled = false;
			player.GetComponent<PlayerController>().canShoot = false;
            if (dial != null)
            {
                dial.transform.FindChild("DialogueSystem").GetComponent<DialogueSystemScript>().ActivateDialogue(true);
				spawnWave = false;
				dial = null;
            }

			if (endDial == true)
			{
	           	player.GetComponent<PlayerController>().levelEnded = true;
				//gameObject.SetActive(false);
                levelEnded = true;
			}
		}

		if (spawnWave == true) 
		{
			timer -= Time.deltaTime;
			if (timer <= 0.0f && spawnCounter < patternAmount) {
				//spawn the pattern based on the spawnCounter
				patternLists [spawnCounter].enemyPattern.SetActive (true);
				spawnCounter += 1;
//				// before last pattern
//				if(spawnCounter == patternAmount - 1)
//				{
//					if (dial != null)
//					{
//						dial.transform.FindChild("DialogueSystem").GetComponent<DialogueSystemScript>().ActivateDialogue(true);
//						spawnWave = false;
//					}
//				}

				if (spawnCounter >= patternLists.Length) {
					//break out of function
                    print("Levelended");
                    GameObject.Find("progressBar").GetComponent<ProgressBar>().doProgression = false;
					return;
				} else {
					//reset to the next timer
					timer = patternLists [spawnCounter].waitTime;
				}
			}
		}
	}

    public float GetLevelDuration()
    {
        float totalDuration = 0f;
        for (int i = 0; i < patternLists.Length; ++i)
        {
            totalDuration += patternLists[i].waitTime;
        }
        return totalDuration;
    }

    public void StartProgressBar()
    {
        GameObject.Find("progressBar").GetComponent<ProgressBar>().doProgression = true;
    }
}
