using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<object[]> Items;

    [SerializeField] private List<Image> _inventoryIconSlots;

    [SerializeField] public Sprite SpriteNotSelectedBasin;
    [SerializeField] public Sprite SpriteNotSelectedBandage;
    [SerializeField] public Sprite SpriteNotSelectedBeton;

    [SerializeField] public Sprite SpriteSelectedBasin;
    [SerializeField] public Sprite SpriteSelectedBandage;
    [SerializeField] public Sprite SpriteSelectedBeton;

    public int _selectedItemIndex = -1;

    void Start()
    {
        Items = new List<object[]>();
    }

    void Update()
    {
        SelectItem();
    }

    public void UpdateIconSlots()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            _inventoryIconSlots[i].sprite = Items[i][0] as Sprite;
        }
    }

    public void SelectItem()
    {
        if (Items.Count != 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if ((string)Items[0][3] == "Selected")
                {
                    _inventoryIconSlots[0].sprite = Items[0][0] as Sprite;
                    Items[0][3] = "NotSelected";
                }
                else
                {
                    _inventoryIconSlots[0].sprite = Items[0][1] as Sprite;
                    Items[0][3] = "Selected";
                    _selectedItemIndex = 0;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if ((string)Items[1][3] == "Selected")
                {
                    _inventoryIconSlots[1].sprite = Items[1][0] as Sprite;
                    Items[1][3] = "NotSelected";
                }
                else
                {
                    _inventoryIconSlots[1].sprite = Items[1][1] as Sprite;
                    Items[1][3] = "Selected";
                    _selectedItemIndex = 1;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if ((string)Items[2][3] == "Selected")
                {
                    _inventoryIconSlots[2].sprite = Items[2][0] as Sprite;
                    Items[2][3] = "NotSelected";
                }
                else
                {
                    _inventoryIconSlots[2].sprite = Items[2][1] as Sprite;
                    Items[2][3] = "Selected";
                    _selectedItemIndex = 2;
                }
            }
        }
        if (_selectedItemIndex != -1)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (i != _selectedItemIndex)
                {
                    _inventoryIconSlots[i].sprite = Items[i][0] as Sprite;
                    Items[i][3] = "NotSelected";
                }
            }
        }
    }
}
