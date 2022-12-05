using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBonuses : MonoBehaviour
{
    private Dictionary<Bonus, int> playerBonusList = new();

    public Dictionary<Bonus, int> PlayerBonusList
    {
        get 
        { 
            return playerBonusList; 
        }
        set 
        {
            playerBonusList = value;
        }
    }

    private void Start()
    {

    }

    public void AddBonus(Bonus bonus)
    {
        if (playerBonusList.ContainsKey(bonus))
        {
            playerBonusList[bonus]++; //FAIRE ATTENTION S'IL Y A LIMITATION DE NIVEAUX
        }

        else
        {
            playerBonusList.Add(bonus,1);
        }

        foreach (KeyValuePair<Bonus, int> bonusCouple in playerBonusList)
        {
            Debug.Log(bonusCouple);
        }

        ApplyBonus(bonus);
    }

    private void ApplyBonus(Bonus bonus)
    {
        switch(bonus.Type)
        {
            case Bonus.BonusType.ColdBlood:
                TryGetComponent<ColdBlood>(out ColdBlood cb);
                if (cb != null)
                {
                    cb.Level++;
                }
                else
                {
                    gameObject.AddComponent<ColdBlood>();
                }
                break;
            case Bonus.BonusType.HumpDay:
                TryGetComponent<HumpDay>(out HumpDay hd);
                if (hd != null)
                {
                    hd.Level++;
                }
                else
                {
                    gameObject.AddComponent<HumpDay>();
                }
                break;
            case Bonus.BonusType.BabyBoom:
                TryGetComponent<BabyBoom>(out BabyBoom bb);
                if (bb != null)
                {
                    bb.Level++;
                }
                else
                {
                    gameObject.AddComponent<BabyBoom>();
                }
                break;
            case Bonus.BonusType.BadgeOfHonor:
                gameObject.AddComponent<BadgeOfHonor>();
                break;
            case Bonus.BonusType.PigeonChested:
                GetComponent<PlayerBehaviour>().PlayerModifiers.DamageReductionModifier -= 0.05f;
                break;
            case Bonus.BonusType.PokerFace:
                break;
            case Bonus.BonusType.RunForest:
                GetComponent<Movement>().Speed *= 1.1f;
                break;
            case Bonus.BonusType.SnailPace:
                GetComponent<Movement>().Speed *= 0.8f;
                GetComponent<SnailPace>().Level += 1;
                break;
            case Bonus.BonusType.TenToOne:
                gameObject.AddComponent<TenToOne>();
                break;
            case Bonus.BonusType.ThatsAboutTheSizeOfIt:
                GetComponent<PlayerBehaviour>().Modifiers.SizeModifier *= 1.5f;
                break;
            case Bonus.BonusType.TwoFaced:
                GetComponent<PlayerBehaviour>().PlayerModifiers.DamageReductionModifier += 1f;
                GetComponent<PlayerBehaviour>().Modifiers.DamageModifier *= 2f;
                break;
            case Bonus.BonusType.Magnet:
                GetComponent<Magnet>().Level += 1;
                break;
        }
    }
}
