using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class State : MonoBehaviour
{
    [SerializeField] protected Player player;
    [SerializeField] protected EnemyAIBrain enemyAIBrain;
    [SerializeField] protected Enemy enemy;
    [SerializeField] protected EnemyStats enemyStats;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected StateManager stateManager;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected AgentAnimations agentAnimations;
    [SerializeField] protected Patrol patrol;

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
        patrol = GetComponent<Patrol>();
    }

    public abstract State RunCurrentState();

    #region Helpers

    public void FaceDirection(Vector2 pointerInput)
    {
        var direction = (Vector3)pointerInput - transform.position; //? Need to investigate this (casting?)
        var result = Vector3.Cross(Vector2.up, direction); //? Need to investigate this

        if (result.z > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (result.z < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    #endregion
}
