using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField] protected ThrowableDataSO throwableData;
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    int layerMask = 1 << 8;


    protected IEnumerator StartGrenadeFuse()
    {
        yield return new WaitForSeconds(throwableData.fuseTimer);
        ExplodeThrowable();
    }

    protected void ExplodeThrowable()
    {
        PlayExplosionVFX();
        ApplyAreaDamage();
        DestroyThrowable();
        ScreenShakeManager.Instance.ShakeCamera(1f, 0.3f);
    }

    #region  Helpers
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

    protected void DestroyThrowable()
    {
        Object.Destroy(gameObject);
    }

    protected Collider2D[] CheckForEnemiesWithinBlast()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, throwableData.explosionRadius, layerMask);
        return colliders;
    }


    // If explodes on impact
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (throwableData.explodesOnImpact && other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            StopCoroutine(StartGrenadeFuse());
            ExplodeThrowable();
        }
    }

    // Visualization for blast radius
    public void DrawRadiusGizmo(float radius)
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, radius);
            Gizmos.color = Color.white;
        }
    }

    #endregion
}
