using UnityEngine;
using System.Collections;

public class DailyReward : MonoBehaviour
{
    bool DailyScreenShowed;
    bool ChestOpenCoolDown;

    System.DateTime LastOpenedTimer;
    System.DateTime TempTimer;
    System.DateTime DisplayRemainingTimer;

    void Start()
    {
        if(PlayerPrefs.GetInt("ppFirstReward") == 1)
        {
            long temp = System.Convert.ToInt64(PlayerPrefs.GetString("ppLastOpenedTimer"));
            LastOpenedTimer.Equals(System.DateTime.FromBinary(temp));
        }
    }

    void Update()
    {
        if (ChestOpenCoolDown == true && DailyScreenShowed == true)
        {
            TempTimer = LastOpenedTimer;
            //DisplayRemainingTimer = System.DateTime.Now.Subtract(LastOpenedTimer);
            if(DisplayRemainingTimer.Hour >= 24)
            {
                ChestOpenCoolDown = false;
            }
        }
    }

    public void OpenDailyReward()
    {
        if (ChestOpenCoolDown == false)
        {
            ChestOpenCoolDown = true;
            LastOpenedTimer.Equals(System.DateTime.Now);
            if (PlayerPrefs.GetInt("ppFirstReward") == 0)
            {
                PlayerPrefs.SetInt("ppFirstReward", 1);
            }
            PlayerPrefs.SetString("ppLastOpenedTimer", LastOpenedTimer.ToBinary().ToString());
            PlayerPrefs.Save();
            Debug.Log("Reward Obtained!");
        }
    }
}
