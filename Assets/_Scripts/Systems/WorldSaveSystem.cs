using System.Collections;
using System.Collections.Generic;
using Unity.RuntimeSceneSerialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveSystem : MonoBehaviour
{
    private Dictionary<bool, GameObject> gameObjectsState;
    private Dictionary<InteractableData, GameObject> interactablesState;
    public List<string> scenesSaved;

    public void SaveScenes()
    {
        scenesSaved.Clear();
        foreach(Scene scene in SceneManager.GetAllScenes())
        {
            scenesSaved.Add(SceneSerialization.SerializeScene(scene));
        }
    }

    public void LoadScenes()
    {
        foreach (string scene in scenesSaved) {
            SceneSerialization.ImportScene(scene);
    }
    }
}
