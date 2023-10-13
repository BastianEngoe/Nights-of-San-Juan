using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class ResetSpawnPosition : MonoBehaviour
{
    private Vector3 spawnPosition;
    public Transform transToRespawn;

    public CharacterController _controller;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;
    }

    private void Update()
    {
        if (transToRespawn.transform.position.y < 5f)
        {
            Respawn(spawnPosition);
        }
    }

    public void Respawn(Vector3 position)
    {
        _controller.enabled = false;
        transToRespawn.transform.position = position;
        _controller.enabled = true;
    }
}
