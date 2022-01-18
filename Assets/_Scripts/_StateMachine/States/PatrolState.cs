using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PatrolState : State
{
    # region Variables
    [field: SerializeField] [field: Range(0.1f, 10f)] public float IdleViewDistance { get; set; } = 5f;

    // States
    public ChaseState chaseState;
    public bool canSeePlayer = false;



    # endregion

    public override State RunCurrentState()
    {
        if (CanSeePlayer() || enemy.hasTakenDamage)
        {
            return chaseState;
        }
        else
            return this; // Returns Idle state
    }

    private void Update() 
    {
        if (stateManager.currentState == this)
        {
            patrol.StartPatrol();
        }
    }

    private void HasTakenDamage()
    {
        enemy.hasTakenDamage = true;
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
        if (Vector3.Distance(enemyAIBrain.Target.transform.position, transform.position) < IdleViewDistance)
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

    // To Draw view distance in editor
    public void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, IdleViewDistance);
            Gizmos.color = Color.white;
        }
    }
    # endregion
}
