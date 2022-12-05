using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdBlood : MonoBehaviour
{
    private PlayerBehaviour player;
    private bool buffIsActive = false;
    // Start is called before the first frame update
    private int level;
    public int Level { 
        get { return level; } 
        set 
        { 
            level = value; 
            UpdateBuff();
        }
    }

    void Start()
    {
        player = GetComponent<PlayerBehaviour>();
        CheckHealth();
    }

    public void CheckHealth()
    {
            if (player.Health < 0.25f * player.MaxHealth && !buffIsActive)
            {
                player.Modifiers.DamageModifier += 0.25f * level;
                buffIsActive = true;
                
            }
            else if (player.Health > 0.25f * player.MaxHealth && buffIsActive)
            {
                player.Modifiers.DamageModifier -= 0.25f * level;
            buffIsActive = false;
            }
    }

    private void UpdateBuff()
    {
        if (buffIsActive)
        {
            player.Modifiers.DamageModifier -= 0.25f * (level - 1);
            player.Modifiers.DamageModifier += 0.25f * level;
        }
    }
}
