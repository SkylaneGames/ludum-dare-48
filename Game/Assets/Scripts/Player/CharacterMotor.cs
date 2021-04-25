using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class CharacterMotor : MonoBehaviour
{
    public float Speed = 5f;

    // To decrease upward movement to due to the implied isometric view. 
    public Vector2 directionMultiplier = new Vector2(1f, 0.8f);

    public MovementState State { get; private set; }

    private float IdleThreshold = Mathf.Epsilon;

    protected Rigidbody2D _rigidbody;

    private Vector2 input = Vector2.zero;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = input * Speed * directionMultiplier;
        UpdateState();
    }

    private void UpdateState()
    {
        if (_rigidbody.velocity.y > IdleThreshold)
        {
            State = MovementState.Up;
        }
        else if (_rigidbody.velocity.y < -IdleThreshold)
        {
            State = MovementState.Down;
        }
        else if (_rigidbody.velocity.x > IdleThreshold)
        {
            State = MovementState.Right;
        }
        else if (_rigidbody.velocity.x < -IdleThreshold)
        {
            State = MovementState.Left;
        }
        else
        {
            State = MovementState.Idle;
        }
    }

    public void UpdatedMovement(Vector2 input)
    {
        this.input = ConstrainNSWE(input);
    }

    private Vector2 ConstrainNSWE(Vector2 input)
    {
        if (Mathf.Abs(input.x) == Mathf.Abs(input.y))
        {
            // If both inputs are the same, i.e. diagonal input is received, favour the new direction over the old one.
            if (this.input.x != 0)
            {
                input.x *= 0;
            }
            else
            {
                input.y *= 0;
            }
        }
        else
        {
            if (Mathf.Abs(input.x) >= Mathf.Abs(input.y))
            {
                input.y *= 0;
            }
            else
            {
                input.x *= 0;
            }
        }


        return input;
    }
}

public enum MovementState
{
    Idle, Up, Down, Left, Right
}