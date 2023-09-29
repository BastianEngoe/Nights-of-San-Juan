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
    [SerializeField] private Dialogue testDialogue;

    private GameObject playerObject;

    private InteractionComponent interactionComponent;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        //addDialogueInput();
        //playerController = FindObjectOfType<ThirdPersonController>();
        //playerObject = playerController.gameObject;
        inputReader.InteractEvent += onInteract;
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
        if (areThey)
        {
            camManager.UpdateCameraState(CameraState.DialogueState);
            
        }
        else camManager.UpdateCameraState(CameraState.MoveState);
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
    }
    public void removeDialogueInput()
    {
        inputReader.OnNextLineEvent -= nextNode;
    }

    //private void sendDialogue(Dialogue dial){
    //    currDialogue = dial;
    //    dialogueSystem.dialogue = dial;
    //    camManager.dialogue = dial;
    //}

    private void onInteract()
    {
        if (interactionComponent.currentTarget != null) {
            InteractableData interactableData = interactionComponent.currentTarget.GetComponent<InteractableData>();
            addDialogueInput();
            dialogueSystem.setDialogue(interactableData.JSONConversation);
            dialogueSystem.StartConversation();
            camManager.UpdateCameraState(CameraState.DialogueState);
            camManager.setSpeakers(interactableData.actors);
        }      
    }
}
