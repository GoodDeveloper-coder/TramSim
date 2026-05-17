using UnityEngine;
using UnityEngine.InputSystem;

public class TramController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _speed = 10f;

    [Header("Inputs")]
    [SerializeField] private InputActionReference _moveInput;
    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float inputY = _moveInput.action.ReadValue<Vector2>().y;
        _rb.AddForce(transform.forward * inputY * _speed, ForceMode.Force);
    }
}
