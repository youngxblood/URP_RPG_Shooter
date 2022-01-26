using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThrowableWeapon : MonoBehaviour
{
    [SerializeField] protected AgentInput agentInput;
    [SerializeField] protected Transform playerTransform;

    [SerializeField] protected GameObject muzzle;
    [SerializeField] protected int ammo;
    [SerializeField] protected ThrowableDataSO throwableData;

    public bool AmmoFull { get => Ammo >= throwableData.ammoCapacity; } // Sets AmmoFull prop when ammo is full (based on weaponData SO)
    protected bool isThrowing = false;
    protected bool reloadCoroutine = false;
    private bool hasThrownThrowable = false;
    [SerializeField] private float throwStrength = 2f;

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
        playerTransform = transform.root;
    }

    private void Start()
    {
        Ammo = throwableData.ammoCapacity; // Sets ammo to max on start    
        UpdateThrowableAmmoText(Ammo);
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
    
    #region Helpers
    // Input key down
    private void ThrowThrowable()
    {
        if (!hasThrownThrowable && Ammo > 0) // Bool is reset on throw key up
        {
            SpawnGrenade(muzzle.transform.position, Quaternion.identity);
            Ammo--;
            UpdateThrowableAmmoText(Ammo);
            hasThrownThrowable = true;
        }   
    }

    // Input key up
    private void StopThrowingThrowable()
    {
        hasThrownThrowable = false;
    }

    public void UpdateThrowableAmmoText(int ammo)
    {
        UIController.Instance.UpdateThrowableAmmoText(Ammo);
    }

    private void SpawnGrenade(Vector3 position, Quaternion rotation)
    {
        var grenadePrefab = Instantiate(throwableData.grenadePrefab, position, rotation);
        grenadePrefab.GetComponent<Rigidbody2D>().AddForce(GetThrowDirection() * throwStrength, ForceMode2D.Impulse);
    }

    private Vector2 GetThrowDirection()
    {
        return muzzle.transform.position - playerTransform.position;
    }

    #endregion
}
