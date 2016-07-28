﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DefendWall : MonoBehaviour
{

    public int wallDurability = 10;
	public int maxDurability;
    public GameObject playerController;
	public GameObject healthArea;

    // Use this for initialization
    void Start()
    {
		maxDurability = wallDurability;

        if (PlayerPrefs.GetInt("ppPlayerGamemode") == 0)
        {
            this.gameObject.SetActive(false);
			healthArea.SetActive (false);
        }
        else if (PlayerPrefs.GetInt("ppPlayerGamemode") == 1)
        {
			this.gameObject.SetActive(true);
			healthArea.SetActive (true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy_Bullet")
        {
            wallDurability -= 1;
            other.gameObject.SetActive(false);
            GetComponent<AudioSource>().Play();
			GetComponent<Animator> ().Play ("ReciveDamage");
			healthArea.transform.FindChild ("HpFront").gameObject.GetComponent<Image> ().fillAmount = ((float)wallDurability / (float)maxDurability);

			if (wallDurability <= 0)
			{
				//this.gameObject.SetActive(false);
				//playerController.SetActive(false);

				GetComponent<Animator> ().Play ("Destroyed");
				playerController.GetComponent<PlayerController>().gameOverPanel.SetActive(true);
				playerController.GetComponent<PlayerController>().playerDied += 1;
				playerController.GetComponent<PlayerController>().pauseBtn.GetComponent<Button>().interactable = false;
				PlayerPrefs.SetInt("ppPlayerDied", playerController.GetComponent<PlayerController>().playerDied);
				PlayerPrefs.Save();
				Time.timeScale = 0;
			}
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
