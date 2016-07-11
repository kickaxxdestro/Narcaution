using UnityEngine;
using System.Collections;

public class BulletDrugEffect : MonoBehaviour {

    public EffectsHandler.DRUG_EFFECTS effect;

    public float effectDuration = 2f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            switch(effect)
            {
                case EffectsHandler.DRUG_EFFECTS.DRUG_EFFECTS_HALLUCINATE:
                    GameObject.Find("EffectsHandler").GetComponent<EffectsHandler>().ActivateHallucinateEffect(effectDuration);
                    break;
                case EffectsHandler.DRUG_EFFECTS.DRUG_EFFECTS_INVERT:
                    GameObject.Find("EffectsHandler").GetComponent<EffectsHandler>().ActivateInvertEffect(effectDuration);
                    break;
                case EffectsHandler.DRUG_EFFECTS.DRUG_EFFECTS_RAINBOW:
                    GameObject.Find("EffectsHandler").GetComponent<EffectsHandler>().ActivateRainbowEffect(effectDuration);
                    break;
                case EffectsHandler.DRUG_EFFECTS.DRUG_EFFECTS_SHAKE:
                    GameObject.Find("EffectsHandler").GetComponent<EffectsHandler>().ActivateShakeEffect(effectDuration);
                    break;
                case EffectsHandler.DRUG_EFFECTS.DRUG_EFFECTS_SPEED:
                    GameObject.Find("EffectsHandler").GetComponent<EffectsHandler>().ActivateSpeedEffect(effectDuration);
                    break;
                case EffectsHandler.DRUG_EFFECTS.DRUG_EFFECTS_VIGNETTE_FOLLOW:
                    GameObject.Find("EffectsHandler").GetComponent<EffectsHandler>().ActivateVignetteFollowEffect(effectDuration);
                    break;
                case EffectsHandler.DRUG_EFFECTS.DRUG_EFFECTS_VIGNETTE_STATIC:
                    GameObject.Find("EffectsHandler").GetComponent<EffectsHandler>().ActivateVignetteStaticEffect(effectDuration);
                    break;
            }
        }
    }
}
