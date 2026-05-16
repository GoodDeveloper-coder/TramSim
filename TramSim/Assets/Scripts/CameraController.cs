using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _sensitivity = 1f;
    [SerializeField] private float _maxRotateAngleX = 60f;
    [SerializeField] private float _maxRotateAngleY = 60f;

    [Header("Inputs")]
    [SerializeField] private InputActionReference _mouseInput;
    private Vector3 _eulerAngles;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _eulerAngles = transform.localEulerAngles;
    }

    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        Vector2 mouseDirection = _mouseInput.action.ReadValue<Vector2>();
        _eulerAngles += new Vector3(-mouseDirection.y, mouseDirection.x, 0f) * _sensitivity * Time.deltaTime;
        _eulerAngles.x = Math.Clamp(_eulerAngles.x, -_maxRotateAngleX, _maxRotateAngleX);
        _eulerAngles.y = Math.Clamp(_eulerAngles.y, -_maxRotateAngleY, _maxRotateAngleY);
        transform.localEulerAngles = _eulerAngles;
    }
}
