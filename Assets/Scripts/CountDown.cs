using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 60f;

    public Text countdownText;

    private Snake snake;

    private void Start()
    {
        currentTime = startingTime;
        snake = GameObject.FindObjectOfType<Snake>();
    }

    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;

        if(snake.state == Snake.State.Alive)
        {
            Debug.Log("Lose");
            countdownText.text = currentTime.ToString("0");
        }    

        if( currentTime < 0 )
        {
            currentTime = 0;
            snake.state = Snake.State.Lose;
        }
    }

    public void ResetCountDown()
    {
        currentTime = startingTime;
    }
}
