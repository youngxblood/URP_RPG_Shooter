using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] protected Player player;
    [SerializeField] protected EnemyAIBrain enemyAIBrain;
    [SerializeField] protected EnemyStats enemyStats;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected StateManager stateManager;

    public void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemyAIBrain = transform.root.GetComponent<EnemyAIBrain>();
        enemyStats = transform.root.GetComponentInChildren<EnemyStats>();
        rb = transform.root.GetComponent<Rigidbody2D>();
        stateManager = transform.root.GetComponent<StateManager>();
    }

    public abstract State RunCurrentState();
}
