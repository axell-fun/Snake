using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    [SerializeField] private Material _meshSpawner;
    [SerializeField] private Eat _eat;
    [SerializeField] private int _numberPointsWithFood;
    [SerializeField] private int _startSpawnPositionX;
    [SerializeField] private int _distanceBetweenObjects;
    [SerializeField] private GameObject[] _food;
    [SerializeField] private GameObject[] _centerLine;
    [SerializeField] private int[] _spawnPositionZ;


    private void Awake()
    {
        _eat.ChangeMesh(_meshSpawner);
        
        for (int i = 0; i < _numberPointsWithFood; i++)
        {
            Instantiate(_food[Random.Range(0, _food.Length)], new Vector3(transform.position.x - _startSpawnPositionX + i * _distanceBetweenObjects, transform.position.y, _spawnPositionZ[Random.Range(0, _spawnPositionZ.Length)]), Quaternion.identity);
            Instantiate(_centerLine[Random.Range(0, _centerLine.Length)], new Vector3(transform.position.x - _startSpawnPositionX + i * _distanceBetweenObjects, transform.position.y, 0), Quaternion.identity);
        }
    }
}
