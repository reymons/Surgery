using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [NonSerialized] public List<Item> Items = new List<Item>();

    [SerializeField] private List<Image> _inventoryIconSlots;

    [SerializeField] public Sprite SpriteNotSelectedBasin;
    [SerializeField] public Sprite SpriteNotSelectedBandage;
    [SerializeField] public Sprite SpriteNotSelectedBeton;
    [SerializeField] public Sprite SpriteNotSelectedBetonSoaked;
    [SerializeField] public Sprite SpriteNotSelectedBasinWater;

    [SerializeField] public Sprite SpriteSelectedBasin;
    [SerializeField] public Sprite SpriteSelectedBandage;
    [SerializeField] public Sprite SpriteSelectedBeton;
    [SerializeField] public Sprite SpriteSelectedBetonSoaked;
    [SerializeField] public Sprite SpriteSelectedBasinWater;



    [SerializeField] public Sprite SpriteEmpty;

    [NonSerialized] public int SelectedItemIndex = -1;

    void Update()
    {
        SelectItem();
    }

    public void SetNewItemSprite()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            _inventoryIconSlots[i].sprite = Items[i].SpriteNotSelected;
        }
    }

    public void SelectItem()
    {
        if (Items.Count != 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && !Items[0].IsBlocked)
            {
                if (Items[0].IsSelected)
                {
                    _inventoryIconSlots[0].sprite = Items[0].SpriteNotSelected;
                    Items[0].IsSelected = false;
                    SelectedItemIndex = -1;
                }
                else
                {
                    _inventoryIconSlots[0].sprite = Items[0].SpriteSelected;
                    Items[0].IsSelected = true;
                    SelectedItemIndex = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && Items.Count >= 2)
            {
                if (!Items[1].IsBlocked)
                {
                    if (Items[1].IsSelected)
                    {
                        _inventoryIconSlots[1].sprite = Items[1].SpriteNotSelected;
                        Items[1].IsSelected = false;
                        SelectedItemIndex = -1;
                    }
                    else
                    {
                        _inventoryIconSlots[1].sprite = Items[1].SpriteSelected;
                        Items[1].IsSelected = true;
                        SelectedItemIndex = 1;
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && Items.Count == 3)
            {
                if (!Items[2].IsBlocked)
                {
                    if (Items[2].IsSelected)
                    {
                        _inventoryIconSlots[2].sprite = Items[2].SpriteNotSelected;
                        Items[2].IsSelected = false;
                        SelectedItemIndex = -1;
                    }
                    else
                    {
                        _inventoryIconSlots[2].sprite = Items[2].SpriteSelected;
                        Items[2].IsSelected = true;
                        SelectedItemIndex = 2;
                    }
                }
            }
        }
        if (SelectedItemIndex != -1)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (i != SelectedItemIndex && !Items[i].IsBlocked)
                {
                    _inventoryIconSlots[i].sprite = Items[i].SpriteNotSelected;
                    Items[i].IsSelected = false;
                }
            }
        }
    }

    public void RemoveItemFromSlot(int slot)
    {
        _inventoryIconSlots[slot].sprite = SpriteEmpty;
        Items[slot].IsBlocked = true;
    }
}
