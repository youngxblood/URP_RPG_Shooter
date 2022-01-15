using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] GameObject weaponParent;
    [SerializeField] private Weapon playerWeapon;
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private GameObject[] playerLifeHearts;

    [Header("Life UI")]
    [SerializeField] private Sprite fullHealthSprite;
    [SerializeField] private Sprite emptyHealthSprite;

    private static UIController _instance;
    public static UIController Instance { get { return _instance; } }

    [Header("Life Slider")]
    [SerializeField] private Slider slider;

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

    public void UpdateHealthBar(int health)
    {
        slider.value = health;
    }

    public void SetMaxHealthValue(int health)
    {
        slider.maxValue = health;
    }

    public void FillHeartSprite(int i)
    {
        playerLifeHearts[i - 1].GetComponent<Image>().sprite = fullHealthSprite;
    }

    public void EmptyHeartSprite(int i)
    {
        playerLifeHearts[i - 1].GetComponent<Image>().sprite = emptyHealthSprite;
    }
}
