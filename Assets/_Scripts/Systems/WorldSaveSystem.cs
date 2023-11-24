using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class WorldSaveSystem : MonoBehaviour
{
    private Tuple<bool, int> gameObjectsState;
    private Tuple<GameObject, int> instantiatedState;
    private Dictionary<string, Tuple<bool,int>> savedObjects= new Dictionary<string, Tuple<bool, int>>();
    private Dictionary<string, Tuple<Vector3, Quaternion>> objectsToInstantiate = new Dictionary<string, Tuple<Vector3,Quaternion>>();
    public static WorldSaveSystem instance;

    private void Awake()
    {
        instance = this;
    }

    public void SaveAll()
    {
        PlayerPrefs.SetFloat("PlayerPosX",GameManager.instance.playerObject.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY",GameManager.instance.playerObject.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ",GameManager.instance.playerObject.transform.position.z);
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
            saveData += objectsToInstantiate[key].Item1.x+"$";
            saveData += objectsToInstantiate[key].Item1.y+"$";
            saveData += objectsToInstantiate[key].Item1.z+"$";
            saveData += objectsToInstantiate[key].Item2.x+"$";
            saveData += objectsToInstantiate[key].Item2.y+"$";
            saveData += objectsToInstantiate[key].Item2.z+"$";
            saveData += objectsToInstantiate[key].Item2.w+"|";
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
        if (obj.name == "TrasguPuzzleStart")
        {
            Debug.Log("eu");
        }
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

    private void AsyncLoad(Scene currScene, LoadSceneMode loadSceneMode)
    {
        GameManager.instance.playerObject.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerPosX"), PlayerPrefs.GetFloat("PlayerPosY"), PlayerPrefs.GetFloat("PlayerPosZ"));
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
                int n=0;
                List<string> info = new List<string>(data.Split('|'));
                info.RemoveAt(0);
                while (info[0] != "InstantiatedObjects")
                {
                    List<String> currObjData = new List<string>(info[0].Split('$'));
                    n++;
                    
                    if (currObjData.Count == 4) currObjData.RemoveAt(1);
                    GameObject currObject = null;
                    foreach (GameObject gameObject in sceneObjects)
                    {
                        
                        if (gameObject.name == currObjData[0])
                        {
                            
                            currObject = gameObject;
                            currObject.SetActive(bool.Parse(currObjData[1]));
                            InteractableData currData = currObject.GetComponent<InteractableData>();
                            if (currData != null && int.Parse (currObjData[2])!=0)
                                currData.AdvanceEventNum(int.Parse(currObjData[2]));
                            info.RemoveAt(0);
                            break;
                        }
                    }
                    if (currObject==null)
                    {
                        Debug.Log("Fail at: " + currObjData[0]);
                        info.RemoveAt(0);
                    }
                    Debug.Log(n);
                }
                info.RemoveAt(0);
                while (info.Count != 0&&info[0]!="")
                {
                    List<String> currObjData = new List<string>(info[0].Split('$'));

                    GameObject currObject =Resources.Load<GameObject>("Prefabs/"+currObjData[0]);
                    AddLoadedGameObject(currObject,new Vector3(float.Parse(currObjData[1]), float.Parse(currObjData[2]), float.Parse(currObjData[3])),new Quaternion(float.Parse(currObjData[4]),float.Parse(currObjData[5]),float.Parse(currObjData[6]),float.Parse(currObjData[7])));
                    SceneManager.MoveGameObjectToScene(currObject, currScene);
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
            SceneManager.sceneLoaded -= AsyncLoad;
        }
        SceneManager.sceneLoaded -= AsyncLoad;
    }

    private GameObject AddLoadedGameObject(GameObject gameObject)
    {
        GameObject instantiated= Instantiate(gameObject);

        int numOfDataChanged = 0;

        InteractableData data = instantiated.GetComponent<InteractableData>();

        PlayableDirector playableDirector= instantiated.GetComponent<PlayableDirector>();

        if (playableDirector != null)
        {
            playableDirector.playOnAwake = false;
            playableDirector.enabled = false;
        }

         if (data != null)
            numOfDataChanged = data.triggerCount;

        RemoveAditionalCameras(instantiated);

        objectsToInstantiate.Add(gameObject.name, new Tuple<Vector3, Quaternion>(instantiated.transform.position,new Quaternion(instantiated.transform.rotation.x, instantiated.transform.rotation.y,instantiated.transform.rotation.z, instantiated.transform.rotation.w)));
        return instantiated;

    }
    private GameObject AddLoadedGameObject(GameObject gameObject, Vector3 position, Quaternion rotation)
    {
        GameObject instantiated= Instantiate(gameObject, position, rotation);
        int numOfDataChanged = 0;
        InteractableData data = instantiated.GetComponent<InteractableData>();

        PlayableDirector playableDirector = instantiated.GetComponent<PlayableDirector>();

        if (playableDirector != null)
        {
            playableDirector.playOnAwake = false;
            playableDirector.enabled = false;
        }

        if (data != null)
            numOfDataChanged = data.triggerCount;

        objectsToInstantiate.Add(gameObject.name, new Tuple<Vector3, Quaternion>(position, new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w)));

        RemoveAditionalCameras(instantiated);

        return instantiated;

    }

    public GameObject AddGameObject(GameObject gameObject,Vector3 position, Quaternion rotation)
    {
        GameObject instantiated = Instantiate(gameObject, position, rotation);

        objectsToInstantiate.Add(gameObject.name, new Tuple<Vector3, Quaternion>(position, rotation));

        return instantiated;
    }

    private void RemoveAditionalCameras(GameObject gameObject)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            RemoveAditionalCameras(gameObject.transform.GetChild(i).gameObject);
        }
        Camera camera= gameObject.GetComponent<Camera>();
        if(camera != null)
            camera.enabled = false;
    }

    public bool CheckSave()
    {
        return (PlayerPrefs.GetString("SavedData", "") != "");
    }
}
