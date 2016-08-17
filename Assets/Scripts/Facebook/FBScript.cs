using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;

public class FBScript : MonoBehaviour
{
    public GameObject DialogLoggedIn;
    public GameObject DialogLoggedOut;
    public GameObject DialogUsername;
    public GameObject DialogProfilePic;
    ProfileInfoLoader profileInfoLoader;

    // Use this for initialization
    void Awake()
    {
        FacebookManager.Instance.InitFB();
        DealWithFBMenus(FB.IsLoggedIn, true);
    }

    void Start()
    {
        profileInfoLoader = GameObject.Find("Profile").GetComponent<ProfileInfoLoader>();
    }

    public void FBLoginAndShare()
    {
        if(!FB.IsLoggedIn)
        {
            List<string> permissions = new List<string>();
            permissions.Add("public_profile");
            FB.LogInWithReadPermissions(permissions, AuthCallBack);
        }
        else
        {
            Share();
        }
        Debug.Log("Logged in");
    }

    void AuthCallBack(IResult result)
    {
        if (result.Error != null)
        {
            Debug.Log(result.Error);
        }
        else
        {
            if (FB.IsLoggedIn)
            {
                FacebookManager.Instance.IsLoggedIn = true;
                FacebookManager.Instance.GetProfile();
                Debug.Log("Logged in!");
            }
            else
            {
                Debug.Log("Not Logged in!");
            }

            DealWithFBMenus(FB.IsLoggedIn, false);
            Share();
        }
    }

    void DealWithFBMenus(bool isLoggedIn, bool alreadyLoggedIn)
    {
        //if (isLoggedIn)
        //{
        //    DialogLoggedIn.SetActive(true);
        //    DialogLoggedOut.SetActive(false);
        //    if (FacebookManager.Instance.ProfileName != null)
        //    {
        //        Text UserName = DialogUsername.GetComponent<Text>();
        //        UserName.text = FacebookManager.Instance.ProfileName;
        //        if (!alreadyLoggedIn)
        //        {
        //            //profileInfoLoader.facebookLoginChanges(FacebookManager.Instance.ProfileName);
        //        }

        //    }
        //    else
        //    {
        //        StartCoroutine("WaitForProfileName");
        //    }

        //    if (FacebookManager.Instance.ProfilePic != null)
        //    {
        //        Image ProfilePic = DialogProfilePic.GetComponent<Image>();
        //        ProfilePic.sprite = FacebookManager.Instance.ProfilePic;
        //        if (!alreadyLoggedIn)
        //        {
        //            //profileInfoLoader.facebookLoginChanges(FacebookManager.Instance.ProfilePic);
        //        }
        //    }
        //    else
        //    {
        //        StartCoroutine("WaitForProfilePic");
        //    }

        //}
        //else
        //{
        //    DialogLoggedIn.SetActive(false);
        //    //DialogLoggedOut.SetActive(true);
        //}
    }

    IEnumerator WaitForProfileName()
    {
        while (FacebookManager.Instance.ProfileName == null)
        {
            yield return null;
        }

        DealWithFBMenus(FB.IsLoggedIn, false);
    }

    IEnumerator WaitForProfilePic()
    {
        while (FacebookManager.Instance.ProfilePic == null)
        {
            yield return null;
        }

        DealWithFBMenus(FB.IsLoggedIn, false);
    }

    public void Share()
    {
        FacebookManager.Instance.Share();
    }

    public void Invite()
    {
        FacebookManager.Instance.Invite();
    }

    public void ShareWithPlayers()
    {
        FacebookManager.Instance.ShareWithPlayers();
    }
}
