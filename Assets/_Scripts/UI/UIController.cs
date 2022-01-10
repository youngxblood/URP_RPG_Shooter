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

    private void Awake() 
    {
        // scoreText = scoreTextObj.GetComponent<TextMeshPro>();  
        playerWeapon = weaponParent.GetComponentInChildren<Weapon>();
    }

    private void OnEnable() 
    {
        playerWeapon.UpdateAmmo += UpdateScoreText;
        weaponManager.ChangeWeapon += ChangeWeaponUI;
    }

    private void OnDisable() 
    {
        playerWeapon.UpdateAmmo -= UpdateScoreText;    
        weaponManager.ChangeWeapon -= ChangeWeaponUI;
    }

    public void UpdateScoreText(int score)
    {
        scoreText.SetText(score.ToString());
    }

    public void ChangeWeaponUI(Weapon newWeapon)
    {
        playerWeapon = newWeapon;
    }
}
