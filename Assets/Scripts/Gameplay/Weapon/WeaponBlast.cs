using UnityEngine;
using System.Collections;

public class WeaponBlast : Weapon
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
            attackTimer = LevelXFiringSpeed[weaponLevel - 1];
            switch (weaponLevel)
            {
                case 1:
                    base.GenerateBullet(new Vector3(transform.parent.position.x, transform.parent.position.y + 0.7f), new Vector3(0.8f, 0.8f, 1f), 0f);
                    break;
                case 2:
                    base.GenerateBullet(new Vector3(transform.parent.position.x - 0.2f, transform.parent.position.y + 0.7f), new Vector3(0.8f, 0.8f, 1f), 0f);
                    base.GenerateBullet(new Vector3(transform.parent.position.x + 0.2f, transform.parent.position.y + 0.7f), new Vector3(0.8f, 0.8f, 1f), 0f);
                    break;
                case 3:
                    base.GenerateBullet(new Vector3(transform.parent.position.x - 0.2f, transform.parent.position.y + 0.7f), new Vector3(0.8f, 0.8f, 1f), 0f);
                    base.GenerateBullet(new Vector3(transform.parent.position.x + 0.2f, transform.parent.position.y + 0.7f), new Vector3(0.8f, 0.8f, 1f), 0f);
                    break;
                case 4:
                    base.GenerateBullet(new Vector3(transform.parent.position.x - 0.3f, transform.parent.position.y + 0.6f), new Vector3(0.8f, 0.8f, 1f), 0f);
                    base.GenerateBullet(new Vector3(transform.parent.position.x + 0.3f, transform.parent.position.y + 0.6f), new Vector3(0.8f, 0.8f, 1f), 0f);
                    base.GenerateBullet(new Vector3(transform.parent.position.x, transform.parent.position.y + 0.7f), new Vector3(0.8f, 0.8f, 1f), 0f);
                    break;
                case 5:
                    base.GenerateBullet(new Vector3(transform.parent.position.x - 0.3f, transform.parent.position.y + 0.6f), new Vector3(0.8f, 0.8f, 1f), 0f);
                    base.GenerateBullet(new Vector3(transform.parent.position.x + 0.3f, transform.parent.position.y + 0.6f), new Vector3(0.8f, 0.8f, 1f), 0f);
                    base.GenerateBullet(new Vector3(transform.parent.position.x, transform.parent.position.y + 0.7f), new Vector3(0.8f, 0.8f, 1f), 0f);
                    break;
                default:
                    print("default case");
                    break;
            }
        }
    }
}
