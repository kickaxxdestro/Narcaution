using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Barrier : MonoBehaviour {
    
    public enum BARRIER_ABILITY
    {
        ABILITY_1,
        ABILITY_2,
        ABILITY_3,
        ABILITY_4,
        ABILITY_5,
    }

    public const int maxBarrierLevel = 5;

    public string name;
    public int ID;
    public Sprite icon;

    public float minimumActivationHealth;
    public float reflectedSpeed;        //Speed bullets are reflected at
    public float size;
    public BARRIER_ABILITY ability;

    public float[] LevelXReflectedDamage;
    public float[] LevelXRegeneration;
    public float[] LevelXDurability;
    public int[] LevelXCost;

    public string barrierDescription;

    protected int barrierLevel;
    protected float regenerationRate;
    protected float durability;    //Max health
    protected float reflectedDamage;

    public float currentHealth;
    public bool barrierActive = false;

    //public Image barrierDisplay;
    //public Image barrierMinDisplay;
    //public GameObject barrierButton;

    private AudioSource audioSource;
    public AudioClip onAudio;
    public AudioClip offAudio;

	// Use this for initialization
	protected void Start () {
        barrierLevel = PlayerPrefs.GetInt("ppCurrentBarrierLevel", 1);
        durability = LevelXDurability[barrierLevel - 1];
        regenerationRate = LevelXRegeneration[barrierLevel - 1];
        reflectedDamage = LevelXReflectedDamage[barrierLevel - 1];

        currentHealth = durability;

        this.transform.localScale = new Vector3(size, size, 1f);

        //barrierMinDisplay.fillAmount = minimumActivationHealth / durability;

        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
            print("Barrier missing audio source");

        //barrierActive = false;
        //GetComponent<Collider2D>().enabled = false;
        //GetComponent<SpriteRenderer>().enabled = false;
        //barrierButton.GetComponent<ImageSwap>().SetToDisabledImage();
	}
	
	// Update is called once per frame
	protected void Update () {
		/*if (Time.timeScale == 0)
            return;
		
	    if(barrierActive)
        {
            currentHealth -= Time.fixedDeltaTime;
            if(currentHealth <= 0f)
            {
                currentHealth = 0f;
                DisableBarrier();
            }
        }
        else
        {
            if(currentHealth < durability)
                currentHealth += regenerationRate * Time.deltaTime;
        }
        barrierDisplay.fillAmount = currentHealth / durability;

        if(currentHealth < minimumActivationHealth)
        {
            barrierMinDisplay.fillAmount = currentHealth / durability;
            if( barrierMinDisplay.fillAmount > minimumActivationHealth / durability)
                barrierMinDisplay.fillAmount = minimumActivationHealth / durability;
        }*/
	}

    public void ToggleBarrier()
    {
		/*
        if (barrierActive)
            DisableBarrier();
        else
            EnableBarrier();
            */
    }

    virtual public bool EnableBarrier()
    {
        if(currentHealth > minimumActivationHealth)
        {
            barrierActive = true;
            GetComponent<Collider2D>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
            //barrierButton.GetComponent<ImageSwap>().SetToEnabledImage();
            audioSource.clip = onAudio;
            audioSource.Play();
            return true;
        }
        return false;
    }

    virtual public bool DisableBarrier()
    {
        barrierActive = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        //barrierButton.GetComponent<ImageSwap>().SetToDisabledImage();
        audioSource.clip = offAudio;
        audioSource.Play();
        return true;
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Enemy_Bullet"))
        {
            coll.gameObject.GetComponent<bulletMove>().reflectDamage = reflectedDamage;
            print("BOING");
            coll.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(coll.gameObject.GetComponent<Rigidbody2D>().velocity.x * reflectedSpeed, coll.gameObject.GetComponent<Rigidbody2D>().velocity.y * reflectedSpeed);
        }
    }
}
