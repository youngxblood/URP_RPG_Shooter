using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : EnemyAttack
{
    public override void Attack(int damage)
    {
        if(waitBeforeNextAttack == false)
        {
            var hittable = GetTarget().GetComponent<IHittable>();
            hittable?.GetHit(damage, gameObject);
            StartCoroutine(WaitBeforeAttackCoroutine());
        }
    }
}
