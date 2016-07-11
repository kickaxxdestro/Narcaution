﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorldListManager : MonoBehaviour {

    public GameObject[] worldList;
    public float transitionZoomAmount = 1.1f;
    public Color disabledColor = Color.gray;
    public Sprite disabledLevelButtonSpriteWorld1;
    public Sprite disabledLevelButtonSpriteWorld2;
    public Sprite disabledLevelButtonSpriteWorld3;
    public Sprite disabledLevelButtonSpriteWorld4;
    public Sprite disabledLevelButtonSpriteWorld5;

    public GameObject header;
	public GameObject map;
    float transitionZoomAmountOrigin;
    float transitionZoomAmountOriginY;
    public float transitionZoomSpeed = 0.005f;
    GameObject selectedWorld;
    bool transitioning = false;
    bool zoomDir;//true = zoom in, false = zoom out
    int currentLevel;
    bool currentSelectionType = false;

	// Use this for initialization
	void Start () {
        transitionZoomAmountOrigin = transform.localScale.x;
        transitionZoomAmountOriginY = transform.localScale.y;
        currentLevel = PlayerPrefs.GetInt("ppCurrentLevel", 1);

		if (currentLevel <= 4)
            worldList[1].GetComponent<WorldButton>().Disable();
        if (currentLevel <= 8)
            worldList[2].GetComponent<WorldButton>().Disable();
        if (currentLevel <= 12)
            worldList[3].GetComponent<WorldButton>().Disable();
        if (currentLevel <= 16)
            worldList[4].GetComponent<WorldButton>().Disable();

		this.gameObject.GetComponent<CameraDragControl> ().SetBounds (this.gameObject.GetComponent<BoxCollider2D> ().bounds);
		this.gameObject.GetComponent<BoxCollider2D> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (transitioning)
            UpdateTransitions();
	}

    void UpdateTransitions()
    {
        if(zoomDir)
        {
            float newScale = 0.0f;
            float newScaleY = 0.0f;
            if(transitionZoomAmountOrigin > transitionZoomAmountOriginY)
            {
                newScale = Mathf.Lerp(this.transform.localScale.x, transitionZoomAmount, Time.deltaTime * 3);
                newScaleY = Mathf.Lerp(this.transform.localScale.y, (transitionZoomAmount * (transitionZoomAmountOriginY/transitionZoomAmountOrigin)), Time.deltaTime * 3);
            }
            else if (transitionZoomAmountOrigin < transitionZoomAmountOriginY)
            {
                newScale = Mathf.Lerp(this.transform.localScale.x, (transitionZoomAmount * (transitionZoomAmountOrigin/transitionZoomAmountOriginY)), Time.deltaTime * 3);
                newScaleY = Mathf.Lerp(this.transform.localScale.y, transitionZoomAmount, Time.deltaTime * 3);
            }

            this.transform.localScale = new Vector3(newScale, newScaleY, this.transform.localScale.z);

			map.transform.position = Vector3.Lerp (map.transform.position, selectedWorld.transform.position, Time.deltaTime * 3);

			if (this.transform.localScale.x >= transitionZoomAmount - 0.0001f || this.transform.localScale.y >= transitionZoomAmount - 0.0001f)
            {
                transitioning = false;
                this.transform.localScale = new Vector3(transitionZoomAmount, transitionZoomAmount);

				this.gameObject.GetComponent<CameraDragControl> ().SetBounds (selectedWorld.GetComponent<SpriteRenderer>().bounds);
				this.gameObject.GetComponent<CameraDragControl> ().SetBoundModeOn (true);
            }
            //scrollRectScript.GetComponent<ScrollRect>().enabled = false;
        }
        else
        {
            float newScale = Mathf.Lerp(this.transform.localScale.x, transitionZoomAmountOrigin, Time.deltaTime * 3);
            float newScaleY = Mathf.Lerp(this.transform.localScale.y, transitionZoomAmountOriginY, Time.deltaTime * 3);

            this.transform.localScale = new Vector3(newScale, newScaleY, this.transform.localScale.z);

			map.transform.position = Vector3.Lerp (map.transform.position, transform.position, Time.deltaTime * 3);

			if (this.transform.localScale.x <= transitionZoomAmountOrigin + 0.0001f || this.transform.localScale.y <= transitionZoomAmountOriginY + 0.0001f)
            {
                transitioning = false;
                this.transform.localScale = new Vector3(transitionZoomAmountOrigin, transitionZoomAmountOrigin);

				this.gameObject.GetComponent<BoxCollider2D> ().enabled = true;
				this.gameObject.GetComponent<CameraDragControl> ().SetBounds (this.gameObject.GetComponent<BoxCollider2D> ().bounds);
				this.gameObject.GetComponent<BoxCollider2D> ().enabled = false;

				this.gameObject.GetComponent<CameraDragControl> ().SetBoundModeOn (true);
            }
            //scrollRectScript.GetComponent<ScrollRect>().enabled = true;
        }
    }

    //Fade out all world objects excluding currently selected object
    public void DoSelectedTransitions(GameObject selectedWorld)
    {
        this.selectedWorld = selectedWorld;

        foreach(GameObject world in worldList)
        {
            if (world == selectedWorld)
            {
                world.GetComponent<WorldButton>().DoFadeOutDisplay();
                continue;
            }
            world.GetComponent<WorldButton>().DoOutTransition();
        }

        zoomDir = true;
        transitioning = true;
        currentSelectionType = true;
        header.GetComponent<AlphaFader>().DoFadeOut();

		this.gameObject.GetComponent<CameraDragControl> ().SetBoundModeOn (false);
    }

    public void DoDeselectedTransitions()
    {
        foreach(GameObject world in worldList)
        {
            world.GetComponent<WorldButton>().DoInTransition();
        }

        zoomDir = false;
        transitioning = true;
        currentSelectionType = false;
        header.GetComponent<AlphaFader>().DoFadeIn();

		this.gameObject.GetComponent<CameraDragControl> ().SetBoundModeOn (false);
    }

    public void EnableCurrentWorldChildButtons()
    {
        selectedWorld.GetComponent<WorldButton>().EnableChildButtons();
        print("enabled");
    }

    public void DoBackButtonInteraction()
    {
        if (currentSelectionType)
        {
            DoDeselectedTransitions();
            GameObject.Find("Main Camera").GetComponent<CameraControl2D>().InterpolatePositionToZero();
        }
        else
            GameObject.Find("SceneHandler").GetComponent<SceneHandler>().LoadScene("mainMenuSliding");
    }
}

