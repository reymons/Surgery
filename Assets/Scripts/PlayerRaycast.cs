using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] private GameObject _water;
    [SerializeField] public Text _grabText;
    
    [SerializeField] private Inventory _inventory;


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
        if (Physics.Raycast(ray, out RaycastHit hit, 3))
        {
            GameObject hitObj = hit.collider.gameObject;
            if (hitObj.GetComponent<CustomTag>() != null)
            {
                CustomTag hitTags = hitObj.GetComponent<CustomTag>();
                if (hitTags.HasTag("Sink") && Input.GetMouseButtonDown(0))
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
                else if (hitTags.HasTag("WashingHands") && Input.GetMouseButtonDown(0) 
                    && _water.activeSelf && !Global.BasinIsInSink && Global.GrabTimer <= 0)
                {
                    Global.HandsAreWashed = true;
                    Global.ForAnimHandsAreWashed = true;
                    _grabText.text = hitObj.GetComponent<GrabAndConsumptionText>().GrabText;
                    Global.TextTimer = 3;
                }
                else if (hitTags.HasTag("Grabbable") && Input.GetMouseButtonDown(0) && Global.GrabTimer <= 0 && !hitTags.HasTag("Returnable"))
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
                //TODO: Объединить условия таймера и кнопки мыши
                else if (Input.GetMouseButtonDown(0) && hitTags.HasTag("Returnable") && Global.GrabTimer <= 0)
                {
                    Global.PlayerGrabbed = true;
                    hitObj.SetActive(false);
                    
                    Global.GrabTimer = 1.5f;
                    hitTags.Replace("Returnable", "");

                    if (!Global.BasinIsFilled)
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
                    Global.BasinIsInSink = false;
                }
            }
        }
    }
}
