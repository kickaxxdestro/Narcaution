using UnityEngine;
using System.Collections;

public class SentryBulletBehaviour : MonoBehaviour {

    float bulletDamage = 2.5f;
    GameObject bulletParticlePooler;

	// Use this for initialization
	void Start () {
        bulletParticlePooler = GameObject.Find("bulletParticlePooler");
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Minion") || other.CompareTag("TutorialEnemy"))
        {
            other.GetComponent<EnemyGeneralBehaviour>().hpCount -= bulletDamage;
            GameObject go = bulletParticlePooler.GetComponent<ObjectPooler>().GetPooledObject();
            go.transform.position = transform.position;
            go.SetActive(true);
            Destroy(this.gameObject);
        }
    }

	// Update is called once per frame
	void Update () {
        if (transform.position.y > SystemVariables.current.CameraBoundsY)
            Destroy(this.gameObject);
	}
}
