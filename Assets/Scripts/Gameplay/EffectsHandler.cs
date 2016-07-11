using UnityEngine;
using System.Collections;

/// <summary>
/// Handler which manages the activation and deactivation all (drug) effects
/// Each effect has their own boolean as multiple effects can be active at the same time
/// </summary>

public class EffectsHandler : MonoBehaviour {

    public GameObject colourMaskHandler;
    public GameObject backgroundManager;
    public GameObject vignetteStatic;
    public GameObject vignetteFollow;
    public GameObject hallucinationParticleSystem;

    //Vignette static effect
    bool effectVignetteStaticActive = false;
    float effectVignetteStaticTimer = 0f;

    //Vignette follow effect
    bool effectVignetteFollowActive = false;
    float effectVignetteFollowTimer = 0f;

    //Invert effect
    bool effectInvertActive = false;
    float effectInvertTimer = 0f;

    //Hallucinate effect
    bool effectHallucinateActive = false;
    float effectHallucinateTimer = 0f;

    //Speed effect
    bool effectSpeedActive = false;
    float effectSpeedTimer = 0f;

    public enum DRUG_EFFECTS
    {
        DRUG_EFFECTS_RAINBOW,
        DRUG_EFFECTS_SHAKE,
        DRUG_EFFECTS_VIGNETTE_STATIC,
        DRUG_EFFECTS_VIGNETTE_FOLLOW,
        DRUG_EFFECTS_INVERT,
        DRUG_EFFECTS_HALLUCINATE,
        DRUG_EFFECTS_SPEED
    }

	// Use this for initialization
	void Start () {
        if (colourMaskHandler == null)
            colourMaskHandler = GameObject.Find("ColourMask");
	}
	
	// Update is called once per frame
	void Update () {
        //Vignette static effect
        if(effectVignetteStaticActive)
        {
            effectVignetteStaticTimer -= Time.deltaTime;
            if(effectVignetteStaticTimer <= 0f)
            {
                effectVignetteStaticActive = false;
                vignetteStatic.gameObject.SetActive(false);
            }
        }

        //Vignette follow effect
        if (effectVignetteFollowActive)
        {
            effectVignetteFollowTimer -= Time.deltaTime;
            if (effectVignetteFollowTimer <= 0f)
            {
                effectVignetteFollowActive = false;
                vignetteFollow.gameObject.SetActive(false);
            }
        }

        //Invert effect
        if (effectInvertActive)
        {
            effectInvertTimer -= Time.deltaTime;
            if(effectInvertTimer <= 0f)
            {
                effectInvertActive = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().SetReversedControls(false);
            }
        }

        //Hallucinate effect
        if(effectHallucinateActive)
        {
            effectHallucinateTimer -= Time.deltaTime;
            if (effectHallucinateTimer <= 0f)
            {
                effectHallucinateActive = false;
                hallucinationParticleSystem.GetComponent<ParticleSystem>().Stop();
            }
        }

        //Speed effect
        if (effectSpeedActive)
        {
            effectSpeedTimer -= Time.deltaTime;
            if (effectSpeedTimer <= 0f)
            {
                effectSpeedActive = false;
                Time.timeScale = 1f;
            }
        }
	}

    public void ActivateEffect(DRUG_EFFECTS effect, float duration)
    {
        switch (effect)
        {
            case DRUG_EFFECTS.DRUG_EFFECTS_RAINBOW:
                ActivateRainbowEffect(duration);
                break;
            case DRUG_EFFECTS.DRUG_EFFECTS_SHAKE:
                ActivateShakeEffect(duration);
                break;
            case DRUG_EFFECTS.DRUG_EFFECTS_VIGNETTE_STATIC:
                ActivateVignetteStaticEffect(duration);
                break;
            case DRUG_EFFECTS.DRUG_EFFECTS_VIGNETTE_FOLLOW:
                break;
            case DRUG_EFFECTS.DRUG_EFFECTS_INVERT:
                break;
            case DRUG_EFFECTS.DRUG_EFFECTS_HALLUCINATE:
                break;
            case DRUG_EFFECTS.DRUG_EFFECTS_SPEED:
                Time.timeScale = 2f;
                break;
            default:
                break;
        }
    }

    public void ActivateRainbowEffect(float duration)
    {
        colourMaskHandler.GetComponent<ColourMaskController>().ActivateColourMask(ColourMaskController.COLOURMODE.COLOURMODE_RAINBOW, duration);
        GameObject.FindGameObjectWithTag("Player").transform.FindChild("DrugEffectDisplay").GetComponent<DrugEffectDisplay>().DisplayEffect(DRUG_EFFECTS.DRUG_EFFECTS_RAINBOW);
    }

    public void ActivateShakeEffect(float duration)
    {
        backgroundManager.GetComponent<BackgroundHandler>().ActivateCurrentBackgroundShake(duration);
        GameObject.FindGameObjectWithTag("Player").transform.FindChild("DrugEffectDisplay").GetComponent<DrugEffectDisplay>().DisplayEffect(DRUG_EFFECTS.DRUG_EFFECTS_SHAKE);
    }

    public void ActivateVignetteStaticEffect(float duration)
    {
        effectVignetteStaticActive = true;
        effectVignetteStaticTimer = duration;
        vignetteStatic.gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").transform.FindChild("DrugEffectDisplay").GetComponent<DrugEffectDisplay>().DisplayEffect(DRUG_EFFECTS.DRUG_EFFECTS_VIGNETTE_STATIC);
    }

    public void ActivateVignetteFollowEffect(float duration)
    {
        effectVignetteFollowActive = true;
        effectVignetteFollowTimer = duration;
        vignetteFollow.gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").transform.FindChild("DrugEffectDisplay").GetComponent<DrugEffectDisplay>().DisplayEffect(DRUG_EFFECTS.DRUG_EFFECTS_VIGNETTE_FOLLOW);
    }

    public void ActivateInvertEffect(float duration)
    {
        effectInvertActive = true;
        effectInvertTimer = duration;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().SetReversedControls(true);
        GameObject.FindGameObjectWithTag("Player").transform.FindChild("DrugEffectDisplay").GetComponent<DrugEffectDisplay>().DisplayEffect(DRUG_EFFECTS.DRUG_EFFECTS_INVERT);
    }

    public void ActivateHallucinateEffect(float duration)
    {
        effectHallucinateActive = true;
        effectHallucinateTimer = duration;
        hallucinationParticleSystem.GetComponent<ParticleSystem>().Play();
        GameObject.FindGameObjectWithTag("Player").transform.FindChild("DrugEffectDisplay").GetComponent<DrugEffectDisplay>().DisplayEffect(DRUG_EFFECTS.DRUG_EFFECTS_HALLUCINATE);
    }

    public void ActivateSpeedEffect(float duration)
    {
        effectSpeedActive = true;
        effectSpeedTimer = duration;
        Time.timeScale = 2f;
        GameObject.FindGameObjectWithTag("Player").transform.FindChild("DrugEffectDisplay").GetComponent<DrugEffectDisplay>().DisplayEffect(DRUG_EFFECTS.DRUG_EFFECTS_SPEED);
    }

    public void ActivateFrozenEffect()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().SetFrozen(true);
        GameObject.FindGameObjectWithTag("Player").transform.FindChild("DrugEffectDisplay").GetComponent<DrugEffectDisplay>().DisplayTapUI(true);
    }
}
