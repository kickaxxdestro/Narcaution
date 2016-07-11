using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IceBossBehaviour : MonoBehaviour {
	
	public float moveSpeed;
	
	Vector3 origPos;
	Transform iceChunkPooler;
	int moveDir = 1;
	
	float iceChunkTimer;
	GameObject iceChunk;
	GameObject[] iceChunkArr;

    //Ice boulder attack
    public GameObject iceBoulderPrefab;
    GameObject iceBoulder;
    bool boulderSpawned = false;
    patternList icePatternList;

    float stateChangeDelay = 0f;
	
	[System.NonSerialized]
	public int iceChunkCount = 0;
    bool attacked = false;
	
	enum AIState
	{
		state_Spawn_Self,
		state_Spawn_Ice,
		state_Shoot_Ice,
        state_Spawn_IceBoulder,
        state_Shoot_IceBoulder,
        state_Shoot_Projectiles,
        state_return_to_origin,
	};
	AIState currentState;
	
	void Start()
	{
		GameObject.Find("introChecker").GetComponent<EnemyChecker>().iceAppeared += 1;
        icePatternList = GetComponent<patternList>();
		GetComponent<bossHealthbar> ().setBossHp ();
	}
	
	//init upon enabled, called faster than Start func
	void Awake()
	{
		origPos = transform.position;
		iceChunkArr = new GameObject[3];
		currentState = AIState.state_Spawn_Self;
		iceChunkTimer = 1.0f;
		iceChunkPooler = transform.FindChild("iceChunkPooler");
	}
	
	//couroutine change AI state with a delay time
	IEnumerator ChangeAIStateDelay(AIState state, float time)
	{
		yield return new WaitForSeconds(time);
		currentState = state;
		iceChunkTimer = 1.0f;
		StopCoroutine("ChangeAIStateDelay");
	}
	
	void SetInvulnerable(bool invul)
	{
		if(invul)
		{
			GetComponent<Collider2D>().enabled = false;
			GetComponent<SpriteRenderer>().color = Color.grey;
		}
		else if (!invul)
		{
			GetComponent<Collider2D>().enabled = true;
			GetComponent<SpriteRenderer>().color = Color.white;
		}
	}
	
	//function to create the iceChunk
	void CreateIceChunk()
	{
		iceChunk = iceChunkPooler.GetComponent<ObjectPooler>().GetPooledObject();
		if(iceChunk == null)
		{
			return;
		}
		else
		{
			iceChunk.GetComponent<IceChunkBehaviour>().defPos = transform.position - (transform.up * 2.0f);
			iceChunk.GetComponent<IceChunkBehaviour>().moveActive = true;
			iceChunk.SetActive(true);
			iceChunkArr[iceChunkCount] = iceChunk;
			iceChunkCount += 1;
			CancelInvoke("CreateIceChunk");
		}
	}
	
	void ShootIceChunks()
	{
		//shoot ice chunks
		for(int i = 0; i < iceChunkArr.Length; ++i)
		{
			if(iceChunkArr[i] != null)
			{
				iceChunkArr[i].GetComponent<IceChunkBehaviour>().shootActive = true;
				iceChunkArr[i] = null;
				iceChunkCount -= 1;
			}
		}
	}
	void SetIsShootingFalse()
	{
		GetComponent<Animator>().SetBool("isShooting", false);
	}
	
    void CreateIceBoulder()
    {
        iceBoulder = Instantiate(iceBoulderPrefab) as GameObject;
        iceBoulder.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1f);
        iceBoulder.transform.localScale = Vector3.zero;
        boulderSpawned = true;
    }

	// Update is called once per frame
	void Update () {
		
		switch(currentState)
		{
            case AIState.state_Spawn_Self:
                //dialogue
                StartCoroutine(ChangeAIStateDelay(AIState.state_Spawn_Ice, 1.5f));
                break;

            case AIState.state_Spawn_Ice:
                if (stateChangeDelay > 0f)
                {
                    stateChangeDelay -= Time.deltaTime;
                    return;
                }

                //constant moving back and forth
                transform.position += new Vector3(moveSpeed, 0, 0) * moveDir * Time.deltaTime;
                //changes the direction when it reaches the boundaries
                if (transform.position.x <= -SystemVariables.current.CameraBoundsX + (transform.localScale.x * 0.5f) || transform.position.x >= SystemVariables.current.CameraBoundsX - (transform.localScale.x * 0.5f))
                {
                    moveDir = -moveDir;
                }

                //iceChunkTimer update
                iceChunkTimer -= Time.deltaTime;
                if (iceChunkTimer <= 0.0f)
                {
                    Invoke("CreateIceChunk", 0.0f);
                    iceChunkTimer = 1.0f;
                    if (iceChunkCount >= 3)
                    {
                        StartCoroutine(ChangeAIStateDelay(AIState.state_Shoot_Ice, 2.0f));
                    }
                }
                break;

            case AIState.state_Shoot_Ice:

                if (Vector3.Distance(transform.position, origPos) > 0.01f)
                {
                    //moves back to center position
                    SetInvulnerable(true);
                    transform.position = Vector3.MoveTowards(transform.position, origPos, Time.deltaTime);

                    //change animation to shooting animation
                    GetComponent<Animator>().SetBool("isReturning", true);

                    for (int i = 0; i < iceChunkArr.Length; ++i)
                    {
                        float gap = i * 2.25f;
                        iceChunkArr[i].transform.position = Vector3.Lerp(iceChunkArr[i].transform.position, new Vector3(-2.25f + gap, iceChunkArr[i].transform.position.y, 0), Time.deltaTime * 4.0f);
                    }
                }
                else
                {
                    SetInvulnerable(false);
                    //change animation to shooting animation
                    GetComponent<Animator>().SetBool("isShooting", true);
                    GetComponent<Animator>().SetBool("isReturning", false);

                    //shoots ice chunks out
                    stateChangeDelay = 2f;
                    currentState = AIState.state_Shoot_Projectiles;
                    
                }
                break;
            case AIState.state_Spawn_IceBoulder:
                if(stateChangeDelay > 0f)
                {
                    stateChangeDelay -= Time.deltaTime;
                    return;
                }

                if(!boulderSpawned)
                {
                    CreateIceBoulder();
                    return;
                }

                Vector3 targetPos = new Vector3(origPos.x, origPos.y - 1f);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime);
                //float newScale = iceBoulder.transform.localScale.x + (1f * Time.deltaTime);

                iceBoulder.transform.localScale = Vector3.Lerp(iceBoulder.transform.localScale, new Vector3(8f, 8f), Time.deltaTime * 0.5f);//new Vector3(newScale, newScale, 1f);
                

                if (iceBoulder.transform.localScale.x >= 5f)
                {
                    iceBoulder.transform.localScale = new Vector3(5f, 5f, 1f);
                    stateChangeDelay = 2f;
                    currentState = AIState.state_Shoot_IceBoulder;
                }
                GetComponent<Animator>().SetBool("isReturning", true);

                break;
            case AIState.state_Shoot_IceBoulder:
                if (stateChangeDelay > 0f)
                {
                    if(stateChangeDelay < 0.5f)
                    {
                        //Update animations
                        GetComponent<Animator>().SetBool("isShooting", true);
                        GetComponent<Animator>().SetBool("isReturning", false);
                    }
                    stateChangeDelay -= Time.deltaTime;
                    return;
                }

                iceBoulder.GetComponent<Rotate2DObject>().enabled = false;
                iceBoulder.transform.position += new Vector3(0, -10f) * Time.deltaTime;

                if (iceBoulder.transform.position.y < -SystemVariables.current.CameraBoundsY - (iceBoulder.transform.localScale.y * 0.5f))
                {
                    Destroy(iceBoulder);
                    boulderSpawned = false;
                    stateChangeDelay = 0f;
                    currentState = AIState.state_return_to_origin;
                }
                break;
            case AIState.state_Shoot_Projectiles:
                if (stateChangeDelay > 0f)
                {
                    stateChangeDelay -= Time.deltaTime;
                    return;
                }

                if (!attacked)
                {
                    icePatternList.attack(0);
                    attacked = true;
                }

                if (icePatternList.SpawnersDone)
                {
                    //inhController.SetBool("isShooting", false);

                    stateChangeDelay = 3f;
                    currentState = AIState.state_Spawn_IceBoulder;
                    attacked = false;
                }
                break;
            case AIState.state_return_to_origin:
                if (stateChangeDelay > 0f)
                {
                    stateChangeDelay -= Time.deltaTime;
                    return;
                }

                if (Vector3.Distance(transform.position, origPos) > 0.01f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, origPos, Time.deltaTime);
                }
                else
                {
                    stateChangeDelay = 1.5f;
                    currentState = AIState.state_Spawn_Ice;
                }
                break;
		}
	}

    void OnDisable()
    {
        Destroy(iceBoulder);
        for (int i = 0; i < iceChunkArr.Length; ++i)
        {
            if (iceChunkArr[i] != null)
            {
                Destroy(iceChunkArr[i]);
            }
        }
    }

    void OnDestroy()
    {
        print("ded");
        Destroy(iceBoulder);
        for (int i = 0; i < iceChunkArr.Length; ++i)
        {
            if (iceChunkArr[i] != null)
            {
                Destroy(iceChunkArr[i]);
            }
        }
    }
}
