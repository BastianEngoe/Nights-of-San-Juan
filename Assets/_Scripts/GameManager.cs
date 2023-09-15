using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ThirdPersonController playerController;
    private GameObject playerObject;
    void Start()
    {
        playerController = FindObjectOfType<ThirdPersonController>();
        playerObject = playerController.gameObject;
    }

    private void Update()
    {

    }
    
    public void CanPlayerMove(bool canThey)
    {
        playerController.canMove = canThey;
    }

    public void CanPlayerJump(bool canThey)
    {
        playerController.canJump = canThey;
    }
}
