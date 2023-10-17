using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableData : MonoBehaviour
{
    [SerializeField] public TextAsset JSONConversation;
    [SerializeField] public List<GameObject> actors;
    [SerializeField] private UnityEngine.Event nextEvent;
    public void addOject(GameObject o)
    {
        actors.Add(o);
    }

    public void changeData(TextAsset nextJSONConversation, List<GameObject> nextActors)
    {
        JSONConversation = nextJSONConversation;
        actors.Clear();
        actors.AddRange(nextActors);
    }
}
