using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaXanaQuestController : MonoBehaviour
{
    public UnityEvent result;
    
    public void CheckInventory()
    {
        if (GameManager.instance.SearchInventory("StJohnsWort", 3, false))
        {
            Debug.Log("Has wort");
            
            GameManager.instance.RemoveFromInventory("StJohnsWort", false);
            result.Invoke();
        }
        else
        {
            Debug.Log("No wort");
        }
    } 
}
