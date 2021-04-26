using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterInventory))]
[RequireComponent(typeof(UnityEngine.InputSystem.PlayerInput))]
public class PlayerController : CharacterController
{
    public CharacterInventory Inventory { get; private set; }
    private UnityEngine.InputSystem.PlayerInput _input;
    void Awake()
    {
        Inventory = GetComponent<CharacterInventory>();
        _input = GetComponent<UnityEngine.InputSystem.PlayerInput>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void DisableInput()
    {
        _input.enabled = false;
    }
}
