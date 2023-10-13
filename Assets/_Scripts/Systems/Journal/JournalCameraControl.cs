using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalCameraControl : MonoBehaviour
{
    [SerializeField] private Transform camPivotTransform;
    [SerializeField] private Transform journalTransform;
    [SerializeField] private float adjustSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, camPivotTransform.position, Time.deltaTime * adjustSpeed);
        //transform.LookAt(journalTransform);
    }
}
