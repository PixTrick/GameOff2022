using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBehaviour : MonoBehaviour, IEnemy
{
    private static Rigidbody2D target;
    private Rigidbody2D rb;
    private EnemyBehaviour enemyBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        enemyBehaviour = GetComponent<EnemyBehaviour>();
    }

    public void Move()
    {
        if (rb != null && target != null)
        {
            Vector2 direction = (target.position - rb.position).normalized;
            if (direction.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            rb.MovePosition(rb.position + direction * enemyBehaviour.Speed * Time.fixedDeltaTime);
        }
    }
}
