using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IEatable
{
    public bool CanBeEatenBy(SnakeBodyData snakeBody)
    {
        return true;
    }

    public bool OnEatenBy(SnakeBodyData snake)
    {
        snake.SnakeController.Kill();

        return false;
    }
}
