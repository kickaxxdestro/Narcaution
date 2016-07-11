using UnityEngine;
using System.Collections;

public class galleryScript : MonoBehaviour {
	
	//control
	Vector3 currentMousePos;
	Vector3 lastMousePos;
	Vector3 deltaMousePos;
	int numberOfTouches = 0;
	
	public float moveSpeed = 1.0f;
	
	// Update is called once per frame
	void Update () {
	
		#if UNITY_EDITOR
		
		//calculating the delta position of the Mouse
		currentMousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		deltaMousePos = currentMousePos - lastMousePos;
		lastMousePos = currentMousePos;
		
		//move the player when mouse is held down
		if (Input.GetMouseButton (0)) {
			transform.position += new Vector3 (deltaMousePos.x, deltaMousePos.y, 0) * moveSpeed;
			//refer the its function for FireBullet
			
		}
		#endif
		
		#if UNITY_ANDROID
		//calculates number of touches in total, mainly for debugging purposes
		numberOfTouches = Input.touchCount;
		
		//move the player based on the deltaPosition of the Touch
		for (int i = 0; i < numberOfTouches; ++i)
		{
			Touch touch = Input.GetTouch(i);
			
			if(touch.phase == TouchPhase.Began)
			{
				currentMousePos = Camera.main.ScreenToWorldPoint(touch.position);
				lastMousePos = currentMousePos;
				
			}
			else if(touch.phase == TouchPhase.Moved)
			{
				currentMousePos = Camera.main.ScreenToWorldPoint(touch.position);
				Vector3 moveDir = currentMousePos - lastMousePos;
				transform.position += new Vector3(moveDir.x, moveDir.y, 0) * moveSpeed;
				lastMousePos = currentMousePos;
				
			}
			
		}
		
	
		#endif
		}
	}
