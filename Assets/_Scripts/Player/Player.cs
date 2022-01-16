using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IAgent, IHittable
{
    [field: SerializeField] public int Health { get; set; }
    [field: SerializeField] public int MaxHealth { get; set; }
    [field: SerializeField] public int CurrentLives { get; set; }
    public bool isDead;
    [field: SerializeField] public UnityEvent OnDeath { get; set; }
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerManager playerManager;
    public SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject deathVFX;

    private void Awake()
    {
        UIController.Instance.SetMaxHealthValue(Health);
        MaxHealth = Health;
        playerStats = GetComponentInChildren<PlayerStats>();
        playerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        CurrentLives = playerStats.playerData.maxLives;
    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (!isDead)
        {
            DamagePlayer(1);
            UIController.Instance.UpdateHealthBar(Health);

            if (Health <= 0 && !isDead)
            {
                OnDeath?.Invoke();
                PlayDeathVFX();
                isDead = true;

                if (CurrentLives > 1) // For Heart UI
                {
                    UIController.Instance.EmptyHeartSprite(CurrentLives);
                    CurrentLives -= 1;
                    playerManager.RespawnPlayer();
                    StartCoroutine(DeathCoroutine());
                }
                else
                {
                    UIController.Instance.EmptyHeartSprite(CurrentLives);
                    CurrentLives -= 1;
                    DisablePlayerObj();
                }
            }
        }
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(0.21f);
        spriteRenderer.enabled = false;
    }


    public void DisablePlayerObj()
    {
        gameObject.SetActive(false);
    }

    public void PlayDeathVFX()
    {
        var explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);
        Object.Destroy(explosion, 0.2f);
    }

    public void DamagePlayer(int newHealth)
    {
        Health = Health - newHealth;
        UIController.Instance.UpdateHealthBar(Health);
    }

    public void HealPlayer(int newHealth)
    {
        Health += newHealth;
        Health = Mathf.Clamp(Health, 0, MaxHealth);
        UIController.Instance.UpdateHealthBar(Health);
    }

}