using UnityEngine;
using System.Collections;

public class DrugEffectDisplay : MonoBehaviour {

    public Sprite[] effectDisplayList;
    public GameObject tapUI;

    public void DisplayEffect(EffectsHandler.DRUG_EFFECTS type)
    {
        switch(type)
        {
            case EffectsHandler.DRUG_EFFECTS.DRUG_EFFECTS_RAINBOW:
                this.GetComponent<SpriteRenderer>().sprite = effectDisplayList[0];
                break;
            case EffectsHandler.DRUG_EFFECTS.DRUG_EFFECTS_SHAKE:
                this.GetComponent<SpriteRenderer>().sprite = effectDisplayList[1];
                break;
            case EffectsHandler.DRUG_EFFECTS.DRUG_EFFECTS_VIGNETTE_STATIC:
                this.GetComponent<SpriteRenderer>().sprite = effectDisplayList[2];
                break;
            case EffectsHandler.DRUG_EFFECTS.DRUG_EFFECTS_VIGNETTE_FOLLOW:
                this.GetComponent<SpriteRenderer>().sprite = effectDisplayList[3];
                break;
            case EffectsHandler.DRUG_EFFECTS.DRUG_EFFECTS_INVERT:
                this.GetComponent<SpriteRenderer>().sprite = effectDisplayList[4];
                break;
            case EffectsHandler.DRUG_EFFECTS.DRUG_EFFECTS_HALLUCINATE:
                this.GetComponent<SpriteRenderer>().sprite = effectDisplayList[5];
                break;
            case EffectsHandler.DRUG_EFFECTS.DRUG_EFFECTS_SPEED:
                this.GetComponent<SpriteRenderer>().sprite = effectDisplayList[6];
                break;
            default:
                break;
        }
        this.GetComponent<AlphaFader>().fadeColor = Color.white;
        this.GetComponent<AlphaFader>().DoFadeOut(3.5f);
    }

    public void DisplayTapUI(bool on)
    {
        tapUI.SetActive(on);
    }
}
