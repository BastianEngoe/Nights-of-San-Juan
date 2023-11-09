using System;
using UnityEngine;
using UnityEngine.Events;

public class ForwardTrace : MonoBehaviour
{
    
    [SerializeField] private TraceManager _traceManager;
    [SerializeField] private UnityEvent EnterLocation;

    private void Awake()
    {
        _traceManager = FindObjectOfType<TraceManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is triggering");
            EnterLocation.Invoke();
        }
    }

    public void ForwardTraceLocation(string location)
    {
        if (location == _traceManager.currentLocation)
        {
            Debug.Log("Player has left: " + _traceManager.currentLocation);
            _traceManager.currentLocation = null;
            return;
        }
        _traceManager.currentLocation = location;
        _traceManager.LocationVisited(location);
    }
}
