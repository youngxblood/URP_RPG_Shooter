using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRenderer; 
    [SerializeField] protected TrapAudioPlayer trapAudioPlayer;
    [SerializeField] protected Animator animator;

    [SerializeField] protected int trapDamage = 1;
    [SerializeField] protected float trapTimeActive = 2f;
    [SerializeField] protected float trapTimeToActivate = 2f;
    [SerializeField] protected bool canDamageTarget = false;
    [SerializeField] protected float damageDelay = 1f; // How frequently the trap deals damage

    private void Awake() 
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        trapAudioPlayer = GetComponentInChildren<TrapAudioPlayer>();
        animator = GetComponentInChildren<Animator>();    
    }

    private void OnDisable() 
    {
        StopAllCoroutines();    
    }
}
