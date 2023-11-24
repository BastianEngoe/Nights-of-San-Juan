using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaXanaQuestController : MonoBehaviour
{
    //public UnityEvent result;
    [SerializeField] private InteractableData laXanaDialogue;
    
    public void CheckInventory()
    {
        if (GameManager.instance.Wort >= 3)
        {
            Debug.Log("Has wort");
            
            laXanaDialogue.TriggerEventFromElse();
            //GameManager.instance.RemoveFromInventory("StJohnsWort", false);
            //result.Invoke();
        }
        else
        {
            Debug.Log("No wort");
        }
    } 
}
