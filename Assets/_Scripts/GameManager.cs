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
    [SerializeField] private InputReader inputReader;

    [SerializeField] private JournalManager journalManager;

    [SerializeField] private GameObject playerObject;

    [SerializeField] private InteractionComponent interactionComponent;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        inputReader.InteractEvent += onInteract;
        inputReader.OnToggleJournal += ToggleJournal;
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
            if(dialogueSystem.nextLine())
                camManager.nextNode();
        }
        else{
            removeDialogueInput();
            dialogueSystem.EndDialogue();
            InCutscene(false);
        }
    }

    public void addDialogueInput()
    {
        inputReader.OnNextLineEvent += nextNode;
        inputReader.InteractEvent -= onInteract;

    }
    public void removeDialogueInput()
    {
        inputReader.OnNextLineEvent -= nextNode;
        inputReader.InteractEvent += onInteract;
    }

    private void onInteract()
    {
        if (interactionComponent.currentTarget != null) {
            InteractableData interactableData = interactionComponent.currentTarget.GetComponent<InteractableData>();
            addDialogueInput();
            //SetPlayerMovement(false); //We don't detect when the dialogue starts, so we need to do sth with this
            dialogueSystem.setDialogue(interactableData.JSONConversation);
            camManager.setSpeakers(interactableData.actors);
            camManager.UpdateCameraState(CameraState.DialogueState);
            dialogueSystem.StartConversation();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

        }
    }
    ///<summary>
    ///Controls when does the journal show up, and locks the player movement
    ///</summary>
    private void ToggleJournal()
    {
        if(!journalManager.IsActive)
        {
            //toggle the actual journal
            journalManager.ShowJournal();
            //lock the player
            SetPlayerMovement(false);
            //toggle the controls for the journal
            inputReader.OnPageLeftEvent += journalManager.TurnLeftPage;
            inputReader.OnPageRightEvent += journalManager.TurnRightPage;
        }
        else 
        {
            //toggle the actual journal
            journalManager.QuitJournal();
            //lock the player
            SetPlayerMovement(true);
            //toggle the controls for the journal
            inputReader.OnPageLeftEvent -= journalManager.TurnLeftPage;
            inputReader.OnPageRightEvent -= journalManager.TurnRightPage;
        }
        Debug.Log("Journal active: " + journalManager.IsActive);
    }

    private void SetPlayerMovement(bool toWhat)
    {
        var thirdPersonController = playerObject.GetComponent<ThirdPersonController>();
        thirdPersonController.canMove = toWhat;
        //thirdPersonController.canJump = toWhat;
    }

    public void setCameraState(CameraState newState)
    {
        camManager.UpdateCameraState(newState);
    }
}
