using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Crate : MonoBehaviour, IEnemy
{
    [SerializeField] GameObject coin;
    [SerializeField] private GameObject floatingDisplay;

    private void Start()
    {
        
    }

    public void Move()
    {

    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            GameObject goldText = Instantiate(floatingDisplay, transform.position, Quaternion.identity, transform.parent);
            goldText.transform.GetChild(0).GetComponent<TMP_Text>().text = "All that glitters is gold";
            int randint = Random.Range(30, 75);
            for (int i = 0; i < randint; i++)
            {
                Vector3 randomPositionOffset = new Vector3 (Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
                Instantiate(coin, transform.position + randomPositionOffset, Quaternion.identity, transform.parent);
            }
        }
    }
}
