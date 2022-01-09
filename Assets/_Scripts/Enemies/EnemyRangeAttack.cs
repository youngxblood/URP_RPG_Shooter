using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyRangeAttack : EnemyAttack
{
    [SerializeField] protected GameObject muzzle;
    [SerializeField] protected int ammo;
    [SerializeField] protected WeaponDataSO weaponData;
    [SerializeField] protected GameObject player;
    [field: SerializeField] public UnityEvent OnShoot { get; set; }

    public override void Attack(int damage)
    {
        if (waitBeforeNextAttack == false)
        {
            // var hittable = GetTarget().GetComponent<IHittable>();
            // hittable?.GetHit(damage, gameObject);
            player = GetTarget();

            ShootProjectile();
            StartCoroutine(WaitBeforeAttackCoroutine());
        }
    }

    private void ShootProjectile()
    {
        SpawnBullet(muzzle.transform.position, CalculateAngle(muzzle));
        OnShoot?.Invoke();
    }

    private void SpawnBullet(Vector3 position, Quaternion rotation)
    {
        var bulletPrefab = Instantiate(weaponData.BulletData.bulletPrefab, position, rotation);
        bulletPrefab.GetComponent<Bullet>().BulletData = weaponData.BulletData;
    }

    private Quaternion CalculateAngle(GameObject muzzle)
    {
        Vector3 difference = player.transform.position - transform.position;

        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }
}
