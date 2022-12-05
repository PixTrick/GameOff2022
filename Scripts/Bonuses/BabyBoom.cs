using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyBoom : MonoBehaviour
{
    private int level = 1;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    public float ApplySpawnRateBuff()
    {
        return 4 * Mathf.Exp(-(level + 3.5f) / 3);
    }
}
