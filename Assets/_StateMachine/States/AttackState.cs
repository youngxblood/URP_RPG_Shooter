using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    [field: SerializeField] [field: Range(0.1f, 10f)] public float MaxAttackRange { get; set; } = 5f;

    // States
    public ChaseState chaseState;
    public bool isWithinRange;

    #region StateManagement

    public override State RunCurrentState()
    {
        if (!IsInAttackRange())
        {
            return chaseState;
        }
        else
            return this;
    }

    private void Update()
    {
        if (stateManager.currentState == this)
        {
            AttackTarget();
        }
    }

    public void AttackTarget()
    {
        enemy.PerformAttack();
    }

    #endregion

    #region  Methods

    public bool IsInAttackRange()
    {
        if (Vector3.Distance(enemyAIBrain.Target.transform.position, transform.position) < MaxAttackRange)
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


    // To Draw view distance in editor
    public void OnDrawGizmos()
    {
        if(UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, MaxAttackRange);
            Gizmos.color = Color.white;
        }
    }

    #endregion
}
