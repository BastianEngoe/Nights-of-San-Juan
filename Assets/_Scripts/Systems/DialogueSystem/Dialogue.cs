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

    //[NonSerialized]
    public bool canRespond;

}

[System.Serializable]
public class Response
{
    public int nextConvIndex;
    public string responseText;
}

[System.Serializable]
public class Line
{
    public int speakerIndex;
    public string text;
}
