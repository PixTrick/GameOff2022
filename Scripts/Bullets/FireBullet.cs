using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour, IBullet
{
    private AudioManager audioManager;
    [SerializeField] private BulletStats bulletStats;
    private BulletModifiers bulletModifiers;
    private float naturalDamageBuff = 1;

    public BulletModifiers BulletModifiers
    {
        get { return bulletModifiers; }
        set { bulletModifiers = value; }
    }

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        /*bulletModifiers = GetComponent<PlayerBehaviour>()*/
        GetComponent<Rigidbody2D>().velocity *= bulletStats.BulletSpeed;
        transform.localScale = bulletStats.BulletSize * Mathf.Pow(bulletModifiers.SizeModifier,0.3f) * Vector3.one;
        GetComponent<Rigidbody2D>().mass = bulletStats.BulletVolumicMass * bulletStats.BulletSize;
        Destroy(gameObject, bulletStats.BulletLifeTime);
        audioManager.Play("Fire");
    }

    public BulletStats BulletStats
    {
        get { return bulletStats; }
        set { bulletStats = value; }
    }
    public void ApplyDamage(EnemyBehaviour enemy)
    {
        enemy.TakeDamage(bulletStats.BulletDamage * bulletModifiers.DamageModifier * naturalDamageBuff);
    }

    public void NaturalDamageBuff(int level)
    {
        naturalDamageBuff = Mathf.Pow(level, 0.13f) * Mathf.Log(level, Mathf.Exp(1)) / 6 + 1;
    }
}
 