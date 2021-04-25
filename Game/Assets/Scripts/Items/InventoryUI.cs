using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject ItemUIPrefab;
    public Transform ItemPanel;

    private CharacterInventory _inventory;

    private Collection<ItemUI> uiElements;

    void Awake()
    {
        _inventory = FindObjectOfType<CharacterInventory>();
        _inventory.ItemAdded += OnItemAdded;
        _inventory.ItemRemoved += OnItemRemoved;
    }

    void Start()
    {
        uiElements = new Collection<ItemUI>();
    }

    private void OnItemAdded(Item item)
    {
        uiElements.Add(CreateItemUi(item));
    }

    private void OnItemRemoved(Item item)
    {
        var uiElementToRemove = uiElements.FirstOrDefault(p => p.Item.Name == item.Name);

        if (uiElementToRemove != null)
        {
            uiElements.Remove(uiElementToRemove);
            Destroy(uiElementToRemove.gameObject);
        }
    }

    private ItemUI CreateItemUi(Item item)
    {
        var uiElement = Instantiate(ItemUIPrefab, ItemPanel);

        var itemUi = uiElement.GetComponent<ItemUI>();
        itemUi.Init(item);

        return itemUi;
    }
}
