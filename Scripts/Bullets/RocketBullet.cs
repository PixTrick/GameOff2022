using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBullet : MonoBehaviour, IBullet
{
    private AudioManager audioManager;
    [SerializeField] private BulletStats bulletStats;
    [SerializeField] private GameObject explosion;
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
        GetComponent<Rigidbody2D>().velocity *= bulletStats.BulletSpeed;
        transform.localScale = bulletStats.BulletSize * Mathf.Pow(bulletModifiers.SizeModifier, 0.3f) * Vector3.one;
        GetComponent<Rigidbody2D>().mass = bulletStats.BulletVolumicMass * bulletStats.BulletSize;
        audioManager.Play("Rocket");
    }

    public BulletStats BulletStats
    {
        get { return bulletStats; }
        set { bulletStats = value; }
    }
    public void ApplyDamage(EnemyBehaviour enemy)
    {
        GetComponent<Collider2D>().enabled = false;
        GameObject rocket = Instantiate(explosion, transform.position, Quaternion.identity);
        rocket.tag = "Bullet";
        foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(enemy.transform.position, bulletStats.BulletExplosionRange))
        {
            if (collider2D.enabled && collider2D.CompareTag("Enemy") || collider2D.CompareTag("Crate"))
            {
                Vector2 directionFromExplosion = (collider2D.transform.position - enemy.transform.position);
                EnemyBehaviour collider2DEnemyBehaviour = collider2D.GetComponent<EnemyBehaviour>();
                collider2DEnemyBehaviour.TakeDamage(bulletStats.BulletDamage * Mathf.Abs(bulletStats.BulletExplosionRange - directionFromExplosion.magnitude) / bulletStats.BulletExplosionRange);
                collider2DEnemyBehaviour.DisableFor(bulletStats.BulletDisablingEffectDuration);
                collider2DEnemyBehaviour.Knockback(directionFromExplosion, bulletStats.BulletDamage * (bulletStats.BulletExplosionRange - directionFromExplosion.magnitude));
            }
        }
        Destroy(gameObject);
    }

    public void NaturalDamageBuff(int level)
    {
        naturalDamageBuff = Mathf.Pow(level, 0.13f) * Mathf.Log(level, Mathf.Exp(1)) / 2 + 1;
    }
}
 