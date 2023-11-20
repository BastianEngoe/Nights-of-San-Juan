using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xasu.HighLevel;

public class TrasguPuzzle2Controller : MonoBehaviour
{
    [SerializeField] private TrasguPuzzle2[] puzzleValues;
    [SerializeField] private Transform trasguLocation;
    [SerializeField] private GameObject trasguPrefab;
    [SerializeField] private TraceManager _traceManager;

    private string combination = "243";
    private string combinationAttempt;

    private bool started, completed;

    public void checkCombination()
    {
        if (!started)
        {
            _traceManager = FindObjectOfType<TraceManager>();
            _traceManager.TrasguPuzzleStarted("Puzzle2");
            started = true;
        }
        combinationAttempt = null;

        combinationAttempt = puzzleValues[0].value.ToString() + puzzleValues[1].value.ToString() + puzzleValues[2].value.ToString();

        if (combinationAttempt == combination && !completed)
        {
            _traceManager.TrasguPuzzleCompleted("Puzzle2");
            Instantiate(trasguPrefab, trasguLocation.position, trasguLocation.transform.rotation);
            completed = true;
        }
        
        Debug.Log("Current combination attempt: " + combinationAttempt);
    }

    private void SpawnTrasgu()
    {
        
    }
}
