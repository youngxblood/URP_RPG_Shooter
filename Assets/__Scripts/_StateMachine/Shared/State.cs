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
    protected AIPath aiPath;
    protected AIDestinationSetter aiDestinationSetter;

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
        aiPath = transform.root.GetComponentInChildren<AIPath>();
        aiDestinationSetter = transform.root.GetComponentInChildren<AIDestinationSetter>();
    }

    public abstract State RunCurrentState();

    #region Helpers

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

    public void ClearAITarget()
    {
        aiDestinationSetter.target = null;
    }

    public void DrawRadiusGizmo(float radius)
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
            Gizmos.color = Color.white;
        }
    }

    #endregion
}
