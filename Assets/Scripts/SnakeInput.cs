using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SnakeInput : MonoBehaviour
{
    [Serializable]
    public class InputDirection
    {
        [SerializeField] private KeyCode _Keycode;
        [SerializeField] private Vector2 _Direction;

        public KeyCode KeyCode { get { return _Keycode; } }
        public Vector2 Direction { get { return _Direction; } }
    }
    
    [SerializeField] private InputDirection[] _PossibleInputs;

    public delegate void DirectionUpdateEvent(Vector2 direction);
    public event DirectionUpdateEvent OnDirectionUpdate;
    
    void Update()
    {
        foreach (var direction in _PossibleInputs)
        {
            if (Input.GetKeyDown(direction.KeyCode))
            {
                OnDirectionUpdate(direction.Direction);
            }
        }
    }
}
