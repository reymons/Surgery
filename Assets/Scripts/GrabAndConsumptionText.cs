using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndConsumptionText : MonoBehaviour
{
    [SerializeField]
    private string _grabText;

    [SerializeField]
    private string _consumptionText;


    public string GrabText => _grabText;
    public string ConsumptionText => _consumptionText;
}
