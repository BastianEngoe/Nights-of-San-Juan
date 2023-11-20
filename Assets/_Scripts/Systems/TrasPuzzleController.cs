using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrasPuzzleController : MonoBehaviour
{
    [SerializeField] private Transform trasSpawnLocation;
    [SerializeField] private GameObject trasPrefab;
    private TraceManager _traceManager;

    private void Awake()
    {
        _traceManager = FindObjectOfType<TraceManager>();
    }

    public void onStartPuzzle()
    {
        _traceManager.TrasguPuzzleStarted(gameObject.name);
    }

    public void onCompletePuzzle()
    {
        Instantiate(trasPrefab, trasSpawnLocation.position, trasSpawnLocation.rotation);
        _traceManager.TrasguPuzzleCompleted(gameObject.name);
    }
}
