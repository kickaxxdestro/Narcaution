using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlphaFader : MonoBehaviour {

    bool isFading = false;
    [HideInInspector]
    public bool fadingDone = false;
    bool fadeDir; //true = fade in, false = fade out
    public Color fadeColor = Color.white;
    public float fadeTime = 1f;

    bool isSprite = true;   //is the target object using sprite renderer or image
	bool isCanvasGroup = true;
	bool isText = true;

	// Use this for initialization
	void Start () {
		if (GetComponent<SpriteRenderer> () == null)
			isSprite = false;
		if (GetComponent<CanvasGroup> () == null)
			isCanvasGroup = false;
		if (GetComponent<Text> () == null)
			isText = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isSprite)
			UpdateSpriteFade ();
		else if (isCanvasGroup)
			UpdateCGFade ();
		else if (isText)
			UpdateTextFade ();
		else
            UpdateImageFade();
	}

    void UpdateSpriteFade()
    {
        if (isFading)
        {
            if (fadeDir)
            {
                fadeColor.a -= (1 / fadeTime) * Time.deltaTime;
                GetComponent<SpriteRenderer>().color = fadeColor;
                if (fadeColor.a <= 0f)
                {
                    isFading = false;
                    fadingDone = true;
                    fadeColor.a = 0f;
                    GetComponent<SpriteRenderer>().color = fadeColor;
                }
            }
            else
            {
                fadeColor.a += (1 / fadeTime) * Time.deltaTime;
                GetComponent<SpriteRenderer>().color = fadeColor;
                if (fadeColor.a >= 1f)
                {
                    isFading = false;
                    fadingDone = true;
                    fadeColor.a = 1f;
                    GetComponent<SpriteRenderer>().color = fadeColor;
                }
            }
        }
    }

    void UpdateImageFade()
    {
        if (isFading)
        {
            if (fadeDir)
            {
                fadeColor.a -= (1 / fadeTime) * Time.deltaTime;
                GetComponent<Image>().color = fadeColor;
                if (fadeColor.a <= 0f)
                {
                    isFading = false;
                    fadingDone = true;
                    fadeColor.a = 0f;
                    GetComponent<Image>().color = fadeColor;
                }
            }
            else
            {
                fadeColor.a += (1 / fadeTime) * Time.deltaTime;
                GetComponent<Image>().color = fadeColor;
                if (fadeColor.a >= 1f)
                {
                    isFading = false;
                    fadingDone = true;
                    fadeColor.a = 1f;
                    GetComponent<Image>().color = fadeColor;
                }
            }
        }
    }

	void UpdateCGFade()
	{
		if (isFading)
		{
			if (fadeDir)
			{
				fadeColor.a -= (1 / fadeTime) * Time.deltaTime;
				GetComponent<CanvasGroup>().alpha = fadeColor.a;
				if (fadeColor.a <= 0f)
				{
					isFading = false;
					fadingDone = true;
					fadeColor.a = 0f;
					GetComponent<CanvasGroup>().alpha = fadeColor.a;
				}
			}
			else
			{
				fadeColor.a += (1 / fadeTime) * Time.deltaTime;
				GetComponent<CanvasGroup>().alpha = fadeColor.a;
				if (fadeColor.a >= 1f)
				{
					isFading = false;
					fadingDone = true;
					fadeColor.a = 1f;
					GetComponent<CanvasGroup>().alpha = fadeColor.a;
				}
			}
		}
	}

	void UpdateTextFade()
	{
		if (isFading)
		{
			if (fadeDir)
			{
				fadeColor.a -= (1 / fadeTime) * Time.deltaTime;
				GetComponent<Text>().color = fadeColor;
				if (fadeColor.a <= 0f)
				{
					isFading = false;
					fadingDone = true;
					fadeColor.a = 0f;
					GetComponent<Text>().color = fadeColor;
				}
			}
			else
			{
				fadeColor.a += (1 / fadeTime) * Time.deltaTime;
				GetComponent<Text>().color = fadeColor;
				if (fadeColor.a >= 1f)
				{
					isFading = false;
					fadingDone = true;
					fadeColor.a = 1f;
					GetComponent<Text>().color = fadeColor;
				}
			}
		}
	}

    public void DoFadeOut()
    {
        isFading = true;
        fadeDir = true;
        fadingDone = false;
    }

    public void DoFadeIn()
    {
        isFading = true;
        fadeDir = false;
        fadingDone = false;
    }

    public void DoFadeOut(float fadeTime)
    {
        isFading = true;
        fadeDir = true;
        fadingDone = false;
        this.fadeTime = fadeTime;
    }

    public void DoFadeIn(float fadeTime)
    {
        isFading = true;
        fadeDir = false;
        fadingDone = false;
        this.fadeTime = fadeTime;
    }
}
