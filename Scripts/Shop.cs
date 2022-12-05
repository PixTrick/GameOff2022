using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] private UnityEvent OnTriggerEnter;
    [SerializeField] private UnityEvent OnTriggerExit;
    [SerializeField] private UnityEvent OnDiscussionEnter;

    [SerializeField] private List<Button> buttons = new List<Button>();

    private bool isTriggered = false;

    [SerializeField] private PlayerBehaviour playerBehaviour;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        VerifyButtonCosts();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTriggered && Input.GetButtonDown("Fire1"))
        {
            OnDiscussionEnter?.Invoke();
            transform.parent.GetComponent<ShopsBehaviour>().ShowGoldAndHealth();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Player"))
        {
            OnTriggerEnter?.Invoke();
            isTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("Player"))
        {
            OnTriggerExit?.Invoke();
            isTriggered = false;
        }
    }

    public void VerifyButtonCosts()
    {
        foreach (Button button in buttons)
        {
            if (button.transform.Find("Price") && int.Parse(button.transform.Find("Price").GetComponent<TMP_Text>().text) > playerBehaviour.Gold)
            {
                button.interactable = false;
            }
            else if (button.transform.Find("Cost") && int.Parse(button.transform.Find("Cost").GetComponent<TMP_Text>().text) >= playerBehaviour.Health)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }
        }
    }
}
