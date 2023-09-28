using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public enum CameraState
{
    MoveState,
    DialogueState,
    MenuState,
    CinematicState
}

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;

    [SerializeField] private CameraState cameraState = CameraState.CinematicState;
    public Transform currentSpeaker, dialoguePos;
    public Transform[] speakers;
    public float speakerDistance, directionLength, height, tiltScale;
    private Vector3 betweenSpeakers, direction, directionOffset;
    [SerializeField] private DialogueSystem dialogueSystem;
    private Dialogue currDialogue;

    private void Start()
    {
        SetCamDialoguePos();
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
        for (int i = 0; i < speakers.Length; i++)
        {
            betweenSpeakers += speakers[i].position;
            numSpeakers++;
        }

        betweenSpeakers /= numSpeakers;
        Debug.Log(betweenSpeakers);
        direction = currentSpeaker.position - betweenSpeakers;

        directionOffset.x = direction.z * -1;
        directionOffset.z = direction.x;

        dialoguePos.position = direction * directionLength + new Vector3(0, 1, 0) * height + directionOffset * tiltScale;

        mainCamera.transform.position = dialoguePos.position;

        mainCamera.transform.LookAt(currentSpeaker.position);
    }

    public void nextNode(){
        int index = dialogueSystem.GetLineCurrentIndex();
        currDialogue = dialogueSystem.getDialogue();

        if (currDialogue.lines[index].speakerIndex<speakers.Length) 
            currentSpeaker=speakers[currDialogue.lines[index].speakerIndex];

        SetCamDialoguePos();
    }
}



