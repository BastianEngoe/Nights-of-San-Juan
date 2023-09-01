using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader iReader;
    [SerializeField] private CharacterController charController;
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currMoveVec = iReader.movementValue;
        Vector3 movement = new Vector3(currMoveVec.x, 0, currMoveVec.y);
        charController.Move(movement);

        if (iReader.movementValue == Vector2.zero)
            return;

        playerTransform.rotation = Quaternion.LookRotation(movement);

    }
}
