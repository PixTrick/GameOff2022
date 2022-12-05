using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float explosionRange = 5f;
    [SerializeField] private float explosionDamage = 10f;
    private float explosionDisablingEffectDuration = 0.3f;

    public float ExplosionDamage
    { get { return explosionDamage; } set { explosionDamage = value; } }
    public float ExplosionRange
    { get { return explosionRange; } set { explosionRange = value; } }
    // Start is called before the first frame update
    void Start()
    {
        CreateExplosion();
        Destroy(gameObject,0.3f);
    }

    private void CreateExplosion()
    {
        foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(transform.position, explosionRange))
        {
            if (collider2D.enabled)
            {
                Vector2 directionFromExplosion = (collider2D.transform.position - transform.position);
                float rawDamage = explosionDamage * Mathf.Abs(explosionRange - directionFromExplosion.magnitude) / explosionRange;
                if (collider2D.CompareTag("Enemy"))
                {
                    
                    EnemyBehaviour collider2DEnemyBehaviour = collider2D.GetComponent<EnemyBehaviour>();
                    collider2DEnemyBehaviour.TakeDamage(rawDamage);
                    collider2DEnemyBehaviour.DisableFor(explosionDisablingEffectDuration);
                    collider2DEnemyBehaviour.Knockback(directionFromExplosion, rawDamage * (explosionRange - directionFromExplosion.magnitude));
                }

                else if (collider2D.CompareTag("Player") && transform.CompareTag("Enemy"))
                {
                    PlayerBehaviour collider2DPlayerBehaviour = collider2D.GetComponent<PlayerBehaviour>();
                    collider2DPlayerBehaviour.TakeDamage(rawDamage);
                }
            }
            
        }
    }
}