//public class WorldListManager : MonoBehaviour
//{

//    public GameObject[] worldList;
//    public float transitionZoomAmount = 1.1f;
//    public Color disabledColor = Color.gray;
//    public Sprite disabledLevelButtonSprite;

//    public GameObject header;
//    float transitionZoomAmountOrigin;
//    public float transitionZoomSpeed = 0.005f;
//    GameObject selectedWorld;
//    bool transitioning = false;
//    bool zoomDir;//true = zoom in, false = zoom out
//    int currentLevel;
//    bool currentSelectionType = false;

//    // Use this for initialization
//    void Start()
//    {
//        transitionZoomAmountOrigin = transform.localScale.x;
//        currentLevel = PlayerPrefs.GetInt("ppCurrentLevel", 1);

//        if (currentLevel <= 4)
//            worldList[1].GetComponent<WorldButton>().Disable();
//        if (currentLevel <= 8)
//            worldList[2].GetComponent<WorldButton>().Disable();
//        if (currentLevel <= 12)
//            worldList[3].GetComponent<WorldButton>().Disable();
//        if (currentLevel <= 16)
//            worldList[4].GetComponent<WorldButton>().Disable();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (transitioning)
//            UpdateTransitions();
//    }

//    void UpdateTransitions()
//    {
//        if (zoomDir)
//        {
//            float newScale = Mathf.Lerp(this.transform.localScale.x, transitionZoomAmount, Time.deltaTime * 3);

//            this.transform.localScale = new Vector3(newScale, newScale, this.transform.localScale.z);
//            if (this.transform.localScale.x >= transitionZoomAmount)
//            {
//                transitioning = false;
//                this.transform.localScale = new Vector3(transitionZoomAmount, transitionZoomAmount);
//            }
//        }
//        else
//        {
//            float newScale = Mathf.Lerp(this.transform.localScale.x, transitionZoomAmountOrigin, Time.deltaTime * 3);

//            this.transform.localScale = new Vector3(newScale, newScale, this.transform.localScale.z);

//            if (this.transform.localScale.x <= transitionZoomAmountOrigin)
//            {
//                transitioning = false;
//                this.transform.localScale = new Vector3(transitionZoomAmountOrigin, transitionZoomAmountOrigin);
//            }
//        }
//    }

//    //Fade out all world objects excluding currently selected object
//    public void DoSelectedTransitions(GameObject selectedWorld)
//    {
//        this.selectedWorld = selectedWorld;

//        foreach (GameObject world in worldList)
//        {
//            if (world == selectedWorld)
//            {
//                world.GetComponent<WorldButton>().DoFadeOutDisplay();
//                continue;
//            }
//            world.GetComponent<WorldButton>().DoOutTransition();
//        }

//        zoomDir = true;
//        transitioning = true;
//        currentSelectionType = true;
//        header.GetComponent<AlphaFader>().DoFadeOut();
//    }

//    public void DoDeselectedTransitions()
//    {
//        foreach (GameObject world in worldList)
//        {
//            world.GetComponent<WorldButton>().DoInTransition();
//        }

//        zoomDir = false;
//        transitioning = true;
//        currentSelectionType = false;
//        header.GetComponent<AlphaFader>().DoFadeIn();
//    }

//    public void EnableCurrentWorldChildButtons()
//    {
//        selectedWorld.GetComponent<WorldButton>().EnableChildButtons();
//        print("enabled");
//    }

//    public void DoBackButtonInteraction()
//    {
//        if (currentSelectionType)
//        {
//            DoDeselectedTransitions();
//            GameObject.Find("Main Camera").GetComponent<CameraControl2D>().InterpolatePositionToZero();
//        }
//        else
//            GameObject.Find("SceneHandler").GetComponent<SceneHandler>().LoadScene("mainMenuSliding");
//    }
//}