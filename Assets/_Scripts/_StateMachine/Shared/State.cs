using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class State : MonoBehaviour
{
    protected Player player;
    protected EnemyAIBrain enemyAIBrain;
    protected Enemy enemy;
    protected EnemyStats enemyStats;
    protected Rigidbody2D rb;
    protected StateManager stateManager;
    protected SpriteRenderer spriteRenderer;
    protected AgentAnimations agentAnimations;
    protected Patrol patrol;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemyAIBrain = transform.root.GetComponent<EnemyAIBrain>();
        enemy = transform.root.GetComponent<Enemy>();
        enemyStats = transform.root.GetComponentInChildren<EnemyStats>();
        rb = transform.root.GetComponent<Rigidbody2D>();
        stateManager = transform.root.GetComponent<StateManager>();
        spriteRenderer = transform.root.GetComponentInChildren<SpriteRenderer>();
        agentAnimations = transform.root.GetComponentInChildren<AgentAnimations>();
        patrol = transform.root.GetComponentInChildren<Patrol>();
    }

    public abstract State RunCurrentState();

    #region Helpers

    // public void FaceDirection(Vector2 enemyTarget)
    // {
    //     var direction = (Vector3)enemyTarget - transform.position; //? Need to investigate this (casting?)
    //     var result = Vector3.Cross(Vector2.up, direction); //? Need to investigate this

    //     if (result.z > 0)
    //     {
    //         spriteRenderer.flipX = true;
    //     }
    //     else if (result.z < 0)
    //     {
    //         spriteRenderer.flipX = false;
    //     }
    // }

    public void FaceDirection()
    {
        if (enemy.ObjVelocity.x >= 0.1f)
        {
            spriteRenderer.flipX = false;
        } 
        else if (enemy.ObjVelocity.x <= -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    #endregion
}
