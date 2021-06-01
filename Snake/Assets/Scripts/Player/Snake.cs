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
    private int _coin;

    public int Health => _health;
    public int Coin => _coin;

    public event UnityAction DamageReceived;
    public event UnityAction HealthChanged;
    public event UnityAction CoinChanged;
    public event UnityAction Died;

    private void Awake()
    {
        _move = GetComponent<Movement>();
        _foodCollection = GetComponent<FoodCollection>();

        _health = _move.Tails.Count;
        HealthChanged?.Invoke();
    }

    private void OnEnable()
    {
        _foodCollection.PeaksHit += TakeDamage;
        _foodCollection.FoodCollected += ChangeHealth;
        _foodCollection.CoinTaken += ChangeCoins;
        _foodCollection.BadEatTaken += Die;
        _foodCollection.FeverActivated += ResetCoins;
    }

    private void OnDisable()
    {
        _foodCollection.PeaksHit -= TakeDamage;
        _foodCollection.FoodCollected -= ChangeHealth;
        _foodCollection.CoinTaken -= ChangeCoins;
        _foodCollection.BadEatTaken -= Die;
        _foodCollection.FeverActivated -= ResetCoins;
    }

    private void ResetCoins()
    {
        _coin = 0;
        CoinChanged?.Invoke();
    }

    private void ChangeCoins()
    {
        _coin++;
        CoinChanged?.Invoke();
    }

    private void ChangeHealth()
    {
        _health = _move.Tails.Count;
        HealthChanged?.Invoke();

        if (_health <= 0)
            Died?.Invoke();
    }

    private void Die()
    {
        Died?.Invoke();
    }

    private void TakeDamage()
    {
        DamageReceived?.Invoke();
        ChangeHealth();
    }
}
