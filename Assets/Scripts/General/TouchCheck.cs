using UnityEngine;
using System.Collections;

public class TouchCheck : MonoBehaviour {

    float doubleTapTimer;

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
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            Collider2D hit = Physics2D.OverlapPoint(touchPos);

            if (hit && hit == gameObject.GetComponent<Collider2D>())
            {
                return true;
            }
        }
        return false;
    }

    public bool GetTouchInputOnCollider()
    {
        foreach (Touch mytouch in Input.touches)
        {
            if (mytouch.phase == TouchPhase.Began)
            {
                Vector3 wp = Camera.main.ScreenToWorldPoint(mytouch.position);
                Vector2 touchPos = new Vector2(wp.x, wp.y);
                Collider2D hit = Physics2D.OverlapPoint(touchPos);

                if (hit && hit == gameObject.GetComponent<Collider2D>())
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckDoubleTap()
    {
        foreach (Touch mytouch in Input.touches)
        {
            if (mytouch.phase == TouchPhase.Began)
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
        if (Input.GetMouseButtonDown(0))
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
