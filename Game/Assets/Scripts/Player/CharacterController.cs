using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CharacterInventory))]
public class CharacterController : MonoBehaviour
{
    public CharacterInventory Inventory { get; private set; }

    void Awake()
    {
        Inventory = GetComponent<CharacterInventory>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
