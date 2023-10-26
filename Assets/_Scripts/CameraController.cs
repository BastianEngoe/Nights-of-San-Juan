using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;
    
    private float lerpFactor = 2f;
    private Transform targetTransform;

    private CameraState state = CameraState.MoveState;
    private Vector3 currPos;

    // Start is called before the first frame update
    void Start()
    {
        targetTransform = player.transform;
        currPos = targetTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case CameraState.DialogueState:
                Quaternion targetRotation = Quaternion.LookRotation(currPos - transform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, lerpFactor);
                break;
            case CameraState.CinematicState: 
                break;
            case CameraState.MenuState:
                break;
            case CameraState.MoveState:
                break;
        }
    }

    //Assign a new target to look at
    public void SetTargetPosition(Vector3 newPos)
    {
        currPos = newPos;
    }
    //Set a new camera position with the configured offset
    public void SetCamPosition(Vector3 newPos)
    {
        newPos += offset;
        transform.position = newPos;
    }

    //Changes a new state, if its dialogue changes current position to look at to the new target
    public void SetState(CameraState newState) {
        if(newState == CameraState.DialogueState)
            currPos = targetTransform.position;
        state = newState;
    }

    //Sets a new offset position
    public void SetOffset(Vector3 cameraOffset)
    {
        offset = cameraOffset;
    }
}
