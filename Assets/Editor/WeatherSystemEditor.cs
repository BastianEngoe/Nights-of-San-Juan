using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WeatherSystem))]

public class WeatherSystemEditor : Editor //This was supposed to be used to have a custom button in the Unity inspector where we can
                                          //more easily swap weather as a debug tool. But haven't finished it yet. -Bastian

{
    /*public override void OnInspectorGUI()
    {
        WeatherSystem weatherSystem = (WeatherSystem)target;
        if(GUILayout.Button("Reset"))
        {
            
        }
    }*/
}
