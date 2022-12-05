using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private static float defaultBulletLifeTime = 5f;

    private void Start()
    {
        Destroy(gameObject, defaultBulletLifeTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("Crate"))
        {
            GetComponent<IBullet>().ApplyDamage(collision.transform.GetComponent<EnemyBehaviour>());
        }
    }
}
