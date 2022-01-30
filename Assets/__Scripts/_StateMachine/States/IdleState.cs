using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    [SerializeField] [field: Range(0.1f, 10f)] private float idleViewDistance = 5;

    // States (set in inspector)
    [SerializeField] private ChaseState chaseState;
    [SerializeField] private PatrolState patrolState;
    [SerializeField] private bool canSeePlayer = false;

    public override State RunCurrentState()
    {
        if (CanSeePlayer() || enemy.hasTakenDamage)
        {
            return chaseState;
        }
        if (stateManager.patrolEnabled)
        {
            ClearAITarget();
            aiPath.maxSpeed = 1;
            return patrolState;
        }
        else
        {
            ClearAITarget();
            return this; // Returns Idle state
        }

    }

    # region Enable/Disable
    private void OnEnable()
    {
        enemy.OnDamaged += HasTakenDamage;
    }

    private void OnDisable()
    {
        enemy.OnDamaged -= HasTakenDamage;
    }

    # endregion

    # region Helpers
    public bool CanSeePlayer()
    {
        if (Vector3.Distance(enemyAIBrain.Target.transform.position, transform.position) < idleViewDistance)
        {
            if (canSeePlayer == false)
            {
                canSeePlayer = true;
            }
        }
        else
        {
            canSeePlayer = false;
        }
        return canSeePlayer;
    }

    private void HasTakenDamage()
    {
        enemy.hasTakenDamage = true;
    }

    #if UNITY_EDITOR
    // To Draw view distance in editor
    public void OnDrawGizmos()
    {
        DrawRadiusGizmo(idleViewDistance);
    }
    #endif

    #endregion
}
