using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColourMaskController : MonoBehaviour {

    public enum COLOURMODE
    {
        COLOURMODE_RAINBOW,
        COLOURMODE_TO_TRANSPARENT,
        COLOURMODE_TO_ALPHA_GREY,
    }
    public float colourChangeFrequency = 1f;
    COLOURMODE mode;
    float maskDuration;
    bool maskActive = false;
    Color maskColor;
    float colourIndex = 0;

	// Use this for initialization
	void Start () {
        //maskColor = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
        if (!maskActive)
            return;
        
        if(maskDuration <= 0f)
        {
            ActivateColourMask(COLOURMODE.COLOURMODE_TO_TRANSPARENT, 10f);
            colourIndex = 0;
            return;
        }

        switch(mode)
        {
            case COLOURMODE.COLOURMODE_RAINBOW:
                //maskColor.r -= (Time.deltaTime * 0.18f);
                //if (maskColor.r < 0f)
                //    maskColor.r = 1f;
                //maskColor.g -= (Time.deltaTime * 0.1f);
                //if (maskColor.g < 0f)
                //    maskColor.g = 1f;
                //maskColor.b += (Time.deltaTime * 0.15f);
                //if (maskColor.b > 1f)
                //    maskColor.b = 0f;
                //GetComponent<SpriteRenderer>().color = maskColor;

                maskColor.r = (Mathf.Sin(colourChangeFrequency * (int)colourIndex + 0) * 127 + 128) / 255;
                maskColor.g = (Mathf.Sin(colourChangeFrequency * (int)colourIndex + 2) * 127 + 128) / 255;
                maskColor.b = (Mathf.Sin(colourChangeFrequency * (int)colourIndex + 4) * 127 + 128) / 255;
                GetComponent<SpriteRenderer>().color = maskColor;


                colourIndex += Time.deltaTime * 60;
                print(colourIndex);
                //if (colourIndex >= 22)
                //    colourIndex = 0;

                maskDuration -= Time.deltaTime;
                break;                       
            case COLOURMODE.COLOURMODE_TO_TRANSPARENT:
                maskColor.a -= (Time.deltaTime * 2f);
                maskColor.r -= (Time.deltaTime * 2f);
                maskColor.g -= (Time.deltaTime * 2f);
                maskColor.b -= (Time.deltaTime * 2f);

                if (maskColor.a <= 0f && maskColor.r <= 0f && maskColor.r <= 0f && maskColor.r <= 0f)
                {
					maskActive = false;
					if(GetComponent<Image> ())
						GetComponent<Image> ().enabled = false;
					else if(GetComponent<SpriteRenderer> ())
						GetComponent<SpriteRenderer> ().enabled = false;
                }
                if (GetComponent<SpriteRenderer>())
                    GetComponent<SpriteRenderer>().color = maskColor;
                else if (GetComponent<Image>())
                    GetComponent<Image>().color = maskColor;
                break;
            case COLOURMODE.COLOURMODE_TO_ALPHA_GREY:
                maskColor.a += (Time.deltaTime * 2f);

                if (maskColor.a >= 0.5f)
                {
                    maskColor.a = 0.5f;
                    maskActive = false;
                }

			if (GetComponent<SpriteRenderer>())
				GetComponent<SpriteRenderer>().color = maskColor;
			else if (GetComponent<Image>())
				GetComponent<Image>().color = maskColor;
                break;
            default:
                break;
        }
	}

    public void DebugTestRainbow()
    {
        ActivateColourMask(COLOURMODE.COLOURMODE_RAINBOW, 10f);
    }

    public void DoToTransparent()
    {
        ActivateColourMask(COLOURMODE.COLOURMODE_TO_TRANSPARENT, 10f);
    }

	public void DoToGrey(float duration)
	{
		ActivateColourMask(COLOURMODE.COLOURMODE_TO_ALPHA_GREY, duration);
	}

    public void ActivateColourMask(COLOURMODE mode, float duration)
    {
        this.mode = mode;
        this.maskDuration = duration;
        maskActive = true;

		if (GetComponent<SpriteRenderer>())
			GetComponent<SpriteRenderer>().color = maskColor;
		else if (GetComponent<Image>())
			GetComponent<Image>().color = maskColor;

        switch (mode)
        {
            case COLOURMODE.COLOURMODE_RAINBOW:
                maskColor = Color.white;
                maskColor.a = 0.5f;
                break;
            case COLOURMODE.COLOURMODE_TO_TRANSPARENT:

                break;
            case COLOURMODE.COLOURMODE_TO_ALPHA_GREY:
                maskColor = Color.black;
                maskColor.a = 0f;
                break;
            default:
                break;
        }

    }
}
