using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCounter : MonoBehaviour
{
    [SerializeField] private Text _healthText;
    [SerializeField] private Snake _snake;

    private void ChangeHealthText()
    {
        _healthText.text = _snake.Health.ToString();
    }

    private void OnEnable()
    {
        _snake.HealthChanged += ChangeHealthText;
    }

    private void OnDisable()
    {
        _snake.HealthChanged -= ChangeHealthText;
    }
}
