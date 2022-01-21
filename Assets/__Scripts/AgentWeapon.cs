using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    protected float desiredAngle;
    [SerializeField] protected WeaponRenderer weaponRenderer;
    [SerializeField] protected Weapon weapon; // Ref to weapon script

    // Props
    public Weapon Weapon
    {
        get { return weapon; }
        set { weapon = value; }
    }

    public WeaponRenderer WeaponRenderer
    {
        get { return weaponRenderer; }
        set { weaponRenderer = value; }
    }

    private void Awake()
    {
        AssignWeapon();
    }

    #region  Helpers
    private void AssignWeapon()
    {
        weaponRenderer = GetComponentInChildren<WeaponRenderer>();
        weapon = GetComponentInChildren<Weapon>();
    }

    public virtual void AimWeapon(Vector2 pointerPosition)
    {
        var aimDirection = (Vector3)pointerPosition - transform.position;
        desiredAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg; //! Investigate the math
        AdjustWeaponRendering();
        transform.rotation = Quaternion.AngleAxis(desiredAngle, Vector3.forward);
    }

    protected void AdjustWeaponRendering()
    {
        if (weaponRenderer != null)
        {
            weaponRenderer.FlipSprite(desiredAngle > 90 || desiredAngle < -90); // Flips sprite when looking left with weapon
            weaponRenderer.RenderBehindHead(desiredAngle < 172 && desiredAngle > 8);
        }
    }

    public void Shoot()
    {
        if(weapon != null)
            weapon.TryShooting();
    }

    public void StopShooting()
    {   
        if(weapon != null)
            weapon.StopShooting();
    }
    
    #endregion
}
