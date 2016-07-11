using UnityEngine;
using System.Collections;

public class RotateAroundPivot : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Returns a copy of rotated Vector
    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, float angle)
    {
        Vector3 dir = point - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(0, 0, angle) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }
}
