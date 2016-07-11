using UnityEngine;
using System.Collections;

public class HomingBehaviour : MonoBehaviour {

	public GameObject enemyObject;
	
	public float homingDamage;
	public float homingSpeed;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.tag == "Enemy" && other.gameObject == enemyObject)
		{
			other.GetComponent<EnemyGeneralBehaviour>().hpCount -= homingDamage;
			enemyObject = null;
			gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//find direction of the enemy 
		Vector3 dir = enemyObject.transform.position - transform.position;
		
		//rotate to face target
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg));
		
		//move to the target
		//normalized is to set everything 
		transform.position += dir.normalized * Time.deltaTime * homingSpeed;
		
		//enemy not in the area set the homing missle as false		
		if(enemyObject.activeInHierarchy == false)
		{
			enemyObject = null;
			gameObject.SetActive(false);
		}
	}
}
