using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class bossHealthbar : MonoBehaviour {

	GameObject barObj;
	EnemyGeneralBehaviour boss;
	RectTransform frontBar, backBar;
    float defaultHp, currHp;
	bool fillLerp;

	void Awake () {
		barObj = GameObject.FindWithTag ("bar");
		foreach (Transform child in barObj.transform) {
			if(child.tag == "barFront")
				frontBar = child.GetComponent<RectTransform> ();

			if(child.tag == "barBack")
				backBar = child.GetComponent<RectTransform> ();

			child.gameObject.SetActive(true);
		}
	}

	public void setBossHp () {
		boss = this.GetComponent<EnemyGeneralBehaviour> ();

		defaultHp = boss.hp;
		
		fillLerp = true;

        frontBar.GetComponent<Image>().fillAmount = 0f;
        backBar.GetComponent<Image>().fillAmount = 1f;

        GameObject.Find("progressBar").SetActive(false);
	}

	void Start () {
	}

	// Update is called once per frame
	void Update () {
		currHp = boss.hpCount;

        if (fillLerp)
        {
            if (frontBar.GetComponent<Image>().fillAmount < 1f)
                frontBar.GetComponent<Image>().fillAmount += Time.deltaTime;
            else
            {
                frontBar.GetComponent<Image>().fillAmount = 1;
                fillLerp = false;
            }
        }
        else
            frontBar.GetComponent<Image>().fillAmount = (currHp / defaultHp);
	}

    void OnDisable()
    {
        print("Boss disable");
        frontBar.GetComponent<Image>().fillAmount = 0f;
    }

    void OnDestroy()
    {
        frontBar.GetComponent<Image>().fillAmount = 0f;
        print("destroy");
    }
}
