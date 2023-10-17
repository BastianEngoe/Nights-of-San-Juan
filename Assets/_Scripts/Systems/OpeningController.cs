using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class OpeningController : MonoBehaviour
{
    public CanvasGroup TitleCanvas;
    [SerializeField] private float cAlpha = 1, t;
    private bool startFade;
    public PlayableDirector cutController;
    public Animator beaAnim;

    void Update()
    {

        if (startFade)
        {
            cAlpha = Mathf.Lerp(1, 0, t);
            t += 0.75f * Time.deltaTime;
            TitleCanvas.alpha = cAlpha;
        }
    }

    void StartCutscene()
    {
        cutController.Play();
        beaAnim.enabled = true;
        Invoke("BeaLook", 4f);
    }
    void BeaLook()
    {
        beaAnim.SetBool("Look", true);
    }

    public void onSceneStart(){
        startFade = true;
        Invoke("StartCutscene", 2.5f);
    }
}