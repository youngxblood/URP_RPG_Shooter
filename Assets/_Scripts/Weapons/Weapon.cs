using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected GameObject muzzle;
    [SerializeField] protected int ammo;
    [SerializeField] protected WeaponDataSO weaponData;

    public int Ammo
    {
        get { return ammo; }
        set
        {
            ammo = Mathf.Clamp(value, 0, weaponData.ammoCapacity);
        }
    }

    public bool AmmoFull { get => Ammo >= weaponData.ammoCapacity; } // Sets AmmoFull prop when ammo is full (based on weaponData SO)
    protected bool isShooting = false;
    [SerializeField] protected bool reloadCoroutine = false;

    // Events
    [field: SerializeField] public UnityEvent OnShoot { get; set; }
    [field: SerializeField] public UnityEvent OnShootNoAmmo { get; set; }

    public delegate void UpdateAmmoUI(int ammo);
    public event UpdateAmmoUI UpdateAmmo;

    private void Start()
    {
        Ammo = weaponData.ammoCapacity; // Sets ammo to max on start    
    }
    public void TryShooting()
    {
        isShooting = true;
    }

    public void StopShooting()
    {
        isShooting = false;
    }

    public void Reload(int ammo)
    {
        Ammo += ammo;
        UpdateAmmo?.Invoke(Ammo); // C# event
    }


    private void Update()
    {
        UseWeapon();
    }

    private void UseWeapon()
    {
        if (isShooting && reloadCoroutine == false)
        {
            if (Ammo > 0)
            {
                Ammo--; // Subtract ammo
                UpdateAmmo?.Invoke(Ammo); // C# event

                OnShoot?.Invoke();
                for (int i = 0; i < weaponData.GetBulletCountToSpawn(); i++)
                {
                    ShootBullet();
                }
            }
            else
            {
                isShooting = false;
                OnShootNoAmmo?.Invoke();
                return;
            }
            FinishShooting();
        }
    }

    private void FinishShooting()
    {
        StartCoroutine(DelayNextShotCoroutine());
        if (weaponData.AutomaticFire == false)
        {
            isShooting = false;
        }
    }

    protected IEnumerator DelayNextShotCoroutine()
    {
        reloadCoroutine = true;
        yield return new WaitForSeconds(weaponData.WeaponDelay);
        reloadCoroutine = false;
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
