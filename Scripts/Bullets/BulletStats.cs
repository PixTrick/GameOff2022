using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Bullet Stats")]
public class BulletStats : ScriptableObject
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletSize;
    [SerializeField] private float bulletVolumicMass;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private float bulletDisablingEffectDuration;
    [SerializeField] private float bulletExplosionRange;

    public float BulletSpeed
    { 
        get { return bulletSpeed; } set { bulletSpeed = value; } 
    }
    public float BulletDamage
    { 
        get { return bulletDamage; } set { bulletDamage = value; } 
    }

    public float BulletSize
    { 
        get { return bulletSize; } set { bulletSize = value; } 
    }

    public float BulletVolumicMass
    { 
        get { return bulletVolumicMass; } set { bulletVolumicMass = value; } 
    }

    public float BulletLifeTime
    {
        get { return bulletLifeTime; } set { bulletLifeTime = value; }
    }

    public float BulletDisablingEffectDuration
    {
        get { return bulletDisablingEffectDuration; } set { bulletDisablingEffectDuration = value;}
    }

    public float BulletExplosionRange
    {
        get { return bulletExplosionRange; } set { bulletExplosionRange = value;}
    }
}
