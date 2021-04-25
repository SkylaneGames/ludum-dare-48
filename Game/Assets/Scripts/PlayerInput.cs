using System.Collections;
using System.Collections.Generic;
using Interaction;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterMotor))]
public class PlayerInput : MonoBehaviour
{
    protected CharacterMotor _motor;
    protected InteractionSystem _interaction;

    void Awake()
    {
        _motor = GetComponent<CharacterMotor>();
        _interaction = GetComponentInChildren<InteractionSystem>();
    }

    public void OnMove(InputValue value)
    {
        var input = value.Get<Vector2>();

        _motor.UpdatedMovement(input);
    }

    public void OnInteract()
    {
        _interaction.Interact();
    }
}
