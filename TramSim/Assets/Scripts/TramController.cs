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

    [Header("Inputs")]
    [SerializeField] private InputActionReference _moveInput;
    private int _nextRailPoint = 0;
    private float _currentSpeed = 0f;
    private Vector3 _moveDirection;
    private Vector3 _eulerAngles;
    private float _followRotation = 0f;

    void Start()
    {
        _moveDirection = transform.forward;
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
            _currentSpeed -= _speed * Time.deltaTime / 3;
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
}
