using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

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
        WorldSaveSystem.instance.AddGameObject(trasPrefab, trasSpawnLocation.position, trasSpawnLocation.rotation);
        trasPrefab.GetComponent<PlayableDirector>().Play();
        GameManager.instance.TrasguPuzzlesCompleted += 1;
        _traceManager.TrasguPuzzleCompleted(gameObject.name);
    }
}
