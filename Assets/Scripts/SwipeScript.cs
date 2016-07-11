using UnityEngine;
using System.Collections;

public class SwipeScript : MonoBehaviour {

	public GameObject current;
	MainMenuTransition transitioner;

	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;

	// Use this for initialization
	void Start () {
        // First one in gallery
        current = GameObject.Find ("Heroin");

		// to access transitions
		transitioner = gameObject.GetComponent<MainMenuTransition> ();
	}


	void Swipe()
	{
#if UNITY_EDITOR
		if(Input.GetMouseButtonDown(0))
		{
			firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
		}
		if(Input.GetMouseButtonUp(0))
		{
			secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);

			currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

			currentSwipe.Normalize();

			// Next
			if(currentSwipe.x < 0 && (currentSwipe.y > -0.5f || currentSwipe.y < 0.5f))
			{
				Debug.Log("left swipe");
				if(current.GetComponent<NewGalleryScript>().myNext != null)
				{
					current.GetComponentInParent<MainMenuTransition>().ThisPanel_LerpToLeft();
					current.GetComponent<NewGalleryScript>().myNext.GetComponentInParent<MainMenuTransition>().ThisPanel_LerpToCenter_FromRight();
					current = current.GetComponent<NewGalleryScript>().myNext;
				}
			}
			// Previous
			if(currentSwipe.x > 0 && (currentSwipe.y > -0.5f || currentSwipe.y < 0.5f))
			{
				Debug.Log("right swipe");
				if(current.GetComponent<NewGalleryScript>().myPrev != null)
				{
					current.GetComponentInParent<MainMenuTransition>().ThisPanel_LerpToRight();
					current.GetComponent<NewGalleryScript>().myPrev.GetComponentInParent<MainMenuTransition>().ThisPanel_LerpToCenter_FromLeft();
					current = current.GetComponent<NewGalleryScript>().myPrev;
				}
			}
		}
#endif


#if UNITY_ANDROID
		//	Android
		if(Input.touches.Length > 0)
		{
			Touch t = Input.GetTouch(0);
			if(t.phase == TouchPhase.Began)
			{
				firstPressPos = new Vector2(t.position.x,t.position.y);
			}
			if(t.phase == TouchPhase.Ended)
			{
				secondPressPos = new Vector2(t.position.x,t.position.y);

				currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

				currentSwipe.Normalize();
				
				// Next
				if(currentSwipe.x < 0 && (currentSwipe.y > -0.5f || currentSwipe.y < 0.5f))
				{
					Debug.Log("left swipe");
					if(current.GetComponent<NewGalleryScript>().myNext != null)
					{
						current.GetComponentInParent<MainMenuTransition>().ThisPanel_LerpToLeft();
						current.GetComponent<NewGalleryScript>().myNext.GetComponentInParent<MainMenuTransition>().ThisPanel_LerpToCenter_FromRight();
						current = current.GetComponent<NewGalleryScript>().myNext;
					}
				}
				// Previous
				if(currentSwipe.x > 0 && (currentSwipe.y > -0.5f || currentSwipe.y < 0.5f))
				{
					Debug.Log("right swipe");
					if(current.GetComponent<NewGalleryScript>().myPrev != null)
					{
						current.GetComponentInParent<MainMenuTransition>().ThisPanel_LerpToRight();
						current.GetComponent<NewGalleryScript>().myPrev.GetComponentInParent<MainMenuTransition>().ThisPanel_LerpToCenter_FromLeft();
						current = current.GetComponent<NewGalleryScript>().myPrev;
					}
				}
			}
		}
#endif
	}

	// Update is called once per frame
	void Update () {
		Swipe ();
	}
}
