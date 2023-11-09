using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CameraManager camManager;

    [SerializeField] private DialogueSystem dialogueSystem;

    [SerializeField] public JournalManager journalManager;

    public GameObject playerObject;

    [SerializeField] private InteractionComponent interactionComponent;

    [SerializeField] public InputManager inputManager;

    [SerializeField] private GameObject playerCameraTarget;

    [SerializeField] private UIQuillController quillController;

    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private TraceManager traceManager;

    public static GameManager instance;

    private UnityEvent nextInter= new UnityEvent();

    private bool newJournalEntryAdded = false;
    private bool onConversation = false;
    private bool onPause = false;


    private void Awake()
    {
        instance = this;
        traceManager = FindObjectOfType<TraceManager>();
    }

    void Start()
    {
        //Assign Player reference to all dialogues and to the event of loading an scene
        DialogueSetup();
        SceneManager.sceneLoaded += ConfigureDialogues;
    }


    public void NextNode(){
        if(dialogueSystem.GetLineCurrentIndex() < dialogueSystem.dialogue.conversations[dialogueSystem.GetConvCurrentIndex()].lines.Length - 1 )
        {
            if(dialogueSystem.ProccessLine())
                camManager.NextNode();
        }
        else{
            inputManager.RemoveDialogueInput();
            dialogueSystem.EndDialogue();
            onConversation = false;
            nextInter.Invoke();
            if(newJournalEntryAdded) quillController.QuillAppear();
            nextInter.RemoveAllListeners();
            newJournalEntryAdded = false;
        }
    }

    //Interaction event, sets up conversation front end
    public void OnInteract()
    {
        if (interactionComponent.currentTarget != null&&!onConversation)
        {
            InteractableData interactableData = interactionComponent.currentTarget.GetComponent<InteractableData>();
            traceManager.NPCInteracted(interactableData.actors[0].name);
            if (interactableData.triggerEventWhenFinished)
            {
                nextInter.AddListener(interactableData.TriggerNextEvent);
            }
            if (interactableData.JSONConversation) {
                if (interactableData.events[0].journalEntryToUnlock!="")
                newJournalEntryAdded = true;
            inputManager.AddDialogueInput();
            dialogueSystem.setDialogue(interactableData.JSONConversation);
            SetUpCamera(interactableData);
            dialogueSystem.StartConversation();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            onConversation = true;
            }
            else
            {
                nextInter.Invoke();
            }
        }
    }

    //Sets camera values
    private void SetUpCamera(InteractableData interactableData)
    {
        camManager.SetSpeakers(interactableData.actors);
        camManager.SetOffset(interactableData.cameraOffset);
        camManager.UpdateCameraState(CameraState.DialogueState);
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

    //Activates and Deactivates movement
    private void SetPlayerMovement(bool toWhat)
    {
        ThirdPersonController thirdPersonController = playerObject.GetComponent<ThirdPersonController>();
        thirdPersonController.canMove = toWhat;
    }

    //Changes camera state
    public void SetCameraState(CameraState newState)
    {
        camManager.UpdateCameraState(newState);
    }

    //Event formatted for Scene Manager, sets up dialogues when a scene is loaded
    private void ConfigureDialogues(Scene scene, LoadSceneMode mode)
    {
        DialogueSetup();
    }

    //Methods to activate and deactivate player
    //NOTE: Dont use these methods while loading scenes
    public void DeactivatePlayer()
    {
        playerObject.SetActive(false);
    }
    public void ActivatePlayer()
    {
        playerObject.SetActive(true);
    }


    //Assings player to npcs dialogue and references to dialogue manager
    private void DialogueSetup()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("Interactable");

        foreach (GameObject npc in npcs)
        {
            InteractableData data = npc.GetComponent<InteractableData>();
            if (data != null)
            {
                data.journalManager = journalManager;
                bool hasPlayer = false;
                for(int i =0; i < data.actors.Count; i++)
                {
                    if(data.actors[i]==playerCameraTarget)
                        hasPlayer = true;
                }
                if(!hasPlayer)
                data.AddOject(playerCameraTarget);
            }
        }
    }

    public void TogglePause()
    {
        onPause = !onPause;
        
        if (onPause)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            camManager.UpdateCameraState(CameraState.MenuState);
            inputManager.onPause = true;
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            camManager.UpdateCameraState(CameraState.MoveState);
            inputManager.onPause = false;
        }
    }
}
