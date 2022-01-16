using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedPowerup : Collectible
{
    private AgentMovement playerMovement;
    [SerializeField] private float accelerationBuff, decelerationBuff = 100f;
    [SerializeField] private float maxSpeedBuff = 3f;
    private Color originalColor;
    [SerializeField] private Color speedBuffColor;

    private void Start()
    {
        originalColor = player.spriteRenderer.color;
        originalColor.a = 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerMovement = collision.GetComponent<AgentMovement>();
            playerMovement.acceleration += accelerationBuff;
            playerMovement.deacceleration += decelerationBuff;
            playerMovement.maxSpeed += 3f;
            speedBuffColor.a = 1f;
            player.spriteRenderer.color = speedBuffColor;
            
            // Coroutine for buff duration and reset
            StartCoroutine(ApplyBuffCoroutine());
            StartCoroutine(DestroyObjectCoroutine(buffDuration + 0.3f));
        }
    }

    public override IEnumerator ApplyBuffCoroutine()
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(buffDuration);

        // Reset player stats and color
        playerMovement.acceleration -= accelerationBuff;
        playerMovement.deacceleration -= decelerationBuff;
        playerMovement.maxSpeed -= 3f;
        player.spriteRenderer.color = originalColor;
    }
}

