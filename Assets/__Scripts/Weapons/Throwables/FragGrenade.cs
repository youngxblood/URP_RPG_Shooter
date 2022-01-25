using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragGrenade : Throwable
{
    protected Rigidbody2D rb;

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
        
        StartCoroutine(StartGrenadeFuse());
    }

    public void OnDrawGizmos()
    {
        DrawRadiusGizmo(throwableData.explosionRadius);
    }
}
