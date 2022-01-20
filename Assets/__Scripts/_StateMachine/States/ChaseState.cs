using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    // Vars
    [field: SerializeField] [field: Range(0.1f, 10f)] public float AggroViewDistance { get; set; } = 7f;
    [field: SerializeField] [field: Range(0.1f, 10f)] public float AggroResetRange { get; set; } = 11f;

    [SerializeField]
    protected float currentVelocity = 3f;
    protected Vector2 movementDirection;

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
        if (!CanSeePlayer() && !enemy.hasTakenDamage)
            return idleState;
        else
            return this;
    }

    private void Update()
    {
        if (stateManager.currentState == this)
        {
            ChasePlayer();
            FaceDirection();
            agentAnimations.SetWalkAnimation(true); // TASK - Need to make this only fire once in the future
            IsOutsideOfAggroResetRange();
        }
    }

    // State logic
    public void ChasePlayer()
    {
        // Vector3 direction = player.transform.position - transform.position;
        // MoveAgent(direction);
        if (aiDestinationSetter != null)
            aiDestinationSetter.target = player.transform;
    }

    # region Helpers
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
        if (Vector3.Distance(enemyAIBrain.Target.transform.position, transform.position) < enemyStats.EnemyData.AttackRange)
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

    public void IsOutsideOfAggroResetRange()
    {
        if (Vector3.Distance(enemyAIBrain.Target.transform.position, transform.position) > AggroResetRange)
        {
            if (enemy.hasTakenDamage == true)
            {
                enemy.hasTakenDamage = false;
            }
        }
    }

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

    // To Draw view distance in editor
    public void OnDrawGizmos()
    {
        DrawRadiusGizmo(AggroViewDistance);
    }

    # endregion
}
