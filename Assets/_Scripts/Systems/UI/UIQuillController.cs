using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIQuillController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    
    public void DisableAnimator()
    {
        anim.enabled = false;
    }
    
    public void QuillAppear()
    {
        anim.enabled = true;
        anim.Play("QuillAppear");
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (anim.enabled)
            {
                anim.SetTrigger("read");
            }
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            if (!anim.enabled)
            {
                QuillAppear();
            }
        }
    }
}
