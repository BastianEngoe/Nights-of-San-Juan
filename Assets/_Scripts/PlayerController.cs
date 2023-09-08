using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader iReader;
    
    /*
    [SerializeField] private CharacterController charController;
    [SerializeField] private float charSpeed = 1.0f;

    [SerializeField, Range(0.01f, 1.0f)] private float movementLerpFactor = 0.05f;

    [SerializeField] private float gravity = 9.8f;

    private float vSpeed = 0.0f; 
    private Transform playerTransform;
    private Transform camTransform;
    */
    
    
    // Start is called before the first frame update
    void Start()
    {
    /*
        playerTransform = transform;
        camTransform = Camera.main.transform;
    */

        iReader.InteractEvent += onInteract;

    }

    // Update is called once per frame
    void Update()
    {
        /*
        Vector2 currMoveVec = iReader.movementValue;

        if (charController.isGrounded)
            vSpeed = 0;
        vSpeed -= gravity * Time.deltaTime;
        
        Vector3 gravityVector = new Vector3(0, vSpeed, 0);

        charController.Move(movement * Time.deltaTime * charSpeed + gravityVector);

        if (iReader.movementValue == Vector2.zero)
            return;

        playerTransform.rotation = Quaternion.LookRotation(movement);
        
        */
        
    }

    void OnDestroy()
    {
        iReader.InteractEvent -= onInteract;
        
    }

    private void onInteract(){
        Debug.Log("Trying to interact");
        throw new NotImplementedException();
    }
}