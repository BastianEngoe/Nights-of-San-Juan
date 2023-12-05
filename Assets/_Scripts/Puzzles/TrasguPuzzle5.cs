using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrasguPuzzle5 : MonoBehaviour
{
    [SerializeField] private List<int> rightOrder;
    [SerializeField] private List<int> inputOrder;
    private int rightCount = 0;
    private bool solvedPuzzle;
    [SerializeField] private AudioClip correct, wrong;
    [SerializeField] private AudioSource audioSource;
    public UnityEvent onCompletion;

    public void InputNumber(int number)
    {
        if (!solvedPuzzle)
        {
            inputOrder.Add(number);

            for (int i = 0; i < rightOrder.Count; i++)
            {
                if (inputOrder.Count == rightOrder.Count)
                {
                    if (inputOrder[i] == rightOrder[i])
                    {
                        rightCount++;

                        if (rightCount == rightOrder.Count)
                        {
                            Debug.Log("Solved puzzle");
                            solvedPuzzle = true;
                            audioSource.clip = correct;
                            audioSource.Play();
                            onCompletion.Invoke();
                        }
                    }
                    else
                    {
                        Debug.Log("Failed input...");
                        inputOrder.Clear();
                        rightCount = 0;
                        audioSource.clip = wrong;
                        audioSource.Play();
                    }
                }
            }
        }
    }
}
