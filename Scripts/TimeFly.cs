using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFly : MonoBehaviour
{
    [SerializeField] float slowMotionForce;
    [SerializeField] float slowMotionDuration;
    private Movement playerMovement;
    private bool buffIsActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position += Time.fixedDeltaTime * (playerMovement.transform.position - transform.position);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerMovement = other.GetComponent<Movement>();
            Time.timeScale /= slowMotionForce;
            Time.fixedDeltaTime /= slowMotionForce;
            playerMovement.Speed *= slowMotionForce;
            Destroy(gameObject,slowMotionDuration/slowMotionForce);
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            buffIsActive = true;
        }
    }

    private void ResetSlowMo()
    {
        Time.timeScale *= slowMotionForce;
        Time.fixedDeltaTime *= slowMotionForce;
        playerMovement.Speed /= slowMotionForce;
    }

    private void OnDestroy()
    {
        if (buffIsActive)
        {
            ResetSlowMo();
        }
    }
}
