using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreenHandler : MonoBehaviour {

    [HideInInspector]
    public string sceneToLoad;
    public GameObject slidingPanel1;
    public GameObject slidingPanel2;
    public float sliderSpeed = 1f;

    float panelOffsetX;
    Vector3 panel1OriginalPos;
    Vector3 panel2OriginalPos;
    float sliderTimer = 0f;

    bool transitioning = false;
    bool transitionDir = true;  //true = in, false = out

    int updateBGM = 0;

    private AudioSource closeSound;
    bool soundPlayed = false;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        closeSound = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start () {
        panelOffsetX =  Screen.width* 1.8f;

        panel1OriginalPos = slidingPanel1.transform.localPosition;
        panel2OriginalPos = slidingPanel2.transform.localPosition;

        slidingPanel1.transform.localPosition = new Vector3(panel1OriginalPos.x - panelOffsetX, slidingPanel1.transform.localPosition.y, slidingPanel1.transform.localPosition.z);
        slidingPanel2.transform.localPosition = new Vector3(panel2OriginalPos.x + panelOffsetX, slidingPanel1.transform.localPosition.y, slidingPanel1.transform.localPosition.z);
    }
	
	// Update is called once per frame
	void Update () {
        //if (GetComponent<Canvas>().worldCamera == null)
        //    GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
	    if(transitioning)
        {
            sliderTimer += Time.fixedDeltaTime;
            if(transitionDir)
            {
                if ((panel1OriginalPos.x - slidingPanel1.transform.localPosition.x) < 300f && !soundPlayed)
                {
                    soundPlayed = true;
                    closeSound.volume = PlayerPrefs.GetFloat("ppSFXVolume", 1.0f);
                    closeSound.Play();
                }

                if ((panel1OriginalPos.x - slidingPanel1.transform.localPosition.x) < Time.fixedDeltaTime * sliderSpeed)
                {
                    slidingPanel1.transform.localPosition = new Vector3(panel1OriginalPos.x, slidingPanel1.transform.localPosition.y, slidingPanel1.transform.localPosition.z);
                    slidingPanel2.transform.localPosition = new Vector3(panel2OriginalPos.x, slidingPanel2.transform.localPosition.y, slidingPanel2.transform.localPosition.z);
                    transitioning = false;
                    sliderTimer = 0f;
                    LoadNextScene();
                }
                else
                {
                    slidingPanel1.transform.localPosition = new Vector3(Mathf.Lerp(panel1OriginalPos.x - panelOffsetX, panel1OriginalPos.x, sliderTimer * sliderSpeed), slidingPanel1.transform.localPosition.y, slidingPanel1.transform.localPosition.z);
                    //slidingPanel1.transform.Translate(Time.fixedDeltaTime * -sliderSpeed, 0f, 0f);
                    slidingPanel2.transform.localPosition = new Vector3(Mathf.Lerp(panel2OriginalPos.x + panelOffsetX, panel2OriginalPos.x, sliderTimer * sliderSpeed), slidingPanel2.transform.localPosition.y, slidingPanel2.transform.localPosition.z);
                }
            }
            else
            {
                if ((slidingPanel1.transform.localPosition.x - (panel1OriginalPos.x - panelOffsetX)) < Time.fixedDeltaTime * sliderSpeed)
                {
                    slidingPanel1.transform.localPosition = new Vector3(panel1OriginalPos.x - panelOffsetX, slidingPanel1.transform.localPosition.y, slidingPanel1.transform.localPosition.z);
                    slidingPanel2.transform.localPosition = new Vector3(panel2OriginalPos.x + panelOffsetX, slidingPanel2.transform.localPosition.y, slidingPanel2.transform.localPosition.z);
                    transitioning = false;
                    sliderTimer = 0f;
                    slidingPanel1.SetActive(false);
                    slidingPanel2.SetActive(false);
                }
                else
                {
                    slidingPanel1.transform.localPosition = new Vector3(Mathf.Lerp(panel1OriginalPos.x, panel1OriginalPos.x - panelOffsetX, sliderTimer * sliderSpeed), slidingPanel1.transform.localPosition.y, slidingPanel1.transform.localPosition.z);
                    //slidingPanel1.transform.Translate(Time.fixedDeltaTime * -sliderSpeed, 0f, 0f);
                    slidingPanel2.transform.localPosition = new Vector3(Mathf.Lerp(panel2OriginalPos.x, panel2OriginalPos.x + panelOffsetX, sliderTimer * sliderSpeed), slidingPanel2.transform.localPosition.y, slidingPanel2.transform.localPosition.z);
                }
            }
        }
        if(updateBGM == 1)
        {
            ++updateBGM;
        }
        else if(updateBGM == 2)
        {
            updateBGM = 0;
            AudioManager.audioManager.UpdateBGM();
        }
	}

    public void DoTransitionIn()
    {
        transitioning = true;
        transitionDir = true;
        slidingPanel1.SetActive(true);
        slidingPanel2.SetActive(true);
    }

    public void DoTransitionOut()
    {
        transitioning = true;
        transitionDir = false;
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
        updateBGM = 1;
    }

    void OnLevelWasLoaded(int level)
    {
        DoTransitionOut();
        soundPlayed = false;
    }
}
