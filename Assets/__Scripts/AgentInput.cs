using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class AgentInput : MonoBehaviour, IAgentInput
{
    private Camera mainCamera;
    private CinemachineVirtualCamera cinemachineCamera;
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
        cinemachineCamera = FindObjectOfType<CinemachineVirtualCamera>();  
    }

    private void Update()
    {
        GetMovementInput();
        GetPointerInput();
        GetFireInput();
        GetThrowableInput();
        GetCameraZoomInput();
    }

    #region Helpers
    private void GetMovementInput()
    {
        OnMovementKeyPressed?.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), (Input.GetAxisRaw("Vertical"))));
    }

    private void GetPointerInput()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.nearClipPlane;
        var mouseInWorldSpace = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        OnPointerPositionChanged?.Invoke(mouseInWorldSpace);
    }

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
    }

    private void GetThrowableInput()
    {
        if (Input.GetButtonDown("Fire2"))
            StartThrowWeapon.Invoke();
        if (Input.GetButtonUp("Fire2"))
            StopThrowWeapon.Invoke();
    }

    private void GetCameraZoomInput()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            ZoomCameraIn();
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
            ZoomCameraOut();
    }

    private void ZoomCameraIn()
    {
        cinemachineCamera.m_Lens.OrthographicSize = Mathf.Clamp(cinemachineCamera.m_Lens.OrthographicSize - 1, 3, 10); 
    }

    private void ZoomCameraOut()
    {
        cinemachineCamera.m_Lens.OrthographicSize = Mathf.Clamp(cinemachineCamera.m_Lens.OrthographicSize + 1, 3, 10);
    }

    #endregion
}
