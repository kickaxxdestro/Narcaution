using UnityEngine;
using System.Collections;

public class SystemVariables : MonoBehaviour {
	
	public static SystemVariables current;
	
	public float CameraBoundsX;
	public float CameraBoundsY;
	
	public float buffer = 0.1f;
	
	void Awake()
	{
		current = this;
	}
	
	void Start()
	{
		CameraBoundsY = Camera.main.orthographicSize;
		CameraBoundsX = Camera.main.aspect * CameraBoundsY;
	}
}
