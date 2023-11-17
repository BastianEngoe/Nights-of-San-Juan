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
            saveData += key+"$";
            saveData += savedObjects[key].Item1+"$";
            saveData += savedObjects[key].Item2+"|";
        }

        saveData += "InstantiatedObjects|";
        foreach(var key in objectsToInstantiate.Keys)
        {
            saveData += key+"$";
            saveData += objectsToInstantiate[key]+"|";
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
            savedObjects.Add(obj.name, new Tuple<bool, int>(obj.activeSelf, numOfDataChanged));
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
            savedObjects.Add(obj.name + "$" + numRep, new Tuple<bool, int>(obj.activeSelf, numOfDataChanged));
        }catch (Exception e)
        {
            numRep++;
            SaveRepeated(obj, numRep, numOfDataChanged);
        }
    }


    private void GetGameObjectsForLoad(GameObject gameObject, ref List<GameObject> sceneObjects)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GetGameObjectsForLoad(gameObject.transform.GetChild(i).gameObject,ref sceneObjects);
        }
        sceneObjects.Add(gameObject);
    }

    public void LoadAll()
    {
        SceneManager.sceneLoaded+=AsyncLoad;
    }

    private void AsyncLoad(Scene mockScene, LoadSceneMode loadSceneMode)
    {
        string data = PlayerPrefs.GetString("SavedData", "");
        List<GameObject> sceneObjects = new List<GameObject>();

        foreach (Scene scene in SceneManager.GetAllScenes())
        {
            if (scene.name != "BootScene")
            {
                foreach (GameObject gameObject in scene.GetRootGameObjects())
                {
                    GetGameObjectsForLoad(gameObject, ref sceneObjects);
                }
            }
        }
        try
        {
            if (data != "")
            {
                List<string> info = new List<string>(data.Split('|'));
                info.RemoveAt(0);
                while (info[0] != "InstantiatedObjects")
                {
                    List<String> currObjData = new List<string>(info[0].Split('$'));
                    if (currObjData.Count == 4) currObjData.RemoveAt(1);
                    foreach (GameObject gameObject in sceneObjects)
                    {
                        if (gameObject.name == currObjData[0])
                        {
                            GameObject currObject = gameObject;
                            currObject.SetActive(bool.Parse(currObjData[1]));
                            InteractableData currData = currObject.GetComponent<InteractableData>();
                            if (currData != null) currData.AdvanceEventNum(int.Parse(currObjData[2]));
                            info.RemoveAt(0);
                        }
                    }
                    if (gameObject.name == "")
                    {
                        Debug.Log("Fail at: " + currObjData[0]);
                    }
                    
                }
                info.RemoveAt(0);
                while (info.Count != 0&&info[0]!="")
                {
                    List<String> currObjData = new List<string>(info[0].Split('$'));

                    GameObject currObject = AddGameObject(Resources.Load(currObjData[0], typeof(GameObject)) as GameObject);
                    InteractableData currData = currObject.GetComponent<InteractableData>();
                    if (currData != null) currData.AdvanceEventNum(int.Parse(currObjData[1]));
                    info.RemoveAt(0);
                }
            }
            else
            {
                Debug.Log("No Data to be loaded");
            }
        }
        catch (Exception e)
        {
            Debug.Log("Load fail: " + e);
        }
        SceneManager.sceneLoaded -= AsyncLoad;
    }

    public GameObject AddGameObject(GameObject gameObject)
    {
        int numOfDataChanged = 0;
        InteractableData data = gameObject.GetComponent<InteractableData>();
         if (data != null)
            numOfDataChanged = data.triggerCount;

        objectsToInstantiate.Add(gameObject.name, numOfDataChanged);
        return Instantiate(gameObject);

    }
}
