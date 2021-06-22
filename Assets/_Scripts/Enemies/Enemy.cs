using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHittable, IAgent
{
    
    [field: SerializeField] public EnemyDataSO EnemyData { get; set; }
    [field: SerializeField] public int Health { get; private set; } = 2;
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }
    [field: SerializeField] public UnityEvent OnDeath { get; set; }

    private void Start() 
    {
        Health = EnemyData.MaxHealth; // Initializes enemy health as the max health set in the scripted object    
    }
    public void GetHit(int damage, GameObject damageDealer)
    {
        Health--;
        OnGetHit?.Invoke();
        if (Health <= 0)
        {
            OnDeath?.Invoke();
            StartCoroutine(WaitToDie()); // Delay is needed so OnDeath event/audio clip can fire
        }

        IEnumerator WaitToDie()
        {
            yield return new WaitForSeconds(0.34f);
            Destroy(gameObject);
        }
    }
}
