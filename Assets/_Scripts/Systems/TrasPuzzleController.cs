using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrasPuzzleController : MonoBehaviour
{
    [SerializeField] private Transform trasSpawnLocation;
    [SerializeField] private GameObject trasPrefab;

    public void onCompletePuzzle()
    {
        Instantiate(trasPrefab, trasSpawnLocation);
    }
}
