using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RambbitWeapon : MonoBehaviour, IShootable
{
    [SerializeField] private WeaponStats weaponStats;
    [SerializeField] private Rigidbody2D rambbit;
    [SerializeField] private Rigidbody2D rambbitBullet;
    [SerializeField] private float shootDuration;
    [SerializeField] private float walkDuration;
    private bool canSubShoot = true;
    private bool canWalk = true;
    [SerializeField] private int level = 0;

    private bool canShoot = true;
    private bool coroutineStarted;
    private Vector2 targetPosition;
    private Animator animator;

    private void Start()
    {
        
    }

    public void Fire()
    {
        if (level > 0)
        {
            if (canShoot)
            {
                if (!coroutineStarted)
                {
                    canSubShoot = false;
                    StartCoroutine(ShootDuration());
                    StartCoroutine(TransitionTime());
                    coroutineStarted = true;
                    animator.ResetTrigger("Run");
                    animator.SetTrigger("Jump");
                }
                if (canSubShoot)
                {
                    canSubShoot = false;
                    Vector3 movementDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
                    float targetAngle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
                    Rigidbody2D shot = Instantiate(rambbitBullet, rambbit.position, Quaternion.Euler(0f, 0f, targetAngle));
                    shot.velocity = movementDirection;
                    shot.GetComponent<IBullet>().BulletModifiers = GetComponent<PlayerBehaviour>().Modifiers;
                    shot.GetComponent<IBullet>().NaturalDamageBuff(level);

                    StartCoroutine(ShootDelay());
                    if (movementDirection.x < 0f)
                    {
                        rambbit.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    else
                    {
                        rambbit.GetComponent<SpriteRenderer>().flipX = true;
                    }
                }
            }

            else
            {
                if (!coroutineStarted)
                {
                    canWalk = false;
                    StartCoroutine(WalkTime());
                    StartCoroutine(TransitionTime());
                    coroutineStarted = true;
                    targetPosition = transform.position + 10 * new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
                    animator.ResetTrigger("Jump");
                    animator.SetTrigger("Run");
                }

                if (canWalk)
                {
                    rambbit.MovePosition(rambbit.position + Time.fixedDeltaTime * (targetPosition - rambbit.position));
                }

            }
        }
    }

    private void InitiateRambbit()
    {
        rambbit = Instantiate(rambbit, transform.position, Quaternion.identity);
        animator = rambbit.GetComponent<Animator>();
    }

    public void LevelUp()
    {
        if (level == 0)
        {
            InitiateRambbit();
        }
        level += 1;
    }

    private float NaturalFireRateBuff()
    {
        return 1 - 2 * Mathf.Exp(-(level + 3.5f) / 7);
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(weaponStats.ReloadTime * (1 - 0.6f*NaturalFireRateBuff()));
        canSubShoot = true;
    }

    IEnumerator ShootDuration()
    {
        yield return new WaitForSeconds(shootDuration);
        canShoot = false;
        coroutineStarted = false;
    }

    IEnumerator WalkTime()
    {
        yield return new WaitForSeconds(walkDuration);
        canShoot = true;
        coroutineStarted = false;
    }

    IEnumerator TransitionTime()
    {
        yield return new WaitForSeconds(0.4f);
        canSubShoot = true;
        canWalk = true;
    }
}
