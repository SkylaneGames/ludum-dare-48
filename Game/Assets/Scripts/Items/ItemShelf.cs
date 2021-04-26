using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interaction;
using MissionSystem.JobSystem;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemShelf : MonoBehaviour, IInteractable
{
    public string ShelfId => $"{Category}-{SubCategory}";

    public ItemCategory Category;
    public ItemSubCategory SubCategory;

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

        return player.Inventory.Items.FirstOrDefault() != null;
    }

    public void Highlight()
    {
        HighlightObject.Show();
    }

    public void Interact(CharacterController interacter, Action callback = null)
    {
        var player = interacter as PlayerController;
        var item = player?.Inventory.TakeItem(ShelfId);

        if (item != null && item.Name == ShelfId)
        {
            if (JobSystem.Instance.CompleteJob(item))
            {
                Debug.Log("Complete");
                AngryBoss.Instance.OnJobSuccess();
            }
            else
            {
                Debug.Log("Shelf full");
                AngryBoss.Instance.OnShelfFull();
            }
        }
        else
        {
                Debug.Log("Wrong");
            player?.Inventory.TakeFirstItem(); // Remove the first item from the player's inventory.
            AngryBoss.Instance.OnWrongShelf(Category == item?.Category);
        }

        RemoveHighlight();
    }

    public void RemoveHighlight()
    {
        HighlightObject.Hide();
    }
}
