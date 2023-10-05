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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementValue = context.ReadValue<Vector2>();
    }

    public void OnNextLine(InputAction.CallbackContext context)
    {
        if (!context.performed && context.action.WasReleasedThisFrame()){
            OnNextLineEvent?.Invoke();
        }
    }
}
