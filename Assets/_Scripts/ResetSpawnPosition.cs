using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSpawnPosition : MonoBehaviour
{
    private Vector3 spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;
    }

    private void Update()
    {
        if (transform.position.y < 5f)
        {
            Respawn(spawnPosition);
        }
    }

    public void Respawn(Vector3 position)
    {
        transform.position = position;
    }
}
