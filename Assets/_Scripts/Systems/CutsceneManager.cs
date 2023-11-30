using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    //The main purpose for this script is for Unity Timelines (cutscenes) to gain access to the GameManager/Player so we can
    //control player movement and visibility for its duration.

    public void HidePlayer()
    {
        GameManager.instance.DeactivatePlayer();
        CameraManager.instance.UpdateCameraState(CameraState.CinematicState);
    }

    public void ShowPlayer()
    {
        GameManager.instance.ActivatePlayer();
        CameraManager.instance.UpdateCameraState(CameraState.MoveState);
    }
}
