using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public ChaseState chaseState;
    public bool outOfAttackRange;

    public override State RunCurrentState()
    {
        if (outOfAttackRange)
        {
            return chaseState;
        }
        else
            return this;
    }
}
