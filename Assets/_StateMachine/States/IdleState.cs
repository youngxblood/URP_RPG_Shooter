using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    # region Variables
    [field: SerializeField] [field: Range(0.1f, 10f)] public float IdleViewDistance { get; set; } = 5f;

    // States
    public ChaseState chaseState;
    public bool canSeePlayer = false;
    // public bool hasTakenDamage = false;

    # endregion

    public override State RunCurrentState()
    {
        if (CanSeePlayer() || enemy.hasTakenDamage)
        {
            return chaseState;
        } else
            return this; // Returns Idle state
    }

    private void Update() 
    {
        if (stateManager.currentState == this)
        {
            // HasTakenDamage();
        }    
    }

    public bool CanSeePlayer()
    {
        if(Vector3.Distance(enemyAIBrain.Target.transform.position, transform.position) < IdleViewDistance)
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

    private void OnEnable() 
    {
        Enemy.OnDamaged += HasTakenDamage;
    }

    private void OnDisable() 
    {
        Enemy.OnDamaged -= HasTakenDamage;
    }

    # region Helpers
    // To Draw view distance in editor
    public void OnDrawGizmos()
    {
        if(UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, IdleViewDistance);
            Gizmos.color = Color.white;
        }
    }

    # endregion
}
