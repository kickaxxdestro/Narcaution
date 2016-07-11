using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class PlayerController : MonoBehaviour
{
    //Player gamemode
    bool gamemode = true;// true = attacking, false = defending

    //google play achievements
    public int enemiesKilled;
    public int playerDied;

    //control
    Vector3 currentMousePos;
    Vector3 lastMousePos;
    Vector3 deltaMousePos;
    int numberOfTouches = 0;

    public float moveSpeed = 1.0f;
    public float expandSpeed = 1.0f;

    public float playerBoundary = 0.5f;

    //how fast the bullet will respawn
    public float bulletAttackTimer = 0.125f;
    public float bulletOffset = 0.3f;
    int pulseCounter = 0;
    bool projectileReturn = true;

    //how fast the homing bullet will respawn
    public float homingAttackTimer = 1f;
    public float homingOffset = 0.3f;

    //how fast the beam will respawn
    public float beamAttackTimer = 3f;
    public float beamOffset = 0.3f;

    //how fast the bombbullet will respawn
    public float bombBulletAttackTimer = 2f;
    public float bombBulletOffset = 0.3f;

    //health
    //public int emotionPoint = 3;
    public int emotionPoint;
    public int shieldCount = 2;
    List<GameObject> shieldList = new List<GameObject>();
    public GameObject shield;

    //bomb
    public GameObject Bomb;

    //If player been hit then it will go into invulnerable mode
    public bool invuln = false;
    public float invulnTimer = 3.0f;

    //How many set of bullet depend on the the combocount
    public int comboCount = 0;
    public int lastComboCount = 0;
    public int topComboCount = 0;
    public int[] comboSet;

    public bool canShoot;

    //bullet
    public Transform shootPoint;


    //homing bullet
    Transform homingPooler;

    Transform bombPooler;
    Transform beamPooler;

    //Lifeline
    [System.NonSerialized]
    public Transform rotateShield;
    [System.NonSerialized]
    public Transform firstShield;
    [System.NonSerialized]
    public Transform secondShield;
    bool firstLife = true;
    bool secondLife = true;

    bool toShoot = false;

    public List<GameObject> enemyLockedOnList;

    [System.NonSerialized]
    public bool levelEnded = false;

    [System.NonSerialized]
    public bool controllable = true;

    public bool tutorialCheck;

    //Audio
    Transform playerShootAudioPooler;
    Transform playerHomingShootAudioPooler;
    Transform playerBeamShootAudioPooler;
    Transform playerGetHitPooler;
    Transform EmpAudio;

    //Plus bullet
    Transform plusBullet;
    Color fadeTimer = new Color(1, 1, 1, 1);
    bool bulletIncrement = false;

    //minus bullet
    Transform minusBullet;
    bool bulletDecrease = false;

    public GameObject pauseBtn;

    public bool isFrozen;
    public int freezeBreakCount;
    Transform frostbite;
    public Sprite[] frostbiteSprites;

    //Weapon
    [HideInInspector]
    public Weapon weapon;
    //int currentWeaponLevel;

    //Barrier
    [HideInInspector]
    public Barrier barrier;
    //int currentBarrierLevel;

    //Effects
    bool reverseControls = false;

    public GameObject gameOverPanel;

    //Sentries
    public Vector3 sentryOffset;
    public GameObject sentryOffense;
    public GameObject sentryDefense;
    GameObject sentry;

    bool usingMissle = false;
    bool usingBomb = false;

    TouchCheck touchCheck;

    void Awake()
    {
        enemiesKilled = PlayerPrefs.GetInt("ppEnemiesKilled", 0);
        playerDied = PlayerPrefs.GetInt("ppPlayerDied", 0);
        frostbiteSprites = Resources.LoadAll<Sprite>("Frostbite");

        if (PlayerPrefs.GetInt("ppPlayerGamemode", 0) == 0)
            gamemode = true;
        else
            gamemode = false;
    }

    void Start()
    {
        touchCheck = GetComponent<TouchCheck>();
        //Set current weapon
        //currentWeaponLevel = PlayerPrefs.GetInt("ppCurrentWeaponLevel", 1);
        //currentBarrierLevel = PlayerPrefs.GetInt("ppCurrentBarrierLevel", 1);

        shootPoint = transform.FindChild("shootPoint");
        //bulletPooler = transform.FindChild("bulletPooler");
        rotateShield = transform.FindChild("RotateShield");
        if (!tutorialCheck)
        {
            //emotionPoint = (shieldCount * 3) + 1;
            Debug.Log("Selected Level: " + PlayerPrefs.GetInt("ppSelectedLevel", -1));

            //if (PlayerPrefs.GetString("ppSelectedLevel") == "Level20")
                emotionPoint = (shieldCount * 3) + 1;
            //else
            //    emotionPoint = 100000;

            for (int i = 0; i < shieldCount; i++)
            {
                foreach (Transform child in rotateShield)
                {
                    if (child.name == i.ToString())
                    {
                        child.gameObject.SetActive(true);
                        GameObject go = child.gameObject;
                        shieldList.Add(go);
                    }
                }
            }
        }
        else
        {
            emotionPoint = 2;
            foreach (Transform child in rotateShield)
            {
                if (child.name == "tutorialShield")
                {
                    child.gameObject.SetActive(true);
                    GameObject go = child.gameObject;
                    shieldList.Add(go);
                }
            }
        }
        //		firstShield = rotateShield.FindChild("FirstShield");
        //		secondShield = rotateShield.FindChild("SecondShield");

        //cresPooler = transform.FindChild("cresPooler");
        //pulsePooler = transform.FindChild("pulsePooler");
        homingPooler = transform.FindChild("homingPooler");
        bombPooler = transform.FindChild("bombPooler");
        beamPooler = transform.FindChild("laserBeamPooler");

        enemyLockedOnList = new List<GameObject>();

        //audio
        playerShootAudioPooler = transform.FindChild("PlayerShootSoundPooler");
        playerHomingShootAudioPooler = transform.FindChild("PlayerHomingShootSoundPooler");
        playerBeamShootAudioPooler = transform.FindChild("PlayerBeamShootSoundPooler");
        playerGetHitPooler = transform.FindChild("PlayerGetHitPooler");
        EmpAudio = transform.FindChild("EmpSoundPooler");

        //plus bullet
        plusBullet = transform.FindChild("plusBullet");

        //minus bullet
        minusBullet = transform.FindChild("minusBullet");


        frostbite = transform.FindChild("FrostBite");
        canShoot = false;
        freezeBreakCount = 20;

        //Check for sentry
        if(PlayerPrefs.GetInt("ppSentryEquipped", 0) == 1)
        {
            PlayerPrefs.SetInt("ppNumSentry", PlayerPrefs.GetInt("ppNumSentry", 0) - 1);
            if (PlayerPrefs.GetInt("ppNumSentry", 0) <= 0)
                PlayerPrefs.SetInt("ppSentryEquipped", 0);
            if (gamemode)
            {
                //Offense
                sentry =  Instantiate(sentryOffense) as GameObject;
            }
            else
            {
                //Defense
                sentry = Instantiate(sentryDefense) as GameObject;
            }
            sentry.transform.SetParent(this.transform, false);
            sentry.transform.localPosition = sentryOffset;
        }

        if (PlayerPrefs.GetInt("ppBombEquipped", 0) == 1)
        {
            PlayerPrefs.SetInt("ppNumBombs", PlayerPrefs.GetInt("ppNumBombs", 0) - 1);
            if (PlayerPrefs.GetInt("ppNumBombs", 0) <= 0)
                PlayerPrefs.SetInt("ppBombEquipped", 0);

            usingBomb = true;
        }

        if (PlayerPrefs.GetInt("ppMissleEquipped", 0) == 1)
        {
            PlayerPrefs.SetInt("ppNumMissles", PlayerPrefs.GetInt("ppNumMissles", 0) - 1);
            if (PlayerPrefs.GetInt("ppNumMissles", 0) <= 0)
                PlayerPrefs.SetInt("ppMissleEquipped", 0);

            usingMissle = true;
        }

        PlayerPrefs.Save();
    }

    void FireBombBullet()
    {
        //FireBullet ();

        if (bombBulletAttackTimer <= 0.0f)
        {
            GameObject go = bombPooler.GetComponent<ObjectPooler>().GetPooledObject();
            if (go == null)
            {
                return;
            }

            go.transform.position = new Vector3(shootPoint.position.x - bulletOffset * 4f, shootPoint.position.y + bulletOffset * 2);
            go.SetActive(true);


            GameObject go2 = bombPooler.GetComponent<ObjectPooler>().GetPooledObject();
            if (go2 == null)
            {
                return;
            }
            go2.transform.position = new Vector3(shootPoint.position.x + bulletOffset * 4f, shootPoint.position.y + bulletOffset * 2);
            go2.SetActive(true);


            //to play audio
            GameObject audio = playerShootAudioPooler.GetComponent<ObjectPooler>().GetPooledObject();
            audio.SetActive(true);

            //resetting the attackTimer back to the original time
            bombBulletAttackTimer = 2f;
        }
    }

    void FireBeamBullet()
    {
        //FireBullet ();

        beamAttackTimer -= Time.deltaTime;
        if (beamAttackTimer <= 0.0f)
        {
            GameObject go = beamPooler.GetComponent<ObjectPooler>().GetPooledObject();
            if (go == null)
            {
                return;
            }

            go.transform.position = new Vector3(shootPoint.position.x, shootPoint.position.y - 0.625f);
            go.SetActive(true);


            //to play audio
            GameObject audio = playerBeamShootAudioPooler.GetComponent<ObjectPooler>().GetPooledObject();
            audio.SetActive(true);

            //resetting the attackTimer back to the original time
            beamAttackTimer = 3f;
        }
    }

    void FireHomingBullet()
    {
        //FireBullet ();
        if (homingAttackTimer <= 0.0f)
        {
            GameObject go1 = homingPooler.GetComponent<ObjectPooler>().GetPooledObject();
            if (go1 == null)
            {
                return;
            }

            // Homing missiles
            //setting the pooled object's position, rotation and active to true
            go1.transform.position = new Vector3(shootPoint.position.x - 0.6f, shootPoint.position.y - 0.6f);
            go1.transform.localEulerAngles = new Vector3(0f, 0f, 45f);
            go1.SetActive(true);

            GameObject go2 = homingPooler.GetComponent<ObjectPooler>().GetPooledObject();
            if (go2 == null)
            {
                return;
            }

            go2.transform.position = new Vector3(shootPoint.position.x + 0.6f, shootPoint.position.y - 0.6f);
            go2.transform.localEulerAngles = new Vector3(0f, 0f, -45f);
            go2.SetActive(true);


            //to play audio
            GameObject audio = playerHomingShootAudioPooler.GetComponent<ObjectPooler>().GetPooledObject();
            audio.SetActive(true);

            //resetting the attackTimer back to the original time
            homingAttackTimer = 1f;
        }

    }
    //void FireBullet()
    //{
    //    bulletAttackTimer -= Time.deltaTime;
    //    //if(bulletAttackTimer <= 0.0f)
    //    {
    //        GameObject go = bulletPooler.GetComponent<ObjectPooler>().GetPooledObject();
    //        if (go == null)
    //        {
    //            return;
    //        }

    //        //setting the pooled object's position, rotation and active to true
    //        go.transform.position = new Vector3(shootPoint.position.x, shootPoint.position.y + bulletOffset * 2);
    //        go.transform.rotation = transform.rotation;

    //        go.SetActive(true);

    //        //to play audio
    //        GameObject audio = playerShootAudioPooler.GetComponent<ObjectPooler>().GetPooledObject();
    //        audio.SetActive(true);

    //        switch (lastComboCount)
    //        {
    //            case 1:
    //                {
    //                    GameObject go1 = bulletPooler.GetComponent<ObjectPooler>().GetPooledObject();
    //                    if (go1 == null)
    //                    {
    //                        return;
    //                    }

    //                    //setting the pooled object's position, rotation and active to true
    //                    go.transform.position = new Vector3(shootPoint.position.x - bulletOffset, shootPoint.position.y);
    //                    go.transform.rotation = transform.rotation;
    //                    go.SetActive(true);

    //                    go1.transform.position = new Vector3(shootPoint.position.x + bulletOffset, shootPoint.position.y);
    //                    go1.transform.rotation = transform.rotation;
    //                    go1.SetActive(true);

    //                }
    //                break;
    //            case 2:
    //                {
    //                    //GetComponent<BulletBehaviour>().
    //                    GameObject go1 = bulletPooler.GetComponent<ObjectPooler>().GetPooledObject();
    //                    if (go1 == null)
    //                    {
    //                        return;
    //                    }

    //                    go1.transform.position = new Vector3(shootPoint.position.x + bulletOffset, shootPoint.position.y);
    //                    go1.transform.rotation = transform.rotation;
    //                    go1.SetActive(true);
    //                    GameObject go2 = bulletPooler.GetComponent<ObjectPooler>().GetPooledObject();

    //                    if (go2 == null)
    //                    {
    //                        return;
    //                    }
    //                    go2.transform.position = new Vector3(shootPoint.position.x, shootPoint.position.y + bulletOffset * 2);
    //                    go2.transform.rotation = transform.rotation;
    //                    go2.SetActive(true);

    //                    go.transform.position = new Vector3(shootPoint.position.x - bulletOffset, shootPoint.position.y);
    //                    go.transform.rotation = transform.rotation;
    //                    go.SetActive(true);

    //                }
    //                break;
    //            case 3:
    //                {
    //                    GameObject go1 = bulletPooler.GetComponent<ObjectPooler>().GetPooledObject();
    //                    if (go1 == null)
    //                    {
    //                        return;
    //                    }

    //                    go1.transform.position = new Vector3(shootPoint.position.x + 0.3f, shootPoint.position.y);
    //                    go1.transform.rotation = transform.rotation;
    //                    go1.SetActive(true);

    //                    GameObject go2 = bulletPooler.GetComponent<ObjectPooler>().GetPooledObject();
    //                    if (go2 == null)
    //                    {

    //                        return;
    //                    }
    //                    go2.transform.position = new Vector3(shootPoint.position.x - 0.3f, shootPoint.position.y);
    //                    go2.transform.rotation = transform.rotation;
    //                    go2.SetActive(true);

    //                    GameObject go3 = bulletPooler.GetComponent<ObjectPooler>().GetPooledObject();
    //                    if (go3 == null)
    //                    {

    //                        return;
    //                    }
    //                    go3.transform.position = new Vector3(shootPoint.position.x - 0.6f, shootPoint.position.y - 0.6f);
    //                    go3.transform.rotation = transform.rotation;
    //                    go3.SetActive(true);

    //                    go.transform.position = new Vector3(shootPoint.position.x + 0.6f, shootPoint.position.y - 0.6f);
    //                    go.transform.rotation = transform.rotation;
    //                    go.SetActive(true);

    //                }
    //                break;
    //            case 4:
    //                {
    //                    GameObject go1 = bulletPooler.GetComponent<ObjectPooler>().GetPooledObject();
    //                    if (go1 == null)
    //                    {
    //                        return;
    //                    }
    //                    go1.transform.position = new Vector3(shootPoint.position.x + 0.3f, shootPoint.position.y + 0.6f);
    //                    go1.transform.rotation = transform.rotation;
    //                    go1.SetActive(true);
    //                    GameObject go2 = bulletPooler.GetComponent<ObjectPooler>().GetPooledObject();

    //                    if (go2 == null)
    //                    {
    //                        return;
    //                    }
    //                    go2.transform.position = new Vector3(shootPoint.position.x - 0.3f, shootPoint.position.y + 0.6f);
    //                    go2.transform.rotation = transform.rotation;
    //                    go2.SetActive(true);

    //                    GameObject go3 = bulletPooler.GetComponent<ObjectPooler>().GetPooledObject();
    //                    if (go3 == null)
    //                    {
    //                        return;
    //                    }
    //                    go3.transform.position = new Vector3(shootPoint.position.x - 0.6f, shootPoint.position.y - 0.6f);
    //                    go3.transform.rotation = transform.rotation;
    //                    go3.SetActive(true);

    //                    GameObject go4 = bulletPooler.GetComponent<ObjectPooler>().GetPooledObject();
    //                    if (go4 == null)
    //                    {
    //                        return;
    //                    }

    //                    go4.transform.position = new Vector3(shootPoint.position.x + 0.6f, shootPoint.position.y - 0.6f);
    //                    go4.transform.rotation = transform.rotation;
    //                    go4.SetActive(true);

    //                    go.transform.position = new Vector3(shootPoint.position.x, shootPoint.position.y + 0.9f);
    //                    go.transform.rotation = transform.rotation;
    //                    go.SetActive(true);
    //                }
    //                break;
    //        }
    //        //resetting the attackTimer back to the original time
    //        bulletAttackTimer = 0.125f;
    //    }

    //}

    GameObject GenerateBullet(Transform pooler, Vector3 bulletPosition, Vector3 bulletScale, float bulletRotation)
    {
        GameObject go = pooler.GetComponent<ObjectPooler>().GetPooledObject();
        if (go == null)
            return null;

        //go.transform.position = new Vector3(bulletPosition.x, bulletPosition.y);
        go.transform.position = bulletPosition;
        go.transform.rotation = transform.rotation;
        go.transform.localScale = new Vector3(bulletScale.x, bulletScale.y, 1f);
        if (bulletRotation != 0f)
            go.transform.localEulerAngles = new Vector3(0f, 0f, bulletRotation);
        go.SetActive(true);

        //to play audio
        GameObject audio = playerShootAudioPooler.GetComponent<ObjectPooler>().GetPooledObject();
        audio.SetActive(true);

        return go;
    }

    void addBulletText()
    {
        plusBullet.GetComponent<SpriteRenderer>().enabled = true;
        fadeTimer.a -= Time.deltaTime * 1.0f;
        plusBullet.GetComponent<SpriteRenderer>().color = fadeTimer;

        if (plusBullet.GetComponent<SpriteRenderer>().color.a <= 0)
        {
            fadeTimer = new Color(1, 1, 1, 1);
            plusBullet.GetComponent<SpriteRenderer>().enabled = false;
            bulletIncrement = false;

        }
    }

    void minusBulletText()
    {
        minusBullet.GetComponent<SpriteRenderer>().enabled = true;
        fadeTimer.a -= Time.deltaTime * 1.0f;
        minusBullet.GetComponent<SpriteRenderer>().color = fadeTimer;

        if (minusBullet.GetComponent<SpriteRenderer>().color.a <= 0)
        {
            fadeTimer = new Color(1, 1, 1, 1);
            minusBullet.GetComponent<SpriteRenderer>().enabled = false;
            bulletDecrease = false;
        }
    }
    void checkComboSet()
    {

        topComboCount = comboCount;
        if (lastComboCount < 4)
        {
            if (comboCount >= comboSet[lastComboCount])
            {
                ++lastComboCount;
                bulletIncrement = true;

            }
        }
    }

    void FlyAway()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        transform.position = Vector3.Lerp(transform.position, new Vector3(0, SystemVariables.current.CameraBoundsY), Time.deltaTime * 1.0f);

        //DrugBusters Achievements
        if (enemiesKilled >= 100)
        {
            //googleplay achievement unlock
            //DrugBusters
            Social.ReportProgress("CgkI__bt5ooSEAIQBA", 100.0f, (bool success) =>
            {
                if (success)
                {
                    if (PlayerPrefs.GetInt("ppDrugBuster") == 0)
                    {
                        PlayerPrefs.SetInt("ppDrugBuster", 1);
                    }
                }

            });

            if (PlayerPrefs.GetInt("ppDrugBuster") == 1)
            {
                // increment achievement
                PlayGamesPlatform.Instance.IncrementAchievement(
                    "CgkI__bt5ooSEAIQEA", 5, (bool success) =>
                    {
                    });
                PlayerPrefs.SetInt("ppDrugBuster", 2);
            }
        }
        if (enemiesKilled >= 500)
        {
            //googleplay achievement unlock
            //Ramage
            Social.ReportProgress("CgkI__bt5ooSEAIQDA", 100.0f, (bool success) =>
            {
                if (success)
                {
                    if (PlayerPrefs.GetInt("ppRampage") == 0)
                    {
                        PlayerPrefs.SetInt("ppRampage", 1);
                    }
                }

            });

            if (PlayerPrefs.GetInt("ppRampage") == 1)
            {
                // increment achievement
                PlayGamesPlatform.Instance.IncrementAchievement(
                    "CgkI__bt5ooSEAIQEA", 5, (bool success) =>
                    {
                    });
                PlayerPrefs.SetInt("ppRampage", 2);
            }

        }

        PlayerPrefs.SetInt("ppEnemiesKilled", enemiesKilled);
        PlayerPrefs.Save();
        CancelInvoke("FlyAway");
    }

    public void ToggleBarrier()
    {
        barrier.ToggleBarrier();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.tag == "Enemy" || other.tag == "Ice_Chunk" || other.tag == "Enemy_Bullet" || other.tag == "LeafPad" || other.tag == "Minion") 
            && invuln == false)
        {
            DoDamaged();
            //if (other.tag == "Ice_Chunk")
            //{
            //    isFrozen = true;
            //    freezeBreakCount = 20;
            //    frostbite.gameObject.GetComponent<SpriteRenderer>().sprite = frostbiteSprites[freezeBreakCount - 1];
            //    frostbite.gameObject.SetActive(true);
            //}
        }
    }

    void Update()
    {
        if (Time.timeScale == 0)
            return;

        rotateShield.Rotate(0, 0, 60 * Time.deltaTime);

        if(homingAttackTimer > 0f)
            homingAttackTimer -= Time.deltaTime;
        if (bombBulletAttackTimer > 0f)
            bombBulletAttackTimer -= Time.deltaTime;

        //To constantly update the combosystem
        checkComboSet();

        //show bullet increment text
        //if (bulletIncrement == true)
        //{
        //    addBulletText();

        //}

        //// show bullet decrese text
        //if (bulletDecrease == true)
        //{
        //    minusBulletText();
        //}


        //		GameObject.Find ("ComboChain").GetComponent<Text> ().text = comboCount.ToString () + " " + "\nCombo";
        //GameObject.Find("ComboChain").GetComponent<Text>().text = comboCount.ToString() + " ";
        //GameObject.Find("ComboChainShadow").GetComponent<Text>().text = comboCount.ToString() + " ";

        //player cant exit the boundary
        if (transform.position.y + playerBoundary > SystemVariables.current.CameraBoundsY)
        {
            transform.position = new Vector3(transform.position.x, SystemVariables.current.CameraBoundsY - playerBoundary);

        }
        else if (transform.position.y - playerBoundary < -SystemVariables.current.CameraBoundsY)
        {
            transform.position = new Vector3(transform.position.x, -SystemVariables.current.CameraBoundsY + playerBoundary);
        }
        else if (transform.position.x + playerBoundary > SystemVariables.current.CameraBoundsX)
        {
            transform.position = new Vector3(SystemVariables.current.CameraBoundsX - playerBoundary, transform.position.y);
        }
        else if (transform.position.x - playerBoundary < -SystemVariables.current.CameraBoundsX)
        {
            transform.position = new Vector3(-SystemVariables.current.CameraBoundsX + playerBoundary, transform.position.y);
        }

        //PLayer health
        if (emotionPoint <= 0)
        {
            //GetComponent<CamShakeSimple>().CallCameraShake();
            gameObject.SetActive(false);
            gameOverPanel.SetActive(true);
            playerDied += 1;
            PlayerPrefs.SetInt("ppPlayerDied", playerDied);
            PlayerPrefs.Save();
            pauseBtn.GetComponent<Button>().interactable = false;
            Time.timeScale = 0;
        }

        //if player been hit then will go into the invulnerable mode.
        if (invuln == true)
        {
            invulnTimer -= Time.deltaTime;
        }
        if (invulnTimer <= 0)
        {
            invulnTimer = 3.0f;
            invuln = false;
            GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (levelEnded == false && controllable == true)
        {
#if UNITY_EDITOR

            //calculating the delta position of the Mouse
            currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            deltaMousePos = currentMousePos - lastMousePos;
            lastMousePos = currentMousePos;

            //move the player when mouse is held down
            if (isFrozen == false)
            {
                if (Input.GetMouseButton(0))
                {
                    if(reverseControls)
                        transform.position += new Vector3(-deltaMousePos.x, -deltaMousePos.y, 0) * moveSpeed;
                    else
                        transform.position += new Vector3(deltaMousePos.x, deltaMousePos.y, 0) * moveSpeed;

                    if(gamemode && canShoot)
                    {
                            weapon.FireWeapon();
                        if(usingMissle)
                            FireHomingBullet();
                        if (usingBomb)
                            FireBombBullet();
                    }
                    else if (touchCheck.CheckDoubleClick() && canShoot)
                        barrier.ToggleBarrier();
                }
            }
            else if (isFrozen == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (freezeBreakCount > 0)
                    {
                        frostbite.gameObject.GetComponent<SpriteRenderer>().sprite = frostbiteSprites[freezeBreakCount - 1];
                        --freezeBreakCount;

                    }
                    else
                    {
                        isFrozen = false;
                        frostbite.gameObject.SetActive(false);
                        transform.FindChild("DrugEffectDisplay").GetComponent<DrugEffectDisplay>().DisplayTapUI(false);
                    }
                }
            }

#endif
#if UNITY_STANDALONE_WIN
            //calculating the delta position of the Mouse
            currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            deltaMousePos = currentMousePos - lastMousePos;
            lastMousePos = currentMousePos;

            //move the player when mouse is held down
            if (isFrozen == false)
            {
                if (Input.GetMouseButton(0))
                {
                    if (reverseControls)
                        transform.position += new Vector3(-deltaMousePos.x, -deltaMousePos.y, 0) * moveSpeed;
                    else
                        transform.position += new Vector3(deltaMousePos.x, deltaMousePos.y, 0) * moveSpeed;

                    if (gamemode && canShoot)
                    {
                        weapon.FireWeapon();
            if(usingMissle)
                            FireHomingBullet();
                        if (usingBomb)
                            FireBombBullet();
                    }
                    else if (touchCheck.CheckDoubleClick() && canShoot)
                        barrier.ToggleBarrier();
                }
            }
            else if (isFrozen == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (freezeBreakCount > 0)
                    {
                        frostbite.gameObject.GetComponent<SpriteRenderer>().sprite = frostbiteSprites[freezeBreakCount - 1];
                        --freezeBreakCount;

                    }
                    else
                    {
                        isFrozen = false;
                        frostbite.gameObject.SetActive(false);
                                    transform.FindChild("DrugEffectDisplay").GetComponent<DrugEffectDisplay>().DisplayTapUI(false);
                    }
                }
            }
#endif

#if UNITY_ANDROID
            DoTouchControl();
#elif UNITY_IOS
            DoTouchControl();
#endif
        }

        //if level ended, fly away
        else if (levelEnded == true)
        {
            GetComponent<SpriteRenderer>().color = Color.grey;

            Invoke("FlyAway", 0.0f);
            if (emotionPoint == (shieldCount * 3) + 1)
            {
                //googleplay achievement unlock
                //Drugfree achievment
                Social.ReportProgress("CgkI__bt5ooSEAIQCg", 100.0f, (bool success) =>
                {
                    if (success)
                    {
                        if (PlayerPrefs.GetInt("ppDrugFree") == 0)
                        {
                            PlayerPrefs.SetInt("ppDrugFree", 1);
                        }
                    }

                });

                if (PlayerPrefs.GetInt("ppDrugFree") == 1)
                {
                    // increment achievement
                    PlayGamesPlatform.Instance.IncrementAchievement(
                        "CgkI__bt5ooSEAIQEA", 5, (bool success) =>
                        {
                        });
                    PlayerPrefs.SetInt("ppDrugFree", 2);
                }
            }
            else if (emotionPoint == 1 && PlayerPrefs.GetInt("ppSelectedLevel") == 21)
            {
                //googleplay achievement unlock
                //This time its personal achievment
                Social.ReportProgress("CgkI__bt5ooSEAIQEw", 100.0f, (bool success) =>
                {
                    if (success)
                    {
                        if (PlayerPrefs.GetInt("ppItsPersonal") == 0)
                        {
                            PlayerPrefs.SetInt("ppItsPersonal", 1);
                        }
                    }

                });

                if (PlayerPrefs.GetInt("ppItsPersonal") == 1)
                {
                    // increment achievement
                    PlayGamesPlatform.Instance.IncrementAchievement(
                        "CgkI__bt5ooSEAIQEw", 5, (bool success) =>
                        {
                        });
                    PlayerPrefs.SetInt("ppItsPersonal", 2);
                }
            }

            if (GetComponent<ScoringSystemStar>().enemiesKilled <= 0)
            {
                //googleplay achievement unlock
                //No Blood Shall Spill 
                Social.ReportProgress("CgkI__bt5ooSEAIQFA", 100.0f, (bool success) =>
                {
                    if (success)
                    {
                        if (PlayerPrefs.GetInt("ppNoBloodShallSpill") == 0)
                        {
                            PlayerPrefs.SetInt("ppNoBloodShallSpill", 1);
                        }
                    }

                });

                if (PlayerPrefs.GetInt("ppNoBloodShallSpill") == 1)
                {
                    // increment achievement
                    PlayGamesPlatform.Instance.IncrementAchievement(
                        "CgkI__bt5ooSEAIQFA", 5, (bool success) =>
                        {
                        });
                    PlayerPrefs.SetInt("ppNoBloodShallSpill", 2);
                }

            }

            if (GetComponent<ScoringSystemStar>().enemiesKilled == 1
               && (PlayerPrefs.GetInt("ppSelectedLevel", -1) == 13
                || PlayerPrefs.GetInt("ppSelectedLevel", -1) == 14
                || PlayerPrefs.GetInt("ppSelectedLevel", -1) == 15))
            {
                //googleplay achievement unlock
                //Prejudiced
                Social.ReportProgress("CgkI__bt5ooSEAIQFQ", 100.0f, (bool success) =>
                {
                    if (success)
                    {
                        if (PlayerPrefs.GetInt("ppPrejudiced") == 0)
                        {
                            PlayerPrefs.SetInt("ppPrejudiced", 1);
                        }
                    }

                });

                if (PlayerPrefs.GetInt("ppPrejudiced") == 1)
                {
                    // increment achievement
                    PlayGamesPlatform.Instance.IncrementAchievement(
                        "CgkI__bt5ooSEAIQFQ", 5, (bool success) =>
                        {
                        });
                    PlayerPrefs.SetInt("ppPrejudiced", 2);
                }

            }

            if ((PlayerPrefs.GetString("ppLevel9HighestGrade") == "S"
                && PlayerPrefs.GetString("ppLevel10HighestGrade") == "S"
                && PlayerPrefs.GetString("ppLevel11HighestGrade") == "S"))
            {
                //googleplay achievement unlock
                //Shoot.Save.Score
                Social.ReportProgress("CgkI__bt5ooSEAIQFw", 100.0f, (bool success) =>
                {
                    if (success)
                    {
                        if (PlayerPrefs.GetInt("ppShootSaveScore") == 0)
                        {
                            PlayerPrefs.SetInt("ppShootSaveScore", 1);
                        }
                    }

                });

                if (PlayerPrefs.GetInt("ppShootSaveScore") == 1)
                {
                    // increment achievement
                    PlayGamesPlatform.Instance.IncrementAchievement(
                        "CgkI__bt5ooSEAIQFw", 5, (bool success) =>
                        {
                        });
                    PlayerPrefs.SetInt("ppShootSaveScore", 2);
                }

            }

        }
    }

    void UpdateAchievements()
    {
        if (enemiesKilled >= 1)
        {
            //Google play achievement
            //First NO
            Social.ReportProgress("CgkI__bt5ooSEAIQAg", 100.0f, (bool success) =>
            {
                if (success)
                {
                    if (PlayerPrefs.GetInt("ppFirstNo") == 0)
                    {
                        PlayerPrefs.SetInt("ppFirstNo", 1);
                    }
                }

            });

            if (PlayerPrefs.GetInt("ppFirstNo") == 1)
            {
                // increment achievement
                PlayGamesPlatform.Instance.IncrementAchievement(
                    "CgkI__bt5ooSEAIQEA", 5, (bool success) =>
                    {
                    });
                PlayerPrefs.SetInt("ppFirstNo", 2);
            }

        }
        if (playerDied >= 10)
        {
            //Google play achievement
            //Overdose
            Social.ReportProgress("CgkI__bt5ooSEAIQEQ", 100.0f, (bool success) =>
            {
                if (success)
                {
                    if (PlayerPrefs.GetInt("ppOverdose") == 0)
                    {
                        PlayerPrefs.SetInt("ppOverdose", 1);
                    }
                }

            });

            if (PlayerPrefs.GetInt("ppOverdose") == 1)
            {
                // increment achievement
                PlayGamesPlatform.Instance.IncrementAchievement(
                    "CgkI__bt5ooSEAIQEA", 5, (bool success) =>
                    {
                    });
                PlayerPrefs.SetInt("ppOverdose", 2);
            }
        }
        //if (topComboCount >= 100)
        //{
        //    //googleplay achievement unlock
        //    //TriggerHappy achievemnt
        //    Social.ReportProgress("CgkI__bt5ooSEAIQAw", 100.0f, (bool success) =>
        //    {

        //        if (success)
        //        {
        //            if (PlayerPrefs.GetInt("ppHappyTrigger") == 0)
        //            {
        //                PlayerPrefs.SetInt("ppHappyTrigger", 1);
        //            }
        //        }

        //    });

        //    if (PlayerPrefs.GetInt("ppHappyTrigger") == 1)
        //    {
        //        // increment achievement
        //        PlayGamesPlatform.Instance.IncrementAchievement(
        //            "CgkI__bt5ooSEAIQEA", 5, (bool success) =>
        //            {
        //            });
        //        PlayerPrefs.SetInt("ppHappyTrigger", 2);
        //    }

        //}
        //if (topComboCount >= 500)
        //{
        //    //googleplay achievement unlock
        //    //combo addiction achievemnt
        //    Social.ReportProgress("CgkI__bt5ooSEAIQCw", 100.0f, (bool success) =>
        //    {
        //        if (success)
        //        {
        //            if (PlayerPrefs.GetInt("ppComboAddiction") == 0)
        //            {
        //                PlayerPrefs.SetInt("ppComboAddiction", 1);
        //            }
        //        }

        //    });

        //    if (PlayerPrefs.GetInt("ppComboAddiction") == 1)
        //    {
        //        // increment achievement
        //        PlayGamesPlatform.Instance.IncrementAchievement(
        //            "CgkI__bt5ooSEAIQEA", 5, (bool success) =>
        //            {
        //            });
        //        PlayerPrefs.SetInt("ppComboAddiction", 2);
        //    }
        //}
        //if (topComboCount >= 750)
        //{
        //    //googleplay achievement unlock
        //    //Akombo
        //    Social.ReportProgress("CgkI__bt5ooSEAIQFg", 100.0f, (bool success) =>
        //    {
        //        if (success)
        //        {
        //            if (PlayerPrefs.GetInt("ppAkombo") == 0)
        //            {
        //                PlayerPrefs.SetInt("ppAkombo", 1);
        //            }
        //        }

        //    });

        //    if (PlayerPrefs.GetInt("ppAkombo") == 1)
        //    {
        //        // increment achievement
        //        PlayGamesPlatform.Instance.IncrementAchievement(
        //            "CgkI__bt5ooSEAIQFg", 5, (bool success) =>
        //            {
        //            });
        //        PlayerPrefs.SetInt("ppAkombo", 2);
        //    }
        //}
    }

    public void DoDamaged()
    {
        if (barrier)
        {
            if (barrier.barrierActive)
                return;
        }
        //get hit audio
        //to play audio
        GameObject audio = playerGetHitPooler.GetComponent<ObjectPooler>().GetPooledObject();
        audio.SetActive(true);

        //			//call bomb
        //			GameObject go = Instantiate (Bomb, transform.position, Quaternion.identity) as GameObject;
        //			GameObject audio1 = EmpAudio.GetComponent<ObjectPooler> ().GetPooledObject ();
        //			audio1.SetActive (true);

        emotionPoint -= 1;
        invuln = true;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        topComboCount = 0;
        comboCount = 0;
        //print("damaged");
        if (lastComboCount > 0)
        {
            --lastComboCount;
            bulletDecrease = true;
        }
        //if (emotionPoint > 1)
            //GetComponent<CamShakeSimple>().CallCameraShake();
        /*if(firstLife == true && secondLife == true)
        {
            firstShield.GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CamShakeSimple>().CallCameraShake();
            firstLife = false;
        }
        else if(firstLife == false && secondLife == true)
        {
            secondShield.GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CamShakeSimple>().CallCameraShake();
        }*/
        if (emotionPoint >= 1)
        {
            if (shieldList[0].GetComponent<ShieldBehaviour>().removeShield())
            {
                //call bomb
                GameObject go1 = Instantiate(Bomb, transform.position, Quaternion.identity) as GameObject;
                GameObject audio1 = EmpAudio.GetComponent<ObjectPooler>().GetPooledObject();
                audio1.SetActive(true);

                shieldList[0].gameObject.SetActive(false);
                shieldList.RemoveAt(0);
            }
        }
    }

    public void SetReversedControls(bool status)
    {
        if (status)
            reverseControls = true;
        else
            reverseControls = false;
    }

    public void SetFrozen(bool status)
    {
        if(status)
        {
            isFrozen = true;
            freezeBreakCount = 20;
            frostbite.gameObject.GetComponent<SpriteRenderer>().sprite = frostbiteSprites[freezeBreakCount - 1];
            frostbite.gameObject.SetActive(true);
        }
        else
        {
            isFrozen = false;
            frostbite.gameObject.SetActive(false);
        }
    }

    void DoTouchControl()
    {

        //calculates number of touches in total, mainly for debugging purposes
        numberOfTouches = Input.touchCount;

        //move the player based on the deltaPosition of the Touch
        for (int i = 0; i < numberOfTouches; ++i)
        {
            Touch touch = Input.GetTouch(i);

            if (isFrozen == false)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    currentMousePos = Camera.main.ScreenToWorldPoint(touch.position);
                    lastMousePos = currentMousePos;
                    toShoot = true;
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    currentMousePos = Camera.main.ScreenToWorldPoint(touch.position);
                    Vector3 moveDir = currentMousePos - lastMousePos;
                    if (reverseControls)
                        transform.position += new Vector3(-moveDir.x, -moveDir.y, 0) * moveSpeed;
                    else
                        transform.position += new Vector3(moveDir.x, moveDir.y, 0) * moveSpeed;

                    lastMousePos = currentMousePos;
                }
                if (canShoot && gamemode)
                {
                    weapon.FireWeapon();
                    if (usingMissle)
                        FireHomingBullet();
                    if (usingBomb)
                        FireBombBullet();
                }
                else if (touchCheck.CheckDoubleTap() && canShoot)
                    barrier.ToggleBarrier();
            }
            else if (isFrozen == true)
            {
                if (touch.phase == TouchPhase.Ended)
                {
                    if (freezeBreakCount > 0)
                    {
                        frostbite.gameObject.GetComponent<SpriteRenderer>().sprite = frostbiteSprites[freezeBreakCount - 1];
                        --freezeBreakCount;

                    }
                    else
                    {
                        isFrozen = false;
                        frostbite.gameObject.SetActive(false);
                        transform.FindChild("DrugEffectDisplay").GetComponent<DrugEffectDisplay>().DisplayTapUI(false);
                    }
                }
            }
        }


    }
}

