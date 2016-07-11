using UnityEngine;
using System.Collections;

public class LSDBehaviour : MonoBehaviour
{

    public enum state
    {
        state_idle,
        state_move,
        //state_waitMinion,
        state_spawnMinion,
        state_minionAttack,
        state_homing,
        state_resetAttack,
        state_aoe
        //state_vulnerable
    }

    public state lsdState;
    public GameObject[] minionArr;

    int dir = 1;
    float speed = 3, vulTime = 3.0f;
    int moveCycle;
    bool attacked;

    patternList lsdAttackControl;

    Vector3 initPos;
    Animator lsdController;

    //couroutine change AI state with a delay time
    IEnumerator ChangeAIStateDelay(state newState, float time)
    {
        yield return new WaitForSeconds(time);
        lsdState = newState;
        StopCoroutine("ChangeAIStateDelay");
    }

    // Use this for initialization
    void Start()
    {
        this.name = "LSD_Boss";

        lsdState = state.state_idle;
        initPos = transform.position;

        lsdController = GetComponent<Animator>();
        lsdAttackControl = GetComponent<patternList>();

        GetComponent<bossHealthbar>().setBossHp();
    }

    void SetInvul(bool invul)
    {
        if (invul)
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else if (!invul)
        {
            GetComponent<Collider2D>().enabled = true;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    void aiState()
    {
        switch (lsdState)
        {
            case state.state_idle:
                attacked = false;
                moveCycle = 0;

                StartCoroutine(ChangeAIStateDelay(state.state_move, vulTime));
                break;

            case state.state_move:
                if (moveCycle < 4)
                {
                    transform.position += new Vector3(speed, 0, 0) * dir * Time.deltaTime;
                    //changes the direction when it reaches the boundaries
                    if (transform.position.x <= -SystemVariables.current.CameraBoundsX + (transform.localScale.x * 0.5f) || transform.position.x >= SystemVariables.current.CameraBoundsX - (transform.localScale.x * 0.5f))
                    {
                        dir = -dir;
                        moveCycle++;
                    }
                }

                else
                    StartCoroutine(ChangeAIStateDelay(state.state_spawnMinion, 0.0f));
                //StartCoroutine(ChangeAIStateDelay(state.state_aoe, 0.0f));
                break;

            case state.state_spawnMinion:
                lsdController.SetBool("spawningTurret", true);

                float targetDist = (transform.position - initPos).sqrMagnitude;

                if (targetDist > 0.001)
                {
                    transform.position = Vector3.Lerp(transform.position, initPos, (speed / 2) * Time.deltaTime);
                }
                else
                {
                    transform.position = new Vector3(Mathf.Round(initPos.x), initPos.y, 0);

                    foreach (GameObject minion in minionArr)
                    {
                        //minion.transform.position = transform.position;
                        minion.SetActive(true);
                    }
                    SetInvul(true);

                    StartCoroutine(ChangeAIStateDelay(state.state_minionAttack, 0.0f));
                }

                break;

            case state.state_minionAttack:
                lsdController.SetBool("spawningTurret", false);
                bool active = false;

                foreach (GameObject minion in minionArr)
                {
                    if (minion.activeInHierarchy)
                    {
                        active = true;
                        break;
                    }
                }

                if (!active)
                {
                    SetInvul(false);
                    StartCoroutine(ChangeAIStateDelay(state.state_homing, 0.0f));
                }
                break;

            case state.state_homing:
                if (!attacked)
                {
                    lsdController.SetBool("isHoming", true);
                    lsdAttackControl.attack(0);

                    attacked = true;
                }

                if (lsdAttackControl.SpawnersDone)
                {
                    StartCoroutine(ChangeAIStateDelay(state.state_resetAttack, 0.0f));
                }
                break;

            case state.state_resetAttack:
                attacked = false;
                lsdController.SetBool("isHoming", false);
                StartCoroutine(ChangeAIStateDelay(state.state_aoe, 1.0f));
                break;

            case state.state_aoe:
                if (!attacked)
                {
                    lsdController.SetBool("aoeAttack", true);
                    lsdAttackControl.attack(1);

                    attacked = true;
                }

                if (lsdAttackControl.SpawnersDone)
                {
                    lsdController.SetBool("aoeAttack", false);
                    StartCoroutine(ChangeAIStateDelay(state.state_idle, 0.0f));
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        aiState();
        //print (lsdState);
    }
}
