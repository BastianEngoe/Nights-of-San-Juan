using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera, dialogCamera, menuCamera, cinemaCamera;
    public CameraState cameraState;
    public Transform speakerOne, speakerTwo, currentSpeaker, dialougPos;
    public float speakerDistance;
    private Vector3 betweenSpeakers, lerp;
    
    public bool currentSpeakerTest;

    void Update()
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

                betweenSpeakers = (speakerTwo.position - speakerOne.position);
                speakerDistance = Vector3.Distance(speakerOne.position, speakerTwo.position);
                lerp = Vector3.Lerp(speakerOne.position, speakerTwo.position, speakerDistance);
                dialougPos.position = lerp;


                if (currentSpeakerTest)
                {
                    currentSpeaker = speakerOne;
                }
                else
                {
                    currentSpeaker = speakerTwo;
                }

                dialogCamera.transform.LookAt(currentSpeaker.position);

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
}

public enum CameraState
{
    MoveState,
    DialState,
    MenuState,
    CinematicState
}
