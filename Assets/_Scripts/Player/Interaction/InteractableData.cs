using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.HighDefinition;

[System.Serializable]
public class InteractableData : MonoBehaviour
{
    public TextAsset JSONConversation;
    public List<GameObject> actors;
    public List<Event> events;

    public Vector3 cameraOffset = new Vector3(0, 1, 0);
    public bool triggerEventWhenFinished;

    [HideInInspector] public JournalManager journalManager;

    //Adds an object
    public void AddOject(GameObject o)
    {
        actors.Add(o);
    }

    //If any of the fields have something assigned it triggers / changes them
    public void TriggerNextEvent()
    {
        if(events[0].journalEntryToUnlock!="") journalManager.UnlockQuest(events[0].journalEntryToUnlock);
        if(events[0].nextConversation!=null) ChangeConversation(events[0].nextConversation);
        if (events[0].nextActors != null && events[0].nextActors.Count>0) ChangeActors(events[0].nextActors);
        if (events[0].nextEvents != null)
        {
            for (int i = 0; i < events[0].nextEvents.Count; i++)
            {
                events[0].nextEvents[i].nextEvent.events.RemoveAt(0);
                events[0].nextEvents[i].nextEvent.triggerEventWhenFinished = events[0].nextEvents[i].nextEventTriggers;
            }
        }
        if(events!=null&&events.Count>1) events.RemoveAt(0);
        else if (events != null&&events.Count==1) {
            triggerEventWhenFinished = false;
        }

        if (events[0].customEvent != null) { events[0].customEvent.Invoke(); }
    }

    //Changes actors in the conversation
    public void ChangeActors(List<Actors> newActors)
    {
        actors.Clear();

        for (int i = 0; i < newActors.Count; i++)
        {
            actors.Add(newActors[i].actor);
            newActors[i].actor.SetActive(newActors[i].setActive);
        }
    }
    
    //Changes the JSON conversation
    public void ChangeConversation(TextAsset newJSONConversation)
    {
        JSONConversation = newJSONConversation;
    }
}
