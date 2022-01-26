using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Weapon currentWeapon;
    [SerializeField] private Weapon[] equippedWeapons; // Set in the inspector

    // Used in AgentWeapon/PlayerWeapon (see WeaponParent object)
    [SerializeField] private PlayerWeapon playerWeapon;

    // Event to switch weapons for ammo UI
    public delegate void SwitchWeapon(Weapon newWeapon);
    public event SwitchWeapon ChangeWeapon;

    private void Awake() 
    {
        currentWeapon = player.GetComponentInChildren<Weapon>();
        playerWeapon = player.GetComponentInChildren<PlayerWeapon>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetWeapon(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetWeapon(1);
        }
    }

    private void SetWeapon(int v)
    {
        if (currentWeapon != equippedWeapons[v])
        {
            ChangeWeapon.Invoke(equippedWeapons[v]); // Changes weapon UI
            
            currentWeapon.gameObject.SetActive(false);
            equippedWeapons[v].gameObject.SetActive(true);
            currentWeapon = equippedWeapons[v];
            currentWeapon.UpdateAmmoText(currentWeapon.Ammo);
            

            playerWeapon.Weapon = currentWeapon;
            playerWeapon.WeaponRenderer = currentWeapon.GetComponent<WeaponRenderer>();

            
        }
    }
}
