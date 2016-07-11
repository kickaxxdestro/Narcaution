﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class FBscript : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        FB.Init(SetInit, OnHideUnity);
	}

    void SetInit()
    {
        if(FB.IsLoggedIn)
        {
            Debug.Log("Logged in!");
        }
        else
        {
            Debug.Log("Not Logged in!");
        }
    }

    void OnHideUnity(bool isGameShown)
    {
        if(!isGameShown)
        {
            Time.timeScale = 0;
        }
        else 
        {
            Time.timeScale = 1;
        }
    }

    public void FBLogin()
    {
        List<string> permissions = new List<string>();
        permissions.Add("public_profile");

        FB.LogInWithReadPermissions(permissions, AuthCallBack);
    }

    void AuthCallBack(IResult result)
    {
        if(result.Error != null)
        {
            Debug.Log(result.Error);
        }
        else
        {
            if (FB.IsLoggedIn)
            {
                Debug.Log("Logged in!");
            }
            else
            {
                Debug.Log("Not Logged in!");
            }
        }

    }
}
