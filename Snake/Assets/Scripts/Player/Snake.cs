using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(FoodCollection))]
public class Snake : MonoBehaviour
{
    private FoodCollection _foodCollection;
    private Movement _move;

    private int _health;

    public event UnityAction DamageReceived;

    private void Awake()
    {
        _move = GetComponent<Movement>();
        _foodCollection = GetComponent<FoodCollection>();

        _health = _move.Tails.Count;
    }

    private void OnEnable()
    {
        _foodCollection.PeaksHit += TakeDamage;
    }

    private void OnDisable()
    {
        _foodCollection.PeaksHit += TakeDamage;
    }

    private void TakeDamage()
    {
        DamageReceived?.Invoke();
        _health = _move.Tails.Count;
    }
}
