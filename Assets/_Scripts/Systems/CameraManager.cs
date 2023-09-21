using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera, dialogCamera, menuCamera, cinemaCamera;

    [SerializeField] private CameraState cameraState = CameraState.CinematicState;
    public Transform speakerOne, speakerTwo, currentSpeaker, dialougPos;
    public float speakerDistance, directionLength, height, tiltScale;
    private Vector3 betweenSpeakers, direction, directionYeet;
    public Dialogue dialogue;
    int index = 0;
    public bool currentSpeakerTest;

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
                mainCamera.enabled = true;
                dialogCamera.enabled = false;
                menuCamera.enabled = false;
                cinemaCamera.enabled = false;
                break;
            case CameraState.DialState:
                mainCamera.enabled = false;
                dialogCamera.enabled = true;
                menuCamera.enabled = false;
                cinemaCamera.enabled = false;
                SetCamPosition();
                break;
            case CameraState.MenuState:
                mainCamera.enabled = false;
                dialogCamera.enabled = false;
                menuCamera.enabled = true;
                cinemaCamera.enabled = false;
                break;
            case CameraState.CinematicState:
                mainCamera.enabled = false;
                dialogCamera.enabled = false;
                menuCamera.enabled = false;
                cinemaCamera.enabled = true;
                break;
        }
    }

    private void SetCamPosition()
    {
        int numSpeakers=0;
        for (int i = 0; i < dialogue.nodes[index].speakers.Length; i++)
        {
            betweenSpeakers += dialogue.nodes[index].speakers[i].position;
            numSpeakers++;
            //Debug.Log(numSpeakers);
        }

        betweenSpeakers /= numSpeakers;
        Debug.Log(betweenSpeakers);
        direction = currentSpeaker.position - betweenSpeakers;

        directionYeet.x = direction.z * -1;
        directionYeet.z = direction.x;

        dialougPos.position = direction * directionLength + new Vector3(0, 1, 0) * height + directionYeet * tiltScale;

        currentSpeaker = dialogue.nodes[index].currentSpeaker;

        dialogCamera.transform.LookAt(currentSpeaker.position);
    }

    public void nextNode(){
        index++;
        // SetCamPosition();
    }
}

public enum CameraState
{
    MoveState,
    DialState,
    MenuState,
    CinematicState
}
