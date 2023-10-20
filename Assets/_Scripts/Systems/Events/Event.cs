using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Event
{
    public List<Actors> nextActors;
    public TextAsset nextConversation;
    public string journalEntryToUnlock;
    public UnityEvent customEvent;
    public List<NextEvent> nextEvents;
}

[System.Serializable]
public class Actors
{
    public GameObject actor;
    public bool setActive;
}

public class NextEvent
{
    public InteractableData nextEvent;
    public bool nextEventTriggers;
}