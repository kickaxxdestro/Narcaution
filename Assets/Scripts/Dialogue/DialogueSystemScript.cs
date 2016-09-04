using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class UserDialogue : System.Object{
	public int userChoice = 0;
    public DialogueUserScript user = null;
	public string message = "";
}

public class DialogueSystemScript : MonoBehaviour {
	
	public DialogueUserScript[] allUsers;
	public UserDialogue[] dialogueList;
	
	int messageCounter = 0;
	
	public int dialogueAmount = 0;
	
	Transform dialoguebox;
	Transform nameTitle;
	Transform messageTitle;
	
	bool popIn = false;
	
	GameObject pauseBtn;
	GameObject level;
	
	float dialogueTimer;

	public bool firstStop = false;
	public bool dialogueActive;

	GameObject myplayer;

    SentryAttackBehaviour sentryAttackBehaviour;

	public void ActivateDialogue(bool active)
	{
		if(active == true)
		{
			dialogueActive = true;
			myplayer.GetComponent<PlayerController>().canShoot = false;
			transform.parent.gameObject.SetActive(true);
			pauseBtn.GetComponent<Button>().interactable = true;
			Time.timeScale = 1.0f;
            if (PlayerPrefs.GetInt("ppSentryEquipped", 0) == 1)
            {
                myplayer.GetComponent<PlayerController>().sentry.GetComponent<SentryAttackBehaviour>().fireBullets = false;
            }
			StartCoroutine(TypeMessage());  
		}
		else if (active == false)
        { 
			dialogueActive = false;
			myplayer.GetComponent<PlayerController>().canShoot = true;
			Time.timeScale = 1.0f;
			pauseBtn.GetComponent<Button>().interactable = true;
			messageCounter += 1;
            if (PlayerPrefs.GetInt("ppSentryEquipped", 0) == 1)
            {
                if (firstStop == false)
                {
                    myplayer.GetComponent<PlayerController>().sentry.GetComponent<SentryAttackBehaviour>().fireBullets = true;
                }
            }
			transform.parent.gameObject.SetActive(false);
		}
	}
	public static IEnumerator WaitForRealSeconds(float time)
	{
		float start = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < start + time)
		{
			yield return null;
		}

	}
	
	IEnumerator TypeMessage()
	{
		if(dialogueList[messageCounter].user.userName == "Stop")
		{
			ActivateDialogue(false);
			if(firstStop == false){
				firstStop = true;
				level.GetComponent<LevelGeneratorScript>().spawnWave = true;
				level.GetComponent<LevelGeneratorScript>().StartProgressBar();
				level.GetComponent<LevelGeneratorScript>().CheckTutorial(); 
			}
			else if(firstStop == true && level.GetComponent<LevelGeneratorScript>().endDial == false)
			{
				level.GetComponent<LevelGeneratorScript>().endDial = true;
			}
		}
		else
		{
			nameTitle.GetComponent<Text>().text = dialogueList[messageCounter].user.userName;
			messageTitle.GetComponent<Text>().text = "";
			dialoguebox.GetComponent<Image>().sprite = dialogueList[messageCounter].user.DialogueBox;

            int charCount = 0;
			foreach(char letter in dialogueList[messageCounter].message.ToCharArray())
			{
				messageTitle.GetComponent<Text>().text += letter;
                if (charCount <= 0)
                {
                    transform.parent.GetComponent<AudioSource>().Play();
                    charCount = 4;
                }
                else
                    --charCount;
				yield return StartCoroutine(WaitForRealSeconds(0.01f));
				if(messageTitle.GetComponent<Text>().text != dialogueList[messageCounter].message)
				{
					popIn = false;
				}
				else if(messageTitle.GetComponent<Text>().text == dialogueList[messageCounter].message)
				{
					popIn = true;
				}
			}
		}
	}
	
	void Awake()
	{
		level = GameObject.FindGameObjectWithTag ("Level");
		pauseBtn = GameObject.Find("PauseBtn");
		dialogueAmount = dialogueList.Length;
		nameTitle = transform.FindChild("chatBox").FindChild("nameTitle");
		messageTitle = transform.FindChild("chatBox").FindChild("messageTitle");
		dialoguebox = transform.FindChild("chatBox");

		myplayer = GameObject.Find ("player");
		dialogueTimer = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale != 0f)
		{
			if (messageCounter < dialogueAmount) {

				if (Input.GetMouseButtonDown (0)) {
	//			if(dialogueTimer <= 0f){
					if (popIn == true) {
						messageCounter += 1;
						StopAllCoroutines ();
						StartCoroutine (TypeMessage ());
					} else if (popIn == false) {
						StopAllCoroutines ();
						messageTitle.GetComponent<Text> ().text = dialogueList [messageCounter].message;
						popIn = true;
					}
	//				dialogueTimer = 5.0f;
				}
		}
//			if(popIn == true)
//			{
//				dialogueTimer -= Time.deltaTime;
//			}
//
//			// type next message after 2 seconds
//			if(dialogueTimer <= 0f)
//			{
//				messageCounter += 1;
//				StopAllCoroutines ();
//				StartCoroutine (TypeMessage ());
//				dialogueTimer = 2.0f;
//			}
		} 
	}
}
