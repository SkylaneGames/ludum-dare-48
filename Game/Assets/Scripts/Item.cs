using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string Name => $"{Category}-{SubCategory}";
    public ItemCategory Category;
    public ItemSubCategory SubCategory;
    public Sprite Sprite;
}

public static class ItemExtensions
{
    public static Item CreateInstance(this Item item)
    {
        var clone = ScriptableObject.CreateInstance<Item>();

        clone.Category = item.Category;
        clone.SubCategory = item.SubCategory;
        clone.Sprite = item.Sprite;

        return clone;
    }
}
