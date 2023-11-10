using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xasu;
using Xasu.HighLevel;
using System.Threading.Tasks;
using UnityEngine.UI;
using System.Text;
using System;

public class TraceManager : MonoBehaviour
{
    [SerializeField] private string email;
    [SerializeField] private int userLength=7;
    private string username;
    public string currentLocation;
    
    public async void Start()
    {
        username=PlayerPrefs.GetString("Username", RandomStringBuilder());
        PlayerPrefs.SetString("Username", username);
        await Task.Yield();
        while (XasuTracker.Instance.Status.State == TrackerState.Uninitialized)
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

        InvokeRepeating("TrackPlayerPosition", 30f, 30f);
    }

    private string RandomStringBuilder()
    {
        StringBuilder randomString = new StringBuilder();
        System.Random random = new System.Random();

        char letter;

        for (int i = 0; i < userLength; i++)
        {
            double flt = random.NextDouble();
            int shift = Convert.ToInt32(Math.Floor(25 * flt));
            letter = Convert.ToChar(shift + 65);
            randomString.Append(letter);
        }
        return randomString.ToString();
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

    public async void TrackPlayerPosition()
    {
        //Debug.Log("playerpos!");
        //await AccessibleTracker.Instance.Stayed(currentLocation, AccessibleTracker.AccessibleType.Accessible)
        //    .WithResultExtensions(new Dictionary<string, object>
        //    {
        //        {"http://NoSJ/PlayerPosition", GameManager.instance.playerObject.transform.position}    
        //    });
    }

    public async void TrasguPuzzleStarted(string puzzle)
    {
        await CompletableTracker.Instance.Progressed(puzzle, CompletableTracker.CompletableType.Level, 0.5f);
    }

    public async void TrasguPuzzleCompleted(string puzzle)
    {
        await CompletableTracker.Instance.Completed(puzzle, CompletableTracker.CompletableType.Completable);
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
