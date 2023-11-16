using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveSystem : MonoBehaviour
{
    private Tuple<bool, int> gameObjectsState;
    private Tuple<GameObject, int> instantiatedState;
    private Dictionary<string, Tuple<bool,int>> savedObjects;
    private Dictionary<string, int> objectsToInstantiate;
    public static WorldSaveSystem instance;

    private void Awake()
    {
        instance = this;
    }

    private void SaveAll()
    {
        foreach(Scene scene in SceneManager.GetAllScenes())
        {
            foreach(GameObject obj in scene.GetRootGameObjects())
            {
                int numOfDataChanged = 0;
                InteractableData data = obj.GetComponent<InteractableData>();
                if (data != null)
                    numOfDataChanged = data.triggerCount;

                savedObjects.Add(obj.name, new Tuple<bool, int>(obj.activeSelf, numOfDataChanged));
            }
        }

        string saveData = "SavedObjects(";
        foreach(var key in savedObjects.Keys)
        {
            saveData += key+"(";
            saveData += savedObjects[key].Item1+"(";
            saveData += savedObjects[key].Item2+"(";
        }

        saveData += "InstantiatedObjects(";
        foreach(var key in objectsToInstantiate.Keys)
        {
            saveData += key+"(";
            saveData += objectsToInstantiate[key];
        }

        PlayerPrefs.SetString("SavedData", saveData);
    }

    private bool LoadAll()
    {
        try
        {
            return true;
        }
        catch(Exception e)
        {
            Debug.Log("Load fail: " +e);
            return false;
        }
    }

    public void AddGameObject(GameObject gameObject)
    {
        Instantiate(gameObject);
        int numOfDataChanged = 0;
        InteractableData data = gameObject.GetComponent<InteractableData>();
         if (data != null)
            numOfDataChanged = data.triggerCount;

        objectsToInstantiate.Add(gameObject.name, numOfDataChanged);
    }
}
