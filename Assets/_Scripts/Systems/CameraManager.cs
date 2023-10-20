using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public enum CameraState
{
    MoveState,
    DialogueState,
    MenuState,
    CinematicState,

    JournalState
}

public class CameraManager : MonoBehaviour
{
    public CameraController cameraController;

    public CinemachineBrain cinemaBrain;

    public JournalCameraControl journalCameraControl;

    [SerializeField] private CameraState cameraState = CameraState.CinematicState;
    public Transform currentSpeaker, dialoguePos;
    public Transform[] speakers; //Move to Events
    public float speakerDistance, directionLength, height, tiltScale;
    private Vector3 direction, directionOffset;
    [SerializeField] private DialogueSystem dialogueSystem;
    private Dialogue currDialogue;


    public void UpdateCameraState(CameraState cameraState)
    {
        this.cameraState = cameraState;
        cameraController.SetState(cameraState);
        ChangeCamera();
    }

    private void ChangeCamera()
    {
        switch (cameraState)
        {
            case CameraState.MoveState:
                cinemaBrain.enabled = true;
                journalCameraControl.enabled = false;
                //cameraController.enabled = false;
                break;
            case CameraState.DialogueState:
                cinemaBrain.enabled = false;
                //int index = dialogueSystem.GetLineCurrentIndex();
                //currentSpeaker = speakers[currDialogue.lines[index].speakerIndex];
                SetCamDialoguePos();
                break;
            case CameraState.MenuState:
                //cinemaBrain.enabled = false;
                break;
            case CameraState.CinematicState:
                break;
            case CameraState.JournalState:
                cinemaBrain.enabled = false;
                journalCameraControl.enabled = true;
                break;
            
        }
    }

    private void SetCamDialoguePos()
    {
        Vector3 betweenSpeakers= Vector3.zero;
        int numSpeakers=speakers.Length;
        for (int i = 0; i < numSpeakers; i++)
        {
            betweenSpeakers += speakers[i].position;
        }

        betweenSpeakers /= numSpeakers;
        Debug.Log(betweenSpeakers);
        direction = currentSpeaker.position - betweenSpeakers;

        directionOffset.x = direction.z * -1;
        directionOffset.z = direction.x;

        cameraController.SetCamPosition(betweenSpeakers);
        cameraController.SetTargetPosition(currentSpeaker.position);

    }

    public void nextNode(){
        int index = dialogueSystem.GetLineCurrentIndex();
        currDialogue = dialogueSystem.getDialogue();

        if (currDialogue.lines[index].speakerIndex < speakers.Length)
            currentSpeaker = speakers[currDialogue.lines[index].speakerIndex];

        SetCamDialoguePos();
    }

    public void setSpeakers(List<GameObject> newSpeakers)
    {
        currDialogue = dialogueSystem.getDialogue();

        Transform[] speakersContainer = new Transform[newSpeakers.Count];
        for (int i = 0;i < newSpeakers.Count;i++)
        {
            speakersContainer[i] = newSpeakers[i].GetComponent<Transform>();
        }
        currentSpeaker = speakersContainer[currDialogue.lines[dialogueSystem.GetLineCurrentIndex()].speakerIndex];
        speakers = speakersContainer;
    }
    public void setOffset(Vector3 cameraOffset)
    {
        cameraController.SetOffset(cameraOffset);
    }
}




