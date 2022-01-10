using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] GameObject weaponParent;
    [SerializeField] private Weapon playerWeapon;
    [SerializeField] private WeaponManager weaponManager;

    private static UIController _instance;
    public static UIController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        playerWeapon = weaponParent.GetComponentInChildren<Weapon>();
    }




    private void OnEnable()
    {
        playerWeapon.UpdateAmmo += UpdateAmmoText;
        weaponManager.ChangeWeapon += ChangeWeaponUI;
    }

    private void OnDisable()
    {
        playerWeapon.UpdateAmmo -= UpdateAmmoText;
        weaponManager.ChangeWeapon -= ChangeWeaponUI;
    }

    public void UpdateAmmoText(int score)
    {
        scoreText.SetText(score.ToString());
    }

    public void ChangeWeaponUI(Weapon newWeapon)
    {
        playerWeapon = newWeapon;
    }
}
