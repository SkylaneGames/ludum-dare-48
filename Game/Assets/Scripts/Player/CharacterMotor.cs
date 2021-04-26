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

    private Animator _animator;
    private SpriteRenderer[] _spriteRenderers;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        UpdateState();
        _rigidbody.velocity = input * Speed * directionMultiplier;

        var updatedZPos = transform.position;
        updatedZPos.z = updatedZPos.y;
        transform.position = updatedZPos;
    }

    private void UpdateState()
    {
        bool down = false;
        bool up = false;
        bool side = false;
        bool idle = false;

        if (_rigidbody.velocity.y > IdleThreshold)
        {
            State = MovementState.Up;
            up = true;
        }
        else if (_rigidbody.velocity.y < -IdleThreshold)
        {
            State = MovementState.Down;
            down = true;
        }
        else if (_rigidbody.velocity.x > IdleThreshold)
        {
            State = MovementState.Right;
            side = true;
            foreach (var sprite in _spriteRenderers)
            {
                sprite.flipX = true;
            }
        }
        else if (_rigidbody.velocity.x < -IdleThreshold)
        {
            State = MovementState.Left;
            side = true;
            foreach (var sprite in _spriteRenderers)
            {
                sprite.flipX = false;
            }
        }
        else
        {
            State = MovementState.Idle;
            idle = true;
        }

        _animator.SetBool("Walking.Down", down);
        _animator.SetBool("Walking.Up", up);
        _animator.SetBool("Walking.Side", side);
        _animator.SetBool("Idle", idle);
        _animator.speed = Speed / 3f;
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