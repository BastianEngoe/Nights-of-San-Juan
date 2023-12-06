using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 movementValue { get; private set; }
    private Controls controls;
    public event Action InteractEvent;
    public event Action OnToggleJournal;
    public event Action OnNextLineEvent; 
    public event Action OnPageLeftEvent; 
    public event Action OnPageRightEvent; 
    public event Action OnCutsceneStartEvent; 
    public event Action OnPauseEvent;
    public event Action OnMoveMenuEvent;


    public void OnInteract(InputAction.CallbackContext context)
    {
        if(!context.performed) {return;}
        InteractEvent?.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        controls = new Controls();    
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    void OnDestroy()
    {
        controls.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementValue = context.ReadValue<Vector2>();
        if (!context.performed && context.action.WasReleasedThisFrame())
        {
            OnMoveMenuEvent?.Invoke();
        }
    }

    public void OnNextLine(InputAction.CallbackContext context)
    {
        if (!context.performed && context.action.WasReleasedThisFrame()){
            OnNextLineEvent?.Invoke();
        }
    }

    void Controls.IPlayerActions.OnToggleJournal(InputAction.CallbackContext context)
    {
        if (!context.performed && context.action.WasReleasedThisFrame()){
            OnToggleJournal?.Invoke();
        }
    }

    public void OnPageLeft(InputAction.CallbackContext context)
    {
        if (!context.performed && context.action.WasReleasedThisFrame()){
            OnPageLeftEvent?.Invoke();
        }
    }

    public void OnPageRight(InputAction.CallbackContext context)
    {
        if (!context.performed && context.action.WasReleasedThisFrame()){
            OnPageRightEvent?.Invoke();
        }
    }

    public void OnStartCutscene(InputAction.CallbackContext context)
    {
        if (!context.performed && context.action.WasReleasedThisFrame()){
            OnCutsceneStartEvent?.Invoke();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (!context.performed && context.action.WasReleasedThisFrame())
            OnPauseEvent?.Invoke();
    }

    public void ClearMovementValue()
    {
        movementValue = Vector2.zero;
    }
}
