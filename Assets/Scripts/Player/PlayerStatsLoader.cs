using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStatsLoader : MonoBehaviour {

    public Text nameText;
    public Text gamemodeText;
    public Text currentLevelText;
    public Image gamemodeIcon;
    public GameObject toggleButton;

    public Sprite offenseIcon;
    public Sprite defenseIcon;

	// Use this for initialization
	void Start () {
        if (nameText)
            nameText.text = string.Concat("Name: ", PlayerPrefs.GetString("ppPlayerName", "player"));
        if (gamemodeText)
            gamemodeText.text = string.Concat("Mode: ", PlayerPrefs.GetInt("ppPlayerGamemode", 0) == 0 ? "Offense" : "Defense");
        int currentLevel = PlayerPrefs.GetInt("ppCurrentLevel", 1);
        if (currentLevelText)
        {
            if (currentLevel == 21)
                currentLevelText.text = "Current Level: 5-5";
            else if (currentLevel == 22)
                currentLevelText.text = "";
            else
                currentLevelText.text = string.Concat("Current Level: ", (((currentLevel - 1) / 4) + 1) + "-" + (currentLevel - (((currentLevel - 1) / 4) * 4)));

        }

        if (gamemodeIcon)
            gamemodeIcon.sprite = PlayerPrefs.GetInt("ppPlayerGamemode", 0) == 0 ? offenseIcon : defenseIcon;

        if (PlayerPrefs.GetInt("ppCurrentLevel", 1) < 22)
            toggleButton.SetActive(false);

        this.enabled = false;
    }

    public void ToggleGamemode()
    {
        PlayerPrefs.SetInt("ppPlayerGamemode", PlayerPrefs.GetInt("ppPlayerGamemode", 0) == 0 ? 1 : 0);
        PlayerPrefs.Save();
        if (gamemodeText)
            gamemodeText.text = string.Concat("Mode: ", PlayerPrefs.GetInt("ppPlayerGamemode", 0) == 0 ? "Offense" : "Defense");
        if (gamemodeIcon)
            gamemodeIcon.sprite = PlayerPrefs.GetInt("ppPlayerGamemode", 0) == 0 ? offenseIcon : defenseIcon;
    }
}
