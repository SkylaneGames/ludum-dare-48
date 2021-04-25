using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
    public event Action<Item> ItemAdded;
    public event Action<Item> ItemRemoved;

    private int _maxSize;

    public List<Item> Items;

    public int Count => Items.Count(p => p != null);

    // Start is called before the first frame update
    void Start()
    {
        _maxSize = Items.Count;
    }

    public bool AddItem(Item item)
    {
        if (Count < _maxSize)
        {
            Items.Add(item);
            ItemAdded?.Invoke(item);
            return true;
        }

        return false;
    }

    public void RemoveItems(IEnumerable<Item> items)
    {
        Items.RemoveAll(p => items.Contains(p));
        foreach (var item in items)
        {
            RemoveItem(item);
        }
    }

    public void Clear()
    {
        foreach (var item in Items)
        {
            ItemRemoved?.Invoke(item);
        }

        Items.Clear();
    }

    public void RemoveItem(Item item)
    {
        if (item != null && Items.Contains(item))
        {
            Items.Remove(item);
            ItemRemoved?.Invoke(item);
        }
    }

    public Item TakeItem(string itemName)
    {
        var item = Items.FirstOrDefault(p => p.Name == itemName);
        RemoveItem(item);
        return item;
    }

    public Item TakeFirstItem()
    {
        var item = Items.FirstOrDefault();
        RemoveItem(item);
        return item;
    }
}
