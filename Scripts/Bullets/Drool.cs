using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drool : MonoBehaviour
{
    private bool canHit = true;
    private static float hitDelay = 0.8f;
    private static float hitdamage = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DroolHittingDelay()
    {
        canHit = false;
        yield return new WaitForSeconds(hitDelay);
        canHit = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyBehaviour>().TakeDamage(HitDamage());
            if (canHit)
            {
                StartCoroutine(DroolHittingDelay());
            }
        }
    }

    private float HitDamage()
    {
        if (canHit)
        {
            return hitdamage;
        }
        return 0;
    }
}
