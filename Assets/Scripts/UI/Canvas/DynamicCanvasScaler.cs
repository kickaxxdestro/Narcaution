using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DynamicCanvasScaler : MonoBehaviour {
	
	void Start()
	{
		float refResoX = 1280 * Camera.main.aspect;
		GetComponent<CanvasScaler>().referenceResolution = new Vector2(refResoX, 1280);
	}
}
