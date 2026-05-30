using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TramController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _maxSpeed = 100f;
    [SerializeField] private float _minRotateRailDistance = 0.5f;
    [SerializeField] private List<Transform> _railPoints = new List<Transform>();

    [Header("UI")]
    [SerializeField] private TMP_Text _speedText;
    [SerializeField] private TMP_Text _openDoorsText;

    [Header("Inputs")]
    [SerializeField] private InputActionReference _moveInput;
    [SerializeField] private InputActionReference _openDoorInput;
    private int _nextRailPoint = 0;
    private float _currentSpeed = 0f;
    private Vector3 _moveDirection;
    private Vector3 _eulerAngles;
    private float _followRotation = 0f;
    private bool _canOpenDoor = false;
    public bool _openDoors { get; private set; }

    void Start()
    {
        _moveDirection = transform.forward;
        _openDoorInput.action.started += DoorsTrigger;
    }

    void Update()
    {
        Move();
        RotateRail();
    }

    void Move()
    {
        float inputY = _moveInput.action.ReadValue<Vector2>().y;
        if (inputY != 0f)
        {
            _currentSpeed += _speed * inputY * Time.deltaTime;
        }
        else
        {
            _currentSpeed -= _speed * Time.deltaTime / 5;
        }

        _currentSpeed = Math.Clamp(_currentSpeed, 0, _maxSpeed);
        transform.position += _moveDirection * _currentSpeed * Time.deltaTime;
        _speedText.text = "Speed: " + (int)_currentSpeed;
    }

    void RotateRail()
    {
        if (Vector3.Distance(transform.position, _railPoints[_nextRailPoint].position) <= _minRotateRailDistance)
        {
            _moveDirection = _railPoints[_nextRailPoint++].forward;
            _followRotation -= 45f;

            if (_nextRailPoint >= _railPoints.Count)
                _nextRailPoint = 0;
        }
        _eulerAngles = Vector3.Lerp(_eulerAngles, new Vector3(0, _followRotation, 0), 45 * Time.deltaTime);
        transform.localEulerAngles = _eulerAngles;
    }

    void DoorsTrigger(InputAction.CallbackContext context)
    {
        if (!_canOpenDoor)
            return;

        _openDoors = !_openDoors;
        if (_openDoors)
        {
            SetCanOpenDoor(false);
            GameManager._instance.AddCoins(100);
        }
    }

    public void SetCanOpenDoor(bool canOpenDoor)
    {
        _canOpenDoor = canOpenDoor;
        _openDoorsText.gameObject.SetActive(_canOpenDoor);
    }
    
    void OnDestroy()
    {
        _openDoorInput.action.started -= DoorsTrigger;
    }
}
