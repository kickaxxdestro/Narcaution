using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TutorialScript : MonoBehaviour {

    public enum TutorialPhases
    {
		preparePhase,
		shootPhase,
		enemyPhase,
		getHitPhase,
		practicePhase,
        rankPhase,
		endPhase,
		backPhase
	};
   	public TutorialPhases currentPhase;

	//[System.NonSerialized]
    public float actionsDone = 0;

    public GameObject offenseDialoguePrefab;
    public GameObject defenseDialoguePrefab;

    GameObject tutorialDialogue;
    public GameObject[] tutorialEnemyWave;
    public GameObject noDamageEnemy;
    float enemyTimer;
	bool spawnShootEnemy = false;

	GameObject player;
	
	GameObject tutInstructions;

	public bool diaActivated;

    bool gamemode; //true = offense, false = defense

    public GameObject levelEndPanel;

    int numClicks = 0;

    void Awake()
    {
    	tutInstructions = GameObject.Find("scoring");
    	player = GameObject.FindGameObjectWithTag("Player");
        currentPhase = TutorialPhases.preparePhase;

        gamemode = PlayerPrefs.GetInt("ppPlayerGamemode", 0) == 0;

        if (gamemode)
            tutorialDialogue = Instantiate(offenseDialoguePrefab) as GameObject;
        else
            tutorialDialogue = Instantiate(defenseDialoguePrefab) as GameObject;

        tutorialDialogue.SetActive(true);
        enemyTimer = 1.0f;
		player.GetComponent<PlayerController>().tutorialCheck = true;

        levelEndPanel.SetActive(false);
    }

    // Use this for initialization
    void Start () 
    {
		PlayerPrefs.SetString("ppSelectedLevel", "TutorialLevel");
		tutorialDialogue.transform.FindChild("DialogueSystem").GetComponent<DialogueSystemScript>().ActivateDialogue(true);
		diaActivated = false;
	}
	
	IEnumerator ChangeStateDelay(TutorialPhases state, float time)
	{
		yield return new WaitForSeconds(time);
		currentPhase = state;
		StopCoroutine("ChangeAIStateDelay");
	}
	
	// Update is called once per frame
	void Update () {
		
        switch (currentPhase)
        {
			case TutorialPhases.preparePhase:
			if(tutorialDialogue.transform.FindChild("DialogueSystem").GetComponent<DialogueSystemScript>().firstStop == true)
			{
				currentPhase = TutorialPhases.shootPhase;
			}
			break;

            case TutorialPhases.shootPhase:

            tutInstructions.GetComponent<Text>().text = "Drag anywhere the Screen to Move Around";
#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
            {
                actionsDone += Time.deltaTime;
            }
#endif
#if UNITY_STANDALONE_WIN
            if (Input.GetMouseButton(0))
            {
                actionsDone += Time.deltaTime;
            }
#endif

#if UNITY_ANDROID
			int numberOfTouches = Input.touchCount;
			//move the player based on the deltaPosition of the Touch
			for (int i = 0; i < numberOfTouches; ++i)
			{
				Touch touch = Input.GetTouch(i);

				if(touch.phase == TouchPhase.Began)
				{
					actionsDone += Time.deltaTime;
				}
				else if(touch.phase == TouchPhase.Moved)
				{
					actionsDone += Time.deltaTime;
				}
			}
#endif

#if UNITY_IOS
int numberOfTouches = Input.touchCount;
			//move the player based on the deltaPosition of the Touch
			for (int i = 0; i < numberOfTouches; ++i)
			{
				Touch touch = Input.GetTouch(i);

				if(touch.phase == TouchPhase.Began)
				{
					actionsDone += Time.deltaTime;
				}
				else if(touch.phase == TouchPhase.Moved)
				{
					actionsDone += Time.deltaTime;
				}
			}
#endif

            //check how much actionsDone have done
			if (actionsDone > 1.0f)
            {
//              currentPhase = TutorialPhases.enemyPhase;
				if(diaActivated == false){
					tutorialDialogue.transform.FindChild("DialogueSystem").GetComponent<DialogueSystemScript>().ActivateDialogue(true);
					diaActivated = true;
				}
				if(diaActivated == true && tutorialDialogue.transform.FindChild("DialogueSystem").GetComponent<DialogueSystemScript>().dialogueActive == false)
				{
					currentPhase = TutorialPhases.enemyPhase;
					diaActivated = false;
				}
            }
            break;

			case TutorialPhases.enemyPhase:
            if (gamemode)
			    tutInstructions.GetComponent<Text>().text = "Kill enemies to Reach 15 Combo";
            else
                tutInstructions.GetComponent<Text>().text = "Kill enemies to Reach 5 Combo";
			
            enemyTimer -= Time.deltaTime;
			if (enemyTimer <= 0.0f && diaActivated == false)
            {
                GameObject go;
                if(gamemode)
				    go = Instantiate(tutorialEnemyWave[Random.Range(0, 3)]) as GameObject;
                else
                    go = Instantiate(noDamageEnemy) as GameObject;
				go.SetActive(true);
                enemyTimer = 1.0f;
            }
            if ((gamemode && player.GetComponent<PlayerController>().comboCount >= 15) || (!gamemode && player.GetComponent<PlayerController>().comboCount >= 5))
            {
//				currentPhase = TutorialPhases.getHitPhase;
//				tutorialDialogue.transform.FindChild("DialogueSystem").GetComponent<DialogueSystemScript>().ActivateDialogue(true);

				if(diaActivated == false){
					tutorialDialogue.transform.FindChild("DialogueSystem").GetComponent<DialogueSystemScript>().ActivateDialogue(true);
					diaActivated = true;
				}
				if(diaActivated == true && tutorialDialogue.transform.FindChild("DialogueSystem").GetComponent<DialogueSystemScript>().dialogueActive == false)
				{
					currentPhase = TutorialPhases.getHitPhase;
					diaActivated = false;
				}
			}
			break;
			
			case TutorialPhases.getHitPhase:
			tutInstructions.GetComponent<Text>().text = "See what happens when you get hit";
			if(diaActivated == false)
			{
				player.GetComponent<PlayerController>().controllable = false;
				if(spawnShootEnemy == false)
				{
					GameObject go = Instantiate(tutorialEnemyWave[4]) as GameObject;
					go.SetActive(true);
					spawnShootEnemy = true;
				}
			}
			if (player.GetComponent<PlayerController>().emotionPoint <= 1)
			{
				actionsDone += Time.deltaTime;
				if(actionsDone > 5.0f)
				{

//					currentPhase = TutorialPhases.practicePhase;
//					tutorialDialogue.transform.FindChild("DialogueSystem").GetComponent<DialogueSystemScript>().ActivateDialogue(true);

					if(diaActivated == false){
						tutorialDialogue.transform.FindChild("DialogueSystem").GetComponent<DialogueSystemScript>().ActivateDialogue(true);
						diaActivated = true;
					}
					if(diaActivated == true && tutorialDialogue.transform.FindChild("DialogueSystem").GetComponent<DialogueSystemScript>().dialogueActive == false)
					{
						//player.GetComponent<PlayerController>().firstShield.GetComponent<SpriteRenderer>().enabled = true;
						currentPhase = TutorialPhases.practicePhase;
						diaActivated = false;
					}
				}
			}
			break;
			
			case TutorialPhases.practicePhase:
            if (gamemode)
                tutInstructions.GetComponent<Text>().text = "Get 30 Combo";
            else
                tutInstructions.GetComponent<Text>().text = "Get 10 Combo";

			if(diaActivated == false){
				player.GetComponent<PlayerController>().controllable = true;
				enemyTimer -= Time.deltaTime;
				if (enemyTimer <= 0.0f)
                {
                    GameObject go;
                    if (gamemode)
                        go = Instantiate(tutorialEnemyWave[Random.Range(0, 4)]) as GameObject;
                    else
                        go = Instantiate(noDamageEnemy) as GameObject;

                    go.SetActive(true);
					enemyTimer = Random.Range(0.5f, 1.0f);
				}
			}
            if ((gamemode && player.GetComponent<PlayerController>().comboCount >= 30) || (!gamemode && player.GetComponent<PlayerController>().comboCount >= 10))
            {
//				tutorialDialogue.transform.FindChild("DialogueSystem").GetComponent<DialogueSystemScript>().ActivateDialogue(true);
//				currentPhase = TutorialPhases.endPhase;

				if(diaActivated == false){
					tutorialDialogue.transform.FindChild("DialogueSystem").GetComponent<DialogueSystemScript>().ActivateDialogue(true);
					diaActivated = true;
				}
				if(diaActivated == true && tutorialDialogue.transform.FindChild("DialogueSystem").GetComponent<DialogueSystemScript>().dialogueActive == false)
				{
					currentPhase = TutorialPhases.rankPhase;
					diaActivated = false;
                    levelEndPanel.SetActive(true);
                    player.GetComponent<PlayerController>().controllable = false;
				}
			}
			break;

            case TutorialPhases.rankPhase:
            tutInstructions.GetComponent<Text>().text = "Learn about the ranking system";

#if UNITY_STANDALONE_WIN
            if (Input.GetMouseButtonDown(0))
            {
                ++numClicks;
                UpdateLevelEndPanelDisplay();
            }
#endif
#if UNITY_ANDROID
        foreach (Touch mytouch in Input.touches)
        {
            if (mytouch.phase == TouchPhase.Began)
            {
                 ++numClicks;
                UpdateLevelEndPanelDisplay();
                break;
            }
        }
#elif UNITY_IOS
        foreach (Touch mytouch in Input.touches)
        {
            if (mytouch.phase == TouchPhase.Began)
            {
                ++numClicks;
                UpdateLevelEndPanelDisplay();
                break;
            }
        }
#endif
            if (numClicks >= 6)
            {
                levelEndPanel.SetActive(false);
                if (diaActivated == false)
                {
                    tutorialDialogue.transform.FindChild("DialogueSystem").GetComponent<DialogueSystemScript>().ActivateDialogue(true);
                    diaActivated = true;
                }
                if (diaActivated == true && tutorialDialogue.transform.FindChild("DialogueSystem").GetComponent<DialogueSystemScript>().dialogueActive == false)
                {
                    currentPhase = TutorialPhases.endPhase;
                    diaActivated = false;
                    player.GetComponent<PlayerController>().controllable = true;
                }

            }


            break;
			case TutorialPhases.endPhase:
			tutInstructions.GetComponent<Text>().text = "End of Tutorial";
            print("Endphase");
			//set tutorial complete flag
			PlayerPrefs.SetInt("ppFirstPlay", 1);
            PlayerPrefs.Save();
			Social.ReportProgress("CgkI__bt5ooSEAIQAQ", 100.0f, (bool success) =>{
				
			});
			if(PlayerPrefs.GetInt("ppFirstPlay") > 0)
			{
				currentPhase = TutorialPhases.backPhase;
			}
			break;
			
			case TutorialPhases.backPhase:
			actionsDone += Time.deltaTime;
			if(actionsDone > 4.0f)
			{
                GameObject.Find("SceneHandler").GetComponent<SceneHandler>().LoadScene("mainMenuSliding");
			}
			break;
        }
    }

    void UpdateLevelEndPanelDisplay()
    {
        if (numClicks == 1)
        {
            levelEndPanel.transform.FindChild("Tut1").gameObject.SetActive(true);
            levelEndPanel.transform.FindChild("Tut2").gameObject.SetActive(false);
            levelEndPanel.transform.FindChild("Tut3").gameObject.SetActive(false);
            levelEndPanel.transform.FindChild("Tut4").gameObject.SetActive(false);
        }
        else if(numClicks == 2)
        {
            levelEndPanel.transform.FindChild("Tut1").gameObject.SetActive(false);
            levelEndPanel.transform.FindChild("Tut2").gameObject.SetActive(true);
            levelEndPanel.transform.FindChild("Tut3").gameObject.SetActive(false);
            levelEndPanel.transform.FindChild("Tut4").gameObject.SetActive(false);
        }
        else if (numClicks == 3)
        {
            levelEndPanel.transform.FindChild("Tut1").gameObject.SetActive(false);
            levelEndPanel.transform.FindChild("Tut2").gameObject.SetActive(false);
            levelEndPanel.transform.FindChild("Tut3").gameObject.SetActive(true);
            levelEndPanel.transform.FindChild("Tut4").gameObject.SetActive(false);
        }
        else if(numClicks == 4)
        {
            levelEndPanel.transform.FindChild("Tut1").gameObject.SetActive(false);
            levelEndPanel.transform.FindChild("Tut2").gameObject.SetActive(false);
            levelEndPanel.transform.FindChild("Tut3").gameObject.SetActive(false);
            levelEndPanel.transform.FindChild("Tut4").gameObject.SetActive(true);
        }
        else if (numClicks == 5)
        {
            levelEndPanel.transform.FindChild("Tut1").gameObject.SetActive(false);
            levelEndPanel.transform.FindChild("Tut2").gameObject.SetActive(false);
            levelEndPanel.transform.FindChild("Tut3").gameObject.SetActive(false);
            levelEndPanel.transform.FindChild("Tut4").gameObject.SetActive(false);
        }
    }

    public void SkipTutorial()
    {
        PlayerPrefs.SetInt("ppFirstPlay", 1);
        PlayerPrefs.Save();
        GameObject.Find("SceneHandler").GetComponent<SceneHandler>().LoadScene("levelSelect");

    }
}
