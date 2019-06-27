using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public const int BOARD_SIZE = 16;

    private static readonly Vector2 INITIAL_DIRECTION = Vector2.down;
    private const float DELAY_BETWEEN_MOVEMENTS = 0.1f;

    [SerializeField] private string _PlayerName;
    [SerializeField] private SnakeInput _SnakeInput;
    [SerializeField] private SnakeBodyData _Head;
    [SerializeField] private GameObject _SnakeBodyPrefab;

    public bool IsAlive { get { return _Head.IsAlive; } }
    public string PlayerName { get { return _PlayerName; } }

    private Vector2 _CurrentDirection = INITIAL_DIRECTION;

    private void Awake()
    {
        _SnakeInput.OnDirectionUpdate += SnakeInput_OnDirectionUpdate;
        Initialize();
    }

    public void UpdateSnakeMovement()
    {
        _CurrentWaitTime -= Time.fixedDeltaTime;
        if (_CurrentWaitTime <= 0)
        {
            _CurrentWaitTime += DELAY_BETWEEN_MOVEMENTS;
            _Head.Move(_CurrentDirection);
        }
    }

    private void LateUpdate()
    {
        // Done in late update to handle 2 players eating each other's head off. (Happens after all Updates are done)
        if (!IsAlive)
        {
            gameObject.SetActive(false);
            // Send event for player death
        }
    }

    public void Initialize()
    {
        SnakeBodyData previousSnakeBody = null;
        for (int i = 0; i < 3; ++i)
        {
            GameObject bodyObject = Instantiate(_SnakeBodyPrefab, transform);
            bodyObject.transform.localPosition += i * Vector3.up;
            SnakeBodyData snakeBody = new SnakeBodyData(this, previousSnakeBody, bodyObject.GetComponent<SnakeBody>());
            previousSnakeBody = snakeBody;

            if (_Head == null)
            {
                _Head = snakeBody;
            }
        }
    }

    public void Kill()
    {
        _Head.Kill();
    }

    public void EatFood(IEatable food)
    {
        GameObject bodyObject = Instantiate(_SnakeBodyPrefab, transform);
        new SnakeBodyData(this, _Head, bodyObject.GetComponent<SnakeBody>());
    }

    private void SnakeInput_OnDirectionUpdate(Vector2 direction)
    {
        if (_CurrentDirection != -direction) // Do not allow going backward
        {
            _CurrentDirection = direction;
        }
    }

    float _CurrentWaitTime = DELAY_BETWEEN_MOVEMENTS;
}
