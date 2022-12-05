using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private UnityEvent OnLevelUp;
    [SerializeField] private UnityEvent OnDying;
    private HealthBar healthBar;
    private Animator animator;
    private float maxHealth = 20;
    private float health;
    private float experience = 0;
    private int level = 1;
    private BulletModifiers modifiers = new BulletModifiers();
    private PlayerModifiers playerModifiers = new PlayerModifiers();
    private int gold = 0;
    [SerializeField] private TMP_Text goldText;
    private GameManager gameManager;

    public BulletModifiers Modifiers
    {
        get { return modifiers; }
        set { modifiers = value; }
    }

    public PlayerModifiers PlayerModifiers
    {
        get { return playerModifiers; }
        set 
        { 
            playerModifiers = value;
        }
    }

    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            healthBar.SetHealthBar(health / maxHealth);
            CheckColdBlood();
            CheckHumpDay();
        }
    }

    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public float Experience
    {
        get 
        { 
            return experience; 
        }
        set 
        { 
            experience = value; 
        }
    }

    public int Level
    {
        get
        { 
            return level; 
        }
        set 
        { 
            level = value; 
        }
    }

    public int Gold
    {
        get { return gold;}
        set 
        { 
            gold = value;
            goldText.text = $"<color=#FDAB38><b>U</b></color> {gold}";
        }
    }

    [SerializeField] private ExperienceBar experienceBar;
    [SerializeField] private GameObject damageText;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        Gold = 0;
        animator = GetComponent<Animator>();
        healthBar = GetComponentInChildren<HealthBar>();
        Health = maxHealth;
        modifiers.DamageModifier = 1;
        modifiers.SizeModifier = 1;
        Experience = 0;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
    }

    public void AddExperience(float experience)
    {
        if (Experience + experience >= LevelRequirement(level))
        {
            LevelUp(Experience + experience - LevelRequirement(level));
        }

        else
        {
            Experience += experience;
        }

        experienceBar.UpdateBar(Experience, LevelRequirement(level));
    }

    public void TakeDamage(float damage)
    {
        damage *= playerModifiers.DamageReductionModifier;
        if (Health - damage > 0)
        {
            Health -= damage;
        }
        else
        {
            CheckBadgeOfHonor();
        }
        animator.SetTrigger("TakeDamage");
        animator.ResetTrigger("Idle");
        DamagePopUp(damage);
    }

    private void DamagePopUp(float damage)
    {
        GameObject floatingText = Instantiate(damageText, transform.position - 0.1f * Vector3.forward, Quaternion.identity);
        floatingText.GetComponent<Canvas>().worldCamera = Camera.main;
        Transform child = floatingText.transform.GetChild(0);
        child.GetComponent<TMP_Text>().text = Mathf.RoundToInt(damage).ToString();
    }

    public float LevelRequirement(int level)
    {
        return 7 * Mathf.Pow(level,1.1f);
    }

    private void LevelUp(float experience)
    {
        OnLevelUp?.Invoke();
        Experience = experience;
        Level += 1;
    }

    public void AddGold(int amount)
    {
        if (Gold + amount < 0)
        {

        }

        else
        {
            Gold += amount;
        }
    }

    public void AddHealth(float amount)
    {
        if (Health + amount < MaxHealth && Health + amount > 0)
        {
            Health += amount;
        }

        else if (Health + amount > MaxHealth)
        {
            Health = MaxHealth;
        }
        
        else
        {
            CheckBadgeOfHonor();
        }
    }

    public void Die()
    {
        Health = 0;
        animator.SetTrigger("Die");
        Destroy(gameObject, 1f);
    }

    private void CheckColdBlood()
    {
        TryGetComponent<ColdBlood>(out ColdBlood component);
        if (component != null)
        {
            component.CheckHealth();
        }
    }
    private void CheckHumpDay()
    {
        TryGetComponent<HumpDay>(out HumpDay component);
        if (component != null)
        {
            component.ApplyBuff();
        }
    }

    private void CheckBadgeOfHonor()
    {
        TryGetComponent<BadgeOfHonor>(out BadgeOfHonor component);
        if (component != null)
        {
            component.Revive();
        }

        else
        {
            Die();
        }
    }

    private void OnDestroy()
    {
        if (Health == 0)
        {
            gameManager.PauseGame();
            gameManager.EndGame();
        }
    }
}
