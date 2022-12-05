using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModifiers
{
    private float maxHealthModifier = 1;
    private float damageReductionModifier = 1;

    public float MaxHealthModifier
    { get { return maxHealthModifier; } set { maxHealthModifier = value; } }
    public float DamageReductionModifier
    { get { return damageReductionModifier; } set { damageReductionModifier = value; } }
}
