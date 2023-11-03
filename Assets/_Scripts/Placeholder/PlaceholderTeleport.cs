using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class PlaceholderTeleport : MonoBehaviour
{
    public Transform house, village;
    private GameObject player;
    
    public void MovePlayerToHouse()
    {
        player = FindObjectOfType<ThirdPersonController>().gameObject;
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = house.transform.position;
        player.GetComponent<CharacterController>().enabled = true;
    }
    
    public void MovePlayerToVillage()
    {
        player = FindObjectOfType<ThirdPersonController>().gameObject;
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = village.transform.position;
        player.GetComponent<CharacterController>().enabled = true;
    }
}
