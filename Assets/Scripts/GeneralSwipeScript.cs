using UnityEngine;
using System.Collections;

public class GeneralSwipeScript : MonoBehaviour {

    public GameObject current;
    public bool swipeEnabled = true;
    public bool colliderLimited = false;
    MainMenuTransition transitioner;

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    Vector2 firstDragPos;
    Vector2 currentDragPos;

    bool doCheck = false;
    bool firstTouch = false;
    float xSwipeOffset = 0f;

    // Use this for initialization
    void Start()
    {
        // to access transitions
        transitioner = gameObject.GetComponent<MainMenuTransition>();
    }


    void CheckSwipe()
    {
#if UNITY_EDITOR
		if(Input.GetMouseButtonDown(0))
		{
			firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
            doCheck = true;
            //if (!firstTouch)
            {
               // firstDragPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                //firstTouch = true;
                //current.GetComponentInParent<SliderItem>().ActivateSliding();
            }
		}
       
		if(Input.GetMouseButtonUp(0) && doCheck)
		{
            firstTouch = false;
            //current.GetComponentInParent<SliderItem>().DeactivateSliding();
            //if (xSwipeOffset > Screen.width * 0.5f)
            //    SwipeRight();

			secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
            if ((secondPressPos - firstPressPos).magnitude < 25f)
                return;
            
			currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

			currentSwipe.Normalize();
                
			// Next
			if(currentSwipe.x < 0 && (currentSwipe.y > -0.5f || currentSwipe.y < 0.5f))
			{
                SwipeLeft();
			}
			// Previous
            if (currentSwipe.x > 0 && (currentSwipe.y > -0.5f || currentSwipe.y < 0.5f))
            {
                SwipeRight();
            }
        }
        //if (Input.GetMouseButton(0))
        //{
        //    xSwipeOffset = (Mathf.Abs(Input.mousePosition.x) - Mathf.Abs(firstPressPos.x));
        //    current.GetComponentInParent<SliderItem>().xSlideOffset = xSwipeOffset;
        //    print(xSwipeOffset);
        //}
        //else
        //{
        //    xSwipeOffset = 0f;
        //}
#endif


#if UNITY_ANDROID
		//	Android
		if(Input.touches.Length > 0)
		{
			Touch t = Input.GetTouch(0);
			if(t.phase == TouchPhase.Began)
			{
				firstPressPos = new Vector2(t.position.x,t.position.y);
                doCheck = true;
			}
			if(t.phase == TouchPhase.Ended && doCheck)
			{
				secondPressPos = new Vector2(t.position.x,t.position.y);
                if ((secondPressPos - firstPressPos).magnitude < 25f)
                    return;
                print((secondPressPos - firstPressPos).magnitude);

				currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

				currentSwipe.Normalize();
				
				// Next
				if(currentSwipe.x < 0 && (currentSwipe.y > -0.5f || currentSwipe.y < 0.5f))
				{
                    SwipeLeft();
				}
				// Previous
				if(currentSwipe.x > 0 && (currentSwipe.y > -0.5f || currentSwipe.y < 0.5f))
				{
                    SwipeRight();
				}
			}
		}
#endif
    }

    void CheckColliderSwipe()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            Collider2D hit = Physics2D.OverlapPoint(touchPos);

            if (hit && hit == gameObject.GetComponent<Collider2D>())
            {
                firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                doCheck = true;
                //if (!firstTouch)
                {
                    // firstDragPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    //firstTouch = true;
                    //current.GetComponentInParent<SliderItem>().ActivateSliding();
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && doCheck)
        {
            firstTouch = false;
            //current.GetComponentInParent<SliderItem>().DeactivateSliding();
            //if (xSwipeOffset > Screen.width * 0.5f)
            //    SwipeRight();

            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            if ((secondPressPos - firstPressPos).magnitude < 25f)
                return;

            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            currentSwipe.Normalize();

            // Next
            if (currentSwipe.x < 0 && (currentSwipe.y > -0.5f || currentSwipe.y < 0.5f))
            {
                SwipeLeft();
            }
            // Previous
            if (currentSwipe.x > 0 && (currentSwipe.y > -0.5f || currentSwipe.y < 0.5f))
            {
                SwipeRight();
            }
        }
        //if (Input.GetMouseButton(0))
        //{
        //    xSwipeOffset = (Mathf.Abs(Input.mousePosition.x) - Mathf.Abs(firstPressPos.x));
        //    current.GetComponentInParent<SliderItem>().xSlideOffset = xSwipeOffset;
        //    print(xSwipeOffset);
        //}
        //else
        //{
        //    xSwipeOffset = 0f;
        //}
#endif


#if UNITY_ANDROID
        //	Android
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                firstPressPos = new Vector2(t.position.x, t.position.y);
                doCheck = true;
            }
            if (t.phase == TouchPhase.Ended && doCheck)
            {
                secondPressPos = new Vector2(t.position.x, t.position.y);
                if ((secondPressPos - firstPressPos).magnitude < 25f)
                    return;
                print((secondPressPos - firstPressPos).magnitude);

                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                currentSwipe.Normalize();

                // Next
                if (currentSwipe.x < 0 && (currentSwipe.y > -0.5f || currentSwipe.y < 0.5f))
                {
                    SwipeLeft();
                }
                // Previous
                if (currentSwipe.x > 0 && (currentSwipe.y > -0.5f || currentSwipe.y < 0.5f))
                {
                    SwipeRight();
                }
            }
        }
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (swipeEnabled)
        {
            if (colliderLimited)
                CheckColliderSwipe();
            else
                CheckSwipe();
        }
    }

    public void SwipeLeft()
    {
        //Debug.Log("left swipe");
        if (current.GetComponent<SliderItem>().myNext != null)
        {
            //current.GetComponentInParent<MainMenuTransition>().ThisPanel_LerpToLeft();
            current.GetComponent<SliderItem>().DoLerpToLeft();
            //current.GetComponent<SliderItem>().myNext.GetComponentInParent<MainMenuTransition>().ThisPanel_LerpToCenter_FromRight();
            current.GetComponent<SliderItem>().myNext.GetComponentInParent<SliderItem>().DoLerpToCenter_FromRight();
            current = current.GetComponent<SliderItem>().myNext;
        }
        doCheck = false;
    }

    public void SwipeRight()
    {
        //Debug.Log("right swipe");
        if (current.GetComponent<SliderItem>().myPrev != null)
        {
            //current.GetComponentInParent<MainMenuTransition>().ThisPanel_LerpToRight();
            current.GetComponentInParent<SliderItem>().DoLerpToRight();
            //current.GetComponent<SliderItem>().myPrev.GetComponentInParent<MainMenuTransition>().ThisPanel_LerpToCenter_FromLeft();
            current.GetComponent<SliderItem>().myPrev.GetComponentInParent<SliderItem>().DoLerpToCenter_FromLeft();
            current = current.GetComponent<SliderItem>().myPrev;
        }
        doCheck = false;
    }

    public void SetSwipeCheck(bool enabled)
    {
        this.swipeEnabled = enabled;
        if (!enabled)
            doCheck = false;
    }
}
