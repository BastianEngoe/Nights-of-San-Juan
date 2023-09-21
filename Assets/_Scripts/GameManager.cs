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

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        inputReader.OnNextLineEvent += nextNode;
        //playerController = FindObjectOfType<ThirdPersonController>();
        //playerObject = playerController.gameObject;
        camManager = FindObjectOfType<CameraManager>();
        sendDialogue(testDialogue);
    }

    void OnDestroy()
    {
        inputReader.OnNextLineEvent -= nextNode;
    }

    private void Update()
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
        CanPlayerMove(!areThey);
        if (areThey)
        {
            camManager.UpdateCameraState(CameraState.DialogueState);
            
        }
        else camManager.UpdateCameraState(CameraState.MoveState);
    }

    public void nextNode(){
        if(dialogueSystem.GetCurrentIndex() < dialogueSystem.dialogue.nodes.Length - 1 )
        {
            if(dialogueSystem.nextLine())
            camManager.nextNode();
        }
        else{
            inputReader.OnNextLineEvent -= nextNode;
            dialogueSystem.EndDialogue();
            InCutscene(false);
        }
    }

    private void sendDialogue(Dialogue dial){
        dialogueSystem.dialogue = dial;
        camManager.dialogue = dial;
    }
}
