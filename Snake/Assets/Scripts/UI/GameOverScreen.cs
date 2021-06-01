using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject _screen;
    [SerializeField] private Snake _snake;

    private void GameOver()
    {
        _screen.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnEnable()
    {
        _snake.Died += GameOver;
    }

    private void OnDisable()
    {
        _snake.Died -= GameOver;
    }
}
