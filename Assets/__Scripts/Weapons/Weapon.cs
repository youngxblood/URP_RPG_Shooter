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

    public bool AmmoFull { get => Ammo >= weaponData.ammoCapacity; } // Sets AmmoFull prop when ammo is full (based on weaponData SO)
    protected bool isShooting = false;
    protected bool reloadCoroutine = false;

    // Events
    [field: SerializeField] public UnityEvent OnShoot { get; set; }
    [field: SerializeField] public UnityEvent OnShootNoAmmo { get; set; }

    // public delegate void UpdateAmmoUI(int ammo);
    // public event UpdateAmmoUI UpdateAmmo;

    // Props
    public int Ammo
    {
        get { return ammo; }
        set
        {
            ammo = Mathf.Clamp(value, 0, weaponData.ammoCapacity);
        }
    }

    private void Start()
    {
        Ammo = weaponData.ammoCapacity; // Sets ammo to max on start    
        UIController.Instance.UpdateAmmoText(Ammo);
    }

    private void Update()
    {
        UseWeapon();
    }


    private void OnDisable()
    {
        isShooting = false;
        reloadCoroutine = false;
    }



    public void Reload(int ammo)
    {
        Ammo += ammo;
        UpdateAmmoText();
    }


    private void UseWeapon()
    {
        if (isShooting && reloadCoroutine == false)
        {
            if (Ammo > 0)
            {
                Ammo--; // Subtract ammo
                UpdateAmmoText();

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

    #region Helpers

    public void TryShooting()
    {
        isShooting = true;
    }

    public void StopShooting()
    {
        isShooting = false;
    }

    public void UpdateAmmoText()
    {
        UIController.Instance.UpdateAmmoText(Ammo);
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

    #endregion
}