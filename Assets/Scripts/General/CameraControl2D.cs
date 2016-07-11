using UnityEngine;
using System.Collections;

public class CameraControl2D : MonoBehaviour {

    public float moveSpeed = 3f;
    Vector3 targetPosition;
    Vector3 originPosition;
    GameObject targetObject;


    public enum MOVEMENT_TYPE
    {
        MOVEMENT_TYPE_FOLLOWING,
        MOVEMENT_TYPE_MOVING,
        MOVEMENT_TYPE_MOVING_TO_ORIGIN,
        MOVEMENT_TYPE_NONE
    }

    MOVEMENT_TYPE currentMovement = MOVEMENT_TYPE.MOVEMENT_TYPE_NONE;


	// Use this for initialization
	void Start () {
        originPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        switch (currentMovement)
        {
            case MOVEMENT_TYPE.MOVEMENT_TYPE_FOLLOWING:
                this.transform.position = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, this.transform.position.z);
                break;
            case MOVEMENT_TYPE.MOVEMENT_TYPE_MOVING:
                this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, targetObject.transform.position.x, Time.deltaTime * moveSpeed),
            Mathf.Lerp(this.transform.position.y, targetObject.transform.position.y, Time.deltaTime * moveSpeed),
            this.transform.position.z);

                if ((new Vector2(targetObject.transform.position.x, targetObject.transform.position.y) - new Vector2(transform.position.x, transform.position.y)).magnitude <= 0.01f)
                {                      
                    this.transform.position = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, this.transform.position.z);
                    currentMovement = MOVEMENT_TYPE.MOVEMENT_TYPE_NONE;
                }

                break;
            case MOVEMENT_TYPE.MOVEMENT_TYPE_MOVING_TO_ORIGIN:
                this.transform.position = new Vector3(Mathf.Lerp(this.transform.position.x, originPosition.x, Time.deltaTime * moveSpeed),
            Mathf.Lerp(this.transform.position.y, originPosition.y, Time.deltaTime * moveSpeed),
            this.transform.position.z);

                if ((new Vector2(originPosition.x, originPosition.y) - new Vector2(transform.position.x, transform.position.y)).magnitude <= 0.01f)
                {
                    this.transform.position = new Vector3(originPosition.x, originPosition.y, this.transform.position.z);
                    currentMovement = MOVEMENT_TYPE.MOVEMENT_TYPE_NONE;
                }
                break;
            default:
                break;
        }
	}

    public void SnapToPosition()
    {

    }

    public void MoveToObject(GameObject targetObject)
    {
        this.targetObject = targetObject;
        currentMovement = MOVEMENT_TYPE.MOVEMENT_TYPE_MOVING;
    }

    public void FollowObject(GameObject targetObject)
    {
        this.targetObject = targetObject;
        currentMovement = MOVEMENT_TYPE.MOVEMENT_TYPE_FOLLOWING;
    }

    public void StopFollowing()
    {
        currentMovement = MOVEMENT_TYPE.MOVEMENT_TYPE_NONE;
    }

    public void InterpolatePositionToZero()
    {
        currentMovement = MOVEMENT_TYPE.MOVEMENT_TYPE_MOVING_TO_ORIGIN;
    }

    public void ResetPositionToZero()
    {
        this.transform.position = new Vector3(0, 0, this.transform.position.z);
        currentMovement = MOVEMENT_TYPE.MOVEMENT_TYPE_NONE;
    }
}
