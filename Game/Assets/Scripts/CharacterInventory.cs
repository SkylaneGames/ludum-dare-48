using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterInventory : MonoBehaviour
{
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
            return true;
        }

        return false;
    }

    public bool TakeItem(Item item)
    {
        if (Items.Contains(item))
        {
            Items.Remove(item);
            return true;
        }

        return false;
    }

    public Item TakeFirstItem()
    {
        var item = Items.FirstOrDefault();

        if (item != null)
        {
            Items.Remove(item);
            return item;
        }

        return null;
    }
}
