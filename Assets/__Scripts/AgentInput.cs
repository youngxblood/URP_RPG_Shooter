using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentInput : MonoBehaviour, IAgentInput
{
    private Camera mainCamera;
    private bool fireButtonDown = false;
    private bool secondaryButtonDown = false;

    [field: SerializeField] public UnityEvent<Vector2> OnMovementKeyPressed { get; set; }
    [field: SerializeField] public UnityEvent<Vector2> OnPointerPositionChanged { get; set; }

    [field: SerializeField] public UnityEvent OnFireButtonPressed { get; set; }
    [field: SerializeField] public UnityEvent OnFireButtonReleased { get; set; }

    public delegate void StartThrowGrenadeAction();
    public event StartThrowGrenadeAction StartThrowWeapon;
    public delegate void StopThrowGrenadeAction();
    public event StopThrowGrenadeAction StopThrowWeapon;

    private void Awake()
    {
        mainCamera = Camera.main; //Gets camera based on it's tag    
    }
    

    private void Update()
    {
        GetMovementInput();
        GetPointerInput();
        GetFireInput();
    }

    // Check if fire button is pressed
    private void GetFireInput()
    {
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            if (fireButtonDown == false)
            {
                fireButtonDown = true;
                OnFireButtonPressed?.Invoke();
            }

        }
        else
        {
            if (fireButtonDown)
            {
                fireButtonDown = false;
                OnFireButtonReleased?.Invoke();
            }

        }
        
        // Throwables
        if (Input.GetButtonDown("Fire2"))
            StartThrowWeapon.Invoke(); // Throws grenade
        if (Input.GetButtonUp("Fire2"))
            StopThrowWeapon.Invoke(); // Release M2
    }

    private void GetPointerInput()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.nearClipPlane;
        var mouseInWorldSpace = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        OnPointerPositionChanged?.Invoke(mouseInWorldSpace);
    }

    private void GetMovementInput()
    {
        OnMovementKeyPressed?.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), (Input.GetAxisRaw("Vertical"))));
    }
}