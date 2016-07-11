using UnityEngine;
using System.Collections;

public class CameraDragControl : MonoBehaviour {

	public Camera thecamera;

	public float friction = 5;
	public float elasticity = 5;


	Vector3 camPosOnClick;

	Vector3 clickPositon;
	Vector3 lastFramePositon;

	Vector3 velocity;

	Bounds bound;
	bool isBounded = true;

	bool isDrag = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		#if UNITY_EDITOR
		UseMouse();
		#endif
		#if UNITY_STANDALONE_WIN
		UseMouse();
		#endif

		#if UNITY_ANDROID
		UseTouch();
		#elif UNITY_IOS
		UseTouch();
		#endif

		if (velocity != Vector3.zero) 
		{
			thecamera.transform.position -= velocity;
			velocity = Vector3.Lerp (velocity, Vector3.zero, Time.deltaTime * friction);
		}

		if(!bound.Contains(new Vector3(thecamera.transform.position.x, thecamera.transform.position.y, bound.center.z)) && !isDrag && isBounded)
		{
			velocity = Vector3.zero;
			thecamera.transform.position = Vector3.Lerp (thecamera.transform.position, new Vector3 (bound.center.x, bound.center.y, thecamera.transform.position.z), Time.deltaTime * elasticity);
		}
	}

	void UseMouse()
	{
		if (Input.GetMouseButtonDown (0) && !isDrag) 
		{
			thecamera.GetComponent<CameraControl2D> ().StopFollowing ();
			clickPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			camPosOnClick = thecamera.transform.position;
			velocity = Vector3.zero;
			isDrag = true;
		}
		else if(Input.GetMouseButton (0))
		{
			thecamera.transform.position -= (Camera.main.ScreenToWorldPoint(Input.mousePosition) - clickPositon);
			lastFramePositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
		else if (Input.GetMouseButtonUp (0) && isDrag) 
		{
			velocity = Camera.main.ScreenToWorldPoint (Input.mousePosition) - lastFramePositon;
			isDrag = false;
		}
	}

	void UseTouch()
	{
		foreach(Touch touch in Input.touches) 
		{
			if (touch.phase == TouchPhase.Began && !isDrag) 
			{
				thecamera.GetComponent<CameraControl2D> ().StopFollowing ();
				clickPositon = Camera.main.ScreenToWorldPoint(touch.position);
				camPosOnClick = thecamera.transform.position;
				velocity = Vector3.zero;
				isDrag = true;

				lastFramePositon = Camera.main.ScreenToWorldPoint(touch.position);
			} 
			else if (touch.phase == TouchPhase.Moved) 
			{
				thecamera.transform.position -= (Camera.main.ScreenToWorldPoint(touch.position) - clickPositon);
				lastFramePositon = Camera.main.ScreenToWorldPoint(touch.position);
			}
			else if (touch.phase == TouchPhase.Ended && isDrag)
			{
				velocity = Camera.main.ScreenToWorldPoint (touch.position) - lastFramePositon;
				isDrag = false;
			}
			break;
		}
	}

	public void SetBounds(Bounds bound)
	{
		this.bound = bound;
	}

	public void SetBoundModeOn(bool b)
	{
		isBounded = b;
	}
}
