using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Bonus")]
public class Bonus : ScriptableObject
{
    public enum BonusType { BabyBoom, BadgeOfHonor, ColdFeet, ColdBlood, DieHard, HumpDay, MurphysLaw, PigeonChested, PokerFace, RunForest, SnailPace, TenToOne, ThatsAboutTheSizeOfIt,TimeCureAllWounds, TwoFaced, Magnet };
    [SerializeField] private BonusType type;
    [SerializeField] private new string name;
    [SerializeField] private string description;
    [SerializeField] private Sprite icon;
    [SerializeField] private string[] levelDescriptions;
    [SerializeField] private int maxLevel;

    public string Name
        { get { return name; } set { name = value; } }
    public string Description
        { get { return description; } set { description = value; } }
    public Sprite Icon
        { get { return icon; } set { icon = value; } }
    public BonusType Type
        { get { return type; } set { type = value; } }
    public string[] LevelDescriptions
        { get { return levelDescriptions; } set { levelDescriptions = value; } }
    public int MaxLevel
        { get { return maxLevel; } set { maxLevel = value; } }
}
