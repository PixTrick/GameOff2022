using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private float attractionRange = 8f;
    private float attractionSpeed = 50f;

    private int level = 1;
    public int Level
        { get { return level; } set { level = value; } }

    // Update is called once per frame
    void Update()
    {
        CheckMagnet();
    }

    private void CheckMagnet()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attractionRange * (1 + 0.20f * level));
        if (colliders.Length > 0)
        {
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Coin") || collider.CompareTag("Lemon"))
                {
                    collider.transform.position += attractionSpeed * Time.deltaTime * (transform.position - 2.7f * Vector3.up - collider.transform.position).normalized;
                }
            }
        }
    }
}
