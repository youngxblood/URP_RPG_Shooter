using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragGrenade : Throwable
{
    protected Rigidbody2D rb;
    protected ThrowableAudio throwableAudio;

    public Rigidbody2D RB
    {
        get { return rb; }
        set { rb = value; }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        throwableAudio = GetComponentInChildren<ThrowableAudio>();

        
        StartCoroutine(StartGrenadeFuse());
    }
    private void Start() 
    {
        throwableAudio.PlayThrowClip();
    }

    public void OnDrawGizmos()
    {
        DrawRadiusGizmo(throwableData.explosionRadius);
    }
}
