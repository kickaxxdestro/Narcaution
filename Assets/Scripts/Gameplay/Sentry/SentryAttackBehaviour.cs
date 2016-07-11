using UnityEngine;
using System.Collections;

public class SentryAttackBehaviour : MonoBehaviour {

    [Tooltip("Sentry bullet prefab")]
    public GameObject bullet;
    [Tooltip("Sentry firing speed")]
    public float shootSpeed;
    public float bulletSpeed = 10f;

    float shootTimer;

    private AudioSource shootAudioSource;

    void Awake()
    {
        shootAudioSource = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale == 0f)
            return;

        shootTimer -= Time.deltaTime;
	    if(shootTimer <= 0f)
        {
            shootTimer = shootSpeed;
            ShootBullet();
        }
	}

    void ShootBullet()
    {
        GameObject go = Instantiate(bullet) as GameObject;
        go.GetComponent<Rigidbody2D>().velocity = go.transform.up * bulletSpeed;
        go.transform.position = transform.position;

        shootAudioSource.Play();
    }
}
