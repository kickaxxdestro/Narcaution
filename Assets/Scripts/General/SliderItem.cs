using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderItem : MonoBehaviour
{

    public enum LerpDirection
    {
        LERP_TO_LEFT,
        LERP_TO_RIGHT,
        LERP_TO_DOWN,
        LERP_TO_UP,
        LERP_TO_CENTER_FROM_LEFT,
        LERP_TO_CENTER_FROM_RIGHT,
        LERP_TO_CENTER_FROM_DOWN,
        LERP_TO_CENTER_FROM_UP,
        LERP_TO_CENTER_FROM_DOWN_OPTIONS,
        LERP_NULL,
    }

    public enum StartPos
    {
        STARTPOS_RIGHT,
        STARTPOS_DOWN,
    }

    [HideInInspector]
    public LerpDirection lerpDirection = LerpDirection.LERP_NULL;

    public GameObject myPrev;
    public GameObject myNext;

    public GameObject pageSelector;

    public bool showAtStart;
    public StartPos startPosition = StartPos.STARTPOS_RIGHT;

    private Vector2 centerPosition;
    private float leftPositionX;
    private float rightPositionX;
    private float downPositionY;
    private float upPositionY;
    private int conditionDone = 0;

    float scrollingSpeed = 3;
    float distance = 1000;
    Vector2 default_res = new Vector2(720, 1280);

    [HideInInspector]
    public float xSlideOffset = 0f;
    [HideInInspector]
    public Vector3 originalSliderPos;
    bool sliding = false;

    string WorldList = "";

    // Use this for initialization
    void Start()
    {

        originalSliderPos = transform.position;
        float refResoX = 1280 * Camera.main.aspect;
        default_res = new Vector2(refResoX, 1280);
        Vector2 refAspect = new Vector2(refResoX, refResoX * (1 / Camera.main.aspect));

        // Only need adjust 1 Aspect ratio which is distance
        //distance = distance * (Screen.width/default_res.x);
        distance = distance / 720 / 1280 * refAspect.x * refAspect.y / Camera.main.aspect;
        if (distance <= 0)
        {
            distance = 0.001f;
        }
        // Record: Target Position of "CENTER"      
        centerPosition.x = this.transform.position.x;
        centerPosition.y = this.transform.position.y;

        // Record: Target Position of "LEFT"
        leftPositionX = centerPosition.x + (Vector3.left * Screen.width * 1.5f).x;

        // Record: Target Position of "RIGHT"
        rightPositionX = centerPosition.x + (Vector3.right * Screen.width * 1.5f).x;

        upPositionY = this.transform.position.y + (Vector3.up * Screen.height * 1.5f).y;
        downPositionY = this.transform.position.y + (Vector3.down * Screen.height * 1.5f).y;

        // Every Panel from scroll to the "RIGHT" from the start
        if (showAtStart == false)
        {
            switch (startPosition)
            {
                case StartPos.STARTPOS_RIGHT:
                    this.transform.position = new Vector3(rightPositionX, this.transform.position.y, this.transform.position.z);
                    
                    break;
                case StartPos.STARTPOS_DOWN:
                    this.transform.position = new Vector3(this.transform.position.x, downPositionY, this.transform.position.z);
                    break;
                default:
                    break;
            }
        }

        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(sliding)
        {
            //transform.position = new Vector3(originalSliderPos.x + xSlideOffset, originalSliderPos.y);
        }
        switch (lerpDirection)
        {
            case LerpDirection.LERP_TO_LEFT:

                //Lerping
                if (Mathf.Abs(this.transform.position.x - leftPositionX) > 0.5f)
                {
                    this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, leftPositionX, Time.deltaTime * scrollingSpeed), this.transform.position.y, this.transform.position.z);
                }
                //Lerp Finished Or any other condition
                else if (Mathf.Abs(this.transform.position.x - leftPositionX) <= 0.5f)
                {
                    this.transform.position = new Vector3(leftPositionX, this.transform.position.y, this.transform.position.z);
                    conditionDone++;
                }
                break;
            case LerpDirection.LERP_TO_RIGHT:
                //Lerping
                if (Mathf.Abs(this.transform.position.x - rightPositionX) > 0.5f)
                {
                    this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, rightPositionX, Time.deltaTime * scrollingSpeed), this.transform.position.y, this.transform.position.z);
                }
                //Lerp Finished Or any other condition
                else if (Mathf.Abs(this.transform.position.x - rightPositionX) <= 0.5f)
                {
                    this.transform.position = new Vector3(rightPositionX, this.transform.position.y, this.transform.position.z);
                    conditionDone++;
                }
                break;
            case LerpDirection.LERP_TO_CENTER_FROM_LEFT:
                //Lerping
                if (Mathf.Abs(this.transform.position.x - centerPosition.x) > 0.5f)
                {
                    this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, centerPosition.x, Time.deltaTime * scrollingSpeed), this.transform.position.y, this.transform.position.z);
                }
                //Lerp Finished Or any other condition
                else if (Mathf.Abs(this.transform.position.x - centerPosition.x) <= 0.5f)
                {
                    this.transform.position = new Vector3(centerPosition.x, this.transform.position.y, this.transform.position.z);
                    conditionDone++;
                }
                break;
            case LerpDirection.LERP_TO_CENTER_FROM_RIGHT:
                //Lerping
                if (Mathf.Abs(this.transform.position.x - centerPosition.x) > 0.5f)
                {
                    this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, centerPosition.x, Time.deltaTime * scrollingSpeed), this.transform.position.y, this.transform.position.z);
                }
                //Lerp Finished Or any other condition
                else if (Mathf.Abs(this.transform.position.x - centerPosition.x) <= 0.5f)
                {
                    this.transform.position = new Vector3(centerPosition.x, this.transform.position.y, this.transform.position.z);
                    conditionDone++;
                }
                break;
            case LerpDirection.LERP_TO_DOWN:
                //Lerping
                if (Mathf.Abs(this.transform.position.y - downPositionY) > 0.5f)
                {
                    this.transform.position = new Vector3(this.transform.position.x, Mathf.Lerp(this.transform.position.y, downPositionY, Time.deltaTime * scrollingSpeed), this.transform.position.z);
                }
                //Lerp Finished Or any other condition
                else if (Mathf.Abs(this.transform.position.y - downPositionY) <= 0.5f)
                {
                    this.transform.position = new Vector3(this.transform.position.x, downPositionY, this.transform.position.z);
                    conditionDone++;
                }
                break;
            case LerpDirection.LERP_TO_CENTER_FROM_DOWN:
                //Lerping
                if (Mathf.Abs(this.transform.position.y - centerPosition.y) > 0.5f)
                {
                    this.transform.position = new Vector3(this.transform.position.x, Mathf.Lerp(this.transform.position.y, centerPosition.y, Time.deltaTime * scrollingSpeed), this.transform.position.z);
                }
                //Lerp Finished Or any other condition
                else if (Mathf.Abs(this.transform.position.y - centerPosition.y) <= 0.5f)
                {
                    this.transform.position = new Vector3(this.transform.position.x, centerPosition.y, this.transform.position.z);
                    conditionDone++;
                }
                break;
            case LerpDirection.LERP_TO_UP:
                //Lerping
                if (Mathf.Abs(this.transform.position.y - upPositionY) > 0.5f)
                {
                    this.transform.position = new Vector3(this.transform.position.x, Mathf.Lerp(this.transform.position.y, upPositionY, Time.deltaTime * scrollingSpeed), this.transform.position.z);
                }
                //Lerp Finished Or any other condition
                else if (Mathf.Abs(this.transform.position.y - upPositionY) <= 0.5f)
                {
                    this.transform.position = new Vector3(this.transform.position.x, upPositionY, this.transform.position.z);
                    conditionDone++;
                }
                break;
            case LerpDirection.LERP_TO_CENTER_FROM_UP:
                //Lerping
                if (Mathf.Abs(this.transform.position.y - centerPosition.y) > 0.5f)
                {
                    this.transform.position = new Vector3(this.transform.position.x, Mathf.Lerp(this.transform.position.y, centerPosition.y, Time.deltaTime * scrollingSpeed), this.transform.position.z);
                }
                //Lerp Finished Or any other condition
                else if (Mathf.Abs(this.transform.position.y - centerPosition.y) <= 0.5f)
                {
                    this.transform.position = new Vector3(this.transform.position.x, centerPosition.y, this.transform.position.z);
                    conditionDone++;
                }
                break;
            case LerpDirection.LERP_NULL:
                return;
            default:
                break;
        }
        if (conditionDone >= 2)
        {
            lerpDirection = LerpDirection.LERP_NULL;
            conditionDone = 0;
        }
    }

    public void DoLerpToLeft()
    {
        DeactivateSliding();
        this.transform.position = new Vector3(centerPosition.x, this.transform.position.y, this.transform.position.z);
        lerpDirection = LerpDirection.LERP_TO_LEFT;
        conditionDone = 0;
        DeactivatePageSelector();
    }

    public void DoLerpToCenter_FromLeft()
    {
        DeactivateSliding();
        this.transform.position = new Vector3(leftPositionX, this.transform.position.y, this.transform.position.z);
        lerpDirection = LerpDirection.LERP_TO_CENTER_FROM_LEFT;
        conditionDone = 0;
        ActivatePageSelector();
    }
    public void DoLerpToCenter_FromRight()
    {
        DeactivateSliding();
        this.transform.position = new Vector3(rightPositionX, this.transform.position.y, this.transform.position.z);
        lerpDirection = LerpDirection.LERP_TO_CENTER_FROM_RIGHT;
        conditionDone = 0;
        ActivatePageSelector();
    }

    public void DoLerpToRight()
    {
        DeactivateSliding();
        this.transform.position = new Vector3(centerPosition.x, this.transform.position.y, this.transform.position.z);
        lerpDirection = LerpDirection.LERP_TO_RIGHT;
        conditionDone = 0;
        DeactivatePageSelector();
    }

    public void DoLerpToDown()
    {
        DeactivateSliding();
        this.transform.position = new Vector3(centerPosition.x, this.transform.position.y, this.transform.position.z);
        lerpDirection = LerpDirection.LERP_TO_DOWN;
        conditionDone = 0;
        DeactivatePageSelector();
    }

    public void DoLerpToDown_Options()
    {
        DeactivateSliding();
        this.transform.position = new Vector3(centerPosition.x, this.transform.position.y, this.transform.position.z);
        lerpDirection = LerpDirection.LERP_TO_DOWN;
        conditionDone = 0;
        DeactivatePageSelector();
        CameraDragControl CDCScript = GameObject.Find("WorldList").GetComponent<CameraDragControl>();
        CDCScript.enabled = true;
        for (int i = 1; i < 6; i++)
        {
            WorldList = "World" + i.ToString();
            PolygonCollider2D PC2DScriptEnabled = GameObject.Find(WorldList).GetComponent<PolygonCollider2D>();
            PC2DScriptEnabled.enabled = true;
        }
    }

    public void DoLerpToCenter_FromDown_Options()
    {
        DeactivateSliding();
        this.transform.position = new Vector3(centerPosition.x, downPositionY, this.transform.position.z);
        lerpDirection = LerpDirection.LERP_TO_CENTER_FROM_DOWN;
        conditionDone = 0;
        ActivatePageSelector();
        CameraDragControl CDCScript = GameObject.Find("WorldList").GetComponent<CameraDragControl>();
        CDCScript.enabled = false;
        for (int i = 1; i < 6; i++)
        {
            WorldList = "World" + i.ToString();
            PolygonCollider2D PC2DScriptEnabled = GameObject.Find(WorldList).GetComponent<PolygonCollider2D>();
            PC2DScriptEnabled.enabled = false;
        }
    }

    public void DoLerpToCenter_FromDown()
    {
        DeactivateSliding();
        this.transform.position = new Vector3(centerPosition.x, downPositionY, this.transform.position.z);
        lerpDirection = LerpDirection.LERP_TO_CENTER_FROM_DOWN;
        conditionDone = 0;
        ActivatePageSelector();
    }

    public void DoLerpToUp()
    {
        DeactivateSliding();
        this.transform.position = new Vector3(centerPosition.x, centerPosition.y, this.transform.position.z);
        lerpDirection = LerpDirection.LERP_TO_UP;
        conditionDone = 0;
        DeactivatePageSelector();
    }

    public void DoLerpToCenter_FromUp()
    {
        DeactivateSliding();
        this.transform.position = new Vector3(centerPosition.x, upPositionY, this.transform.position.z);
        lerpDirection = LerpDirection.LERP_TO_CENTER_FROM_UP;
        conditionDone = 0;
        ActivatePageSelector();
    }

    public void ActivatePageSelector()
    {
        if(pageSelector != null)
        {
            pageSelector.GetComponent<PageSelector>().Select();
        }
    }

    public void DeactivatePageSelector()
    {
        if (pageSelector != null)
        {
            pageSelector.GetComponent<PageSelector>().Deselect();
        }
    }

    public void ActivateSliding()
    {
        if (lerpDirection != LerpDirection.LERP_NULL)
            return;
        sliding = true;
        originalSliderPos = transform.position;
    }

    public void DeactivateSliding()
    {
        //sliding = false;
        //transform.position = originalSliderPos;
    }
}
