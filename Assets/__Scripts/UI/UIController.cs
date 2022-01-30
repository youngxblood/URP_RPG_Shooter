using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // Object refs
    [Header("Ammo and Throwables")]
    [SerializeField] private Canvas gameplayCanvas;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private TextMeshProUGUI throwableAmmoText;
    private GameObject player;
    [SerializeField] GameObject weaponParent;
    [SerializeField] private Weapon playerWeapon;
    [SerializeField] private WeaponManager weaponManager;


    [Header("Life UI")]
    [SerializeField] private GameObject[] playerLifeHearts;
    [SerializeField] private Sprite fullHealthSprite;
    [SerializeField] private Sprite emptyHealthSprite;

    private static UIController _instance;
    public static UIController Instance { get { return _instance; } }

    [Header("Life Slider")]
    [SerializeField] private Slider slider;

    [Header("PauseMenu")]
    [SerializeField] private Canvas pauseMenuCanvas;
    public bool gameIsPaused { get; private set; } = false;

    public TextMeshProUGUI test;


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

        // Get obj refs
        player = GameObject.FindGameObjectWithTag("Player");
        weaponParent = player.transform.Find("WeaponParent").gameObject;
        playerWeapon = weaponParent.GetComponentInChildren<Weapon>();
        weaponManager = FindObjectOfType<WeaponManager>();
        
        // Get text refs
        ammoText = gameplayCanvas.transform.Find("Ammo/Background/AmmoCount").gameObject.GetComponent<TextMeshProUGUI>();
        throwableAmmoText = gameplayCanvas.transform.Find("Throwable/Background/BombCount").gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void Start() 
    {
        ClosePauseMenu();   

        

    }

    private void Update() 
    {
        HandlePauseMenu();    
    }

    private void OnEnable()
    {
        // playerWeapon.UpdateAmmo += UpdateAmmoText;
        weaponManager.ChangeWeapon += ChangeWeaponUI;
    }

    private void OnDisable()
    {
        // playerWeapon.UpdateAmmo -= UpdateAmmoText;
        weaponManager.ChangeWeapon -= ChangeWeaponUI;
    }

    //* IN-GAME UI *//
    public void UpdateAmmoText(int ammo)
    {
        ammoText.SetText(ammo.ToString());
    }

    public void UpdateThrowableAmmoText(int ammo)
    {
        throwableAmmoText.SetText(ammo.ToString());
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

    //* PAUSE MENU *//

    private void HandlePauseMenu()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameIsPaused)
                OpenPauseMenu();
            else
                ClosePauseMenu();
        }
    }

    public void OpenPauseMenu()
    {
        Time.timeScale = 0f;
        pauseMenuCanvas.enabled = true;
        gameplayCanvas.enabled = false;
        gameIsPaused = true;
    }

    public void ClosePauseMenu()
    {
        Time.timeScale = 1f;
        pauseMenuCanvas.enabled = false;
        gameplayCanvas.enabled = true;
        gameIsPaused = false;
    }

    public void ReturnToMainMenu()
    {
        // SceneManager.LoadScene(0);
    }
}
