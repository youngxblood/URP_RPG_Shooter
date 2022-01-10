using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] private int pickupAmmoCount = 20;
    private Weapon playerWeapon;
    [SerializeField] private UnityEvent ammoPickup;
    private SpriteRenderer sprite;

    private void Awake() 
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerWeapon = collision.GetComponentInChildren<Weapon>();

            if (!playerWeapon.AmmoFull)
            {
                // playerWeapon.Ammo += pickupAmmoCount;
                playerWeapon.Reload(pickupAmmoCount);
                ammoPickup?.Invoke();
                sprite.enabled = false;
                StartCoroutine(DestroyObjectCoroutine());
            }
        }
    }

    IEnumerator DestroyObjectCoroutine()
    {
        yield return new WaitForSeconds(0.21f);
        Destroy(gameObject);
    }
}
