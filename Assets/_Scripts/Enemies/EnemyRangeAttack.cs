using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : EnemyAttack
{
    [SerializeField] protected GameObject muzzle;
    [SerializeField] protected int ammo;
    [SerializeField] protected WeaponDataSO weaponData;

    public override void Attack(int damage)
    {
        if (waitBeforeNextAttack == false)
        {
            var hittable = GetTarget().GetComponent<IHittable>();
            // hittable?.GetHit(damage, gameObject);
            ShootBullet();
            StartCoroutine(WaitBeforeAttackCoroutine());
        }
    }

    private void ShootBullet()
    {
        SpawnBullet(muzzle.transform.position, CalculateAngle(muzzle));
    }

    private void SpawnBullet(Vector3 position, Quaternion rotation)
    {
        var bulletPrefab = Instantiate(weaponData.BulletData.bulletPrefab, position, rotation);
        bulletPrefab.GetComponent<Bullet>().BulletData = weaponData.BulletData;
    }

    private Quaternion CalculateAngle(GameObject muzzle)
    {
        float spread = UnityEngine.Random.Range(-weaponData.SpreadAngle, weaponData.SpreadAngle); // Spread between negative spread value and it's positive form
        Quaternion bulletSpreadRotation = Quaternion.Euler(new Vector3(0, 0, spread)); // We only want to rotate the bullets on the "Z" angle
        return muzzle.transform.rotation * bulletSpreadRotation; // Multiplying the muzzle's rotation by bullet spread rotation adds the two values together
    }
}
