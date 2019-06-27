using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEatable
{
    bool CanBeEatenBy(SnakeBodyData snake);

    // Returns true if the snake successfully ate the thing and should grow, false if it failed and should die.
    bool OnEatenBy(SnakeBodyData snake);
}
