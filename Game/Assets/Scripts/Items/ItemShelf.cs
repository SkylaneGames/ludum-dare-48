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
        return interacter.Inventory.Items.FirstOrDefault() != null;
    }

    public void Highlight()
    {
        HighlightObject.Show();
    }

    public void Interact(CharacterController interacter, Action callback = null)
    {
        var item = interacter.Inventory.TakeItem(ShelfId);

        if (item != null && item.Name == ShelfId)
        {
            if (JobSystem.Instance.CompleteJob(item))
            {
                // TODO: Success - Complete job, increase dream state.
                Debug.Log("Job Completed");
            }
            else
            {
                // TODO: Supervisor - "We already have enough of those on that shelf!"
                Debug.Log("No job exists for this item");
            }
        }
        else
        {
            Debug.Log("Incorrect Item");
            interacter.Inventory.TakeFirstItem(); // Remove the first item from the player's inventory.
            // TODO: Supervisor - "That doesn't go there!"
            //  - If category is right but the colour is wrong, "**Sigh** How many times have I told you? You need to match the colours!"
        }

        RemoveHighlight();
    }

    public void RemoveHighlight()
    {
        HighlightObject.Hide();
    }
}
