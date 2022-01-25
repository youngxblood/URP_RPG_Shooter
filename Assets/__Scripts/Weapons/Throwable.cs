using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField] protected ThrowableDataSO throwableData;
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer spriteRenderer;

    int layerMask = 1 << 8;
    

    protected void DestroyThrowable()
    {
        Object.Destroy(gameObject);
    }

    protected void PlayExplosionVFX()
    {
        var explosion = Instantiate(throwableData.explosionVFX, transform.position, Quaternion.identity);
        spriteRenderer.enabled = false;
        Object.Destroy(explosion, 0.2f);
    }

    protected void ApplyAreaDamage()
    {
        Collider2D[] enemiesInBlast = new Collider2D[CheckForEnemiesWithinBlast().Length]; 
        enemiesInBlast = CheckForEnemiesWithinBlast();
        
        foreach (var enemy in enemiesInBlast)
        {
            var hittable = enemy.gameObject.GetComponent<IHittable>();
            hittable?.GetHit(throwableData.explosionDamage, gameObject);
        }
    }

    protected Collider2D[] CheckForEnemiesWithinBlast()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, throwableData.explosionRadius, layerMask);
        return colliders;
    }
}
