using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Collectible : MonoBehaviour
{
    protected Player player;
    protected SpriteRenderer spriteRenderer;
    protected CollectibleSounds collectibleSounds;

    private void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();    
        spriteRenderer = GetComponent<SpriteRenderer>();
        collectibleSounds = GetComponentInChildren<CollectibleSounds>();
    }

    public IEnumerator DestroyObjectCoroutine()
    {
        yield return new WaitForSeconds(0.21f);
        Destroy(gameObject);
    }
}
