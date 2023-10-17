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
        if (firstTime)
        {
            firstAnim.gameObject.SetActive(true);
        }  
        mainAnim.enabled = true;
        mainAnim.Play("QuillAppear");
    }
    
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.J))
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
    }
}
