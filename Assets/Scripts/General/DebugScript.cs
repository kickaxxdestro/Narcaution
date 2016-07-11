using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugScript : MonoBehaviour {

    public Text levelInput;

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void SetToOffenseGamemode()
    {
        PlayerPrefs.SetInt("ppPlayerGamemode", 0);
    }

    public void SetToDefenceGamemode()
    {
        PlayerPrefs.SetInt("ppPlayerGamemode", 1);
    }

    public void AddMoney(int amount)
    {
        PlayerPrefs.SetInt("ppPlayerMoney", PlayerPrefs.GetInt("ppPlayerMoney", 0) + 50);
        PlayerPrefs.Save();
    }

    public void SetLevel(int level)
    {
        PlayerPrefs.SetInt("ppCurrentLevel", level);
        PlayerPrefs.Save();
    }

    public void SetLevelFromInputField()
    {
        int tempInt;

        if (int.TryParse(levelInput.text, out tempInt))
        {
            PlayerPrefs.SetInt("ppCurrentLevel", tempInt);
            PlayerPrefs.Save();
        }

    }
}
