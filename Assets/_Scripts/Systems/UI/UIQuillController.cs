using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuillController : MonoBehaviour
{
    [SerializeField] private Animator mainAnim, firstAnim;
    public bool firstTime;
    
    public void DisableAnimator()
    {
        mainAnim.enabled = false;
    }
    
    public void QuillAppear()
    {
        mainAnim.enabled = true;
        mainAnim.Play("QuillAppear");
    }

    private void Start()
    {
        if (firstTime)
        {
            firstAnim.gameObject.SetActive(true);
        }  
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (mainAnim.enabled)
            {
                mainAnim.SetTrigger("read");
                if (firstTime && firstAnim.enabled)
                {
                    firstAnim.SetTrigger("done");
                    firstTime = false;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            if (!mainAnim.enabled)
            {
                QuillAppear();
            }
        }
    }
}
