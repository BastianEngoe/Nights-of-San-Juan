using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalCameraControl : MonoBehaviour
{
    [SerializeField] private Transform camPivotTransform;
    [SerializeField] private Transform journalTransform;
    [SerializeField] private GameObject journal;
    [SerializeField] private float adjustSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, camPivotTransform.position, Time.deltaTime * adjustSpeed);
        transform.LookAt(journal.transform.position);
        //transform.LookAt(journalTransform);
    }
}
