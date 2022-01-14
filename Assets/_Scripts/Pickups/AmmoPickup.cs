using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AmmoPickup : Collectible
{
    [SerializeField] private int pickupAmmoCount = 20;
    private Weapon playerWeapon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerWeapon = collision.GetComponentInChildren<Weapon>();

            if (!playerWeapon.AmmoFull)
            {
                // playerWeapon.Ammo += pickupAmmoCount;
                playerWeapon.Reload(pickupAmmoCount);

                collectibleSounds.PlayPickupSound();
                spriteRenderer.enabled = false;
                StartCoroutine(DestroyObjectCoroutine());
            }
        }
    }
}
