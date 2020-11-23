using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] private GameObject _water;
    [SerializeField] private Text _grabText;

    private float _textTimer;

    void Start()
    {
        _water.SetActive(false);
    }

    void Update()
    {
        UpdateRaycast();
        if (_textTimer < 0)
        {
            _grabText.text = "";
            _textTimer = 0;
        }
        else
            _textTimer -= Time.deltaTime;
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
                        _water.SetActive(false);
                    else
                        _water.SetActive(true);
                }
                else if (hitTags.HasTag("WashingHands") && Input.GetMouseButtonDown(0) && _water.activeSelf)
                {
                    Global.HandsAreWashed = true;
                    _grabText.text = hitObj.GetComponent<GrabText>().Text;
                    _textTimer = 3;
                }
                else if (hitTags.HasTag("Grabbable") && Input.GetMouseButtonDown(0))
                {
                    Global.PlayerGrabbed = true;
                    hitObj.SetActive(false);

                    if (hitTags.HasTag("Bandage"))
                        Global.BandageIsGrabbed = true;
                    else if (hitTags.HasTag("Beton"))
                        Global.BetonIsGrabbed = true;
                    else if (hitTags.HasTag("Basin"))
                        Global.BasinIsGrabbed = true;

                    _grabText.text = hitObj.GetComponent<GrabText>().Text;
                    _textTimer = 3;
                }
            }
        }
    }
}
