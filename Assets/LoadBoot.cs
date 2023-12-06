using System.Collections;
using System.Collections.Generic;
using HH.MultiSceneTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MultiSceneLoader.loadCollection("Boot", collectionLoadMode.Difference);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
