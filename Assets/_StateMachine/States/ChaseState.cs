using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    // Vars
    [field: SerializeField] [field: Range(0.1f, 10f)] public float AggroViewDistance { get; set; } = 7f;
    [field: SerializeField] [field: Range(0.1f, 10f)] public float AttackRange { get; set; } = 1f;

    [SerializeField]
    protected float currentVelocity = 3f;
    protected Vector2 movementDirection;

    // State
    public AttackState attackState;
    public IdleState idleState;

    public bool isWithinRange;
    private bool canSeePlayer;

    // State control
    public override State RunCurrentState()
    {
        if (IsInAttackRange())
        {
            return attackState;
        }
        if (!CanSeePlayer())
            return idleState;
        else
            return this;
    }

    private void Update()
    {
        if (stateManager.currentState == this)
        {
            Debug.Log(this);
            ChasePlayer();
            FaceDirection(enemyAIBrain.Target.transform.position);
            agentAnimations.SetWalkAnimation(true);
        }
    }


    // State logic
    public bool CanSeePlayer()
    {
        if (Vector3.Distance(enemyAIBrain.Target.transform.position, transform.position) > AggroViewDistance)
        {
            if (canSeePlayer == true)
            {
                canSeePlayer = false;
            }
        }
        else
        {
            canSeePlayer = true;
        }
        return canSeePlayer;
    }

    public bool IsInAttackRange()
    {
        if (Vector3.Distance(enemyAIBrain.Target.transform.position, transform.position) < 1f)
        {
            if (isWithinRange == false)
            {
                isWithinRange = true;
            }
        }
        else
        {
            isWithinRange = false;
        }
        return isWithinRange;
    }

    // State logic
    public void ChasePlayer()
    {

        Vector3 direction = player.transform.position - transform.position;
        MoveAgent(direction);

        // rb.velocity = currentVelocity * movementDirection.normalized; //Where the actual movement happens // ANCHOR Do I still need this?
    }


    // Tools
    public void MoveAgent(Vector2 movementInput)
    {
        if (movementInput.magnitude > 0)
        {
            transform.root.transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyStats.MovementData.maxSpeed * Time.deltaTime);
        }
        currentVelocity = CalculateSpeed(movementInput);
    }

    private float CalculateSpeed(Vector2 movementInput)
    {
        if (movementInput.magnitude > 0) // Check if the object is moving
        {
            currentVelocity += enemyStats.MovementData.acceleration * Time.deltaTime;
        }
        else
        {
            currentVelocity -= enemyStats.MovementData.deacceleration * Time.deltaTime;
        }
        return Mathf.Clamp(currentVelocity, 0, enemyStats.MovementData.maxSpeed); //Lower limit is 0, upper limit is the max speed in the MovementData scripted object
    }

    public void FaceDirection(Vector2 pointerInput)
    {
        var direction = (Vector3)pointerInput - transform.position; //? Need to investigate this (casting?)
        var result =  Vector3.Cross(Vector2.up, direction); //? Need to investigate this

        if (result.z > 0)
        {
            spriteRenderer.flipX = true;
        } else if (result.z < 0)
        {
            spriteRenderer.flipX = false;
        }
    }



    // To Draw view distance in editor
    public void OnDrawGizmos()
    {
        if(UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, AggroViewDistance);
            Gizmos.color = Color.white;
        }
    }
}
