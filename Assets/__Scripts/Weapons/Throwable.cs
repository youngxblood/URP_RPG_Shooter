using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField] protected ThrowableDataSO throwableData;
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    protected void PlayExplosionVFX()
    {
        var explosion = Instantiate(throwableData.explosionVFX, transform.position, Quaternion.identity);
        spriteRenderer.enabled = false;
        Object.Destroy(explosion, 0.2f);
    }
}
