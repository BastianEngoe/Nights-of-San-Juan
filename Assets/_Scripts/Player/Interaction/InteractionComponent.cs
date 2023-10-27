using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class InteractionComponent : MonoBehaviour
{
    HashSet<Transform> currentTransformsInRange;
    [SerializeField] private GameObject targetSprite;
    public Transform currentTarget {get; private set;} = null;
    // Start is called before the first frame update
    void Start()
    {
        currentTransformsInRange = new HashSet<Transform>();
        targetSprite.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            Debug.Log(currentTransformsInRange.Count);
        }

        if (currentTransformsInRange.Count > 0)
        {
            currentTarget = FindNearestInteractable();
            targetSprite.SetActive(true);
            targetSprite.transform.position = currentTarget.position;
        }
        else
        {
            targetSprite.SetActive(false); 
            currentTarget = null;
        }


        HashSet < Transform > objectsToRemove = new HashSet<Transform>();


        foreach (Transform t in currentTransformsInRange)
        {
            if (!t.gameObject.activeInHierarchy)
            {
                objectsToRemove.Add(t);
            }
        }

        foreach(Transform t in objectsToRemove)
        {
            currentTransformsInRange.Remove(t);
        }
    }

    //Returns a reference to the nearest Transform to the player
    private Transform FindNearestInteractable()
    {
 
        float minDistanceFound = float.MaxValue;
        Transform nearestCandidateFound = null;
        foreach (Transform candidate in currentTransformsInRange)
        {
            float thisDistance = Vector3.Distance(candidate.transform.position, transform.position);
            if (thisDistance < minDistanceFound)
            {
                nearestCandidateFound = candidate;
                minDistanceFound = thisDistance;
            }
        }
        return nearestCandidateFound;
    }

    //When an object enters the interactable area triggers it gets added if it has Interactable Data
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<InteractableData>()){
            currentTransformsInRange.Add(other.transform);
        }
    }

    //Objects get removed when exiting the interactable area
    private void OnTriggerExit(Collider other)
    {
        currentTransformsInRange.Remove(other.transform);
    }
}
