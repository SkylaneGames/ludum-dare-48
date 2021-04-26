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
        var player = interacter as PlayerController;

        if (player == null) return false;

        return GameManager.Instance.GameStarted;
    }

    public void Highlight()
    {
        HighlightObject.Show();
    }

    public void Interact(CharacterController interacter, Action callback = null)
    {
        var player = interacter as PlayerController;

        if (player == null) return;

        player.Inventory.Clear();
        var requiredItems = GetRequiredItems();

        var newItems = requiredItems.Except(player.Inventory.Items);

        foreach (var item in newItems)
        {
            player.Inventory.AddItem(item);
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
