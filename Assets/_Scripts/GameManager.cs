using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ThirdPersonController playerController;
    [SerializeField] private CameraManager camManager;
    private GameObject playerObject;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        playerController = FindObjectOfType<ThirdPersonController>();
        playerObject = playerController.gameObject;
        camManager = FindObjectOfType<CameraManager>();
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

    public void InCutscene(bool areThey)
    {
        CanPlayerJump(areThey);
        CanPlayerMove(areThey);
        if (areThey)
        {
            camManager.cameraState = CameraState.DialState;
        }
        else camManager.cameraState = CameraState.MoveState;
    }
}
