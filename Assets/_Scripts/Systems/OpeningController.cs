using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private int skip = 0;
    [SerializeField] private GameObject skipText;

    private void Start()
    {
        StartCutscene();
    }

    void Update()
    {
        if (startFade)
        {
            cAlpha = Mathf.Lerp(1, 0, t);
            t += 0.75f * Time.deltaTime;
            TitleCanvas.alpha = cAlpha;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            skip++;
        }

        if (skip == 1)
        {
            skipText.SetActive(true);
        }

        if (skip >= 2)
        {
            SkipCutscene();
        }
    }

    void StartCutscene()
    {
        cutController.Play();
        beaAnim.enabled = true;
        Invoke("BeaLook", 55f);
    }

    void SkipCutscene()
    {
        cutController.Stop();
        cutController.gameObject.SetActive(false);
        GameManager.instance.playerObject.SetActive(true);
    }
    void BeaLook()
    {
        beaAnim.SetBool("Look", true);
    }

}