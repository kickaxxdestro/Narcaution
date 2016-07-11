using UnityEngine;
using System.Collections;

public class EnemyBulletBehaviour : MonoBehaviour {
	
	public float bulletSpeed = 5.0f;
	
	float origSpeed;
	
	void Awake()
	{
		origSpeed = bulletSpeed;
	}
	
	void FixedUpdate()
	{
		//GetComponent<Rigidbody2D>().velocity = -transform.up * bulletSpeed;
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Player") == true)
		{
			//bulletSpeed = origSpeed;
			//gameObject.SetActive(false);
            //transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, -transform.rotation.z);
            //transform.rotation.Set(transform.rotation.x, transform.rotation.y, -transform.rotation.z);
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 20f));
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < -SystemVariables.current.CameraBoundsY)
		{
			bulletSpeed = origSpeed;
			gameObject.SetActive(false);
		}
	}
}
