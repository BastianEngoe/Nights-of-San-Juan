using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Xasu.HighLevel;

public class TrasguPuzzle2 : MonoBehaviour
{
    [HideInInspector] public int value = 1;

    [SerializeField] private TrasguPuzzle2Controller puzzleController;
    private bool isAnimating, isRotating;

    
    public void Rotate90Degrees()
    {
        if (isRotating)
        {
            return;
        }
        isRotating = true;
        transform.DOShakeRotation(0.5f, Vector3.up * 20f, 10, 10f, true, ShakeRandomnessMode.Harmonic).OnComplete((() => ActuallyRotate()));
    }

    private void ActuallyRotate()
    {
        if (isAnimating)
        {
            return;
        }
        isAnimating = true;
        transform.DORotate(new Vector3(0, -90f, 0f), 1.5f, RotateMode.LocalAxisAdd).OnComplete((() => UpdateValue()));
        isRotating = false;
    }

    private void UpdateValue()
    {
        if (value == 4)
        {
            value = 1;
        }
        else
        {
            value++;
        }

        isAnimating = false;
        puzzleController.checkCombination();
    }
}
