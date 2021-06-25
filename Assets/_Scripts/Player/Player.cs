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

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (!dead)
        {
            Health--;
            OnGetHit?.Invoke();

            if (Health <= 0)
            {
                OnDeath?.Invoke();
                StartCoroutine(DeathCoroutine());
            }
        }

        IEnumerator DeathCoroutine()
        {
            yield return new WaitForSeconds(0.21f);
            Destroy(gameObject);
        }
    }
}