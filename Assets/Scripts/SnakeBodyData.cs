using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyData
{
    private const float MOVEMENT_SPEED = 1f;

    public SnakeController SnakeController { get; private set; }
    public Vector3 PreviousPosition { get; private set; }
    public SnakeBodyData Child { get; private set; }
    public SnakeBodyData Parent { get; private set; }
    public SnakeBody BodyObject { get; private set; }

    public bool IsAlive { get; private set; }

    public SnakeBodyData(SnakeController snakeController, SnakeBodyData parent, SnakeBody bodyObject)
    {
        IsAlive = true;
        Parent = parent;
        BodyObject = bodyObject;
        SnakeController = snakeController;
        bodyObject.SnakeBodyData = this;

        if (Parent != null)
        {
            Parent.AssignChild(this);
        }
    }

    public void AssignChild(SnakeBodyData newchild)
    {
        SnakeBodyData previousChild = Child;

        Child = newchild;

        if (previousChild != null)
        {
            newchild.BodyObject.transform.position = PreviousPosition;
            previousChild.Parent = newchild;
            newchild.Child = previousChild;
        }
    }

    public void Move(Vector3 direction)
    {
        if (IsAlive)
        {
            PreviousPosition = BodyObject.transform.position;

            if (Parent == null)
            {
                BodyObject.transform.position += direction * MOVEMENT_SPEED;
            }
            else
            {
                BodyObject.transform.position = Parent.PreviousPosition;
            }

            if (!HasEatenFood())
            {
                if (Child != null && Child != this && Child != Parent)
                {
                    Child.Move(direction);
                }
            }
        }
    }

    public void Kill()
    {
        IsAlive = false;

        if (Child != null)
        {
            Child.Kill();
        }
    }

    private bool HasEatenFood()
    {
        var colliders = Physics2D.OverlapCircleAll(BodyObject.transform.position, 0.4f);

        foreach (var collider in colliders)
        {
            IEatable eatable = collider.GetComponent<IEatable>();

            if (eatable != BodyObject && eatable != null)
            {
                if (eatable.CanBeEatenBy(this))
                {
                    return eatable.OnEatenBy(this);
                }
            }
        }
        
        return false;
    }
}
