using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public void Load(string nameScene)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nameScene);
        Time.timeScale = 1f;
    }
}
