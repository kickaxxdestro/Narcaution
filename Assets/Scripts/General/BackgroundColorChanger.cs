using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;

[System.Serializable]
public class BackgroundColorChangerColorList : System.Object{
	public Color color;
	public float normalizedPercentage;
}

public class BackgroundColorChanger : MonoBehaviour {

	public BackgroundColorChangerColorList[] colorList;

	Color StartColor;
	Color EndColor;

	LevelGeneratorScript theLevel;

	float levelDuration;
	float trackerTimer = 0f;
	bool stop = false;
	int nextColorPosInArray = 1;

	// Use this for initialization
	void Start () {
		theLevel = GameObject.FindGameObjectWithTag ("Level").GetComponent<LevelGeneratorScript> ();
		levelDuration = theLevel.GetLevelDuration();

		if (colorList.Length >= 2) 
		{
			StartColor = colorList [0].color;
			EndColor = colorList [1].color;

			GetComponent<SpriteRenderer> ().color = StartColor;
		}
		else
			stop = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (theLevel.spawnWave && !stop) 
		{
			trackerTimer += Time.deltaTime;

			float nTime = trackerTimer / levelDuration; 

			if (nTime > 1)
			{
				nTime = 1;
				stop = true;
				return;
			}

			Debug.Log (1.0f - ((colorList [nextColorPosInArray].normalizedPercentage - nTime) / (colorList [nextColorPosInArray].normalizedPercentage - colorList [nextColorPosInArray - 1].normalizedPercentage)));
			GetComponent<SpriteRenderer> ().color = Color.Lerp (StartColor, EndColor, 1.0f - ((colorList [nextColorPosInArray].normalizedPercentage - nTime) / (colorList [nextColorPosInArray].normalizedPercentage - colorList [nextColorPosInArray-1].normalizedPercentage)));

			if (nTime >= colorList [nextColorPosInArray].normalizedPercentage) 
			{
				StartColor = colorList [nextColorPosInArray].color;
				nextColorPosInArray++;
				if(nextColorPosInArray >= colorList.Length)
				{
					stop = true;
					return;
				}
				EndColor = colorList [nextColorPosInArray].color;
			}
		}
	}
}
