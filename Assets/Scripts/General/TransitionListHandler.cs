using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Transition : System.Object
{
    public enum TransitionType
    {
        TRANSITION_FADE_IN_IMAGE,
        TRANSITION_FADE_OUT_IMAGE,
        TRANSITION_INSTANT_IN,
        TRANSITION_INSTANT_OUT,
        TRANSITION_TRANSLATE,
        TRANSITION_SCALE,
        TRANSITION_ROTATE,
        TRANSITION_WAIT,
        TRANSITION_FADE_IN_TEXT,
        TRANSITION_FADE_OUT_TEXT,
    }
    public GameObject targetObject;
    public TransitionType transitionType;
    public float transitionTime = 0f;
    public Vector3 transformAmount;
    public float fadeAmount = 1f;

    public bool doWithPrevious = false;

    public bool transitionDone = false;
}

public class TransitionItem : System.Object
{
    //Index of this transition
    public int index;

    //Time remaining for this transition
    public float timer;

    //Color var for fade in/out
    public Color fadeColor;
}

public class TransitionListHandler : MonoBehaviour 
{
    //List of transitions
    public List<Transition> transitionList;

    //Id of this transition list
    public int ListID;

    [System.NonSerialized]

    //All transitions have finished
    bool finished = false;

    //List of active transtions
    List<TransitionItem> activeTransitionList;

    [HideInInspector]
    public GameObject tapToContinueIcon;

	// Use this for initialization
	void Start () 
    {

        activeTransitionList = new List<TransitionItem>();
        
        TransitionItem tItem = new TransitionItem();
        tItem.index = 0;
        tItem.timer = transitionList[0].transitionTime;
        activeTransitionList.Add(tItem);
        print("trans start");
	}
	
