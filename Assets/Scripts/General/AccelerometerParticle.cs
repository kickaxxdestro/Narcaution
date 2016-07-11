using UnityEngine;
using System.Collections;

public class AccelerometerParticle : MonoBehaviour {
	
	public Color[] backgroundColourList;
    public Color[] detailColourList;
	
	int currentColour = 0;
	
	Vector3 currAc;
	Vector3 zeroAc; 
	float acceRange = 3.0f;
	
	// Use this for initialization
	void Start () {
		
		zeroAc = Vector3.zero;
		Input.gyro.enabled = true;
		
        //colorChange = new Color[7];
		
        ////neon yellow
        //colorChange[0] = new Color(0.95f , 0.95f , 0.05f);
		
        ////neon orange
        //colorChange[1] = new Color(1.0f , 0.6f , 0.2f);
		
        ////neon pink
        //colorChange[2] = new Color(0.98f , 0.35f , 0.72f);
		
        ////neon blue
        //colorChange[3] = new Color(0.05f , 0.83f , 0.98f);
		
        ////neon green
        //colorChange[4] = new Color(0.22f , 1.0f , 0.07f);
		
        ////neon red
        //colorChange[5] = new Color(0.86f , 0.0f , 0.28f);
		
        ////neon purple 
        //colorChange[6] = new Color(0.43f , 0.05f , 0.81f);
        //if(GameObject.Find("Start Screen")!= null)
        //{
        //    GameObject.Find("Start Screen").GetComponent<SpriteRenderer>().color = colorChange[bgColor];
        //}

        if (backgroundColourList.Length == 0 || detailColourList.Length == 0)
            this.enabled = false;
        else if(backgroundColourList.Length != detailColourList.Length)
        {
            this.enabled = false;
            print("AccelerometerParticle backgroundColourList and detailColourList should have the same length");
        }

        currentColour = PlayerPrefs.GetInt("ppBackgroundIndex", 0);

        transform.FindChild("Back").GetComponent<SpriteRenderer>().color = backgroundColourList[currentColour];
        transform.FindChild("Detail").GetComponent<SpriteRenderer>().color = detailColourList[currentColour];
	}

    // Update is called once per frame
    void Update()
    {

        currAc = Input.gyro.userAcceleration;

        if (currAc.magnitude > acceRange)
        {
            currentColour = currentColour >= backgroundColourList.Length - 1 ? 0 : currentColour + 1;
            PlayerPrefs.SetInt("ppBackgroundIndex", currentColour);
            PlayerPrefs.Save();
            transform.FindChild("Back").GetComponent<SpriteRenderer>().color = backgroundColourList[currentColour];
            transform.FindChild("Detail").GetComponent<SpriteRenderer>().color = detailColourList[currentColour];

            ////currAc.Normalize();
            //currentColour += 1 ;
            //if(currentColour > 6)
            //{
            //    currentColour = 0;
            //}
            //GameObject.Find("Start Screen").GetComponent<SpriteRenderer>().color = colorChange[currentColour];	

        }
        currAc = zeroAc;

    }	
		
    public void DoShake()
    {
        currentColour = currentColour >= backgroundColourList.Length - 1 ? 0 : currentColour + 1;
        PlayerPrefs.SetInt("ppBackgroundIndex", currentColour);
        PlayerPrefs.Save();
        transform.FindChild("Back").GetComponent<SpriteRenderer>().color = backgroundColourList[currentColour];
        transform.FindChild("Detail").GetComponent<SpriteRenderer>().color = detailColourList[currentColour];
    }
		
}
