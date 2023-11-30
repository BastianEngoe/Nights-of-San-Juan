using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public String tagToTrigger = "Player";
    [SerializeField] private UnityEvent EnterTrigger;
    [SerializeField] private UnityEvent ExitTrigger;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagToTrigger))
        {
            EnterTrigger.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagToTrigger))
        {
            ExitTrigger.Invoke();
        }
    }

    public void RespawnBeatriz(Transform location)
    {
        GameManager.instance.playerObject.GetComponent<CharacterController>().enabled = false;
        GameManager.instance.playerObject.transform.position = location.position;
        GameManager.instance.playerObject.GetComponent<CharacterController>().enabled = true;
    }
}
