using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurgeryCharacterManagement : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;

    [SerializeField] private GameObject _legBandaged;
    [SerializeField] private GameObject _legBetoned;

    [SerializeField] private Text _consumptionText;

    [SerializeField] private PlayerRaycast _playerRaycast;

    void Start()
    {
        _legBandaged.SetActive(false);
        _legBetoned.SetActive(false);
    }

    private void OnMouseDown()
    { 
        int index = _inventory.SelectedItemIndex;
        if (index != -1)
        {
            string itemName = _inventory.Items[index].Name;
            switch (itemName)
            {
                case "Bandage":
                    if (Global.HandsAreWashed)
                    {
                        _legBandaged.SetActive(true);
                        Global.BandageIsApplied = true;
                        _inventory.RemoveItemFromSlot(index);
                        _consumptionText.text = "Вы забинтовали голень!";
                        _playerRaycast.TextTimer = 3;
                    }
                    break;
                    
                case "Beton":
                    if (Global.BetonIsSoaked && Global.BandageIsApplied)
                    {
                        _legBetoned.SetActive(true);
                        Global.BetonIsApplied = true;
                        _inventory.RemoveItemFromSlot(index);
                    }
                    break;
            }
            
        }
    }
}
