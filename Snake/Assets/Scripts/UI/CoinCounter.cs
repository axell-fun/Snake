using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    [SerializeField] private Text _coinText;
    [SerializeField] private Snake _snake;

    private void ChangeCoinText()
    {
        _coinText.text = _snake.Coin.ToString();
    }

    private void OnEnable()
    {
        _snake.CoinChanged += ChangeCoinText;
    }

    private void OnDisable()
    {
        _snake.CoinChanged -= ChangeCoinText;
    }
}
