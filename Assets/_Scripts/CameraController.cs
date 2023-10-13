using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float cameraDistance = 5.0f;
    
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
                Debug.Log(currPos);
                Quaternion targetRotation = Quaternion.LookRotation(currPos - transform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, lerpFactor);

                // Vector3 camPos = (player.transform.position + targetTransform.position) / 2;
                // camPos.y += heightOffset;

                // transform.position = Vector3.Lerp(transform.position, camPos, lerpFactor * Time.deltaTime);
                // transform.LookAt(targetTransform);
                break;
            case CameraState.CinematicState: 
                break;
            case CameraState.MenuState:
                break;
            case CameraState.MoveState:
                break;
        }
    }

    public void setTargetPosition(Vector3 newPos)
    {
        currPos = newPos;
    }
    public void setCamPosition(Vector3 newPos)
    {
        newPos += offset;
        transform.position = newPos;

    }

    public void setState(CameraState newState) {
        if(newState == CameraState.DialogueState)
            currPos = targetTransform.position;
        state = newState;
        }

}
