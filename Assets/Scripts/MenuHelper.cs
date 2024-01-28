using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHelper : MonoBehaviour
{
    GameManager gameManager;
    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Play()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        bool shouldChangeScene = SceneManager.GetActiveScene().name != "Game";
        if (shouldChangeScene)
            SceneManager.LoadScene("Game");
        else
            gameManager.Resume();
    }
}
