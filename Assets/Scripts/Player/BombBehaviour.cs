using UnityEngine;
using System.Collections;

public class BombBehaviour : MonoBehaviour {

	public float bombDamage = 20.0f;
	float currentRotation = 0.0f;
	
	float bombTimer = 0.5f;
	Color fade;
	
	// Use this for initialization
	void Awake () {
		fade = new Color(1,1,1,1);
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.tag == "Enemy" )
		{
			other.GetComponent<EnemyGeneralBehaviour>().hpCount -= bombDamage;
		}
		else if (other.tag == "Enemy_Bullet")
		{
			other.gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		bombTimer -= Time.deltaTime;
		if(bombTimer > 0)
		{
			//transform.localScale += new Vector3(1.0f, 1.0f, 0) * Time.deltaTime * 5;
			fade.a -= Time.deltaTime * 0.5f;
			GetComponent<SpriteRenderer>().color = fade;
			currentRotation += 2 * Time.deltaTime;
			transform.Rotate(0,0,currentRotation);
		}
		else if(bombTimer <= 0)
		{
			Destroy(this.gameObject);
		}
		
	}
}