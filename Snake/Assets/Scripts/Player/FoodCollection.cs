using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FoodCollection : MonoBehaviour
{
    [SerializeField] private Movement _move;

    private int _coinsForFever;

    public event UnityAction FoodCollected;
    public event UnityAction CoinTaken;
    public event UnityAction PeaksHit;
    public event UnityAction BadEatTaken;
    public event UnityAction Finished;
    public event UnityAction FeverActivated;

    private void OnTriggerEnter(Collider other)
    {
        if (_move.IsFever == false)
        {
            if (other.TryGetComponent(out Eat eat))
            {
                Destroy(other.gameObject);
                FoodCollected?.Invoke();
            }

            if (other.TryGetComponent(out Peaks peaks))
            {
                PeaksHit?.Invoke();
            }

            if (other.TryGetComponent(out BadEat badEat))
            {
                BadEatTaken?.Invoke();
            }

            if (other.TryGetComponent(out Finish finish))
            {
                Finished?.Invoke();
            }

            if (other.TryGetComponent(out Coin coin))
            {
                Destroy(other.gameObject);
                CoinTaken?.Invoke();
                _coinsForFever++;

                if (_coinsForFever == 3)
                {
                    FeverActivated?.Invoke();
                    _coinsForFever = 0;
                }
            }
            else
            {
                _coinsForFever = 0;
            }
        }
        else
        {
            if (other.TryGetComponent(out Eat eat))
            {
                Destroy(other.gameObject);
                FoodCollected?.Invoke();
            }

            if (other.tag == "Destroy")
            {
                Destroy(other.gameObject);
            }
        }

    }
}
