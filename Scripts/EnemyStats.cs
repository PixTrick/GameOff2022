using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxHealth;
    [SerializeField] private float experienceDropped;

    public float MaxSpeed
    {
        get { return maxSpeed; }
        set { maxSpeed = value; }
    }
    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public float ExperienceDropped
    {
        get { return experienceDropped; }
        set { experienceDropped = value; }
    }
}