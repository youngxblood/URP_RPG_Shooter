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

    [Header("Enemy Death")]
    private AgentSounds agentSounds;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider;
    private ShadowCaster2D shadowCaster2D;


    // Aggro on damage
    public bool hasTakenDamage = false;
    public event Action OnDamaged;

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
    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (!dead)
        {
            OnDamaged?.Invoke();

            Health--;
            PlayHitSFX();
            
            if (Health <= 0)
            {
                dead = true;
                PerformDeathActions(); // Replacement for OnDeath() UnityEvent 
                StartCoroutine(WaitToDie());
            }
        }


        IEnumerator WaitToDie()
        {
            yield return new WaitForSeconds(0.34f);
            Destroy(gameObject);
        }
    }

    # region Helpers

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

    private void PlayHitSFX()
    {
        agentSounds.PlayHitSound();
    }

    # endregion
}
