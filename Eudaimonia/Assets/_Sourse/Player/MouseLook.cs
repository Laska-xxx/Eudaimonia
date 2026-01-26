using Source.Core;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 10f;
    [SerializeField] private Transform playerBody;
    private float _xRotation = 0f;

    private InputAction _mouseLookAction;

    private void OnEnable()
    {
        _mouseLookAction = InputManager.Instance.GameInput.Player.Look;
    }

    private void OnDisable()
    {
        _mouseLookAction = null;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Vector2 lookVector = _mouseLookAction.ReadValue<Vector2>();

        _xRotation -= lookVector.y * mouseSensitivity;
        _xRotation = Mathf.Clamp(_xRotation, -80f, 90f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * lookVector.x * mouseSensitivity);
    }
}
