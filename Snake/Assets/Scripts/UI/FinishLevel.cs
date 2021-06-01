using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private FoodCollection _foodCollection;
    [SerializeField] private GameObject _screen;

    private void LevelFinished()
    {
        _screen.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnEnable()
    {
        _foodCollection.Finished += LevelFinished;
    }

    private void OnDisable()
    {
        _foodCollection.Finished -= LevelFinished;
    }
}
