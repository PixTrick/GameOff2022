using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudWeapon : MonoBehaviour, IShootable
{
    [SerializeField] private WeaponStats weaponStats;
    [SerializeField] private Rigidbody2D cloud;
    private bool canShoot = true;
    [SerializeField] private int level = 0;

    private void Start()
    {
        
    }

    public void Fire()
    {
        if (level > 0)
        {
            if (canShoot)
            {
                Debug.Log("Fire");
                canShoot = false;
                cloud.GetComponent<Animator>().SetTrigger("Fire");
                StartCoroutine(ShootDelay());
            }

            cloud.transform.localPosition += 0.08f * Vector3.Cross(cloud.transform.GetChild(0).position - transform.position, Vector3.forward).normalized;
        }
    }

    private void InitiateCloud()
    {
        cloud = Instantiate(cloud, transform.position, Quaternion.identity, transform);
        cloud.GetComponent<IBullet>().BulletModifiers = GetComponent<PlayerBehaviour>().Modifiers;
    }

    public void LevelUp()
    {
        if (level == 0)
        {
            InitiateCloud();
        }
        level += 1;
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(weaponStats.ReloadTime);
        canShoot = true;
    }
}
