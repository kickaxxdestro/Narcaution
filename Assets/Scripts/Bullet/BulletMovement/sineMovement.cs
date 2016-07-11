using UnityEngine;
using System.Collections;

public class sineMovement : MonoBehaviour {

    public enum DIRECTION
    {
        DIRECTION_HORIZONTAL,
        DIRECTION_VERTICAL,
    }

    public DIRECTION waveDirection = DIRECTION.DIRECTION_VERTICAL;
    public float distance = 1f;
    public float frequency = 1f;
    public bool increaseDistOverTime = false;
    public float distIncreaseAmount = 0.01f;
    public int tick = 0;
    Vector3 origin;
    [HideInInspector]
    public float originalDist;

    void Awake()
    {
        originalDist = distance;
    }

	// Use this for initialization
	void Start () {

	}

    public void ResetOriginalDistance()
    {
        originalDist = distance;
    }

    public void ResetOriginToCurrent()
    {
        origin = this.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale == 0.0f)
            return;
        ++tick;

        if(waveDirection == DIRECTION.DIRECTION_VERTICAL)
            this.transform.position = new Vector3(this.origin.x + ((distance * Mathf.Sin((float)tick * 0.5f * Mathf.PI * (Time.fixedDeltaTime * frequency)))), this.transform.position.y, transform.position.z);
        //else
        //    this.transform.position = new Vector3(this.transform.position.x, ((distance * Mathf.Sin(tick * 0.5f * Mathf.PI * Time.fixedDeltaTime * frequency))), transform.position.z);

        if (increaseDistOverTime)
            distance += distIncreaseAmount * Time.fixedDeltaTime;
    }

    void OnDisable()
    {
        tick = 0;
        distance = originalDist;
    }
}
