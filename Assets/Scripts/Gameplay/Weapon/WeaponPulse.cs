using UnityEngine;
using System.Collections;

public class WeaponPulse : Weapon
{
    int bulletsStored = 0;
    float pulseTimer = 0f;

    // Use this for initialization
    void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
            return;

        base.Update();
        if (bulletsStored > 0)
        {
            pulseTimer -= Time.fixedDeltaTime;
            if (pulseTimer <= 0f)
            {
                ShootSingleBullet();
                --bulletsStored;
                pulseTimer = 0.05f;
            }
        }
    }

    public override void FireWeapon()
    {
        if (attackTimer <= 0f)
        {


            //Reset attack timer
            attackTimer = shootSpeed;

            bulletsStored = LevelXNumberOfProjectiles[weaponLevel - 1];
        }
    }

    void ShootSingleBullet()
    {
        base.PlayProjectileAudio();
        base.GenerateBullet(new Vector3(transform.parent.position.x, transform.parent.position.y + 0.7f), new Vector3(0.15f, 0.15f, 1f), 0f);
    }
}
