using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandWashing : MonoBehaviour
{
    [SerializeField] private GameObject _water;

    void Start()
    {
        _water.SetActive(false);
    }

    void Update()
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
                }
            }
        }
    }
}
