using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] public InputReader inputReader;
    [SerializeField] private MenusManager menuManager;
    private OpeningController openingController;
    
    [HideInInspector] public bool onJournal = false;
    [HideInInspector] public bool onPause = false ;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        inputReader.OnPauseEvent += TogglePause;
        inputReader.OnToggleJournal += ToggleJournal;
        inputReader.InteractEvent += OnInteract;
        inputReader.OnCutsceneStartEvent += StartIntroScene;
        inputReader.OnMoveMenuEvent += (() => MenusManager.instance.MenuMove(inputReader.movementValue));
    }

    private void TogglePause()
    {
        if(!menuManager.onMenu)
         GameManager.instance.TogglePause();
    }

    //Calls the interact method at the game manager only if the journal is not triggered
    private void OnInteract()
    {
        if (!onPause&&!onJournal){
            GameManager.instance.OnInteract();
        }
    }

    //Toggles the journal from the game manager and toggles the boolean to know wether its active or not
    void ToggleJournal()
    {
        if (!onPause)
        {
            onJournal = !onJournal;
            GameManager.instance.ToggleJournal();
        }
    }

    //Adds controls for dialogue input
    public void AddDialogueInput()
    {
        inputReader.OnNextLineEvent += GameManager.instance.NextNode;
        inputReader.InteractEvent -= GameManager.instance.OnInteract;
        inputReader.OnToggleJournal -= ToggleJournal;

    }
    //Removes controls for dialogue input
    public void RemoveDialogueInput()
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
    public void StartIntroScene()
    {
        inputReader.OnCutsceneStartEvent -= StartIntroScene;
    }

    public void SetMenuControls(bool active)
    {
        if (active)
            inputReader.OnMoveMenuEvent += (() => MenusManager.instance.MenuMove(inputReader.movementValue));
        else
            inputReader.OnMoveMenuEvent -= (() => MenusManager.instance.MenuMove(inputReader.movementValue));
    }

    public void Clear()
    {
        inputReader.ClearMovementValue();
    }
}
