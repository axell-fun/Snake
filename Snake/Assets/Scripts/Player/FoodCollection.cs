using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FoodCollection : MonoBehaviour
{
    public event UnityAction FoodCollected;
    public event UnityAction CoinTaken;
    public event UnityAction PeaksHit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Eat eat))
        {
            Destroy(other.gameObject);
            FoodCollected?.Invoke();
        }

        if (other.TryGetComponent(out Coin coin))
        {
            Destroy(other.gameObject);
            CoinTaken?.Invoke();
        }

        if (other.TryGetComponent(out Peaks peaks))
        {
            PeaksHit?.Invoke();
        }
    }
}
