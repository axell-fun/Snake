using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FoodCollection))]
[RequireComponent(typeof(SnakeColorChange))]
[RequireComponent(typeof(Snake))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _boostSpeed;
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
    private bool _isFever;

    public bool IsFever => _isFever;

    public List<GameObject> Tails => _tails;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        _foodCollection = GetComponent<FoodCollection>();
        _snake = GetComponent<Snake>();
        _snakeColorChange = GetComponent<SnakeColorChange>();

        _playerInput.Player.TapInput.performed += ctx => MoveDirection();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _snake.DamageReceived += RemoveTail;
        _foodCollection.FoodCollected += AddTail;
        _foodCollection.FeverActivated += ActiveFever;
        _snakeColorChange.SnakeHeadColorChanged += ChangeColorTail;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _snake.DamageReceived -= RemoveTail;
        _foodCollection.FoodCollected -= AddTail;
        _foodCollection.FeverActivated -= ActiveFever;
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
        Vector2 direction = _playerInput.Player.TapInput.ReadValue<Vector2>();

        if (direction.x > 600 && !_isFever)
            _direction = new Vector2(1, 0);
        else if (!_isFever)
            _direction = new Vector2(-1, 0);
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

    private void ActiveFever()
    {
        _isFever = true;
        _indexMesh = 3;
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
        _speed *= _boostSpeed;
        _direction = new Vector2(0, 0);
        StartCoroutine(WorkFever());
    }

    private IEnumerator WorkFever()
    {
        yield return new WaitForSeconds(5f);
        _speed /= _boostSpeed;
        _isFever = false;
    }
}