	// Update is called once per frame
	void Update () 
    {
        CheckTouch();

        if (finished)
            return;

        for (int i = 0; i < activeTransitionList.Count; ++i)
        {
            if (transitionList[activeTransitionList[i].index].transitionDone)
                continue;
            switch (transitionList[activeTransitionList[i].index].transitionType)
            {
                case Transition.TransitionType.TRANSITION_FADE_IN_IMAGE:
                    activeTransitionList[i].fadeColor.a += (transitionList[activeTransitionList[i].index].fadeAmount / transitionList[activeTransitionList[i].index].transitionTime) * Time.deltaTime;

                    transitionList[activeTransitionList[i].index].targetObject.GetComponent<Image>().color = activeTransitionList[i].fadeColor;

                    activeTransitionList[i].timer -= Time.deltaTime;

                    break;
                case Transition.TransitionType.TRANSITION_FADE_OUT_IMAGE:
                    activeTransitionList[i].fadeColor.a -= (transitionList[activeTransitionList[i].index].fadeAmount / transitionList[activeTransitionList[i].index].transitionTime) * Time.deltaTime;
                    transitionList[activeTransitionList[i].index].targetObject.GetComponent<Image>().color = activeTransitionList[i].fadeColor;

                    activeTransitionList[i].timer -= Time.deltaTime;
                    break;
                case Transition.TransitionType.TRANSITION_FADE_IN_TEXT:
                    activeTransitionList[i].fadeColor.a += (transitionList[activeTransitionList[i].index].fadeAmount / transitionList[activeTransitionList[i].index].transitionTime) * Time.deltaTime;

                    transitionList[activeTransitionList[i].index].targetObject.GetComponent<Text>().color = activeTransitionList[i].fadeColor;

                    activeTransitionList[i].timer -= Time.deltaTime;

                    break;
                case Transition.TransitionType.TRANSITION_FADE_OUT_TEXT:
                    activeTransitionList[i].fadeColor.a -= (transitionList[activeTransitionList[i].index].fadeAmount / transitionList[activeTransitionList[i].index].transitionTime) * Time.deltaTime;
                    transitionList[activeTransitionList[i].index].targetObject.GetComponent<Text>().color = activeTransitionList[i].fadeColor;

                    activeTransitionList[i].timer -= Time.deltaTime;
                    break;
                case Transition.TransitionType.TRANSITION_INSTANT_IN:
                    transitionList[activeTransitionList[i].index].targetObject.SetActive(true);
                    activeTransitionList[i].fadeColor = Color.white;
                    activeTransitionList[i].fadeColor.a = 1f;

                    transitionList[activeTransitionList[i].index].targetObject.GetComponent<Image>().color = activeTransitionList[i].fadeColor;

                    transitionList[activeTransitionList[i].index].transitionDone = true;
                    break;
                case Transition.TransitionType.TRANSITION_INSTANT_OUT:
                    transitionList[activeTransitionList[i].index].targetObject.SetActive(false);

                    transitionList[activeTransitionList[i].index].transitionDone = true;
                    break;
                case Transition.TransitionType.TRANSITION_TRANSLATE:
                    transitionList[activeTransitionList[i].index].targetObject.transform.localPosition += new Vector3((transitionList[activeTransitionList[i].index].transformAmount.x / transitionList[activeTransitionList[i].index].transitionTime) * Time.deltaTime
                   , (transitionList[activeTransitionList[i].index].transformAmount.y / transitionList[activeTransitionList[i].index].transitionTime) * Time.deltaTime, (transitionList[activeTransitionList[i].index].transformAmount.z / transitionList[activeTransitionList[i].index].transitionTime) * Time.deltaTime);

                    activeTransitionList[i].timer -= Time.deltaTime;
                    break;
                case Transition.TransitionType.TRANSITION_SCALE:
                    transitionList[activeTransitionList[i].index].targetObject.transform.localScale += new Vector3((transitionList[activeTransitionList[i].index].transformAmount.x / transitionList[activeTransitionList[i].index].transitionTime) * Time.deltaTime
                  , (transitionList[activeTransitionList[i].index].transformAmount.y / transitionList[activeTransitionList[i].index].transitionTime) * Time.deltaTime, (transitionList[activeTransitionList[i].index].transformAmount.z / transitionList[activeTransitionList[i].index].transitionTime) * Time.deltaTime);

                    activeTransitionList[i].timer -= Time.deltaTime;
                    break;
                case Transition.TransitionType.TRANSITION_ROTATE:
                    transitionList[activeTransitionList[i].index].targetObject.transform.eulerAngles += new Vector3((transitionList[activeTransitionList[i].index].transformAmount.x / transitionList[activeTransitionList[i].index].transitionTime) * Time.deltaTime
                  , (transitionList[activeTransitionList[i].index].transformAmount.y / transitionList[activeTransitionList[i].index].transitionTime) * Time.deltaTime, (transitionList[activeTransitionList[i].index].transformAmount.z / transitionList[activeTransitionList[i].index].transitionTime) * Time.deltaTime);

                    activeTransitionList[i].timer -= Time.deltaTime;
                    break;
                case Transition.TransitionType.TRANSITION_WAIT:
                    activeTransitionList[i].timer -= Time.deltaTime;
                    break;
            }
            if (activeTransitionList[i].timer <= 0f)
                transitionList[activeTransitionList[i].index].transitionDone = true;
        }

        CheckTimer();
    }

    //Check for touch input
    void CheckTouch()
    {
#if UNITY_EDITOR
         if (Input.GetMouseButtonDown(0))
        {
            DoTouchInteraction();
        }
#endif
#if UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0))
        {
            DoTouchInteraction();
        }
#endif
#if UNITY_ANDROID
        foreach (Touch mytouch in Input.touches)
        {
            if (mytouch.phase == TouchPhase.Began)
            {
                DoTouchInteraction();
                return;
            }
        }
#endif
#if UNITY_IOS
        foreach (Touch mytouch in Input.touches)
        {
            if (mytouch.phase == TouchPhase.Began)
            {
                DoTouchInteraction();
                return;
            }
        }
#endif
    }
        
    void DoTouchInteraction()
    {
        if (finished)
        {
            if(PlayerPrefs.GetInt("ppCutsceneNext", 0) == 0)
                GameObject.Find("SceneHandler").GetComponent<SceneHandler>().LoadScene("gameScene");
            else if (PlayerPrefs.GetInt("ppCutsceneNext", 0) == 1)
                GameObject.Find("SceneHandler").GetComponent<SceneHandler>().LoadScene("galleryNew");
            else if(PlayerPrefs.GetInt("ppCutsceneNext", 0) == 3)
                GameObject.Find("SceneHandler").GetComponent<SceneHandler>().LoadScene("levelSelect");
            else if (PlayerPrefs.GetInt("ppCutsceneNext", 0) == 4)
                GameObject.Find("SceneHandler").GetComponent<SceneHandler>().LoadScene("gameEnd");
        }
        else
            SkipToNextTransition();
    }

