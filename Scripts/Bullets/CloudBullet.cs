using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBullet : MonoBehaviour, IBullet
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("Crate"))
        {
            ApplyDamage(collision.transform.GetComponent<EnemyBehaviour>());
        }
    }

    public void ApplyDamage(EnemyBehaviour enemy)
    {
        enemy.TakeDamage(bulletStats.BulletDamage * bulletModifiers.DamageModifier);
        enemy.DisableFor(bulletStats.BulletDisablingEffectDuration);
    }
    public void NaturalDamageBuff(int level)
    {
        naturalDamageBuff = Mathf.Pow(level, 0.13f) * Mathf.Log(level, Mathf.Exp(1)) / 2 + 1;
    }
}
