using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] private GameObject _water;

    [SerializeField] public Text _grabText;
    
    [SerializeField] private Inventory _inventory;

    [SerializeField] private BasinManagement _basinManagement;

    void Start()
    {
        _water.SetActive(false);
    }

    void Update()
    {
        UpdateRaycast();
        if (Global.TextTimer < 0)
        {
            _grabText.text = "";
            Global.TextTimer = 0;
        }
        else
            Global.TextTimer -= Time.deltaTime;

        if (Global.GrabTimer > 0)
        {
            Global.GrabTimer -= Time.deltaTime;
        }
    }

    private void UpdateRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 5))
        {
            GameObject hitObj = hit.collider.gameObject;
            if (hitObj.GetComponent<CustomTag>() != null)
            {
                CustomTag hitTags = hitObj.GetComponent<CustomTag>();
                if (Input.GetMouseButtonDown(0))
                {
                    int index = _inventory.SelectedItemIndex;

                    if (hitTags.HasTag("Sink"))
                    {
                        if (_water.activeSelf)
                        {
                            _water.SetActive(false);
                            Global.WaterIsActive = false;
                        }
                        else
                        {
                            _water.SetActive(true);
                            Global.WaterIsActive = true;
                        }
                    }
                    else if (hitTags.HasTag("WashingHands") && _water.activeSelf && !_basinManagement.IsInSink && Global.GrabTimer <= 0)
                    {
                        Global.HandsAreWashed = true;
                        Global.ForAnimHandsAreWashed = true;
                        _grabText.text = hitObj.GetComponent<GrabAndConsumptionText>().GrabText;
                        Global.TextTimer = 3;
                    }
                    else if (hitTags.HasTag("Grabbable") && Global.GrabTimer <= 0 && !hitTags.HasTag("Returnable"))
                    {
                        Global.PlayerGrabbed = true;
                        hitObj.SetActive(false);

                        if (hitTags.HasTag("Bandage"))
                        {
                            Global.BandageIsGrabbed = true;
                            _inventory.Items.Add(new Item(_inventory.SpriteNotSelectedBandage, _inventory.SpriteSelectedBandage, "Bandage"));
                        }
                        else if (hitTags.HasTag("Beton"))
                        {
                            Global.BetonIsGrabbed = true;
                            _inventory.Items.Add(new Item(_inventory.SpriteNotSelectedBeton, _inventory.SpriteSelectedBeton, "Beton"));
                        }
                        else if (hitTags.HasTag("Basin"))
                        {
                            Global.BasinIsGrabbed = true;
                            _inventory.Items.Add(new Item(_inventory.SpriteNotSelectedBasin, _inventory.SpriteSelectedBasin, "Basin", hitObj));
                        }

                        _inventory.SetNewItemSprite();
                        Global.GrabTimer = 1.5f;

                        _grabText.text = hitObj.GetComponent<GrabAndConsumptionText>().GrabText;
                        Global.TextTimer = 3;
                    }
                    //TODO: Объединить условия таймера
                    else if (hitTags.HasTag("Returnable") && Global.GrabTimer <= 0 && index == -1)
                    {
                        Global.PlayerGrabbed = true;
                        hitObj.SetActive(false);

                        Global.GrabTimer = 1.5f;
                        hitTags.Replace("Returnable", "");

                        if (!_basinManagement.IsFilled)
                        {
                            _inventory.Items[Global.SlotOfReturnable].SpriteSelected = _inventory.SpriteSelectedBasin;
                            _inventory.Items[Global.SlotOfReturnable].SpriteNotSelected = _inventory.SpriteNotSelectedBasin;
                        }
                        else
                        {
                            _inventory.Items[Global.SlotOfReturnable].SpriteSelected = _inventory.SpriteSelectedBasinWater;
                            _inventory.Items[Global.SlotOfReturnable].SpriteNotSelected = _inventory.SpriteNotSelectedBasinWater;
                        }

                        _inventory.SetNewItemSprite();
                        _grabText.text = hitObj.GetComponent<GrabAndConsumptionText>().GrabText;
                        Global.TextTimer = 3;
                        _basinManagement.IsInSink = false;
                        _basinManagement.IsOnTable = false;
                    }
                    else if (!_basinManagement.IsOnTable && !_basinManagement.IsInSink && index != -1)
                    {
                        if (_inventory.Items[_inventory.SelectedItemIndex].Name == "Basin")
                            if (hitTags.HasTag("WashingHands"))
                                _basinManagement.SetBasinAt("Sink");
                            else if (hitTags.HasTag("Table"))
                                _basinManagement.SetBasinAt("Table");
                    }
                    else if (_basinManagement.IsOnTable || _basinManagement.IsInSink)
                    {
                        if (index != -1)
                        {
                            if (hitTags.HasTag("Basin") && _inventory.Items[index].Name == "Beton" && _basinManagement.IsFilled && !Global.BetonIsSoaked)
                            {
                                Global.PlayerGrabbed = true;
                                Global.BetonIsSoaked = true;
                                
                                _inventory.Items[index].SpriteSelected = _inventory.SpriteSelectedBetonSoaked;
                                _inventory.Items[index].SpriteNotSelected = _inventory.SpriteNotSelectedBetonSoaked;
                                _inventory.SetNewItemSprite();
                                
                                _grabText.text = "Вы замочили гипсовую повязку!";
                                Global.GrabTimer = 1.5f;
                                Global.TextTimer = 3;
                                _inventory.SelectedItemIndex = -1;
                            }
                        }
                    }
                }
            }
        }
    }
}
