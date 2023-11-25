using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private PickupText _pickupText;
    private void Awake()
    {
        _pickupText = FindObjectOfType<PickupText>();
    }

    public void Pickup()
    {
        GameManager.instance.Wort++;
        _pickupText.ItemText("St. John's Wort");
        gameObject.SetActive(false);
    }
}
