using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IAgent, IHittable
{
    [field: SerializeField] public int Health { get; set; }
    private bool dead;
    [field: SerializeField] public UnityEvent OnDeath { get; set; }
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }

    [SerializeField] private GameObject deathVFX;

    private void Awake()
    {
        UIController.Instance.SetMaxHealthValue(Health);
    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (!dead)
        {
            Health--;
            UIController.Instance.UpdateHealthBar(Health);
            OnGetHit?.Invoke();

            if (Health <= 0)
            {
                OnDeath?.Invoke();
                PlayDeathVFX();

                StartCoroutine(DeathCoroutine());
            }
        }

        IEnumerator DeathCoroutine()
        {
            yield return new WaitForSeconds(0.21f);
            Destroy(gameObject);
        }
    }

    public void PlayDeathVFX()
    {
        var explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);
        Object.Destroy(explosion, 0.2f);
    }

}