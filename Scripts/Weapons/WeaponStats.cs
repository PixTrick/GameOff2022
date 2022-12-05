using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Weapon Stats")]
public class WeaponStats : ScriptableObject
{
    [SerializeField] private float reloadTime;

    public float ReloadTime
    {
        get { return reloadTime; }
        set { reloadTime = value; }
    }
}
