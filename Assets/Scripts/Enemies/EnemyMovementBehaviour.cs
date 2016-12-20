using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;

[System.Serializable]
public class PathNodes : System.Object 
{
	public Vector3 translateValue;
	public float translateSpeed = 1;
	public float waitTime = 0.0f;
	public bool isLerp = false;
	public bool isShooting = false;
	public bool shootAtPlayer = false;
}

public class EnemyMovementBehaviour : MonoBehaviour {
	
	public enum BulletType
	{
		typeRound,
		typeLong
	};
	
	GameObject player;
	
	public bool isLooping, loopInfinite, moveAndShoot;
	public int loopAmt = 1;
	
	public PathNodes[] movePoints;

	public int[] shootingPoints;

	Vector3 targetPoint;
	
	float distToPoint;
	float bufferDist = 0.1f * 0.1f;
	float waitTimer = 0.0f;
	Vector3 circularMotion;
	
	Transform shootPoint;
	public Transform[] shootPoints;
	public int shootPointsAmt = 0;
	GameObject longBulletPooler;
	GameObject roundBulletPooler;
	
	//non serialized public variables
	[System.NonSerialized]
	public int moveCounter = 0;
	[System.NonSerialized]
	public Vector3 startPoint;
	[System.NonSerialized]
	public bool willShoot;
	
	public float shootRate = 1.0f;
	float shootTimer = 0.0f;
	
	public BulletType bulletType;

	patternList attackCommand;
	bool firedAlready; //check if enemy fired already at it's current moveCounter
	bool moveCounterAdded;
	//for debugging purposes only
	//[System.NonSerialized]
	public int moveAmount;
	
	void CheckSeen()
	{
		switch(GetComponent<EnemyGeneralBehaviour>().type)
		{
		case EnemyGeneralBehaviour.enemyType.typeCrack:
			GameObject.Find("introChecker").GetComponent<EnemyChecker>().crackAppeared += 1;
			break;
			
		case EnemyGeneralBehaviour.enemyType.typeHeroin:
			GameObject.Find("introChecker").GetComponent<EnemyChecker>().heroinAppeared += 1;
			break;
			
		case EnemyGeneralBehaviour.enemyType.typeBZP:
			GameObject.Find("introChecker").GetComponent<EnemyChecker>().bzpAppeared += 1;
			break;
			
		case EnemyGeneralBehaviour.enemyType.typeKetamine:
			GameObject.Find("introChecker").GetComponent<EnemyChecker>().ketamineAppeared += 1;
			break;

		case EnemyGeneralBehaviour.enemyType.typeIce:
			GameObject.Find("introChecker").GetComponent<EnemyChecker>().iceAppeared += 1;
			break;

		case EnemyGeneralBehaviour.enemyType.typeCannabis:
			GameObject.Find("introChecker").GetComponent<EnemyChecker>().cannabisAppeared += 1;
			break;

		case EnemyGeneralBehaviour.enemyType.typeBuprenorphine:
			GameObject.Find("introChecker").GetComponent<EnemyChecker>().buprenorphineAppeared += 1;
			break;

		case EnemyGeneralBehaviour.enemyType.typeMephedrone:
			GameObject.Find("introChecker").GetComponent<EnemyChecker>().buprenorphineAppeared += 1;
			break;

		case EnemyGeneralBehaviour.enemyType.typeNPS:
			GameObject.Find("introChecker").GetComponent<EnemyChecker>().npsAppeared += 1;
			break;

		case EnemyGeneralBehaviour.enemyType.typeLSD:
			GameObject.Find("introChecker").GetComponent<EnemyChecker>().lsdAppeared += 1;
			break;

		case EnemyGeneralBehaviour.enemyType.typeEcstasy:
			GameObject.Find("introChecker").GetComponent<EnemyChecker>().ecstasyAppeared += 1;
			break;

		case EnemyGeneralBehaviour.enemyType.typeInhalant:
			GameObject.Find("introChecker").GetComponent<EnemyChecker>().inhalantAppeared += 1;
			break;
		}
	}
	
