using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class InteractionComponent : MonoBehaviour
{
    HashSet<Transform> currentTransformsInRange;
    [SerializeField] private GameObject targetSprite;
    public Transform currentTarget {get; private set;} = null;

    private bool isSomeoneOutlined = false;
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
            var newTargetCandidate = FindNearestInteractable();
            

            if(currentTarget != newTargetCandidate)
            {
                if(currentTarget)
                    currentTarget.gameObject.GetComponent<Outline>().enabled = false;
            }
            currentTarget = newTargetCandidate;
            
            //targetSprite.SetActive(true);
            //targetSprite.transform.position = currentTarget.position;


            DrawOutline();

        }
        else
        {
            //targetSprite.SetActive(false);
            if(currentTarget)
                currentTarget.gameObject.GetComponent<Outline>().enabled = false;
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

    private void DrawOutline()
    {
        if (currentTarget.gameObject.GetComponent<Outline>() != null)
        {
            currentTarget.gameObject.GetComponent<Outline>().enabled = true;
        }
        else
        {
            Outline outline = currentTarget.gameObject.AddComponent<Outline>();
            outline.enabled = true;
            currentTarget.gameObject.GetComponent<Outline>().OutlineColor = Color.yellow;
            currentTarget.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
            currentTarget.gameObject.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineVisible;
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
