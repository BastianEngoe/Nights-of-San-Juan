using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera, dialogCamera, menuCamera;
    public CameraState cameraState;

    void Update()
    {
        switch (cameraState)
        {
            case CameraState.MoveState:
                mainCamera.enabled = true;
                dialogCamera.enabled = false;
                menuCamera.enabled = false;
                break;
            case CameraState.DialState:
                mainCamera.enabled = false;
                dialogCamera.enabled = true;
                menuCamera.enabled = false;
                break;
            case CameraState.MenuState:
                mainCamera.enabled = false;
                dialogCamera.enabled = false;
                menuCamera.enabled = true;
                break;
        }
    }
}

public enum CameraState
{
    MoveState,
    DialState,
    MenuState
}
