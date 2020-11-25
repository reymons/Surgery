using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BasinManagement : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    [SerializeField] private Text _mainInfo;

    [SerializeField] private GameObject _water;

    [NonSerialized] public bool IsInSink;

    [NonSerialized] public bool IsOnTable;

    [NonSerialized] public bool IsFilled;

    [SerializeField] private GameObject _sink;

    [SerializeField] private GameObject _table;

    void Start()
    {
        _water.SetActive(false);
    }

    void Update()
    {
        CheckForBasinFilled();
    }

    public void SetBasinAt(string placeName)
    {
        if (_inventory.Items.Count != 0)
        {
            int index = _inventory.SelectedItemIndex;
            if (index != -1)
            {
                if (_inventory.Items[index].Name == "Basin")
                {
                    // Turning on the grab animation
                    Global.PlayerGrabbed = true;

                    if (placeName == "Sink")
                        IsInSink = true;
                    else if (IsFilled)
                        IsOnTable = true;

                    // Setting a new position for the basin and making it visible
                    ChangeBasinPosition(placeName);

                    transform.gameObject.SetActive(true);
                    transform.gameObject.GetComponent<CustomTag>().Replace("", "Returnable");
                    
                    _inventory.Items[index].SpriteNotSelected = _inventory.SpriteEmpty;
                    _inventory.Items[index].SpriteSelected = _inventory.SpriteEmpty;

                    _inventory.SetNewItemSprite();
                    _inventory.SelectedItemIndex = -1;

                    Global.GrabTimer = 1.5f;
                    Global.SlotOfReturnable = index;
                }
            }
        }
    }

    private void ChangeBasinPosition(string placeName)
    {
        if (placeName == "Sink")
        {
            Vector3 sinkPos = _sink.transform.position;
            transform.position = new Vector3(sinkPos.x + 1.57f, sinkPos.y + 5.25f, sinkPos.z);
        }
        else
        {
            Vector3 tablePos = _table.transform.position;
            transform.position = new Vector3(10.423f, 3.913f, 5.124f);
        }
    }

    private void CheckForBasinFilled()
    {
        if (IsInSink)
        {
            if (Global.WaterIsActive && !IsFilled)
            {
                _water.SetActive(true);
                IsFilled = true;
                _mainInfo.text = "Вы налили воду в тазик!";
                Global.TextTimer = 3;
            }
        }
    }
}
