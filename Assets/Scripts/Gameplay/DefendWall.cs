using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DefendWall : MonoBehaviour
{

    int wallDurability = 10;
    public GameObject playerController;

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetInt("ppPlayerGamemode") == 0)
        {
            this.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("ppPlayerGamemode") == 1)
        {
            this.gameObject.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy_Bullet")
        {
            wallDurability -= 1;
            other.gameObject.SetActive(false);
            GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (wallDurability <= 0)
        {
            this.gameObject.SetActive(false);
            playerController.SetActive(false);
            playerController.GetComponent<PlayerController>().gameOverPanel.SetActive(true);
            playerController.GetComponent<PlayerController>().playerDied += 1;
            playerController.GetComponent<PlayerController>().pauseBtn.GetComponent<Button>().interactable = false;
            PlayerPrefs.SetInt("ppPlayerDied", playerController.GetComponent<PlayerController>().playerDied);
            PlayerPrefs.Save();
            Time.timeScale = 0;
        }
    }
}