    //Check if all active transitions have completed 
    void CheckTimer()
    {

        for (int i = 0; i < activeTransitionList.Count; ++i)
        {
            if (!transitionList[activeTransitionList[i].index].transitionDone)
            {
                return;
            }
        }
        GoToNextTransition();
    }

    void SkipToNextTransition()
    {
        for (int i = 0; i < activeTransitionList.Count; ++i)
        {
            if (activeTransitionList[i].timer <= 0f)
                continue;
            switch (transitionList[activeTransitionList[i].index].transitionType)
            {
                case Transition.TransitionType.TRANSITION_FADE_IN_IMAGE:
                    activeTransitionList[i].fadeColor.a += (transitionList[activeTransitionList[i].index].fadeAmount / transitionList[activeTransitionList[i].index].transitionTime) 
                        * (activeTransitionList[i].timer / Time.deltaTime);

                    transitionList[activeTransitionList[i].index].targetObject.GetComponent<Image>().color = activeTransitionList[i].fadeColor;
                    break;
                case Transition.TransitionType.TRANSITION_FADE_OUT_IMAGE:
                    activeTransitionList[i].fadeColor.a -= (transitionList[activeTransitionList[i].index].fadeAmount / transitionList[activeTransitionList[i].index].transitionTime)
                        * (activeTransitionList[i].timer / Time.deltaTime);
                    transitionList[activeTransitionList[i].index].targetObject.GetComponent<Image>().color = activeTransitionList[i].fadeColor;
                    break;
                case Transition.TransitionType.TRANSITION_FADE_IN_TEXT:
                    activeTransitionList[i].fadeColor.a += (transitionList[activeTransitionList[i].index].fadeAmount / transitionList[activeTransitionList[i].index].transitionTime) 
                        * (activeTransitionList[i].timer / Time.deltaTime);

                    transitionList[activeTransitionList[i].index].targetObject.GetComponent<Text>().color = activeTransitionList[i].fadeColor;
                    break;
                case Transition.TransitionType.TRANSITION_FADE_OUT_TEXT:
                    activeTransitionList[i].fadeColor.a -= (transitionList[activeTransitionList[i].index].fadeAmount / transitionList[activeTransitionList[i].index].transitionTime)
                        * (activeTransitionList[i].timer / Time.deltaTime);

                    transitionList[activeTransitionList[i].index].targetObject.GetComponent<Text>().color = activeTransitionList[i].fadeColor;
                    break;
                case Transition.TransitionType.TRANSITION_INSTANT_IN:
                    transitionList[activeTransitionList[i].index].targetObject.SetActive(true);
                    break;
                case Transition.TransitionType.TRANSITION_INSTANT_OUT:
                    transitionList[activeTransitionList[i].index].targetObject.SetActive(false);
                    break;
                case Transition.TransitionType.TRANSITION_TRANSLATE:
                    transitionList[activeTransitionList[i].index].targetObject.transform.localPosition += new Vector3((transitionList[activeTransitionList[i].index].transformAmount.x / (transitionList[activeTransitionList[i].index].transitionTime / Time.deltaTime)) * (activeTransitionList[i].timer / Time.deltaTime)
                   , (transitionList[activeTransitionList[i].index].transformAmount.y / (transitionList[activeTransitionList[i].index].transitionTime / Time.deltaTime)) * (activeTransitionList[i].timer / Time.deltaTime), (transitionList[activeTransitionList[i].index].transformAmount.z / (transitionList[activeTransitionList[i].index].transitionTime / Time.deltaTime)) * (activeTransitionList[i].timer / Time.deltaTime));

                    break;
                case Transition.TransitionType.TRANSITION_SCALE:
                    transitionList[activeTransitionList[i].index].targetObject.transform.localScale += new Vector3((transitionList[activeTransitionList[i].index].transformAmount.x / (transitionList[activeTransitionList[i].index].transitionTime / Time.deltaTime)) * (activeTransitionList[i].timer / Time.deltaTime)
                   , (transitionList[activeTransitionList[i].index].transformAmount.y / (transitionList[activeTransitionList[i].index].transitionTime / Time.deltaTime)) * (activeTransitionList[i].timer / Time.deltaTime), (transitionList[activeTransitionList[i].index].transformAmount.z / (transitionList[activeTransitionList[i].index].transitionTime / Time.deltaTime)) * (activeTransitionList[i].timer / Time.deltaTime));
                    break;
                case Transition.TransitionType.TRANSITION_ROTATE:
                    transitionList[activeTransitionList[i].index].targetObject.transform.eulerAngles += new Vector3((transitionList[activeTransitionList[i].index].transformAmount.x / (transitionList[activeTransitionList[i].index].transitionTime / Time.deltaTime)) * (activeTransitionList[i].timer / Time.deltaTime)
                  , (transitionList[activeTransitionList[i].index].transformAmount.y / (transitionList[activeTransitionList[i].index].transitionTime / Time.deltaTime)) * (activeTransitionList[i].timer / Time.deltaTime), (transitionList[activeTransitionList[i].index].transformAmount.z / (transitionList[activeTransitionList[i].index].transitionTime / Time.deltaTime)) * (activeTransitionList[i].timer / Time.deltaTime));

                    break;
                case Transition.TransitionType.TRANSITION_WAIT:
                    break;
            }
        }
        GoToNextTransition();
    }

