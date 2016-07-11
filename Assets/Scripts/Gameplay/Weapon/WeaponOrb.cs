using UnityEngine;
using System.Collections;

public class WeaponOrb : Weapon
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
            GameObject temp;
            print("wep"+weaponLevel);
            switch (weaponLevel)
            {
                case 1:
                   temp = base.GenerateBullet(new Vector3(transform.parent.position.x, transform.parent.position.y + 0.7f), new Vector3(0.2f, 0.2f, 1f), 0f);
                    temp.GetComponent<sineMovement>().ResetOriginToCurrent();
                    temp.GetComponent<sineMovement>().distIncreaseAmount = 0.4f;
                    break;
                case 2:
                    temp = base.GenerateBullet(new Vector3(transform.parent.position.x, transform.parent.position.y + 0.7f), new Vector3(0.2f, 0.2f, 1f), 0f);
                    temp.GetComponent<sineMovement>().ResetOriginToCurrent();
                    temp.GetComponent<sineMovement>().distIncreaseAmount = 0.6f;

                    break;
                case 3:
                    temp = base.GenerateBullet(new Vector3(transform.parent.position.x, transform.parent.position.y + 0.7f), new Vector3(0.2f, 0.2f, 1f), 0f);
                    temp.GetComponent<sineMovement>().ResetOriginToCurrent();
                    temp.GetComponent<sineMovement>().distIncreaseAmount = 0.6f;

                    temp = base.GenerateBullet(new Vector3(transform.parent.position.x, transform.parent.position.y + 0.7f), new Vector3(0.2f, 0.2f, 1f), 0f);
                    temp.GetComponent<sineMovement>().ResetOriginToCurrent();
                    temp.GetComponent<sineMovement>().tick = 180;
                    temp.GetComponent<sineMovement>().distIncreaseAmount = 0.6f;

                    break;
                case 4:
                    temp = base.GenerateBullet(new Vector3(transform.parent.position.x, transform.parent.position.y + 0.7f), new Vector3(0.2f, 0.2f, 1f), 0f);
                    temp.GetComponent<sineMovement>().ResetOriginToCurrent();
                    temp.GetComponent<sineMovement>().distIncreaseAmount = 0.9f;

                    temp = base.GenerateBullet(new Vector3(transform.parent.position.x, transform.parent.position.y + 0.7f), new Vector3(0.2f, 0.2f, 1f), 0f);
                    temp.GetComponent<sineMovement>().ResetOriginToCurrent();
                    temp.GetComponent<sineMovement>().tick = 180;
                    temp.GetComponent<sineMovement>().distIncreaseAmount = 0.9f;

                    break;
                case 5:
                    temp = base.GenerateBullet(new Vector3(transform.parent.position.x, transform.parent.position.y + 0.7f), new Vector3(0.2f, 0.2f, 1f), 0f);
                    temp.GetComponent<sineMovement>().ResetOriginToCurrent();
                    temp.GetComponent<sineMovement>().distIncreaseAmount = 1f;

                    temp = base.GenerateBullet(new Vector3(transform.parent.position.x, transform.parent.position.y + 0.7f), new Vector3(0.2f, 0.2f, 1f), 0f);
                    temp.GetComponent<sineMovement>().ResetOriginToCurrent();
                    temp.GetComponent<sineMovement>().tick = 180;
                    temp.GetComponent<sineMovement>().distIncreaseAmount = 1f;

                    break;
                default:
                    break;
            }
        }
    }
}
