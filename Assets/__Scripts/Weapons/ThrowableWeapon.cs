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
    [SerializeField] private float throwStrengthMaxForce = 200f;
    private float holdDownStartTime;


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
        agentInput.StartThrowWeapon += StartThrowing;
        agentInput.StopThrowWeapon += FinishThrowing;
    }

    private void OnDisable()
    {
        agentInput.StartThrowWeapon -= StartThrowing;
        agentInput.StopThrowWeapon -= FinishThrowing;
        hasThrownThrowable = false;
    }
    
    #region Helpers
    // Input key down
    private void StartThrowing()
    {
        if (!hasThrownThrowable && Ammo > 0) // Bool is reset on throw key up
        {
            hasThrownThrowable = true;
            holdDownStartTime = Time.time;
        }   
    }

    // Input key up
    private void FinishThrowing()
    {
        float holdDownTime = Time.time - holdDownStartTime;
        
        SpawnGrenade(muzzle.transform.position, CalculateHoldDownForce(holdDownTime));
        Ammo--;
        UpdateThrowableAmmoText(Ammo);
        hasThrownThrowable = false;
    }

    private float CalculateHoldDownForce(float holdTime)
    {
        float maxHoldDownTime = 2f;
        float holdDownTimeNormalized = Mathf.Clamp01(holdTime/maxHoldDownTime);
        float force = holdDownTimeNormalized * throwStrengthMaxForce;
        return force;
    }

    public void UpdateThrowableAmmoText(int ammo)
    {
        UIController.Instance.UpdateThrowableAmmoText(Ammo);
    }

    private void SpawnGrenade(Vector3 position, float force)
    {
        var grenadePrefab = Instantiate(throwableData.grenadePrefab, position, Quaternion.identity);
        grenadePrefab.GetComponent<Rigidbody2D>().AddForce(GetThrowDirection() * force, ForceMode2D.Impulse);
    }

    private Vector2 GetThrowDirection()
    {
        return muzzle.transform.position - playerTransform.position;
    }

    #endregion
}
