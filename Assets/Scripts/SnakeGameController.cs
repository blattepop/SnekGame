using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SnakeGameController : MonoBehaviour
{
    private const float INITIAL_GAME_DELAY = 1f;
    private const float GAME_OVER_TIME = 2f;

    [SerializeField] private SnakeController[] _Snakes;
    [SerializeField] private Text _StatusText;

    private void Awake()
    {
        Time.timeScale = 0f;

        _StatusText.text = "Ready ?..";

        StartCoroutine(StartGame());
    }

    public IEnumerator StartGame()
    {
        yield return new WaitForSecondsRealtime(INITIAL_GAME_DELAY);

        Time.timeScale = 1f;
        _StatusText.text = "";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void FixedUpdate()
    {
        foreach (var snake in _Snakes)
        {
            snake.UpdateSnakeMovement();
        }
    }

    private void LateUpdate()
    {
        if (Time.timeScale > 0)
        {
            int aliveSnakeCount = 0;
            SnakeController lastSnakeAlive = null;

            foreach (var snake in _Snakes)
            {
                if (snake.IsAlive)
                {
                    ++aliveSnakeCount;
                    lastSnakeAlive = snake;
                }
            }

            if (aliveSnakeCount == 1 && _Snakes.Length > 1)
            {
                // Display winner
                _StatusText.text = lastSnakeAlive.PlayerName + " has won !";
                StartCoroutine(RestartNewGame());
            }
            else if (aliveSnakeCount == 0)
            {
                // Declare draw
                if (_Snakes.Length > 1)
                    _StatusText.text = "OMG, It's a DRAW ! :o";
                else
                    _StatusText.text = "You lost :'(";

                StartCoroutine(RestartNewGame());
            }
        }
    }

    private IEnumerator RestartNewGame()
    {
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(GAME_OVER_TIME);

        SceneManager.LoadScene(0);
    }
}
