using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class HealthPickup : Collectible
{
    [SerializeField] private int healthIncrease;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (player.Health < player.MaxHealth)
            {
                player.HealPlayer(healthIncrease);
                collectibleSounds.PlayPickupSound();
                spriteRenderer.enabled = false;
                StartCoroutine(DestroyObjectCoroutine());
            }
        }
    }
}
