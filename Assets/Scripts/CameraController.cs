using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private float heightOffset = 2.0f;
    [SerializeField] private float cameraDistance = 5.0f;
    
    [SerializeField, Range(.0f, .99f)] private float lerpFactor = .2f;
    private Transform camTransform;
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        camTransform = transform;
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = playerTransform.position
         - Vector3.Lerp(camTransform.forward, playerTransform.forward, lerpFactor)
         * cameraDistance;
        offset.y = playerTransform.position.y + heightOffset;
        camTransform.position = offset;
        camTransform.LookAt(playerTransform);
    }
}
