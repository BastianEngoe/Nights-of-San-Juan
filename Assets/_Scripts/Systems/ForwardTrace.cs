using System;
using UnityEngine;
using UnityEngine.Events;

public class ForwardTrace : MonoBehaviour
{
    
    private TraceManager _traceManager;
    private LocationText _locationText;
    [SerializeField] private UnityEvent EnterLocation;

    private void Awake()
    {
        _traceManager = FindObjectOfType<TraceManager>();
        _locationText = FindObjectOfType<LocationText>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
        _locationText.CallLocation(location);
        _traceManager.currentLocation = location;
        _traceManager.LocationVisited(location);
    }
}
