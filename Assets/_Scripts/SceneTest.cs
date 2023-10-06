using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using HH.MultiSceneTools;
using UnityEngine.SceneManagement;

public class SceneTest : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        MultiSceneLoader.loadCollection("Luarca", collectionLoadMode.Difference);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
