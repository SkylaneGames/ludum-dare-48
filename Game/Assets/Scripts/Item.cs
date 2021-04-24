using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public ItemCategory Category;
    public ItemSubCategory SubCategory;
    public Sprite Sprite;
}
