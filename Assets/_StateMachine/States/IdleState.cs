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

    # endregion

    public override State RunCurrentState()
    {
        if (CanSeePlayer())
        {
            return chaseState;
        } else
            return this; // Returns Idle state
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
}
