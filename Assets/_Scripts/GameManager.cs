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

    [SerializeField] private TextAsset journalData;
    private JournalQuestsData journalQuestsData;

    private GameObject playerObject;

    [SerializeField] private InteractionComponent interactionComponent;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        ProccesJournal();
        DontDestroyOnLoad(this);
    }

    void Start()
    {
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
            dialogueSystem.setDialogue(interactableData.JSONConversation);
            camManager.setSpeakers(interactableData.actors);
            camManager.UpdateCameraState(CameraState.DialogueState);
            dialogueSystem.StartConversation();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

        }
    }

    public void setCameraState(CameraState newState)
    {
        camManager.UpdateCameraState(newState);
    }

    private void ProccesJournal()
    {
        journalQuestsData = JsonUtility.FromJson<JournalQuestsData>(journalData.text);
    }
}
