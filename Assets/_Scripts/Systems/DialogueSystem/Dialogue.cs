using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    public string[] strings {get; private set;}
    public bool canRespond {get; private set;}
    public Response[] responses {get; private set;}
    public Dialogue(string[] strings) {
        this.strings = strings;
        canRespond = false;
        responses = null;
    }
}

public class Response{
    public string responseText {get; private set;}
    public Action<Dialogue> response {get; private set;}

}
