using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private PickupText _pickupText;
    private TraceManager _traceManager;
    private void Awake()
    {
        _pickupText = FindObjectOfType<PickupText>();
        _traceManager = FindObjectOfType<TraceManager>();
    }

    public void Pickup()
    {
        GameManager.instance.Wort++;
        _pickupText.ItemText("St. John's Wort");
        _traceManager.PickedUpItem("St. John's Wort");
        gameObject.SetActive(false);
    }
}
