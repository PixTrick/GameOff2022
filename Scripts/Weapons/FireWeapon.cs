using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour, IShootable
{
    [SerializeField] private WeaponStats weaponStats;
    [SerializeField] private Rigidbody2D bullet;
    [SerializeField] private float bulletNumber = 8;
    [SerializeField] private int level = 0;

    private bool canShoot = true;

    public void Fire()
    {
        if (level > 0)
        {
            if (canShoot)
            {
                canShoot = false;
                for (int i = 0; i < bulletNumber; i++)
                {
                    Rigidbody2D shot = Instantiate(bullet, transform.position, Quaternion.Euler(0f, 0f, i * 360 / bulletNumber));
                    shot.velocity = shot.transform.up;
                    shot.GetComponent<IBullet>().BulletModifiers = GetComponent<PlayerBehaviour>().Modifiers;
                    shot.GetComponent<IBullet>().NaturalDamageBuff(level);
                    StartCoroutine(ReloadTime(weaponStats.ReloadTime));
                }

            }
        }
    }

    public void LevelUp()
    {
        level += 1;
        bulletNumber++;
    }

    private float NaturalFireRateBuff()
    {
        return 1 - 2 * Mathf.Exp(-(level + 3.5f) / 7);
    }

    public IEnumerator ReloadTime(float delay)
    {
        
        yield return new WaitForSeconds(delay * (1 - 0.2f * NaturalFireRateBuff()));
        canShoot = true;
    }
}
