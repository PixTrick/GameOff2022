using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumpDay : MonoBehaviour
{
    private PlayerBehaviour player;
    private float previousBuff = 0;
    private int level = 1;
    public int Level
    {
        get { return level; }
        set
        {
            level = value;
            UpdateBuff();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerBehaviour>();
        player.Modifiers.DamageModifier += HumpDayFunction(player.Health);
    }

    private float HumpDayFunction(float x)
    {
        previousBuff = (Mathf.Cos(x / player.MaxHealth * 20 / Mathf.PI + Mathf.PI) * 15 + 5) * level;
        return previousBuff;
    }

    public void ApplyBuff()
    {
        player.Modifiers.DamageModifier -= previousBuff;
        player.Modifiers.DamageModifier += HumpDayFunction(player.Health);
    }

    private void UpdateBuff()
    {
        ApplyBuff();
    }
}
