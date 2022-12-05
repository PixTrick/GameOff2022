using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lemon : MonoBehaviour
{
    private static AudioManager audioManager;
    [SerializeField] private int givenExperience = 1;

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
            audioManager.Play("Lemon");
            collision.GetComponent<PlayerBehaviour>().AddExperience(givenExperience);
            Destroy(gameObject);
        }
    }
}
