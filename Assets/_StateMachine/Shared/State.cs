using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public abstract State RunCurrentState();
}
