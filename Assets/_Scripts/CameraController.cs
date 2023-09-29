using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private float heightOffset = 2.0f;
    [SerializeField] private float cameraDistance = 5.0f;
    
    [SerializeField, Range(.0f, .99f)] private float lerpFactor = .2f;
    private Transform camTransform;
    private Transform targetTransform;

    private CameraState state = CameraState.MoveState;
    private Vector3 currPos;

    // Start is called before the first frame update
    void Start()
    {
        camTransform = transform;
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
                Vector3 offsetMov = targetTransform.position
                 - Vector3.Lerp(camTransform.forward, targetTransform.forward, lerpFactor)
                 * cameraDistance;
                offsetMov.y = targetTransform.position.y + heightOffset;
                camTransform.position = offsetMov;
                camTransform.LookAt(targetTransform);
                break;
        }
    }

    public void setTargetPosition(Vector3 newPos)
    {
        currPos = newPos;
    }
    public void setCamPosition(Vector3 newPos)
    {
        transform.position = newPos;
    }

    public void setState(CameraState newState) { state = newState; }

}
