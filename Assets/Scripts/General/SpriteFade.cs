using UnityEngine;
using System.Collections;

public class SpriteFade : MonoBehaviour {

    public float freezeDuration = 4f;
    public bool instantFade = false;
    public float fadeInSpeed = 1f;
    public float fadeOutSpeed = 1f;
    public Sprite[] imageList;
    int currentIndex = 1;
    float freezeTimer;
    bool fading = false;
    bool fadeDir; //true = fade in, false = false out
    Color fadeColor = Color.white;

    // Use this for initialization
    void Start()
    {
        freezeTimer = freezeDuration;

    }

    // Update is called once per frame
    void Update()
    {
        if (imageList.Length <= 1)
            return;


        if (fading)
        {
            if (fadeDir)
            {
                fadeColor.a += Time.deltaTime * fadeInSpeed;
                GetComponent<SpriteRenderer>().color = fadeColor;
                if (fadeColor.a >= 1f || instantFade)
                {
                    fading = false;
                    freezeTimer = freezeDuration;
                }
            }
            else
            {
                fadeColor.a -= Time.deltaTime * fadeOutSpeed;
                GetComponent<SpriteRenderer>().color = fadeColor;
                if (fadeColor.a <= 0f || instantFade)
                {
                    fadeDir = true;
                    currentIndex = currentIndex >= imageList.Length - 1 ? 0 : currentIndex + 1;
                    GetComponent<SpriteRenderer>().sprite = imageList[currentIndex];
                }
            }
        }
        else
        {
            freezeTimer -= Time.deltaTime;
            if (freezeTimer <= 0f)
            {
                fading = true;
                fadeDir = false;
            }
        }
    }
}
