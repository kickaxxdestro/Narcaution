using UnityEngine;
using System.Collections;

public class WeaponLaser : Weapon
{

    // Use this for initialization
    void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void FireWeapon()
    {
        if (attackTimer <= 0f)
        {
            base.PlayProjectileAudio();

            //Reset attack timer
            attackTimer = shootSpeed;
            switch (weaponLevel)
            {
                case 1:
                    base.GenerateBullet(new Vector3(transform.parent.position.x, transform.parent.position.y + 5f), new Vector3(0.5f, 5f, 1f), 0f);
                    break;
                case 2:
                    base.GenerateBullet(new Vector3(transform.parent.position.x, transform.parent.position.y + 5f), new Vector3(1f, 5f, 1f), 0f);

                    break;
                case 3:
                    base.GenerateBullet(new Vector3(transform.parent.position.x, transform.parent.position.y + 5f), new Vector3(1.5f, 5f, 1f), 0f);

                    break;
                case 4:
                    base.GenerateBullet(new Vector3(transform.parent.position.x, transform.parent.position.y + 5f), new Vector3(2f, 5f, 1f), 0f);

                    break;
                case 5:
                    base.GenerateBullet(new Vector3(transform.parent.position.x, transform.parent.position.y + 5f), new Vector3(2.6f, 5f, 1f), 0f);

                    break;
                default:
                    print("default case");
                    break;
            }
        }
    }
}
