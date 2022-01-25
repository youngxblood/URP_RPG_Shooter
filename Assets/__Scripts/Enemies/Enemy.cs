using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;

public class Enemy : MonoBehaviour, IHittable
{
    [Header("Enemy Stats")]
    private bool dead = false;
    [field: SerializeField] public int Health { get; private set; } = 2;
    [field: SerializeField] public EnemyDataSO EnemyData { get; set; }
    [field: SerializeField] public EnemyAttack enemyAttack { get; set; }
    [SerializeField] private GameObject deathVFX;
    public CombatMonitor combatMonitor;

    [Header("Enemy Death")]
    private AgentSounds agentSounds;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider;
    private ShadowCaster2D shadowCaster2D;

    // Aggro on damage
    public bool hasTakenDamage = false;
    public event Action OnDamaged;

    // Enemy Velocity
    Vector3 PrevPos;
    Vector3 NewPos;
    public Vector3 ObjVelocity;
    public bool isMoving = false;

    private void Awake()
    {
        if (enemyAttack == null)
        {
            enemyAttack = GetComponent<EnemyAttack>();
        }

        agentSounds = GetComponentInChildren<AgentSounds>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        shadowCaster2D = GetComponentInChildren<ShadowCaster2D>();
    }

    private void Start()
    {
        Health = EnemyData.MaxHealth;

        // For getting velocity
        PrevPos = transform.position;
        NewPos = transform.position;
    }

    void FixedUpdate()
    {
        isMoving = CheckIfMoving();
    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (!dead)
        {
            OnDamaged?.Invoke(); // Used to change states

            Health -= damage;
            PlayHitSFX();

            if (Health <= 0)
            {
                dead = true;
                PerformDeathActions();
                StartCoroutine(WaitToDie());
            }
        }
    }

    # region Helpers

    public bool CheckIfMoving()
    {
        NewPos = transform.position;  // each frame track the new position
        ObjVelocity = (NewPos - PrevPos) / Time.fixedDeltaTime;  // velocity = dist/time
        PrevPos = NewPos;  // update position for next frame calculation
        return ObjVelocity.magnitude > 0;
    }

    // Used in AttackState.cs
    public void PerformAttack()
    {
        if (!dead)
        {
            enemyAttack.Attack(EnemyData.Damage);
        }
    }

    public void PerformDeathActions()
    {
        PlayDeathVFX();
        PlayDeathSFX();
        DisableEnemy();
        RemoveFromMonitorList();
    }

    public void PlayDeathVFX()
    {
        var explosion = Instantiate(EnemyData.DeathVFX, transform.position, Quaternion.identity);
        UnityEngine.Object.Destroy(explosion, 0.2f);
    }

    private void PlayDeathSFX()
    {
        agentSounds.PlayDeathSound();
    }

    private void DisableEnemy()
    {
        spriteRenderer.enabled = false;
        capsuleCollider.enabled = false;
        shadowCaster2D.enabled = false;
    }

    private void RemoveFromMonitorList()
    {
        if (combatMonitor != null)
            combatMonitor.RemoveDeadEnemiesFromList();
    }

    // To delay destroying the enemy to allow time for events to fire
    IEnumerator WaitToDie()
    {
        yield return new WaitForSeconds(0.34f);
        Destroy(gameObject);
    }

    private void PlayHitSFX()
    {
        agentSounds.PlayHitSound();
    }

    # endregion
}
