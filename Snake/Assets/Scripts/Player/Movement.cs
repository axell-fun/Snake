using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _stepSize;
    [SerializeField] private List<Transform> _tails;
    [SerializeField] private GameObject _bonePrefab;
    [Range(0, 5), SerializeField] private float _bonesDistance;

    private PlayerInput _playerInput;
    private Vector2 _direction;
    private Vector3 _targetPosition;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        _playerInput.Player.Move.performed += ctx => MoveDirection();
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
        MoveHead(_direction.x);
        MoveTail();
        //Rotate(_direction);
    }

    private void MoveHead(float direction)
    {
        transform.position += Vector3.left * _speed * Time.deltaTime;
        _targetPosition = new Vector3(transform.position.x, transform.position.y, direction * _stepSize);
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
    }

    private void MoveTail()
    {
        float sqrDistance = Mathf.Sqrt(_bonesDistance);
        Vector3 previousPosition = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);

        foreach (var bone in _tails)
        {
            if ((bone.position - previousPosition).sqrMagnitude > sqrDistance)
            {
                Vector3 currentBonePosition = bone.position;
                bone.position = previousPosition;
                previousPosition = currentBonePosition;
            }
            /*else
            {
                break;
            }*/
        }
    }

    private void MoveDirection()
    {
        Vector2 direction = _playerInput.Player.Move.ReadValue<Vector2>();

        _direction = direction;
    }

    private void Rotate(Vector2 direction)
    {
        float angle = direction.x * 20f * Time.deltaTime;
        transform.Rotate(0, angle, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Eat eat))
        {
            Destroy(other.gameObject);

            GameObject bone = Instantiate(_bonePrefab);
            _tails.Add(bone.transform);
        }
    }
}
