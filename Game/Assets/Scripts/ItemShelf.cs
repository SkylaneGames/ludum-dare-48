using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interaction;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemShelf : MonoBehaviour, IInteractable
{
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
        var item = interacter.Inventory.TakeFirstItem();
        if (item != null)
        {
            if (item.Category == Category && item.SubCategory == SubCategory)
            {
                // TODO: Success - Complete job.
            }
            else
            {
                // TODO: Fail - Incorrect item for this shelf.
            }
        }
    }

    public void RemoveHighlight()
    {
        HighlightObject.Hide();
    }
}
