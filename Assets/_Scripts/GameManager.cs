using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CameraManager camManager;

    [SerializeField] private DialogueSystem dialogueSystem;

    [SerializeField] public JournalManager journalManager;

    [SerializeField] private GameObject playerObject;

    [SerializeField] private InteractionComponent interactionComponent;

    [SerializeField] public InputManager inputManager;

    [SerializeField] private GameObject playerCameraTargetPosition;

    [SerializeField] private UIQuillController quillController;

    public static GameManager instance;

    private UnityEvent nextInter= new UnityEvent();

    private bool newJournalEntryAdded = false;
    private bool onConversation = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //Assign Player reference to all dialogues and to the event of loading an scene
        DialogueSetup();
        SceneManager.sceneLoaded += ConfigureDialogues;
    }
    
    public void CanPlayerMove(bool canThey)
    {
        //playerController.canMove = canThey;
    }

    public void CanPlayerJump(bool canThey)
    {
        //playerController.canJump = canThey;
    }

    public void InCutscene(bool areThey, Dialogue dial = null)
    {
        CanPlayerJump(!areThey);
    }

    public void nextNode(){
        if(dialogueSystem.GetLineCurrentIndex() < dialogueSystem.dialogue.conversations[dialogueSystem.GetConvCurrentIndex()].lines.Length - 1 )
        {
            if(dialogueSystem.ProccessLine())
                camManager.NextNode();
        }
        else{
            inputManager.removeDialogueInput();
            dialogueSystem.EndDialogue();
            InCutscene(false);
            onConversation = false;
            nextInter.Invoke();
            if(newJournalEntryAdded) quillController.QuillAppear();
            nextInter.RemoveAllListeners();
            newJournalEntryAdded = false;
        }
    }

    public void OnInteract()
    {
        if (interactionComponent.currentTarget != null&&!onConversation) {
            InteractableData interactableData = interactionComponent.currentTarget.GetComponent<InteractableData>();
            if (interactableData.triggerEventWhenFinished) {
                newJournalEntryAdded = true;
                nextInter.AddListener(interactableData.TriggerNextEvent); }
            inputManager.addDialogueInput();
            dialogueSystem.setDialogue(interactableData.JSONConversation);
            camManager.SetSpeakers(interactableData.actors);
            camManager.SetOffset(interactableData.cameraOffset);
            camManager.UpdateCameraState(CameraState.DialogueState);
            dialogueSystem.StartConversation();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            onConversation = true;
        }
    }
    ///<summary>
    ///Controls when does the journal show up, and locks the player movement
    ///</summary>
    public void ToggleJournal()
    {
        if(!journalManager.IsActive)
        {
            camManager.UpdateCameraState(CameraState.JournalState);
            //toggle the actual journal
            journalManager.ShowJournal();
            //lock the player
            SetPlayerMovement(false);
            //toggle the controls for the journal
            inputManager.SetJournalControls(true);
        }
        else 
        {
            camManager.UpdateCameraState(CameraState.MoveState);
            //toggle the actual journal
            journalManager.QuitJournal();
            //lock the player
            SetPlayerMovement(true);
            //toggle the controls for the journal
            inputManager.SetJournalControls(false);
        }
        Debug.Log("Journal active: " + journalManager.IsActive);
    }

    private void SetPlayerMovement(bool toWhat)
    {
        ThirdPersonController thirdPersonController = playerObject.GetComponent<ThirdPersonController>();
        thirdPersonController.canMove = toWhat;
        //thirdPersonController.canJump = toWhat;
    }

    public void SetCameraState(CameraState newState)
    {
        camManager.UpdateCameraState(newState);
    }

    private void ConfigureDialogues(Scene scene, LoadSceneMode mode)
    {
        DialogueSetup();
    }

    private void DialogueSetup()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("Interactable");

        foreach (GameObject npc in npcs)
        {
            InteractableData data = npc.GetComponent<InteractableData>();
            if (data != null)
            {
                data.journalManager = journalManager;
                data.AddOject(playerCameraTargetPosition);
            }
        }
    }
}
