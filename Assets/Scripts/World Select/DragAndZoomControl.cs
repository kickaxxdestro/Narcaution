using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragAndZoomControl : MonoBehaviour {

	public Camera thecamera;

	public float friction = 5;
	public float elasticity = 5;

	Vector3 clickPositon;
	Vector3 lastFramePositon;

	Vector3 velocity;

	Bounds bound;
	bool isBounded = true;

	bool isDrag = false;

	float lastFrameFingerDistance;

	public float zoomScale = 0.01f;

	public float maxZoomValue = 0.01f;

	public float minZoomValue = 0.01f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*if (Camera.main.gameObject.GetComponent<CameraControl2D>().currentMovement == CameraControl2D.MOVEMENT_TYPE.MOVEMENT_TYPE_NONE)*/
		{
			#if UNITY_EDITOR
			UseMouse ();
			#elif UNITY_STANDALONE_WIN
			UseMouse();
			#elif UNITY_ANDROID
			UseTouch ();
			#elif UNITY_IOS
			UseTouch();
			#endif
		}

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
		if(!EventSystem.current.IsPointerOverGameObject ())
		{
			if (Input.GetMouseButtonDown (0) && !isDrag) 
			{
				thecamera.GetComponent<CameraControl2D> ().StopFollowing ();
				clickPositon = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				velocity = Vector3.zero;
				isDrag = true;
			}
			else if(Input.GetMouseButton (0) && isDrag)
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
	}

	void UseTouch()
	{
		if(Input.touches.Length == 1 && !EventSystem.current.IsPointerOverGameObject (Input.touches [0].fingerId))
		{
			if (Input.touches [0].phase == TouchPhase.Began && !isDrag) 
			{
				thecamera.GetComponent<CameraControl2D> ().StopFollowing ();
				clickPositon = Camera.main.ScreenToWorldPoint (Input.touches [0].position);
				velocity = Vector3.zero;
				isDrag = true;

				lastFramePositon = Camera.main.ScreenToWorldPoint (Input.touches[0].position);
			}
			else if (Input.touches[0].phase == TouchPhase.Moved && isDrag) 
			{
				thecamera.transform.position -= (Camera.main.ScreenToWorldPoint(Input.touches[0].position) - clickPositon);
				lastFramePositon = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
			}
			else if (Input.touches[0].phase == TouchPhase.Ended && isDrag)
			{
				velocity = Camera.main.ScreenToWorldPoint (Input.touches[0].position) - lastFramePositon;
				isDrag = false;
			}
		}
		else if(Input.touches.Length >= 2 && !EventSystem.current.IsPointerOverGameObject (Input.touches [0].fingerId) && !GetComponent<WorldListManager>().getCurrentSelectionType())
		{
			isDrag = false;
			if (Input.touches [1].phase == TouchPhase.Began)
			{
				lastFrameFingerDistance = (Camera.main.ScreenToWorldPoint (Input.touches [0].position) - Camera.main.ScreenToWorldPoint (Input.touches [1].position)).magnitude;
				GetComponent<WorldListManager> ().setTransitioning (false);
			}
			else
			{
				float currentFrameFingerDistance = (Camera.main.ScreenToWorldPoint (Input.touches [0].position) - Camera.main.ScreenToWorldPoint (Input.touches [1].position)).magnitude;

				if (Input.touches [0].phase == TouchPhase.Moved && Input.touches [1].phase == TouchPhase.Moved) 
				{
					float chancgeIndistance = (currentFrameFingerDistance - lastFrameFingerDistance);

					transform.localScale += new Vector3(chancgeIndistance * zoomScale, chancgeIndistance * zoomScale);

					if (transform.localScale.x > maxZoomValue)
						transform.localScale = new Vector3 (maxZoomValue, maxZoomValue);

					if (transform.localScale.y < minZoomValue)
						transform.localScale = new Vector3 (minZoomValue, minZoomValue);

					GetComponent<BoxCollider2D> ().enabled = true;
					SetBounds (this.gameObject.GetComponent<BoxCollider2D> ().bounds);
					GetComponent<BoxCollider2D> ().enabled = false;
				}

				lastFrameFingerDistance = currentFrameFingerDistance;
			}
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
			void OnGUI() {
			GUI.TextField(new Rect(10, 10, 200, 20), Input.touches.Length.ToString(), 25);
			}
}