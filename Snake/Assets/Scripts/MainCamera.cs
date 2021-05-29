using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _cameraPositionX;

    private void Update()
    {
        var newPosition = new Vector3(_target.transform.position.x + _cameraPositionX, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, newPosition, _speed * Time.deltaTime);
    }
}
