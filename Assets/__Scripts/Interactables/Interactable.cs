using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Animator animator;
    [SerializeField] protected BoxCollider2D boxCollider;
    protected InteractableSounds interactableSounds;
    protected bool hasBeenInteractedWith = false;

    private void Awake() 
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();    
        boxCollider = GetComponent<BoxCollider2D>();
        interactableSounds = GetComponentInChildren<InteractableSounds>();
    }

    public virtual void Interact()
    {
        
    }
}
