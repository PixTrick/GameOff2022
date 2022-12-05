using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ShopsBehaviour : MonoBehaviour
{
    [SerializeField] private UnityEvent OnShopsLeave;
    [SerializeField] private Transform player;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private TMP_Text healthBarText;
    [SerializeField] private TMP_Text gold;

    private bool isTrigger = false;
    [SerializeField] private PlayerBehaviour playerBehaviour;

    void Start()
    {
        transform.position = player.position;
    }

    private void OnEnable()
    {
        transform.position = player.position;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnShopsLeave?.Invoke();
            isTrigger = false;
        }
    }

    public void ShowGoldAndHealth()
    {
        gold.text = playerBehaviour.Gold.ToString();
        healthBar.SetHealthBar(playerBehaviour.Health / playerBehaviour.MaxHealth);
        healthBarText.text = $"{Mathf.Round(playerBehaviour.Health)} / {Mathf.Round(playerBehaviour.MaxHealth)}";
    }
}
