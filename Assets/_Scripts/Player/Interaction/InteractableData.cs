using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableData : MonoBehaviour
{
    public TextAsset JSONConversation;
    public List<GameObject> actors;
    public List<Event> events;
    public bool triggerEventWhenFinished;
    
    [HideInInspector] public JournalManager journalManager;

    public void AddOject(GameObject o)
    {
        actors.Add(o);
    }

    public void TriggerNextEvent()
    {
        if(events[0].journalEntryToUnlock!="") journalManager.UnlockQuest(events[0].journalEntryToUnlock);
        if(events[0].nextConversation!=null) ChangeConversation(events[0].nextConversation);
        if (events[0].nextActors != null) ChangeActors(events[0].nextActors);
        if(events!=null||events.Count>0) events.RemoveAt(0);
    }

    public void ChangeActors(List<GameObject> newActors)
    {
        actors.Clear();
        actors.AddRange(newActors);
    }
    
    public void ChangeConversation(TextAsset newJSONConversation)
    {
        JSONConversation = newJSONConversation;
    }
}
