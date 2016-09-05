using UnityEngine;
using System.Collections;

public class EnemyGeneralBehaviour : MonoBehaviour {
	
	public enum enemyType
	{
		typeCrack,
		typeHeroin,
		typeIce,
		typeBZP,
		typeKetamine,
		typeCannabis,
		typeNimetazepam,
		typeBuprenorphine,
		typeMephedrone,
		typeNPS,
		typeLSD,
		typeEcstasy,
		typeInhalant,
		noCountEnemy	//minion/ spawner, not part of the main level
	};
	
	public enemyType type;
	
	public float hp = 1.0f;
	public int points = 50;

	public float hpCount;

	public Material hitMat;
	Material selfMat;
	float hitTimer;

	[System.NonSerialized]
	public bool spawned = false;

	//get player object
	GameObject playerObj;
	
	GameObject enemyDestroyParticlerPooler;
	GameObject damagedSoundPooler;

	public GameObject targetedBullet;
	void Awake()
	{
		enemyDestroyParticlerPooler = GameObject.Find("enemyParticlePooler");
		damagedSoundPooler = GameObject.Find("enemyDamagedSoundPooler");
		playerObj = GameObject.FindGameObjectWithTag("Player");
		selfMat = GetComponent<SpriteRenderer>().material;
	}

	void OnEnable () {
		hpCount = hp;
	}

	void Start()
	{
		hpCount = hp;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        if (other.CompareTag("Bullet") == true || other.CompareTag("BeamBullet"))
		{
			hitTimer = 0.025f;
		}
	}

	void Update()
	{
		if (gameObject.activeInHierarchy) {
			//attract homing bullet
			GameObject homing = playerObj.transform.FindChild ("homingPooler").gameObject;
			foreach (GameObject homingBullet in homing.GetComponent<ObjectPooler>().pooledObjects) {
				if (homingBullet.GetComponent<BulletBehaviour> ().homingTarget == null) {
					homingBullet.GetComponent<BulletBehaviour> ().homingTarget = gameObject;
					targetedBullet = homingBullet;
					break;
				}
			}
		} 
		//hit timer and white flash material
		hitTimer -= Time.deltaTime;
		if(hitTimer >= 0.0f)
		{
			GetComponent<SpriteRenderer>().material = hitMat;
		}
		else if (hitTimer <= 0.0f)
		{
			GetComponent<SpriteRenderer>().material = selfMat;
		}

		if (hpCount <= 0) {
//			targetedBullet.GetComponent<BulletBehaviour>().homingTarget = null;
//			targetedBullet = null;
			//damage = originalDamage;
			//hp = originalHP;
			//GetComponent<SpriteRenderer>().color = Color.white;
			//GetComponent<EnemyMovementBehaviour>().moveCounter = 0;
			if (targetedBullet != null) {
				targetedBullet.GetComponent<BulletBehaviour> ().homingTarget = null;
				targetedBullet = null;
			}

			if (type != enemyType.noCountEnemy) {
                
				if (GameObject.FindGameObjectWithTag ("Player") != null) {
					if (type == enemyType.typeIce || type == enemyType.typeCannabis || type == enemyType.typeInhalant || type == enemyType.typeEcstasy || type == enemyType.typeLSD || type == enemyType.typeNPS)
						GameObject.FindGameObjectWithTag ("Player").GetComponent<ScoringSystemStar> ().currentScore = points + (int)hp;
					else
						GameObject.FindGameObjectWithTag ("Player").GetComponent<ScoringSystemStar> ().currentScore += points;

					GameObject.FindGameObjectWithTag ("Player").GetComponent<ScoringSystemStar> ().enemiesKilled += 1;
				}
				if (GameObject.FindObjectOfType<LevelGeneratorScript> ().totalEnemies > 0) {
					GameObject.FindObjectOfType<LevelGeneratorScript> ().totalEnemies -= 1;
				}
				playerObj.GetComponent<PlayerController> ().enemiesKilled += 1;

				spawned = true;

				AchievementManager.instance ().IncreaseAchievementProgress ("Drug Stoper");
				AchievementManager.instance ().IncreaseAchievementProgress ("Drug Hunter");
				AchievementManager.instance ().IncreaseAchievementProgress ("Drug Buster");
				AchievementManager.instance ().IncreaseAchievementProgress ("Drug Destroyer");
			}

			//playerObj.GetComponent<PlayerController>().enemiesKilled += 1;
			
			//enemy destroy particle
			GameObject go = enemyDestroyParticlerPooler.GetComponent<ObjectPooler> ().GetPooledObject ();
			go.transform.position = transform.position;
			go.SetActive (true);
			
			//enemy damaged sound
			GameObject go2 = damagedSoundPooler.GetComponent<ObjectPooler> ().GetPooledObject ();
			go2.SetActive (true);
			
			gameObject.SetActive (false);
			//Destroy(gameObject);
		} 
		else 
		{
			if (GameObject.FindGameObjectWithTag ("Player") != null)
				if(type == enemyType.typeIce || type == enemyType.typeCannabis || type == enemyType.typeInhalant || type == enemyType.typeEcstasy || type == enemyType.typeLSD || type == enemyType.typeNPS )
					GameObject.FindGameObjectWithTag("Player").GetComponent<ScoringSystemStar>().currentScore = (int)hp - (int)hpCount;
		}
	}
}
