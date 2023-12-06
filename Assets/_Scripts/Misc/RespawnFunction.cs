using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnFunction : MonoBehaviour
{
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }

    public void RespawnStartPos()
    {
        transform.position = startPosition;
    }

    public void Respawn(Transform transLocation)
    {
        GameManager.instance.playerObject.GetComponent<CharacterController>().enabled = false;
        GameManager.instance.playerObject.transform.position = transLocation.position;
        GameManager.instance.playerObject.GetComponent<CharacterController>().enabled = true;
    }
}
