using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private static AudioManager audioManager;
    private static int givenGold = 2;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Player"))
        {
            audioManager.Play("Coin");
            collision.GetComponent<PlayerBehaviour>().AddGold(givenGold);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {

    }
}
