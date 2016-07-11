using UnityEngine;
using System.Collections;

public class EnemyTutorialScript : MonoBehaviour {

    float hp = 1;
	
	GameObject tutorialEnemyParticlePooler;
	
    void Awake()
    {
		tutorialEnemyParticlePooler = GameObject.Find("tutorialEnemyParticlePooler");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            hp -= 0.5f;
            if (hp <= 0)
			{
				//particle pooler
				GameObject go = tutorialEnemyParticlePooler.GetComponent<ObjectPooler>().GetPooledObject();
				go.transform.position = transform.position;
				go.SetActive(true);
				
                gameObject.SetActive(false);
			}
        }
        else if(other.CompareTag("NoDamageBullet"))
        {
            if (other.GetComponent<bulletMove>() != null)
            {
                if (other.GetComponent<bulletMove>().reflected)
                {
                    //particle pooler
                    GameObject go = tutorialEnemyParticlePooler.GetComponent<ObjectPooler>().GetPooledObject();
                    go.transform.position = transform.position;
                    go.SetActive(true);

                    gameObject.SetActive(false);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().comboCount += 1;
                    Destroy(other.gameObject);
                }
            }

        }
    }
	
    // Update is called once per frame
	void Update () {
		
	}
}
