using UnityEngine;
using System.Collections;

public class ShakeTexture : MonoBehaviour {

    public float shakeAmount = 0.1f;
    float shakeDuration = 10f;
    bool shakeActive = false;
    Vector2 originalPosition;
    
    void Awake()
    {
        originalPosition = GetComponent<Renderer>().material.mainTextureOffset;
        print("awake");
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (!shakeActive)
            return;
        shakeDuration -= Time.fixedDeltaTime;

        if(shakeDuration <= 0f)
        {
            shakeActive = false;
            GetComponent<Renderer>().material.mainTextureOffset = originalPosition;
        }
        else
        {
            Vector2 randomshake = Random.insideUnitCircle;
            //print("offset: " + GetComponent<Renderer>().material.mainTextureOffset);
            GetComponent<Renderer>().material.mainTextureOffset = originalPosition + randomshake * shakeAmount;
        }
	}

    public void DoShake(float duration)
    {
        shakeActive = true;
        shakeDuration = duration;
        print("shake");
    }

    public void SetShakeAmount(float shakeAmount)
    {
        this.shakeAmount = shakeAmount;
    }
}
