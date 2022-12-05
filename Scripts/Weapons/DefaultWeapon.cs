using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultWeapon : MonoBehaviour, IShootable
{
    [SerializeField] private WeaponStats weaponStats;
    [SerializeField] private Rigidbody2D bullet;
    [SerializeField] private int level = 0;
    private Animator animator;

    private bool canShoot = true;

    private void Start()
    {
        animator = transform.Find("ArmCanvas").GetComponent<Animator>();
    }

    public void Fire()
    {
        if (level > 0)
        {
            if (canShoot)
            {
                animator.SetTrigger("Fire");
                canShoot = false;
                Vector2 movementDirection = GetComponent<Movement>().MouseDirection.normalized;
                float targetAngle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
                Rigidbody2D shot = Instantiate(bullet, transform.position, Quaternion.Euler(0f, 0f, targetAngle));
                shot.velocity = movementDirection;
                shot.GetComponent<IBullet>().BulletModifiers = GetComponent<PlayerBehaviour>().Modifiers;
                shot.GetComponent<IBullet>().NaturalDamageBuff(level);
                StartCoroutine(ReloadTime(weaponStats.ReloadTime));
            }
            else
            {
                animator.ResetTrigger("Fire");
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

    public IEnumerator ReloadTime(float delay)
    {
        
        yield return new WaitForSeconds(delay * (1 - 0.9f * NaturalFireRateBuff()));
        canShoot = true;
    }
}
