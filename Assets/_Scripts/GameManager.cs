using System;
using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[SerializeField] private ThirdPersonController playerController;
    [SerializeField] private CameraManager camManager;
    [SerializeField] private DialogueSystem dialogueSystem;
    //[SerializeField] private InputReader inputReader;

    [SerializeField] public JournalManager journalManager;

    [SerializeField] private GameObject playerObject;

    [SerializeField] private InteractionComponent interactionComponent;

    [SerializeField] public InputManager inputManager;

    public static GameManager instance;

    private bool onConversation = false;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
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
                camManager.nextNode();
        }
        else{
            inputManager.removeDialogueInput();
            dialogueSystem.EndDialogue();
            InCutscene(false);
            onConversation = false;
        }
    }

    public void onInteract()
    {
        if (interactionComponent.currentTarget != null&&!onConversation) {
            InteractableData interactableData = interactionComponent.currentTarget.GetComponent<InteractableData>();
            inputManager.addDialogueInput();
            //SetPlayerMovement(false); //We don't detect when the dialogue starts, so we need to do sth with this
            dialogueSystem.setDialogue(interactableData.JSONConversation);
            camManager.setSpeakers(interactableData.actors);
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
            camManager.UpdateCameraState(CameraState.MenuState);
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
        var thirdPersonController = playerObject.GetComponent<ThirdPersonController>();
        thirdPersonController.canMove = toWhat;
        //thirdPersonController.canJump = toWhat;
    }

    public void SetCameraState(CameraState newState)
    {
        camManager.UpdateCameraState(newState);
    }
}
