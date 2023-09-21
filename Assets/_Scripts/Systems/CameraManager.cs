using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;

    [SerializeField] private CameraState cameraState = CameraState.CinematicState;
    public Transform currentSpeaker, dialoguePos;
    public float speakerDistance, directionLength, height, tiltScale;
    private Vector3 betweenSpeakers, direction, directionOffset;
    public Dialogue dialogue;
    private int index = 0;

    void Update()
    {
        
    }

    public void UpdateCameraState(CameraState cameraState)
    {
        this.cameraState = cameraState;
        ChangeCamera();
    }

    private void ChangeCamera()
    {
        switch (cameraState)
        {
            case CameraState.MoveState:
                break;
            case CameraState.DialogueState:
                SetCamDialoguePos();
                break;
            case CameraState.MenuState:
                break;
            case CameraState.CinematicState:
                break;
        }
    }

    private void SetCamDialoguePos()
    {
        int numSpeakers=0;
        for (int i = 0; i < dialogue.nodes[index].speakers.Length; i++)
        {
            betweenSpeakers += dialogue.nodes[index].speakers[i].position;
            numSpeakers++;
        }

        betweenSpeakers /= numSpeakers;
        Debug.Log(betweenSpeakers);
        direction = currentSpeaker.position - betweenSpeakers;

        directionOffset.x = direction.z * -1;
        directionOffset.z = direction.x;

        dialoguePos.position = direction * directionLength + new Vector3(0, 1, 0) * height + directionOffset * tiltScale;

        mainCamera.transform.position = dialoguePos.position;

        currentSpeaker = dialogue.nodes[index].currentSpeaker;

        mainCamera.transform.LookAt(currentSpeaker.position);
    }

    public void nextNode(){
        if(dialogue.nodes.Length>index) index++;
        SetCamDialoguePos();
    }
}

public enum CameraState
{
    MoveState,
    DialogueState,
    MenuState,
    CinematicState
}

