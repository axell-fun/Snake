using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FoodCollection))]
[RequireComponent(typeof(SnakeColorChange))]
[RequireComponent(typeof(Snake))]
public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject _test;

    [SerializeField] private float _speed;
    [SerializeField] private float _stepSize;
    [SerializeField] private List<GameObject> _tails;
    [SerializeField] private GameObject _bonePrefab;
    [Range(0, 35), SerializeField] private float _bonesDistance;

    private FoodCollection _foodCollection;
    private SnakeColorChange _snakeColorChange;
    private Snake _snake;
    private PlayerInput _playerInput;
    private MeshRenderer _meshTail;
    private int _indexMesh = 0;
    private Vector2 _direction;
    private Vector3 _targetPosition;

    private bool _moveLeft;

    public List<GameObject> Tails => _tails;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        _foodCollection = GetComponent<FoodCollection>();
        _snake = GetComponent<Snake>();
        _snakeColorChange = GetComponent<SnakeColorChange>();

        _playerInput.Player.Tap.performed += ctx => MoveDirection();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _snake.DamageReceived += RemoveTail;
        _foodCollection.FoodCollected += AddTail;
        _snakeColorChange.SnakeHeadColorChanged += ChangeColorTail;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _snake.DamageReceived -= RemoveTail;
        _foodCollection.FoodCollected -= AddTail;
        _snakeColorChange.SnakeHeadColorChanged -= ChangeColorTail;
    }

    private void Update()
    {
        MoveHead(_direction.x);
        MoveTail();
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
            if ((bone.transform.position - previousPosition).sqrMagnitude > sqrDistance)
            {
                Vector3 currentBonePosition = bone.transform.position;
                bone.transform.position = previousPosition;
                previousPosition = currentBonePosition;
            }
        }
    }

    private void MoveDirection()
    {
        if (_moveLeft)
        {
            _direction = new Vector2(1,0);
            _moveLeft = false;
        }
        else
        {
            _direction = new Vector2(-1, 0);
            _moveLeft = true;
        }
    }

    private void AddTail()
    {
        GameObject bone = Instantiate(_bonePrefab);
        _tails.Add(bone);

        _meshTail = _tails[_tails.Count - 1].GetComponent<MeshRenderer>();
        _meshTail.material = _snakeColorChange.MeshSpawnPoint[_indexMesh - 1].material;
    }

    private void RemoveTail()
    {
        if (_tails.Count >= 1)
        {
            GameObject boneForDestroy = _tails[_tails.Count - 1];
            _tails.RemoveAt(_tails.Count - 1);
            Destroy(boneForDestroy);
        }
    }

    private void ChangeColorTail()
    {
        for (int i = 0; i < _tails.Count; i++)
        {
            _meshTail = _tails[i].GetComponent<MeshRenderer>();
            _meshTail.material = _snakeColorChange.MeshSpawnPoint[_indexMesh].material;
        }

        _indexMesh++;
    }
}
