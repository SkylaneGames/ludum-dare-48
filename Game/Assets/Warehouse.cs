using System;
using System.Collections;
using System.Collections.Generic;
using Interaction;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Warehouse : MonoBehaviour, IInteractable
{
    public string Name => this.name;

    public Transform Transform => this.transform;

    public InteractionHighlight HighlightObject { get; private set; }

    void Awake()
    {
        HighlightObject = GetComponentInChildren<InteractionHighlight>();
    }

    public bool CanInteract(CharacterController interacter)
    {
        return true;
    }

    public void Highlight()
    {
        HighlightObject.Show();
    }

    public void Interact(CharacterController interacter, Action callback = null)
    {
        var requiredItems = GetRequiredItems();

        foreach (var item in requiredItems)
        {
            if (!interacter.Inventory.Items.Contains(item))
            {
                interacter.Inventory.AddItem(item);
            }
        }
    }

    private IEnumerable<Item> GetRequiredItems()
    {
        throw new NotImplementedException();
    }

    public void RemoveHighlight()
    {
        HighlightObject.Hide();
    }
}
