using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] RectTransform bar;

    private float maxLength;

    // Start is called before the first frame update
    void Start()
    {
        maxLength = bar.localScale.x;
    }

    public void SetHealthBar(float ratio)
    {
        bar.localScale = new Vector3 (ratio * maxLength, bar.localScale.y, bar.localScale.z);
    }
}
