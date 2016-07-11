using UnityEngine;
using System.Collections;

public class ScrollingBackground : MonoBehaviour {
	
	public float scrollSpeed;

	public float numberOfSprite;
	public float changeDelayValue;
	public float changeDelay;

	public float xOffset;

	void Start(){
		changeDelay = changeDelayValue;
	}

	void Update () {
	//	transform.position += -transform.up * scrollSpeed * Time.deltaTime;
		
		//if(transform.position.y <= -16.14f)
	//	{
	//		transform.position = new Vector3(0, 16.1f, 0);			
	//  }
		if (changeDelay > 0) {
			changeDelay -= Time.deltaTime;
		} else if (changeDelay <= 0) {

			if(xOffset >= 1){
				xOffset = 1 / numberOfSprite;
			}
			else{
				xOffset += (1 / numberOfSprite);
			}
			changeDelay = changeDelayValue;
		}

		Vector2 offset = new Vector2(xOffset, Time.time * scrollSpeed);
		
		GetComponent<Renderer>().material.mainTextureOffset = offset ;
		
	  }
}