	#region GIZMO_DEBUG
	void OnDrawGizmos()
	{
		Vector3 gizTargetPoint;
		Vector3 gizCurrentPoint = transform.position;
		
		//drawing the bounds of the sprite
		Gizmos.color = Color.white;
		Gizmos.DrawWireCube(transform.position, GetComponent<SpriteRenderer>().bounds.size);
		
		//drawing the collider bounds
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(transform.position + new Vector3(GetComponent<Collider2D>().offset.x, GetComponent<Collider2D>().offset.y), GetComponent<Collider2D>().bounds.size);
		
		//drawing of shooting points
		for(int i = 0; i < transform.childCount; ++i)
		{
			if(transform.GetChild(i).CompareTag("shootPoint") == true)
			{
				Gizmos.color = Color.cyan;
				Gizmos.DrawWireSphere(transform.GetChild(i).position, 0.1f);
			}
		}
		
		//drawinging lines and collider bounds of translating positions
		for(int i = 0; i < movePoints.Length; ++i)
		{
			gizTargetPoint = gizCurrentPoint + movePoints[i].translateValue;
			Gizmos.color = Color.green;
			Gizmos.DrawWireCube(gizTargetPoint + new Vector3(GetComponent<Collider2D>().offset.x, GetComponent<Collider2D>().offset.y), GetComponent<Collider2D>().bounds.size);
			
			//drawing the line gizmos for the moving direction
			Gizmos.color = Color.red;
			Gizmos.DrawLine(gizTargetPoint, gizCurrentPoint);
			gizCurrentPoint = gizTargetPoint;
		}
		
	}
	#endregion GIZMO_DEBUG
	
	void Start()
	{
		startPoint = transform.position;
		if(GetComponent<EnemyGeneralBehaviour>() != null)
		{
			CheckSeen();
		}
	}
	
	void Awake()
	{
		startPoint = transform.position;
		moveAmount = movePoints.Length;
		shootPoint = transform.FindChild("shootPoint");
		
		//initialize array
		//get all shooting points child
		for (int i = 0; i < transform.childCount; ++i)
		{
			if(transform.GetChild(i).CompareTag("shootPoint") == true)
			{
				shootPointsAmt += 1;
			}
		}
		shootPoints = new Transform[shootPointsAmt];
		for (int i = 0; i < shootPointsAmt; ++i)
		{
			if(transform.GetChild(i).CompareTag("shootPoint") == true)
			{
				shootPoints[i] = transform.GetChild(i);
			}
		}
		
		longBulletPooler = GameObject.Find("LongBulletPooler");
		roundBulletPooler = GameObject.Find("RoundBulletPooler");
		player = GameObject.FindGameObjectWithTag("Player");
		attackCommand = this.GetComponent<patternList> ();
	}
	
	//upon enabling, it sets itself to the first movement point
	void OnEnable()
	{
		moveCounter = 0;

		waitTimer = movePoints[moveCounter].waitTime;

		willShoot = movePoints[moveCounter].isShooting;
		targetPoint = transform.position + movePoints[moveCounter].translateValue;
		shootTimer = shootRate;
	}
	
