using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private OpeningController openingController;
    

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

    void OnDestroy()
    {
        inputReader.OnToggleJournal -= ToggleJournal;
        inputReader.InteractEvent -= onInteract;
    }

    private void onInteract()
    {
        if (!onJournal){
            GameManager.instance.OnInteract();
        }
    }

    void ToggleJournal(){
        onJournal = !onJournal;
        GameManager.instance.ToggleJournal();
    }

    public void addDialogueInput()
    {
        inputReader.OnNextLineEvent += GameManager.instance.nextNode;
        inputReader.InteractEvent -= GameManager.instance.OnInteract;
        inputReader.OnToggleJournal -= ToggleJournal;

    }
    public void removeDialogueInput()
    {
        inputReader.OnNextLineEvent -= GameManager.instance.nextNode;
        inputReader.InteractEvent += GameManager.instance.OnInteract;
        inputReader.OnToggleJournal += ToggleJournal;
    }

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

    public void startIntroScene()
    {
        openingController = FindObjectOfType<OpeningController>();
        //openingController.onSceneStart();
        inputReader.OnCutsceneStartEvent -= startIntroScene;
    }

}
