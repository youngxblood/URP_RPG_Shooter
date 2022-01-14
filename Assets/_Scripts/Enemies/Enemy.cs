using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHittable, IAgent
{
    [Header("Enemy Stats")]
    private bool dead = false;
    [field: SerializeField] public int Health { get; private set; } = 2;
    [field: SerializeField] public EnemyDataSO EnemyData { get; set; }
    [field: SerializeField] public EnemyAttack enemyAttack { get; set; }
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }
    [field: SerializeField] public UnityEvent OnDeath { get; set; }
    [SerializeField] private GameObject deathVFX;
    

    // Aggro on damage
    public bool hasTakenDamage = false;
    public event Action OnDamaged;

    private void Awake()
    {
        if (enemyAttack == null)
        {
            enemyAttack = GetComponent<EnemyAttack>();
        }
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
            OnGetHit?.Invoke();
            if (Health <= 0)
            {
                dead = true;
                PlayDeathVFX();

                OnDeath?.Invoke();
                StartCoroutine(WaitToDie()); // Delay is needed so OnDeath event/audio clip can fire
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

    public void PlayDeathVFX()
    {
        var explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);
        UnityEngine.Object.Destroy(explosion, 0.2f);
    }

    # endregion
}
