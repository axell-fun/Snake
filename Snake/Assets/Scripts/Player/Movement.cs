using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _stepSize;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        _playerInput.Player.Move.performed += ctx => Move();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Update()
    {
        transform.position += Vector3.left * _speed * Time.deltaTime;
    }

    private void Move()
    {
        Vector2 direction = _playerInput.Player.Move.ReadValue<Vector2>();

        if (direction.x > 0)
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + _stepSize);
        else if (direction.x < 0)
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + -_stepSize);
    }
}
