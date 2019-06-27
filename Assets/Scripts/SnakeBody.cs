using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody : MonoBehaviour, IEatable
{
    public SnakeBodyData SnakeBodyData { get; set; }

    public bool CanBeEatenBy(SnakeBodyData snake)
    {
        return true;
    }

    public bool OnEatenBy(SnakeBodyData otherSnake)
    {
        bool otherIsHead = otherSnake.Parent == null;
        if (otherIsHead)
        {
            SnakeBodyData.SnakeController.Kill();

            // If we came head to head, kill both snakes
            if (SnakeBodyData.Parent == null)
            {
                otherSnake.SnakeController.Kill();
            }
        }
        else
        {
            otherSnake.SnakeController.Kill();
        }

        return true;
    }
}
