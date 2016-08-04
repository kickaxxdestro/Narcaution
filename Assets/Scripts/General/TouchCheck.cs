using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TouchCheck : MonoBehaviour {

    float doubleTapTimer;

	bool pointerDowned = false;

	Vector2 initialPointerPos;

	int touchedfingerId;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public bool CheckTouchOnCollider()
    {
#if UNITY_EDITOR
        return GetMouseInputOnCollider();
#endif
#if UNITY_STANDALONE_WIN
        return GetMouseInputOnCollider();
#endif

#if UNITY_ANDROID
        return GetTouchInputOnCollider();
#elif UNITY_IOS
        return GetTouchInputOnCollider();
#endif
        return GetTouchInputOnCollider(); //Assuming its always on IOS or Android
    }

    public bool GetMouseInputOnCollider()
	{
		if (!pointerDowned) 
		{
			if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject ()) 
			{
				Vector3 wp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				Vector2 touchPos = new Vector2 (wp.x, wp.y);
				Collider2D hit = Physics2D.OverlapPoint (touchPos);

				if (hit && hit == gameObject.GetComponent<Collider2D> ()) 
				{
					pointerDowned = true;
					initialPointerPos = Input.mousePosition;
				}
			}
			return false;
		} 
		else 
		{
			if (Input.GetMouseButtonUp(0)) 
			{
				Vector3 wp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				Vector2 touchPos = new Vector2 (wp.x, wp.y);

				pointerDowned = false;
				if ((new Vector2(Input.mousePosition.x, Input.mousePosition.y) - initialPointerPos).magnitude <= 2) 
				{
					return true;
				}
			}
			return false;
		}
    }

    public bool GetTouchInputOnCollider()
    {
        foreach (Touch mytouch in Input.touches)
        {
			if (!pointerDowned) 
			{
				if (mytouch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject (mytouch.fingerId)) 
				{
					Vector3 wp = Camera.main.ScreenToWorldPoint (mytouch.position);
					Vector2 touchPos = new Vector2 (wp.x, wp.y);
					Collider2D hit = Physics2D.OverlapPoint (touchPos);

					if (hit && hit == gameObject.GetComponent<Collider2D> ()) 
					{
						touchedfingerId = mytouch.fingerId;
						initialPointerPos = mytouch.position;
						pointerDowned = true;
					}
				}
			}
			else
			{
				if(mytouch.fingerId == touchedfingerId && mytouch.phase == TouchPhase.Ended)
				{
					Vector3 wp = Camera.main.ScreenToWorldPoint (mytouch.position);
					Vector2 touchPos = new Vector2 (wp.x, wp.y);
				
					pointerDowned = false;
					if ((mytouch.position - initialPointerPos).magnitude <= 2) 
					{
						return true;
					}
				}
			}
        }
        return false;
    }

	public bool CheckClickHold()
	{
		if (Input.GetMouseButton(0))
		{
			return true;
		}
		return false;
	}

	public bool CheckTapHold()
	{
		foreach (Touch mytouch in Input.touches)
		{
			if (mytouch.phase == TouchPhase.Stationary || mytouch.phase == TouchPhase.Moved)
			{
				return true;
			}
		}
		return false;
	}

    public bool CheckDoubleTap()
    {
        foreach (Touch mytouch in Input.touches)
        {
			if (mytouch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject())
            {
                if (Time.time < doubleTapTimer + .3f)
                {
                    print("Double tap");
                    return true;
                }
                doubleTapTimer = Time.time;
            }
        }
        return false;
    }

    public bool CheckDoubleClick()
    {
		if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            print("CHECK");
            if (Time.time < doubleTapTimer + .3f)
            {
                print("Double tap");
                return true;
            }
            doubleTapTimer = Time.time;
        }
        return false;
    }
}
