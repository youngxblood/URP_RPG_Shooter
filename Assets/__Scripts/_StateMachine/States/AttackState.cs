using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] [field: Range(0.1f, 10f)] private float maxAttackRange = 5f;

    // States
    public ChaseState chaseState;
    public bool isWithinRange;

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
        if (stateManager.currentState == this && !player.isDead)
        {
            FaceDirection();
            AttackTarget();
        }
    }

    #region  Helpers
    public bool IsInAttackRange()
    {
        if (Vector3.Distance(enemyAIBrain.Target.transform.position, transform.position) < maxAttackRange)
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

    public void AttackTarget()
    {
        enemy.PerformAttack();
    }

    // To Draw view distance in editor
    public void OnDrawGizmos()
    {
        DrawRadiusGizmo(maxAttackRange);
    }

    #endregion
}
