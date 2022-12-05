using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadgeOfHonor : MonoBehaviour
{
    private bool buffUsed = false;
    public void Revive()
    {
        PlayerBehaviour player = GetComponent<PlayerBehaviour>();
        if (!buffUsed)
        {
            player.Health = 0.5f * player.MaxHealth;
            buffUsed = true;
        }
        else
        {
            player.Die();
        }
    }
}
