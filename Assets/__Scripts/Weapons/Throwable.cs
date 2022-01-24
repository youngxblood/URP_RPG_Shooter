using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField] protected ThrowableDataSO throwableData;
    [SerializeField] protected Animator animator;

    private void Awake() 
    {
        animator = GetComponent<Animator>();    
    }

    protected void PlayExplosionVFX()
    {
        var explosion = Instantiate(throwableData.explosionVFX, transform.position, Quaternion.identity);
        Object.Destroy(explosion, 0.2f);
    }
}
