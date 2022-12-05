using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializedBonus
{
    [SerializeField] private Bonus bonus;
    [SerializeField] private int bonusLevel;

    public Bonus Bonus
    { 
        get { return bonus; } 
        set { bonus = value; }
    }
    public int BonusLevel
    { 
        get { return bonusLevel; }
        set { bonusLevel = value; }
    }
}