	void Update () {
		//print (moveCounter.ToString ());
		//calculates the next moving position
		distToPoint = (targetPoint - transform.position).sqrMagnitude;

		if(loopInfinite)
			isLooping = loopInfinite;

		//print (moveCounter.ToString());
		//SHOOTING (NEW)
		//fireOnSpawn :
		//True = Fires once spawned
		//False = Waits till moveCounter to shoot (Check array);

		/*if(!firedAlready) {
			for (int i=0; i<shootingPoints.Length; i++) {
				if (moveCounter == shootingPoints [i]) {
					attackCommand.attack (shootingPoints [i]);	//set attack set to correspond with moveCounter
					firedAlready = true;
					break;
				}
			}
		}*/

		if(!firedAlready) {
			for (int i=0; i< attackCommand.ListOfPatterns.Count; i++) {
				if (moveCounter == attackCommand.ListOfPatterns[i].attackSet) {
					attackCommand.attack (moveCounter);	//set attack set to correspond with moveCounter
					firedAlready = true;
					break;
				}
			}
		}
		
	
		//SHOOTING
		//calculates when is the next time it will shoot the next bullet
		/*
		if(willShoot == true)
		{
			shootTimer -= Time.deltaTime;
			if(shootTimer <= 0.0f)
			{
				//get all the shooting points of the enemy and shoot
				for(int i = 0; i < shootPoints.Length; ++i)
				{

					//determine what kind ofbullet will shoot
					switch(bulletType)
					{
						case BulletType.typeRound:
						GameObject go = roundBulletPooler.GetComponent<ObjectPooler>().GetPooledObject();
						go.transform.position = shootPoints[i].position;
						if(movePoints[moveCounter].shootAtPlayer == true)
						{
							//will shoot at player
							//calculate angle of the bullet that it is going to travel
							Vector3 targetDir = player.transform.position - transform.position;
							float angle = Vector3.Angle(targetDir, -transform.up);
							
							//check for the angle if it is acute
							if(targetDir.x <= 0)
							{
								angle = 360.0f - angle;
							}
							
							go.transform.eulerAngles = new Vector3(0, 0, angle);
						}
						else if (movePoints[moveCounter].shootAtPlayer == false)
						{
							//will not shoot at player
							go.transform.eulerAngles = Vector3.zero;
						}
						
						//increment the bullet speed based on the movement speed of the enemy at that current time
						go.GetComponent<EnemyBulletBehaviour>().bulletSpeed += movePoints[moveCounter].translateSpeed * 0.5f;
						
						//enable the bullet object
						go.SetActive(true);
						break;
						
						case BulletType.typeLong:
						GameObject go1 = longBulletPooler.GetComponent<ObjectPooler>().GetPooledObject();
						go1.transform.position = shootPoints[i].position;
						if(movePoints[moveCounter].shootAtPlayer == true && player != null)
						{
							//will shoot at player
							//calculate angle of the bullet that it is going to travel
							Vector3 targetDir = player.transform.position - transform.position;
							float angle = Vector3.Angle(targetDir, -transform.up);
							
							//check for the angle if it is acute
							if(targetDir.x <= 0)
							{
								angle = 360.0f - angle;
							}
							
							go1.transform.eulerAngles = new Vector3(0, 0, angle);
						}
						else if (movePoints[moveCounter].shootAtPlayer == false)
						{
							//will not shoot at player
							go1.transform.eulerAngles = Vector3.zero;
						}
						
						//enable the bullet object
						go1.Set` mk
						e(true);
						break;
					}
				}
				shootTimer = shootRate;
			}
		}
		*/
		//SHOOTING

		//if the distance is more than 0
		if(distToPoint > bufferDist)
		{
			//constant moving to the next position, is different based on isLerp value
			if(movePoints[moveCounter].isLerp == false)
			{
				transform.position = Vector2.MoveTowards(transform.position, targetPoint, Time.deltaTime * movePoints[moveCounter].translateSpeed);
			}
			else if(movePoints[moveCounter].isLerp == true)
			{
				transform.position = Vector2.Lerp(transform.position, targetPoint, Time.deltaTime * movePoints[moveCounter].translateSpeed);
			}
		}
		//if the distance is less than the buffer
		else if (distToPoint <= bufferDist)
		{
			//if there is a waiting time
			if(waitTimer > 0.0f)
			{
				if(!moveCounterAdded) {
					moveCounter += 1;
					firedAlready = false;
					moveCounterAdded = true;
				}

				if(moveAndShoot)
					waitTimer -= Time.deltaTime;
				
				else {
					if(attackCommand.SpawnersDone) {
						waitTimer -= Time.deltaTime;
					}
					//if attackCommand.SpawnersDone is false (not shooting)
					else {
						if(!firedAlready)
							waitTimer -= Time.deltaTime;
					}
				}
			}
			//else if the waiting time has reached 0
			else if (waitTimer <= 0.0f)
			{
				//once it reaches to its next destination, increment this to calculate the next moving point 	
				//moveCounter += 1;
				moveCounterAdded = false;

				//resets
				//firedAlready = false;

				if(moveCounter >= movePoints.Length)
				{
					//disables OR reuse the object when it has reached the end of its cycle
					if(isLooping == false)
					{
						if(GameObject.FindObjectOfType<LevelGeneratorScript>() != null && this.tag != "Minion")
						{
							GameObject.FindObjectOfType<LevelGeneratorScript>().totalEnemies -= 1;
						}
                        if (GetComponent<EnemyGeneralBehaviour>())
                        {
							if(GetComponent<EnemyGeneralBehaviour>().targetedBullet != null)
                            	GetComponent<EnemyGeneralBehaviour>().targetedBullet.GetComponent<BulletBehaviour>().homingTarget = null;
                            GetComponent<EnemyGeneralBehaviour>().targetedBullet = null;
                        }
						gameObject.SetActive(false);
					}
					else if (isLooping == true)
					{
						if(!loopInfinite) {
							//reset the movement couter and get the next movement points stats
							loopAmt -= 1;
							if(loopAmt <= 0)
							{
								isLooping = false;
								return;
							}
						}
						moveCounter = 0;
						waitTimer = movePoints[moveCounter].waitTime;
						willShoot = movePoints[moveCounter].isShooting;
						targetPoint = transform.position + movePoints[moveCounter].translateValue;
					}
				}
				else
				{
					waitTimer = movePoints[moveCounter].waitTime;
					willShoot = movePoints[moveCounter].isShooting;
					targetPoint = transform.position + movePoints[moveCounter].translateValue;

				}
			}
		}
	}
}