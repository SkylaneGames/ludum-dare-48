using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Image Image;
    public Image Background;

    public Item Item { get; private set; }

    public void Init(Item item)
    {
        Image.sprite = item.Sprite;
        SetSubCategory(item.SubCategory);

        Item = item;
    }

    public void SetSubCategory(ItemSubCategory category)
    {
        switch (category)
        {
            case ItemSubCategory.Red:
                Background.color = new Color(80 / 100f, 36 / 100f, 16 / 100f);
                break;
            case ItemSubCategory.Orange:
                Background.color = new Color(90 / 100f, 67 / 100f, 29 / 100f);
                break;
            case ItemSubCategory.Green:
                Background.color = new Color(45 / 100f, 77 / 100f, 58 / 100f);
                break;
            case ItemSubCategory.Blue:
                Background.color = new Color(21 / 100f, 61 / 100f, 74 / 100f);
                break;
            case ItemSubCategory.Purple:
                Background.color = new Color(69 / 100f, 45 / 100f, 69 / 100f);
                break;
        }
    }
}
