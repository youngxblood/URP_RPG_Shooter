using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Ensures there is always a RigidBody2D component on the object
[RequireComponent(typeof(Rigidbody2D))]
public class AgentMovement : MonoBehaviour
{
    protected Rigidbody2D rigidbody2d;
    [field: SerializeField]
    public MovementDataSO MovementData { get; set; }

    [SerializeField]
    public float currentVelocity = 3f;
    protected Vector2 movementDirection;

    [field: SerializeField]
    public UnityEvent<float> OnVelocityChange { get; set; } //! Unity event with the 'float' datatype

    [Header("Movement Variables")]
    public float acceleration;
    public float deacceleration;
    public float maxSpeed;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        acceleration = MovementData.acceleration;
        deacceleration = MovementData.deacceleration;
        maxSpeed = MovementData.maxSpeed;
    }

    public void MoveAgent(Vector2 movementInput)
    {
        if (movementInput.magnitude > 0)
        {
            movementDirection = movementInput.normalized;
        }
        currentVelocity = CalculateSpeed(movementInput);
    }

    private float CalculateSpeed(Vector2 movementInput)
    {
        if (movementInput.magnitude > 0) // Check if the object is moving
        {
            currentVelocity += acceleration * Time.deltaTime;
        }
        else
        {
            currentVelocity -= deacceleration * Time.deltaTime;
        }
        return Mathf.Clamp(currentVelocity, 0, maxSpeed); //Lower limit is 0, upper limit is the max speed in the MovementData scripted object
    }

    private void FixedUpdate()
    {
        OnVelocityChange?.Invoke(currentVelocity); //! First checks if event is listening, then invokes (sends?) the variable
        rigidbody2d.velocity = currentVelocity * movementDirection.normalized; // Where the actual movement happens
    }
}
