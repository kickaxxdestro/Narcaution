using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WorldTransition : MonoBehaviour {

	Transform worldOne;
	Transform worldTwo;
	Transform worldThree;
	Transform worldFour;
	Transform worldFive;
	
	void Start ()
	{
		worldOne = transform.FindChild("World1");
		worldTwo = transform.FindChild("World2");
		worldThree = transform.FindChild("World3");	
		worldFour = transform.FindChild("World4");
		worldFive = transform.FindChild("World5");
	}
	
	// Update is called once per frame
	void Update () {
		
		if(PlayerPrefs.GetInt("ppWorld1Boss") == 1)
		{
			worldTwo.GetComponent<Button>().interactable = true;
		}
		if(PlayerPrefs.GetInt("ppWorld2Boss") == 1)
		{
			worldThree.GetComponent<Button>().interactable = true;
		}
		if(PlayerPrefs.GetInt("ppWorld3Boss") == 1)
		{
			worldFour.GetComponent<Button>().interactable = true;
		}
		if(PlayerPrefs.GetInt("ppWorld4Boss") == 1)
		{
			worldFive.GetComponent<Button>().interactable = true;
		}
		
	}
}
