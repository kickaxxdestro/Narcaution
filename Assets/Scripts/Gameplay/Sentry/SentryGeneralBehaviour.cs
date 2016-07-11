using UnityEngine;
using System.Collections;

[RequireComponent(typeof(RotateAroundPivot))]
public class SentryGeneralBehaviour : MonoBehaviour {

    public float rotationSpeed = -20f;
    RotateAroundPivot rotationHandle;

	// Use this for initialization
	void Start () {
        rotationHandle = GetComponent<RotateAroundPivot>();
        if (rotationHandle == null)
        {
            print("SentryGeneralBehaviour: RotateAroundPivot script not found");
            this.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale == 0f)
            return;
        this.transform.position = rotationHandle.RotatePointAroundPivot(this.transform.position, this.transform.parent.position, rotationSpeed * Time.deltaTime);
	}
}
