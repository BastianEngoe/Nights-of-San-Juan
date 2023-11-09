using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TrasguPuzzle2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rotate90Degrees()
    {
        transform.DOShakeRotation(0.5f, Vector3.up * 20f, 10, 10f, true, ShakeRandomnessMode.Harmonic)
            .OnComplete(() => ActuallyRotate());
    }

    private void ActuallyRotate()
    {
        transform.DORotate(new Vector3(0, -90f, 0f), 1.5f, RotateMode.LocalAxisAdd);
    }
}
