using UnityEngine;
using System.Collections;

//General weapon class
//[System.Serializable]
public class Weapon : MonoBehaviour {

    public enum PROJECTILE_MOVEMENT
    {
        MOVEMENT_FORWARD,
        MOVEMENT_INSTANT,
        MOVEMENT_CURVED,
    }

    public enum FIRING_PATTERN
    {
        PATTERN_NORMAL,
        PATTERN_CONE,
        PATTERN_SINGLE,
        PATTERN_BURST,
    }

    public string name;                     //Weapon name
    public int ID;                          //Weapon id
    public Sprite icon;

    public float projectileSpeed;           //Projectile speed is same for all 5 levels

    public int[] LevelXCost;                //Cost to upgrade / purchase level X of weapon
    public int[] LevelXNumberOfProjectiles; //Number of projectiles fired at weapon level X
    public float[] LevelXBulletDamage;
    public float[] LevelXFiringSpeed;

    [Tooltip("Type of projectile movement")]
    public PROJECTILE_MOVEMENT projectileMovement = PROJECTILE_MOVEMENT.MOVEMENT_FORWARD;
    [Tooltip("Type of firing pattern")]
    public FIRING_PATTERN firingPattern = FIRING_PATTERN.PATTERN_NORMAL;

    public string weaponDescription;

    public const int maxWeaponLevel = 5;

    GameObject bulletPooler;
    GameObject audioPooler;

    protected int weaponLevel;
    protected float attackTimer;
    protected float bulletDamage;
    protected float shootSpeed;

    protected void Awake()
    {
        weaponLevel = PlayerPrefs.GetInt("ppCurrentWeaponLevel", 1);
        bulletDamage = LevelXBulletDamage[weaponLevel - 1];
        shootSpeed = LevelXFiringSpeed[weaponLevel - 1];
    }

	// Use this for initialization
	void Start () {
	    foreach(Transform child in transform)
        {
            if (child.name == "BulletPooler")
                bulletPooler = child.gameObject;
            else if (child.name == "AudioPooler")
                audioPooler = child.gameObject;
        }
	}
	
	// Update is called once per frame
	protected void Update () {
        if (attackTimer > 0f)
            attackTimer -= Time.deltaTime;
	}

    virtual public void FireWeapon()
    {
    }

    protected GameObject GenerateBullet(Vector3 bulletPosition, Vector3 bulletScale, float bulletRotation)
    {
        GameObject go = bulletPooler.GetComponent<ObjectPooler>().GetPooledObject();
        if (go == null)
            return null;

        //go.transform.position = new Vector3(bulletPosition.x, bulletPosition.y);
        go.transform.position = bulletPosition;
        go.transform.rotation = transform.rotation;
        go.transform.localScale = new Vector3(bulletScale.x, bulletScale.y, 1f);
        if (bulletRotation != 0f)
            go.transform.localEulerAngles = new Vector3(0f, 0f, bulletRotation);
        go.GetComponent<BulletBehaviour>().bulletDamage = this.bulletDamage;
        go.GetComponent<BulletBehaviour>().bulletSpeed = this.projectileSpeed;
        go.SetActive(true);

        return go;
    }

    protected void PlayProjectileAudio()
    {
        //to play audio
        GameObject audio = audioPooler.GetComponent<ObjectPooler>().GetPooledObject();
        audio.SetActive(true);
    }

    public static string GetFiringPattenString(FIRING_PATTERN pattern)
    {
        switch(pattern)
        {
            case FIRING_PATTERN.PATTERN_NORMAL:
                return "Normal";
            case FIRING_PATTERN.PATTERN_CONE:
                return "Cone";
            case FIRING_PATTERN.PATTERN_SINGLE:
                return "Single";
            case FIRING_PATTERN.PATTERN_BURST:
                return "Burst";
            default:
                return "";
        }
    }

    public static string GetProjectileMovementString(PROJECTILE_MOVEMENT movement)
    {
        switch(movement)
        {
            case PROJECTILE_MOVEMENT.MOVEMENT_FORWARD:
                return "Straight";
            case PROJECTILE_MOVEMENT.MOVEMENT_INSTANT:
                return "Instant";
            case PROJECTILE_MOVEMENT.MOVEMENT_CURVED:
                return "Curved";
            default:
                return "";
        }
    }

    public static float CalculateDPS(Weapon weapon, int weaponLevel)
    {
        float fireSpeed, projectileDamage, numProjectiles;
        fireSpeed = weapon.LevelXFiringSpeed[weaponLevel - 1];
        projectileDamage = weapon.LevelXBulletDamage[weaponLevel - 1];
        numProjectiles = weapon.LevelXNumberOfProjectiles[weaponLevel - 1];

        return ((1f / fireSpeed) * projectileDamage * numProjectiles);
    }
}
