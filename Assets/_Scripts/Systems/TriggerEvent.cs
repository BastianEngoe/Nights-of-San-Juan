using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    
    [SerializeField] private UnityEvent EventToTrigger;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventToTrigger.Invoke();
        }
    }

    public void RespawnBeatriz(Transform location)
    {
        GameManager.instance.playerObject.GetComponent<CharacterController>().enabled = false;
        GameManager.instance.playerObject.transform.position = location.position;
        GameManager.instance.playerObject.GetComponent<CharacterController>().enabled = true;
    }
}
