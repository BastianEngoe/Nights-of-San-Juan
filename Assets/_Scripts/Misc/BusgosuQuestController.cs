using UnityEngine;
using UnityEngine.Events;

public class BusgosuQuestController : MonoBehaviour
{
    public UnityEvent WhatToInvoke;
    
    public void CheckTrasguConditions()
    {
        if (GameManager.instance.TrasguPuzzlesCompleted >= 3)
        {
            WhatToInvoke.Invoke();
        }
    }
}
