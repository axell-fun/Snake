using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnakeColorChange : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _meshSpawnPoint;
    [SerializeField] private MeshRenderer _currentMesh;

    private int _indexMesh = 0;

    public MeshRenderer[] MeshSpawnPoint => _meshSpawnPoint;

    public event UnityAction SnakeHeadColorChanged;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SpawnFood spawnFood))
        {
            SnakeHeadColorChanged?.Invoke();
            _currentMesh.materials = _meshSpawnPoint[_indexMesh].materials;
            _indexMesh++;
        }
    }
}
