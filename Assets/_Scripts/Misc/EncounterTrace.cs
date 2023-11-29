using UnityEngine;

public class EncounterTrace : MonoBehaviour
{
    private TraceManager _traceManager;
    public string encounter;
    
    private void Awake()
    {
        _traceManager = FindObjectOfType<TraceManager>();
    }

    public void StartEncounterTrace()
    {
        _traceManager.EncounterStarted(encounter);
    }

    public void CompleteEncounterTrace()
    {
        _traceManager.EncounterCompleted(encounter);
    }
}
