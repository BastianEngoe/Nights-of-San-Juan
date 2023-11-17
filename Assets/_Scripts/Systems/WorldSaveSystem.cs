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
    private Dictionary<string, Tuple<bool,int>> savedObjects= new Dictionary<string, Tuple<bool, int>>();
    private Dictionary<string, int> objectsToInstantiate= new Dictionary<string, int>();
    public static WorldSaveSystem instance;

    private void Awake()
    {
        instance = this;
    }

    public void SaveAll()
    {
        foreach(Scene scene in SceneManager.GetAllScenes())
        {
            if (scene.name != "BootScene")
            {
                foreach (GameObject obj in scene.GetRootGameObjects())
                {
                    SaveGameObjectAndChildren(obj);
                }
            }
        }

        string saveData = "SavedObjects|";
        foreach(var key in savedObjects.Keys)
        {
            saveData += key+"|";
            saveData += savedObjects[key].Item1+"|";
            saveData += savedObjects[key].Item2+"|";
        }

        saveData += "InstantiatedObjects|";
        foreach(var key in objectsToInstantiate.Keys)
        {
            saveData += key+"|";
            saveData += objectsToInstantiate[key];
        }

        PlayerPrefs.SetString("SavedData", saveData);
    }

    private void SaveGameObjectAndChildren(GameObject obj)
    {
        for(int i=0; i<obj.transform.childCount; i++)
        {
            SaveGameObjectAndChildren(obj.transform.GetChild(i).gameObject);
        }
        int numOfDataChanged = 0;
        InteractableData data = obj.GetComponent<InteractableData>();
        if (data != null)
            numOfDataChanged = data.triggerCount;
        try
        {
            savedObjects.Add(obj.name + "$" + obj.transform.position, new Tuple<bool, int>(obj.activeSelf, numOfDataChanged));
        }
        catch(Exception e)
        {
            SaveRepeated(obj, 1, numOfDataChanged);
        }
    }

    private void SaveRepeated(GameObject obj, int numRep, int numOfDataChanged)
    {
        try
        {
            savedObjects.Add(obj.name + "$" + obj.transform.position + "$" + numRep, new Tuple<bool, int>(obj.activeSelf, numOfDataChanged));
        }catch (Exception e)
        {
            numRep++;
            SaveRepeated(obj, numRep, numOfDataChanged);
        }
    }

    private bool LoadAll()
    {
        string data= PlayerPrefs.GetString("SavedData", "");
        try
        {
            if (data != "")
            {
                string[] info = data.Split('|');
                for(int i = 0; i < info.Length; i++)
                {
                    GameObject currObject= GameObject.Find(info[i].ToString());
                    //currObject.SetActive(info[i]);
                    i += 2;
                }
            }
            else
            {
                throw new Exception("No Data to be loaded");
            }
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
