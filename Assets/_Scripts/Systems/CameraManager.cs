using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
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

    [SerializeField] private ThirdPersonController thirdPersonController;

    [SerializeField] private CameraState cameraState = CameraState.CinematicState;
    public Transform currentSpeaker, dialoguePos;
    public Transform[] speakers;
    public float speakerDistance, directionLength, height, tiltScale;
    private Vector3 direction, directionOffset;
    [SerializeField] private DialogueSystem dialogueSystem;
    private Dialogue currDialogue;

    //Changes the state of the camera on this manager and in the camera controller
    public void UpdateCameraState(CameraState newCameraState)
    {
        cameraState = newCameraState;
        cameraController.SetState(newCameraState);
        ChangeCamera();
    }

    //Switches between the possible states
    private void ChangeCamera()
    {
        switch (cameraState)
        {
            case CameraState.MoveState:
                StartCursorTrack();
                journalCameraControl.enabled = false;
                break;
            case CameraState.DialogueState:
                StopCursorTrack();
                SetCamDialoguePos();
                break;
            case CameraState.MenuState:
                break;
            case CameraState.CinematicState:
                break;
            case CameraState.JournalState:
                StopCursorTrack();
                journalCameraControl.enabled = true;
                break;
            
        }
    }

    //Sets the camera between the speakers and updates everything accordingly
    private void SetCamDialoguePos()
    {
        Vector3 betweenSpeakers= Vector3.zero;
        int numSpeakers=speakers.Length;
        for (int i = 0; i < numSpeakers; i++)
        {
            betweenSpeakers += speakers[i].position;
        }

        betweenSpeakers /= numSpeakers;
        direction = currentSpeaker.position - betweenSpeakers;

        directionOffset.x = direction.z * -1;
        directionOffset.z = direction.x;

        cameraController.SetCamPosition(betweenSpeakers);
        cameraController.SetTargetPosition(currentSpeaker.position);

    }

    //Changes the target to focus at
    public void NextNode(){
        int index = dialogueSystem.GetLineCurrentIndex();
        currDialogue = dialogueSystem.getDialogue();

        if (currDialogue.lines[index].speakerIndex < speakers.Length)
            currentSpeaker = speakers[currDialogue.lines[index].speakerIndex];

        SetCamDialoguePos();
    }

    //Updates the dialogue and given the gameobjects that interact with each other
    //the transforms get assigned in the speakers container
    public void SetSpeakers(List<GameObject> newSpeakers)
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

    //Sets a new offset
    public void SetOffset(Vector3 cameraOffset)
    {
        cameraController.SetOffset(cameraOffset);
    }

    public void StopCursorTrack()
    {
        thirdPersonController.LockCameraPosition = true;
        cinemaBrain.enabled = false;
    }
    public void StartCursorTrack()
    {
        thirdPersonController.LockCameraPosition = false;
        cinemaBrain.enabled = true;
    }
}




