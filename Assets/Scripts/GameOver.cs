using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene("Playspace");
        Time.timeScale = 1f;
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
}
