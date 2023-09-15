using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialogue
{
    
    [Serializable]
    public struct conversationNode {
        public string text; 
        public Transform[] speakers;
        public Transform currentSpeaker;
    }
    
    public conversationNode[] nodes;

    public bool canRespond;
    //public Response[] responses; 
    
    // public Dialogue(string[] strings) {
    //     this.strings = strings;
    //     canRespond = false;
    //     responses = null;
    // }
}

[Serializable]
public class Response{
    public string responseText {get; private set;}
    public Transform responseSpeaker { get; private set; }
    public Action<Dialogue> response {get; private set;}

}
