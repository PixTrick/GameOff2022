using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenToOne : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerBehaviour player;
    private float previousBonus = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = GetComponent<PlayerBehaviour>();
    }

    public void CheckKillCount()
    {
        float bonus = 1 + .01f * Mathf.FloorToInt(gameManager.KillScore / 10);
        if (bonus > 0 && bonus > previousBonus)
        {
            if (bonus > 1.01f)
            {
                player.MaxHealth /= bonus - .01f;
                player.Health /= bonus - .01f;
            }
            player.MaxHealth *= bonus;
            player.Health *= bonus;
            previousBonus = bonus;
        }
    }
}
