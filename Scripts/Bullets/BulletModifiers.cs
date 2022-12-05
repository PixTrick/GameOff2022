using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletModifiers
{
    private float damageModifier = 1;
    private float sizeModifier = 1;

    public float DamageModifier
    { get { return damageModifier; } set { damageModifier = value; } }
    public float SizeModifier
    { get { return sizeModifier; } set { sizeModifier = value; } }
}
