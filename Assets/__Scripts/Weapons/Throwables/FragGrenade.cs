using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragGrenade : Throwable
{
    protected Rigidbody2D rigidbody2d;

    private void Awake() 
    {
        StartCoroutine(StartGrenadeFuse());  
          
    }

    private IEnumerator StartGrenadeFuse()
    {
        yield return new WaitForSeconds(throwableData.fuseTimer);
        PlayExplosionVFX();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Cancel timer coroutine and explode?
    }

    private void HitEnemy()
    {
        // Debug.Log("Hit Enemy.");
    }
    private void HitObstacle()
    {
        // Debug.Log("Hitting obstacle."); //TODO: Need to implement proper collision detection
    }
}
