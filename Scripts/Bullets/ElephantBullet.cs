using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElephantBullet : MonoBehaviour, IBullet
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
    public BulletStats BulletStats
    {
        get { return bulletStats; }
        set { bulletStats = value; }
    }

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        transform.localScale = Mathf.Pow(bulletModifiers.SizeModifier, 0.3f) * bulletStats.BulletSize * Vector3.one;
        GetComponent<Rigidbody2D>().mass = bulletStats.BulletVolumicMass * bulletStats.BulletSize;
        audioManager.Play("Elephant");
    }

    public void ApplyDamage(EnemyBehaviour enemy)
    {
        Vector2 directionFromExplosion = (enemy.transform.position - transform.position - transform.localScale.x * Vector3.up);
        enemy.TakeDamage(bulletStats.BulletDamage * naturalDamageBuff);
        enemy.DisableFor(bulletStats.BulletDisablingEffectDuration);
        enemy.Knockback(directionFromExplosion, bulletStats.BulletDamage);
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1f);
        audioManager.Play("Boom");
    }

    public void NaturalDamageBuff(int level)
    {
        naturalDamageBuff = Mathf.Pow(level, 0.13f) * Mathf.Log(level, Mathf.Exp(1)) / 2 + 1;
    }
}
 