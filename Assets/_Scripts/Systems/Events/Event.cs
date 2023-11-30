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
    public List<string> journalEntriesToUnlock;
    public UnityEvent nextCustomEvent;
    public List<NextEvent> nextEvents;
    public bool triggerNextEventWhenFinished;
}

[System.Serializable]
public class Actors
{
    public GameObject actor;
    public bool setActive;
    public Actors(GameObject newActor)
    {
        actor = newActor;
        setActive = true;
    }
}

public class NextEvent
{
    public InteractableData nextEvent;
    public bool nextEventTriggers;
}