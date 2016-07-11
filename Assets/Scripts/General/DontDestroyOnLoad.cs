﻿using UnityEngine;
using System.Collections;

public class DontDestroyOnLoad : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        DontDestroyOnLoad(this.gameObject);
        Destroy(this.GetComponent<DontDestroyOnLoad>());
	}
	
}
