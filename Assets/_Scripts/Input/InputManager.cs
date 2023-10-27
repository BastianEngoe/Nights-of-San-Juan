using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    private OpeningController openingController;
    
    bool onJournal = false;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        inputReader.OnToggleJournal += ToggleJournal;
        inputReader.InteractEvent += onInteract;
        inputReader.OnCutsceneStartEvent += startIntroScene;
    }
    
    //Calls the interact method at the game manager only if the journal is not triggered
    private void onInteract()
    {
        if (!onJournal){
            GameManager.instance.OnInteract();
        }
    }

    //Toggles the journal from the game manager and toggles the boolean to know wether its active or not
    void ToggleJournal(){
        onJournal = !onJournal;
        GameManager.instance.ToggleJournal();
    }

    //Adds controls for dialogue input
    public void addDialogueInput()
    {
        inputReader.OnNextLineEvent += GameManager.instance.NextNode;
        inputReader.InteractEvent -= GameManager.instance.OnInteract;
        inputReader.OnToggleJournal -= ToggleJournal;

    }
    //Removes controls for dialogue input
    public void removeDialogueInput()
    {
        inputReader.OnNextLineEvent -= GameManager.instance.NextNode;
        inputReader.InteractEvent += GameManager.instance.OnInteract;
        inputReader.OnToggleJournal += ToggleJournal;
    }

    //Sets controls for journal
    public void SetJournalControls(bool toWhat){
        if(toWhat){
            inputReader.OnPageLeftEvent += GameManager.instance.journalManager.TurnLeftPage;
            inputReader.OnPageRightEvent += GameManager.instance.journalManager.TurnRightPage;
            inputReader.InteractEvent -= GameManager.instance.OnInteract;
        }else
        {
            inputReader.OnPageLeftEvent -= GameManager.instance.journalManager.TurnLeftPage;
            inputReader.OnPageRightEvent -= GameManager.instance.journalManager.TurnRightPage;
            inputReader.InteractEvent += GameManager.instance.OnInteract;
        }
    }

    //Sets the controls for the intro scene
    public void startIntroScene()
    {
        openingController = FindObjectOfType<OpeningController>();
        inputReader.OnCutsceneStartEvent -= startIntroScene;
    }

}
