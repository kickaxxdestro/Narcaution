using UnityEngine;
using System.Collections;

public class IceBoulderBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            GameObject go = GameObject.Find("bulletParticlePooler").GetComponent<ObjectPooler>().GetPooledObject();
            go.transform.position = transform.position;
            go.SetActive(true);
            go.GetComponent<ParticleSystem>().Play();

            //disable the bullet
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("BeamBullet"))
        {
            //disable the bullet
            //other.gameObject.SetActive(false); //bullet should be disabled in bullet behaviour
            GameObject go = GameObject.Find("bulletParticlePooler").GetComponent<ObjectPooler>().GetPooledObject();
            go.transform.position = transform.position;
            go.SetActive(true);
            go.GetComponent<ParticleSystem>().Play();
        }
        else if (other.CompareTag("Player"))
        {
            GameObject.Find("EffectsHandler").GetComponent<EffectsHandler>().ActivateShakeEffect(2f);
            other.GetComponent<PlayerController>().DoDamaged();
        }

    }
}