    void GoToNextTransition()
    {
        if (activeTransitionList[activeTransitionList.Count - 1].index < transitionList.Count - 1)
        {
            TransitionItem tItem = new TransitionItem();
            tItem.index = activeTransitionList[activeTransitionList.Count - 1].index + 1;
            tItem.timer = transitionList[tItem.index].transitionTime;

            if (transitionList[tItem.index].transitionType == Transition.TransitionType.TRANSITION_FADE_IN_IMAGE || transitionList[tItem.index].transitionType == Transition.TransitionType.TRANSITION_FADE_OUT_IMAGE)
                tItem.fadeColor = transitionList[tItem.index].targetObject.GetComponent<Image>().color;
            else if (transitionList[tItem.index].transitionType == Transition.TransitionType.TRANSITION_FADE_IN_TEXT || transitionList[tItem.index].transitionType == Transition.TransitionType.TRANSITION_FADE_OUT_TEXT)
                tItem.fadeColor = transitionList[tItem.index].targetObject.GetComponent<Text>().color;

            activeTransitionList.Clear();

            activeTransitionList.Add(tItem);

            int nextIndex = 1;

            bool doLoop = true;
            while (doLoop)
            {
                if (tItem.index + nextIndex >= transitionList.Count)
                    doLoop = false;
                else if (transitionList[tItem.index + nextIndex].doWithPrevious)
                {
                    TransitionItem tItem2 = new TransitionItem();
                    tItem2.index = tItem.index + nextIndex;
                    tItem2.timer = transitionList[tItem2.index].transitionTime;

                    if (transitionList[tItem.index + nextIndex].transitionType == Transition.TransitionType.TRANSITION_FADE_IN_IMAGE || transitionList[tItem.index + nextIndex].transitionType == Transition.TransitionType.TRANSITION_FADE_OUT_IMAGE)
                        tItem2.fadeColor = transitionList[tItem.index + nextIndex].targetObject.GetComponent<Image>().color;
                    else if (transitionList[tItem.index + nextIndex].transitionType == Transition.TransitionType.TRANSITION_FADE_IN_TEXT || transitionList[tItem.index + nextIndex].transitionType == Transition.TransitionType.TRANSITION_FADE_OUT_TEXT)
                        tItem2.fadeColor = transitionList[tItem.index + nextIndex].targetObject.GetComponent<Text>().color;

                    activeTransitionList.Add(tItem2);
                }
                else
                    doLoop = false;

                ++nextIndex;
            }

        }
        else
        {
            finished = true;
            tapToContinueIcon.SetActive(true);
        }
    }
}
