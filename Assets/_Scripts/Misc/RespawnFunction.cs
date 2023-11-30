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

    public void Respawn(Vector3 position)
    {
        transform.position = position;
    }
}
