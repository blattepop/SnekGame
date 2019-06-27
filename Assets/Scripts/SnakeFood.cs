using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeFood : MonoBehaviour, IEatable
{
    private const float POSITION_OFFSET = 0.5f;

    private void Awake()
    {
        AssignRandomPosition();
    }

    public bool CanBeEatenBy(SnakeBodyData snake)
    {
        if (snake.Parent == null)
        {
            return true;
        }

        return false;
    }

    public bool OnEatenBy(SnakeBodyData snake)
    {
        snake.SnakeController.EatFood(this);

        AssignRandomPosition();

        return true;
    }

    private void AssignRandomPosition()
    {
        bool isPositionValid = true;
        do
        {
            isPositionValid = true;

            int halfBoard = SnakeController.BOARD_SIZE / 2;
            transform.position = new Vector3(
                Random.Range(-halfBoard, halfBoard + 1) + POSITION_OFFSET, 
                Random.Range(-halfBoard, halfBoard + 1) + POSITION_OFFSET, 
                transform.position.z);

            var colliders = Physics2D.OverlapCircleAll(transform.position, 0.25f);

            foreach (var collider in colliders)
            {
                IEatable eatable = collider.GetComponent<IEatable>();

                if (eatable != null && eatable != this)
                {
                    isPositionValid = false;
                    break;
                }
            }
        }
        while (!isPositionValid);
    }
}
