using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public void Pickup()
    {
        GameManager.instance.AddToInventory(gameObject);
        gameObject.SetActive(false);
    }
}
