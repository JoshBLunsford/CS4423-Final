using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        AudioListener.volume = 0.2f;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("EvilScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
