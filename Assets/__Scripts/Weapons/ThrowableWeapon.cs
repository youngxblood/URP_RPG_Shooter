using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThrowableWeapon : MonoBehaviour
{
    [SerializeField] protected AgentInput agentInput;

    [SerializeField] protected GameObject muzzle;
    [SerializeField] protected int ammo;
    [SerializeField] protected ThrowableDataSO throwableData;

    public bool AmmoFull { get => Ammo >= throwableData.ammoCapacity; } // Sets AmmoFull prop when ammo is full (based on weaponData SO)
    protected bool isThrowing = false;
    protected bool reloadCoroutine = false;
    private bool hasThrownThrowable = false;

    // Events
    // ANCHOR Setup grenade throw event
    public delegate void UpdateAmmoUI(int ammo);
    public event UpdateAmmoUI UpdateAmmo;

    // Props
    public int Ammo
    {
        get { return ammo; }
        set
        {
            ammo = Mathf.Clamp(value, 0, throwableData.ammoCapacity);
        }
    }


    private void Awake() 
    {
        agentInput = transform.root.GetComponent<AgentInput>();   
    }

    private void Start()
    {
        Ammo = throwableData.ammoCapacity; // Sets ammo to max on start    
        UIController.Instance.UpdateAmmoText(Ammo);
    }

    private void OnEnable()
    {
        agentInput.StartThrowWeapon += ThrowThrowable;
        agentInput.StopThrowWeapon += StopThrowingThrowable;
    }

    private void OnDisable()
    {
        agentInput.StartThrowWeapon -= ThrowThrowable;
        agentInput.StopThrowWeapon -= StopThrowingThrowable;
        hasThrownThrowable = false;
    }
    


    public void Reload(int ammo)
    {
        Ammo += ammo;
        // UpdateAmmoText();
    }


    private void ThrowThrowable()
    {
        if (!hasThrownThrowable)
        {
            SpawnGrenade(muzzle.transform.position, Quaternion.identity);
            hasThrownThrowable = true;
        }   
    }


    #region Helpers

    private void StopThrowingThrowable()
    {
        hasThrownThrowable = false;
    }

    // public void UpdateAmmoText()
    // {
    //     UIController.Instance.UpdateAmmoText(Ammo);
    // }

    private void SpawnGrenade(Vector3 position, Quaternion rotation)
    {
        var grenadePrefab = Instantiate(throwableData.grenadePrefab, position, rotation);
    }

    #endregion
}
