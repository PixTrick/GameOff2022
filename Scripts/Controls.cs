using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Controls : MonoBehaviour
{
    private event EventHandler<OnMovementButtonPressedArgs> OnMovementButtonPressed;
    public class OnMovementButtonPressedArgs : EventArgs
    {
        public float horizontal;
        public float vertical;
    }
    private event EventHandler OnMovementButtonReleased;
    private Movement movement;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Movement>();
        OnMovementButtonPressed += movement.Move;
        OnMovementButtonPressed += movement.MoveAnimation;
        OnMovementButtonReleased += movement.Idle;
        OnMovementButtonReleased += movement.IdleAnimation;
    }

    private void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalControl = Input.GetAxisRaw("Horizontal");
        float verticalControl = Input.GetAxisRaw("Vertical");

        if (horizontalControl != 0 || verticalControl != 0)
        {
            OnMovementButtonPressed?.Invoke(this, new OnMovementButtonPressedArgs { horizontal = horizontalControl, vertical = verticalControl });
        }

        else
        {
            OnMovementButtonReleased?.Invoke(this, EventArgs.Empty);
        }
    }
}
