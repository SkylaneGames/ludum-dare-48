using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterInventory))]
public class PlayerController : CharacterController
{
    public CharacterInventory Inventory { get; private set; }

    void Awake()
    {
        Inventory = GetComponent<CharacterInventory>();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
