using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    [SerializeField] private Material _meshSpawner;
    [SerializeField] private int _numberPointsWithFood;
    [SerializeField] private Eat _eat;
    [SerializeField] private Coin _coin;
    [SerializeField] private Peaks _peaks;
    [SerializeField] private GameObject _food;


    private void Awake()
    {
        _eat.ChangeMesh(_meshSpawner);
        
        for (int i = 0; i < _numberPointsWithFood; i++)
        {
            Instantiate(_food, new Vector3(transform.position.x - 6f + i * -10f, transform.position.y, 3f), Quaternion.identity);
            //Instantiate(_food[Random.Range(0, _food.Length)], new Vector3(transform.position.x - 10f + i * -10f, transform.position.y, 0f), Quaternion.identity);
            //Instantiate(_food[Random.Range(0, _food.Length)], new Vector3(transform.position.x - 2f + i * -10f, transform.position.y, -3f), Quaternion.identity);
        }
    }
}
