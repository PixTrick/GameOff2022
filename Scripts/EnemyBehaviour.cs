using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBehaviour : MonoBehaviour
{
    private static AudioManager audioManager;
    [SerializeField] private GameObject damageText;
    [SerializeField] private GameObject lemonDropped;
    [SerializeField] private EnemyStats enemyStats;
    private float maxSpeed;
    private float speed;
    private Animator animator = new Animator();
    private float maxHealth;
    private float health;
    private static float healthNaturalBuff = 1;
    public float Health
    {
        get 
        { 
            return health; 
        }
        set 
        { 
            health = value;
            healthBar.SetHealthBar(health/maxHealth);
        }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public float MaxHealth
    { 
        get { return maxHealth; } 
        set { maxHealth = value; }
    }

    public float HealthNaturalBuff
    {
        get { return healthNaturalBuff; }
        set { healthNaturalBuff = value; }
    }


    [SerializeField] private HealthBar healthBar;
    private IEnemy iEnemy;
    private static GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        healthBar = GetComponentInChildren<HealthBar>();
        animator = GetComponent<Animator>();
        maxSpeed = enemyStats.MaxSpeed;
        speed = maxSpeed;
        maxHealth = enemyStats.MaxHealth * healthNaturalBuff;
        Health = maxHealth;
        
        

        iEnemy = GetComponent<IEnemy>();

        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        iEnemy.Move();
    }

    // Update is called once per frame


    public void TakeDamage(float damage)
    {
        if (damage > 0)
        {
            if (Health - damage > 0)
            {
                Health -= damage;
            }
            else
            {
                if (transform.CompareTag("Enemy"))
                {
                    gameManager.KillScore++;
                }
                Health = 0;
                animator.SetTrigger("Die");
                Destroy(gameObject, 0.5f);
                if (transform.CompareTag("Crate"))
                {
                    audioManager.Play("Crate");
                }
            }
            DamagePopUp(damage);
            animator.SetTrigger("TakeDamage");
        }
    }

    public void DisableFor(float delay)
    {
        StartCoroutine(CannotMoveFor(delay));
    }

    private void DamagePopUp(float damage)
    {
        GameObject floatingText = Instantiate(damageText, transform.position + Random.Range(-1f,1f) * new Vector3 (1f,1f,0f), Quaternion.identity);
        floatingText.GetComponent<Canvas>().worldCamera = Camera.main;
        Transform child = floatingText.transform.GetChild(0);
        child.GetComponent<TMP_Text>().text = Mathf.RoundToInt(damage).ToString();
    }

    public void Knockback(Vector2 direction, float knockbackForce)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(direction * knockbackForce / rb.mass, ForceMode2D.Impulse);
    }

    private void BecomeInvulnerableFor(float time)
    {

    }

    /*private void OnBecameInvisible()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnBecameVisible()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }*/

    private IEnumerator CannotMoveFor(float time)
    {
        Speed = 0f;
        yield return new WaitForSeconds(time);
        Speed = enemyStats.MaxSpeed;
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            Instantiate(lemonDropped, transform.position, Quaternion.identity, transform.parent);
        }
    }
}
