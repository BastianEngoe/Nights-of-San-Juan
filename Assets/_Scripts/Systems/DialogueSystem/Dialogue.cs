using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.Rendering;

[System.Serializable]
public class Dialogue
{
    public int id;
    public Line[] lines;
    public Response[] responses;

    //public bool canRespond;

}

[System.Serializable]
public struct Response
{
    public int nextConvIndex;
    public string responseText;
}

[System.Serializable]
public struct Line
{
    public int speakerIndex;
    public string text;
}

[System.Serializable]
public class DialoguesData
{
    public Dialogue[] conversations;
}