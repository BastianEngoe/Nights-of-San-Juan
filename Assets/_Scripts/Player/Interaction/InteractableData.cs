using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableData : MonoBehaviour
{
    [SerializeField] public TextAsset JSONConversation;
    [SerializeField] public List<GameObject> actors;

    public void addOject(GameObject o)
    {
        actors.Add(o);
    }
}
