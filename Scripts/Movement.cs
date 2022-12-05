using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] float minSpeed = 0.1f;
    [SerializeField] private Transform armTransform;
    [SerializeField] private float turnSmoothTime;
    private float turnSmoothVelocity;

    Vector2 direction = new Vector2(1,0);
    Vector3 mouseDirection = new Vector3(0,1,0);

    public Vector3 MouseDirection
    { get { return mouseDirection; } }

    public Vector2 Direction
    {
        get { return direction; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        armTransform.GetComponent<Canvas>().worldCamera = Camera.main;
        armTransform.rotation = Quaternion.Euler(0f, 0f, 90);
    }

    public void Move(object sender, Controls.OnMovementButtonPressedArgs e)
    {
        direction = e.horizontal * Vector2.right + e.vertical * Vector2.up;

        if (direction.magnitude > minSpeed)
        {
            if (direction.magnitude > 1)
            {
                direction = direction.normalized;
            }

            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }

        MoveArm();
    }

    public void MoveAnimation(object sender, Controls.OnMovementButtonPressedArgs e)
    {
        if (animator != null)
        {
            if (e.horizontal >= 0)
            {
                armTransform.GetComponent<Canvas>().sortingOrder = 2;
                animator.ResetTrigger("Idle");
                animator.ResetTrigger("Run_Left");
                animator.SetTrigger("Run_Right");
            }
            
            else
            {
                armTransform.GetComponent<Canvas>().sortingOrder = 0;
                animator.ResetTrigger("Idle");
                animator.ResetTrigger("Run_Right");
                animator.SetTrigger("Run_Left");
            }
        }
    }

    public void Idle(object sender, EventArgs e)
    {
        MoveArm();
    }

    public void IdleAnimation(object sender, EventArgs e)
    {
        if (animator != null)
        {
            animator.ResetTrigger("Run_Right");
            animator.ResetTrigger("Run_Left");
            animator.SetTrigger("Idle");
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            
        }
        
    }

    private void MoveArm()
    {
        mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mouseDirection.z = 0;
        float targetAngle = Mathf.Atan2(mouseDirection.x, -mouseDirection.y) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(armTransform.eulerAngles.z, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        armTransform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}
