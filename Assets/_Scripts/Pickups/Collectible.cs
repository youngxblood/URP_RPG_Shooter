using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Collectible : MonoBehaviour
{
    protected Player player;
    protected SpriteRenderer playerSpriteRenderer;
    protected SpriteRenderer spriteRenderer;
    protected CollectibleSounds collectibleSounds;
    [SerializeField] protected float buffDuration;

    private void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();   
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        playerSpriteRenderer = player.GetComponentInChildren<SpriteRenderer>();
        collectibleSounds = GetComponentInChildren<CollectibleSounds>();
    }

    public IEnumerator DestroyObjectCoroutine(float secondsUntilDestroy)
    {
        yield return new WaitForSeconds(secondsUntilDestroy);
        Destroy(gameObject);
    }

    public virtual IEnumerator ApplyBuffCoroutine()
    {
        yield return new WaitForSeconds(buffDuration);
    }
}
