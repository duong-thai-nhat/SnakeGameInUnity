using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    private Snake snake;

    private void Start()
    {
        snake = GameObject.FindObjectOfType<Snake>();
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnMenuGame()
    {
        SceneManager.LoadScene(0);
        snake.SetWinNotice(false);
    }

    public void PlayAgainGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }  
}
