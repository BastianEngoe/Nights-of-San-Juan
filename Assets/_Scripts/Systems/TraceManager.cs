using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xasu;
using Xasu.HighLevel;
using System.Threading.Tasks;
using UnityEngine.UI;

public class TraceManager : MonoBehaviour
{
    [SerializeField] private string username, email;
    public string currentLocation;
    
    public async void Start()
    {
        await Task.Yield();
        while(XasuTracker.Instance.Status.State == TrackerState.Uninitialized)
        {
            await Task.Yield();
        }

        XasuTracker.Instance.DefaultActor = new TinCan.Agent
        {
            name = username,
            mbox = "mailto:" + email,
        };

        Debug.Log("Sending Initialized trace");

        await CompletableTracker.Instance.Initialized("MyGame", CompletableTracker.CompletableType.Game);

        Debug.Log("Done!");
        
    }

    public async void AccessedJournal()
    {
        await AccessibleTracker.Instance.Accessed("Journal", AccessibleTracker.AccessibleType.Accessible);
    }

    public async void NPCInteracted(string NPC)
    {
        await GameObjectTracker.Instance.Interacted(NPC, GameObjectTracker.TrackedGameObject.Npc);
    }
    
    public async void LocationVisited(string location)
    {
        await AccessibleTracker.Instance.Accessed(location, AccessibleTracker.AccessibleType.Area);
    }
    
    public async void Progressed()
    {
        await CompletableTracker.Instance.Progressed("MyGame", CompletableTracker.CompletableType.Game, 0.5f);
    }
    
    public async void Completed()
    {
        await CompletableTracker.Instance.Progressed("MyGame", CompletableTracker.CompletableType.Game, 0.5f);
    }
    
    public async void Finalized()
    {
        await CompletableTracker.Instance.Completed("MyGame", CompletableTracker.CompletableType. Game).WithSuccess(true);
    }
}
