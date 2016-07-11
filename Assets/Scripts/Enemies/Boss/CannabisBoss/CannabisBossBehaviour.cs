using UnityEngine;
using System.Collections;

public class CannabisBossBehaviour : MonoBehaviour {

	//ai enum states
	enum AIState
	{
		state_Spawn_self,
		state_Spawn_Leaf,
		state_Back_and_Fro,
		state_Hide_Leaf,
		state_Shuffle_Leaf,
		state_Jump,
		state_Attack,
		state_Return
	};
	AIState currentState;

	Transform leafPadPooler;
	public GameObject[] leafPadArr;

	//for hiding
	bool gotHide = false;
	int hideRand = 0;
	Vector3 hidePos;

	//for shuffling
	int shuffleAmount = 0;
	float shuffleTimer = 0.0f;
	int r1 = 0;
	int r2 = 0;
	int dir = 1;
	
	bool moveBack = false;
	Animator cannabisController;

	public GameObject smoke;
	GameObject theSmoke;
    bool attacked = false;
    patternList cannabisPatternList;

	void Start()
	{
		GameObject.Find("introChecker").GetComponent<EnemyChecker>().cannabisAppeared += 1;
		GetComponent<bossHealthbar> ().setBossHp ();
	}

    void Awake()
    {
        currentState = AIState.state_Spawn_self;
        leafPadPooler = transform.FindChild("leafPadPooler");
        leafPadArr = new GameObject[4];
        cannabisController = GetComponent<Animator>();
		theSmoke = Instantiate(smoke, Vector2.zero, Quaternion.identity) as GameObject;
		theSmoke.SetActive (false);
        cannabisPatternList = GetComponent<patternList>();
    }
	
    //couroutine change AI state with a delay time
    IEnumerator ChangeAIStateDelay(AIState state, float time)
    {
		yield return new WaitForSeconds(time);
		currentState = state;
        attacked = false;
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

	void Update()
	{
		switch(currentState)
		{
            case AIState.state_Spawn_self:
                //spawns itself and go to next state
                StartCoroutine(ChangeAIStateDelay(AIState.state_Spawn_Leaf, 2.0f));
                break;

            case AIState.state_Spawn_Leaf:
                //spawns the leaves
                for (int i = 0; i < leafPadArr.Length; ++i)
                {
                    if (leafPadArr[i] == null)
                    {
                        leafPadArr[i] = leafPadPooler.GetComponent<ObjectPooler>().GetPooledObject();
                        leafPadArr[i].transform.position = transform.position;
                        leafPadArr[i].GetComponent<LeafPadBehaviour>().origPos = new Vector3(-2.0f + (i * 1.3333f), 3f);
                        leafPadArr[i].GetComponent<LeafPadBehaviour>().attackTimer = i * 1f;
                        leafPadArr[i].SetActive(true);
                    }
                }

                //go to hiding in leaf state
                StartCoroutine(ChangeAIStateDelay(AIState.state_Back_and_Fro, 1.0f));
                break;

            case AIState.state_Back_and_Fro:
                SetInvulnerable(false);
                transform.position += new Vector3(3.0f, 0.0f) * dir * Time.deltaTime;
                if (transform.position.x > 2)
                {
                    dir = -dir;
                }
                else if (transform.position.x < -2)
                {
                    dir = -dir;
                }
                StartCoroutine(ChangeAIStateDelay(AIState.state_Hide_Leaf, 8.0f));

                break;

            case AIState.state_Hide_Leaf:
                //set hiding leave
                if (gotHide == false)
                {
                    hideRand = Random.Range(0, leafPadArr.Length);
                    shuffleAmount = Random.Range(8, 10);
                    hidePos = leafPadArr[hideRand].transform.position;
                    gotHide = true;
                }

                //move towards the hiding leaf
                if (Vector3.Distance(hidePos, transform.position) > 0)
                {
                    transform.position = Vector3.MoveTowards(transform.position, hidePos, Time.deltaTime * 2.0f);
                }
                else if (Vector3.Distance(hidePos, transform.position) <= 0)
                {
                    //hide behind the selected leaf
                    transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * 5.0f);
                    StartCoroutine(ChangeAIStateDelay(AIState.state_Shuffle_Leaf, 0.5f));
                }
                break;

            case AIState.state_Shuffle_Leaf:
                theSmoke.SetActive(true);
                //shuffle the leaves up
                shuffleTimer -= Time.deltaTime;
                if (shuffleTimer < 0.0f && shuffleAmount > 0)
                {
                    //get 2 random leaves, if the 2nd leaf is the same as the 1st leaf, it will shuffle again
                    r1 = Random.Range(0, leafPadArr.Length);
                    do
                    {
                        r2 = Random.Range(0, leafPadArr.Length);
                    }
                    while (r2 == r1);

                    shuffleAmount -= 1;

                    //switch places with each other
                    Vector3 temp = leafPadArr[r1].GetComponent<LeafPadBehaviour>().origPos;
                    leafPadArr[r1].GetComponent<LeafPadBehaviour>().origPos = leafPadArr[r2].GetComponent<LeafPadBehaviour>().origPos;
                    leafPadArr[r2].GetComponent<LeafPadBehaviour>().origPos = temp;

                    shuffleTimer = 0.4f;
                }
                else if (shuffleAmount <= 0)
                {
                    //once shuffle finish, proceed to jump out state

                    transform.position = leafPadArr[hideRand].transform.position;
                    StartCoroutine(ChangeAIStateDelay(AIState.state_Jump, 1.0f));
                }
                break;

            case AIState.state_Jump:

                theSmoke.SetActive(false);
                //jumping out from leaf
                if (transform.localScale.x < 1)
                {
                    transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * 4.0f);
                    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 2.5f, 0), Time.deltaTime * 3.0f);
                }
                else if (transform.localScale.x >= 1)
                {
                    StartCoroutine(ChangeAIStateDelay(AIState.state_Attack, 0.0f));
                }
                break;

            case AIState.state_Attack:
                //move forward to attack
                if (moveBack == false)
                {
                    if (Vector3.Distance(transform.position, new Vector3(transform.position.x, 2.75f)) > 0.1f)
                    {
                        cannabisController.SetBool("isCharging", true);
                        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 3.0f), Time.deltaTime * 2.0f);
                    }
                    else
                    {
                        moveBack = true;
                    }
                }
                else if (moveBack == true)
                {
                    if (Vector3.Distance(transform.position, new Vector3(transform.position.x, -SystemVariables.current.CameraBoundsY)) > 0.1f)
                    {
                        cannabisController.SetBool("isCharging", false);
                        cannabisController.SetBool("isAttacking", true);
                        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -SystemVariables.current.CameraBoundsY), Time.deltaTime * 3.0f);
                    }
                    else
                    {
                        StartCoroutine(ChangeAIStateDelay(AIState.state_Return, 0.0f));
                    }
                }
                break;

            case AIState.state_Return:
                cannabisController.SetBool("isAttacking", false);
                moveBack = false;
                if (!attacked)
                {
                    cannabisPatternList.attack(0);
                    attacked = true;
                }
                if (Vector3.Distance(new Vector3(transform.position.x, 2.5f), transform.position) > 0)
                {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, 2.5f), Time.deltaTime);
                }
                else
                {
                    StartCoroutine(ChangeAIStateDelay(AIState.state_Back_and_Fro, 0.0f));
                    gotHide = false;
                }
                break;
        }
	}

    void OnDisable()
    {
        for (int i = 0; i < leafPadArr.Length; ++i)
        {
            if (leafPadArr[i] != null)
            {
                leafPadArr[i].SetActive(false);
            }
        }
    }

}
