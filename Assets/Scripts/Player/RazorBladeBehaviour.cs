using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RazorBladeBehaviour : MonoBehaviour {

	float razorBladeSpeed = 8.0f;
	public float razorBladeDamage = 0.5f;
	public float targetRadius = 6f;
	int razorBladeHealth = 3;
	
	public float attackTimer = 0.8f;
	
	List<GameObject> targetEnemysList = new List<GameObject>();
	
	bool isBouncing;
	GameObject currentEnemy;
	GameObject closestEnemy;
	
	void OnEnable()
	{		
	
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		for(int i = 0; i < enemies.Length; ++i)
			targetEnemysList.Add(enemies[i]);
		
			
		razorBladeHealth = 3;
		
		isBouncing = false;
		currentEnemy = null;
		closestEnemy = null;
	}
	
	
	void FindClosestEnemy ()
	{
		if(targetEnemysList == null)
			return;
			
		foreach(GameObject enemy in targetEnemysList)
		{
			if(enemy != currentEnemy)
				continue;
			
			if((enemy.transform.position - transform.position).sqrMagnitude < targetRadius)
			{
				closestEnemy = enemy;
				return;
			}
		}
		
		closestEnemy = null;
	}
	
	void FixedUpdate()
	{
		if(!isBouncing)
			GetComponent<Rigidbody2D>().velocity = transform.up * razorBladeSpeed;
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Enemy") == true  )
		{
			isBouncing = true;
			
			other.GetComponent<EnemyGeneralBehaviour>().hpCount -= razorBladeDamage;
			if(!other.gameObject.activeSelf)
				targetEnemysList.Remove(other.gameObject);
				
			currentEnemy = other.gameObject;
			
			-- razorBladeHealth;
			FindClosestEnemy();
			if(razorBladeHealth <= 0)
			{
				gameObject.SetActive(false);
			}
			
			
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.CompareTag("Enemy") == true && razorBladeHealth > 0)
		{
			gameObject.SetActive(false);
		}
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(transform.position.y > SystemVariables.current.CameraBoundsY)
		{
			gameObject.SetActive(false);
		}
		
		if(closestEnemy == null)
		{
			if(closestEnemy == null)
			{
				if(isBouncing)
					isBouncing = false;
				
				return;
			}
			
			if(closestEnemy.activeInHierarchy == false)
				gameObject.SetActive(false);
			
			transform.position = Vector3.MoveTowards(transform.position, closestEnemy.transform.position, razorBladeSpeed*Time.deltaTime);
			
			
				
			return;
		}
		
		if(closestEnemy.activeInHierarchy == false)
			gameObject.SetActive(false);
			
		transform.position = Vector3.MoveTowards(transform.position, closestEnemy.transform.position, razorBladeSpeed*Time.deltaTime);
		
	
	}
}
