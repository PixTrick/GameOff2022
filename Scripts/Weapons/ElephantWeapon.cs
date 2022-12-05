using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantWeapon : MonoBehaviour, IShootable
{
    [SerializeField] private WeaponStats weaponStats;
    [SerializeField] private Rigidbody2D elephant;
    [SerializeField] private int level = 0;

    private bool canShoot = true;
    private Vector2 targetPosition;

    public void Fire()
    {
        if (level > 0)
        {
            if (canShoot)
            {
                canShoot = false;
                targetPosition = transform.position + 10 * new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
                Rigidbody2D shot = Instantiate(elephant, targetPosition, Quaternion.Euler(0f, 0f, 0f));
                shot.GetComponent<IBullet>().BulletModifiers = GetComponent<PlayerBehaviour>().Modifiers;
                shot.GetComponent<IBullet>().NaturalDamageBuff(level);
                StartCoroutine(ShootDelay());
            }
        }
    }

    public void LevelUp()
    {
        level += 1;
    }

    private float NaturalFireRateBuff()
    {
        return 1 - 2 * Mathf.Exp(-(level + 3.5f) / 7);
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(weaponStats.ReloadTime * (1 - 0.3f * NaturalFireRateBuff()));
        canShoot = true;
    }
}
