using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveSystem : MonoBehaviour
{
    private Dictionary<bool, GameObject> gameObjectsState;
    private Dictionary<InteractableData, GameObject> interactablesState;

    private void SaveAll()
    {
        foreach(Scene scene in SceneManager.GetAllScenes())
        {
            foreach(GameObject obj in scene.GetRootGameObjects())
            {

            }
        }
    }
}
