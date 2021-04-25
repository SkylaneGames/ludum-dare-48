using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interaction;
using MissionSystem.JobSystem;
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
        interacter.Inventory.Clear();
        var requiredItems = GetRequiredItems();

        var newItems = requiredItems.Except(interacter.Inventory.Items);

        foreach (var item in newItems)
        {
            interacter.Inventory.AddItem(item);
        }
    }

    private IEnumerable<Item> GetRequiredItems()
    {
        var items = JobSystem.Instance.GetRequiredItems();

        return items;
    }

    public void RemoveHighlight()
    {
        HighlightObject.Hide();
    }
}
