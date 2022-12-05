using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCureAllWounds : MonoBehaviour
{
    private int level;
    private float regenDelay;
    private float regenAmount;
    public int Level
    { get { return level; } set { level = value; } }

    private bool canHeal;
    private PlayerBehaviour playerBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        playerBehaviour = GetComponent<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canHeal)
        {
            StartCoroutine(HealingDelay());
        }
    }

    IEnumerator HealingDelay()
    {
        canHeal = false;
        yield return new WaitForSeconds(regenDelay);
        playerBehaviour.AddHealth(regenAmount);
        canHeal = true;
    }
}
