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
    private bool isMoving = false;

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
            FaceDirection(patrol.currentTarget);
            StartCoroutine(CheckIfMoving());

            if(isMoving)
                agentAnimations.SetWalkAnimation(true);
            else
                agentAnimations.SetWalkAnimation(false);
        }
    }

    public IEnumerator CheckIfMoving() //ANCHOR: This kinda sucks, would be good to make it cleaner/more performant
    {
        var pos1 = transform.root.transform.position;
        yield return new WaitForSeconds(0.1f);
        var pos2 = transform.root.transform.position;

        isMoving = (pos1 != pos2);
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
