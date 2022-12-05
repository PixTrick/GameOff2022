using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultBullet : MonoBehaviour, IBullet
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
        transform.localScale = bulletStats.BulletSize * bulletModifiers.SizeModifier * Vector3.one;
        GetComponent<Rigidbody2D>().mass = bulletStats.BulletVolumicMass * bulletStats.BulletSize;
        audioManager.Play("Pew");
    }

    public BulletStats BulletStats
    {
        get { return bulletStats; }
        set { bulletStats = value; }
    }
    public void ApplyDamage(EnemyBehaviour enemy)
    {
        enemy.TakeDamage(bulletStats.BulletDamage * bulletModifiers.DamageModifier * naturalDamageBuff);
        enemy.DisableFor(bulletStats.BulletDisablingEffectDuration);
        Destroy(gameObject);
    }

    public void NaturalDamageBuff(int level)
    {
        naturalDamageBuff = Mathf.Pow(level, 0.13f) * Mathf.Log(level, Mathf.Exp(1)) / 2 + 1;
    }
}
 