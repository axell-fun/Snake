using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : MonoBehaviour
{
    [SerializeField] private MeshRenderer _mesh;

    public void ChangeMesh(Material newMesh)
    {
        _mesh.material = newMesh;
    }
}
