using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBehaviour : MonoBehaviour, IEnemy
{
    [SerializeField] private float triggerDistance;
    [SerializeField] private GameObject explosion;
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move()
    {
        if (rb != null && target != null)
        {
            Vector2 direction = target.position - rb.position;
            if (direction.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            if(direction.magnitude > triggerDistance)
            {
                rb.MovePosition(rb.position + direction.normalized * enemyBehaviour.Speed * Time.fixedDeltaTime);
            }
            else
            {
                GetComponent<Animator>().SetTrigger("DogExplode");
                Destroy(gameObject, 0.8f);
            }
        }
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }
}
