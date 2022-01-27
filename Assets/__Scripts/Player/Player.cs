using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IHittable
{
    [Header("Player Stats")]
    public bool isDead = false;
    public int Health;
    [field: SerializeField] public int MaxHealth { get; set; }
    [field: SerializeField] public int CurrentLives { get; set; }

    [Header("Object Refs")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerManager playerManager;
    public SpriteRenderer spriteRenderer;
    [SerializeField] private AgentSounds agentSounds;
    [SerializeField] private GameObject deathVFX;


    private void Awake()
    {
        playerStats = GetComponentInChildren<PlayerStats>();
        playerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerManager>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        agentSounds = GetComponentInChildren<AgentSounds>();

        // Health/lives initialization
        Health = playerStats.playerData.maxHealth;
        MaxHealth = Health;
        CurrentLives = playerStats.playerData.maxLives;
    }

    // This is used to delay getting the references to wait for the UIController instance to be created
    private void Start()
    {
        UIController.Instance.SetMaxHealthValue(MaxHealth);
        UIController.Instance.UpdateHealthBar(MaxHealth);
    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (!isDead)
        {
            DamagePlayer(damage);
            UIController.Instance.UpdateHealthBar(Health);

            if (Health <= 0 && !isDead)
            {
                isDead = true;

                if (CurrentLives > 1)
                {
                    UIController.Instance.EmptyHeartSprite(CurrentLives);
                    CurrentLives -= 1;
                    playerManager.RespawnPlayer();
                    PerformDeathActions();
                    DisablePlayer();
                }
                else
                {
                    UIController.Instance.EmptyHeartSprite(CurrentLives);
                    CurrentLives -= 1;
                    PerformDeathActions();
                    DisablePlayerObj();
                }
            }
        }
    }

    #region Helpers

    public void PerformDeathActions()
    {
        PlayDeathVFX();
        PlayDeathSFX();
    }

    public void PlayDeathVFX()
    {
        var explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);
        Object.Destroy(explosion, 0.2f);
    }

    private void PlayDeathSFX()
    {
        agentSounds.PlayDeathSound();
    }

    private void DisablePlayer()
    {
        StartCoroutine(DeathCoroutine());
    }

    public void DisablePlayerObj()
    {
        gameObject.SetActive(false);
    }

    IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(0.21f);
        spriteRenderer.enabled = false;
    }

    public void DamagePlayer(int damage)
    {
        Health = Health - damage;
        agentSounds.PlayHitSound();
        UIController.Instance.UpdateHealthBar(Health);
    }

    public void HealPlayer(int newHealth)
    {
        Health += newHealth;
        Health = Mathf.Clamp(Health, 0, MaxHealth);
        UIController.Instance.UpdateHealthBar(Health);
    }
    # endregion
}