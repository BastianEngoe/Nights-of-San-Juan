using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalCameraControl : MonoBehaviour
{
    [SerializeField] private Transform camPivotTransform;
    [SerializeField] private Transform journalTransform;
    [SerializeField] private GameObject journal;
    [SerializeField] private float animDuration = 1;
    [SerializeField] private float offset = -5;

    private float offsetFactor;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, camPivotTransform.position, Time.deltaTime * animDuration) + transform.parent.right * offset * (float)Math.Sin(Math.PI + offsetFactor / 180);
        transform.LookAt(journal.transform.position);
        //transform.LookAt(journalTransform);
        Debug.Log(offsetFactor);
        offsetFactor -= 180f * Time.deltaTime / animDuration;
        if(offsetFactor < 0)
            offsetFactor = 0;
    }

    public void ResetOffsetFactor()
    {
        offsetFactor = 180f;
    }
}
