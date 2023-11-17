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
    public UnityEvent eventToTrigger;
    public Vector3 cameraOffset = new Vector3(0, 1, 0);
    public bool triggerEventWhenFinished;
    public int triggerCount = 0;

    [HideInInspector] public JournalManager journalManager;

    private void OnEnable()
    {
        GameManager.instance.DialogueSetup();
    }

    //Adds an object
    public void AddOject(GameObject o)
    {
        actors.Add(o);
    }

    //If any of the fields have something assigned it triggers / changes them
    public void TriggerNextEvent()
    {
        if(eventToTrigger != null) { 
            eventToTrigger.Invoke(); }

        if (events.Count > 0)
        {
            AdvanceData();
        }
    }

    public void TriggerEventFromMethod()
    {

        if (eventToTrigger != null)
        {
            eventToTrigger.Invoke();
        }
        if(events.Count>0)
        {
            AdvanceData();
        }
    }

    private void AdvanceData()
    {
        if (events[0].journalEntryToUnlock != "") journalManager.UnlockQuest(events[0].journalEntryToUnlock);
        if (events[0].nextConversation != null) ChangeConversation(events[0].nextConversation);
        if (events[0].nextActors != null && events[0].nextActors.Count > 0) ChangeActors(events[0].nextActors);
        eventToTrigger = new UnityEvent();
        bool nextEventTrigger = events[0].triggerNextEventWhenFinished;
        eventToTrigger = events[0].nextCustomEvent;
        if (events != null && events.Count > 1) events.RemoveAt(0);
        triggerEventWhenFinished = nextEventTrigger;
        triggerCount++;
    }

    public void AdvanceEventNum(int n)
    {
        for(int i = 0; i<n; i++)
        {
            AdvanceData();  
        }
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
