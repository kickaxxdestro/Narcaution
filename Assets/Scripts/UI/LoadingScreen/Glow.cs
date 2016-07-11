using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Glow : MonoBehaviour
{

    // Use this for initialization
    public Image image;
    public float changeSpeed;
    public float minAlpha; //Value cannot be below 0.0f and above 1.0f
    public float maxAlpha; 

    Color v_tempColor;
    bool b_colorIncrease;
    bool b_colorDecrease;
    bool b_colorChange;
   
    void Start()
    {
        v_tempColor = image.color;
        b_colorDecrease = true;
        b_colorIncrease = false;
        b_colorChange = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (b_colorChange == true)
        {
            v_tempColor.r -= changeSpeed * Time.unscaledDeltaTime;
            v_tempColor.g -= changeSpeed * Time.unscaledDeltaTime;
            v_tempColor.b -= changeSpeed * Time.unscaledDeltaTime;
            image.color = v_tempColor;
            if (image.color.r <= minAlpha)
            {
                b_colorChange = false;
            }
        }
        else
        {
            v_tempColor.r += changeSpeed * Time.unscaledDeltaTime;
            v_tempColor.g += changeSpeed * Time.unscaledDeltaTime;
            v_tempColor.b += changeSpeed * Time.unscaledDeltaTime;
            image.color = v_tempColor;

            if (image.color.r >= maxAlpha)
            {
                b_colorChange = true;
            }
        }
    }
}
