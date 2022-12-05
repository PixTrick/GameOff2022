using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExperienceBar : MonoBehaviour
{
    [SerializeField] RectTransform bar;

    private void Start()
    {
        UpdateBar(0, 1);
    }
    public void UpdateBar(float experience, float experienceRequired)
    {
        bar.localScale = new Vector3 (bar.localScale.x, experience / experienceRequired, bar.localScale.z);
    }
}
