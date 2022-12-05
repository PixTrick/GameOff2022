using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpMenu : MonoBehaviour
{
    [SerializeField] private List<Bonus> bonusList;
    [SerializeField] private List<Button> buttonList;
    private Bonus[] bonusChosen;

    private PlayerBonuses playerBonuses;
    // Update is called once per frame
    void Start()
    {
        playerBonuses = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBonuses>();
    }

    private void OnEnable()
    {
        ShowBonuses(ShuffleBonuses());
    }

    private int[] ShuffleBonuses()
    {
        
        int[] randint = new int[3];
        bonusChosen = new Bonus[3];
        if (bonusList.Count > 3)
        {
            randint[0] = Random.Range(0, bonusList.Count);
            randint[1] = Random.Range(0, bonusList.Count);
            randint[2] = Random.Range(0, bonusList.Count);

            while (randint[0] == randint[1] || randint[0] == randint[2] || randint[1] == randint[2])
            {
                randint[0] = Random.Range(0, bonusList.Count);
                randint[1] = Random.Range(0, bonusList.Count);
                randint[2] = Random.Range(0, bonusList.Count);
            }

            return randint;
        }
        
        else
        {
            randint[0] = 0; randint[1] = 1; randint[2] = 2;
            return randint;
        }
    }

    private void ShowBonuses(int[] randint)
    {
        foreach (Button button in buttonList)
        {
            int index = buttonList.IndexOf(button);
            if (bonusList[randint[index]])
            {
                bonusChosen[index] = bonusList[randint[index]];
                buttonList[index].transform.GetChild(1).GetComponent<Image>().sprite = bonusChosen[index].Icon;
                buttonList[index].transform.GetChild(0).GetComponent<TMP_Text>().text = $"<align=\"center\"><b>{bonusChosen[index].Name}</b><br><br>{bonusChosen[index].Description}";
            }
        }
    }

    public void AddBonusButton1()
    {
        playerBonuses.AddBonus(bonusChosen[0]);
        CheckIfBonusMaxed(bonusChosen[0]);
    }

    public void AddBonusButton2()
    {
        playerBonuses.AddBonus(bonusChosen[1]);
        CheckIfBonusMaxed(bonusChosen[1]);
    }

    public void AddBonusButton3()
    {
        playerBonuses.AddBonus(bonusChosen[2]);
        CheckIfBonusMaxed(bonusChosen[2]);
    }

    private void CheckIfBonusMaxed(Bonus bonus)
    {
        if (playerBonuses.PlayerBonusList[bonus] == bonus.MaxLevel)
        {
            Debug.Log("Level Maxed");
            bonusList.Remove(bonus);
        }
    }
}
