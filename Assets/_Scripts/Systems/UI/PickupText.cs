using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PickupText : MonoBehaviour
{
    [SerializeField] private TMP_Text pickupText;

    private Vector3 startPosition;

    public void Awake()
    {
        startPosition = transform.position;
    }

    public void ItemText(string item)
    {
        pickupText.text = "Picked up " + item;

        pickupText.DOFade(1f, 0f);
        pickupText.rectTransform.DOMove(new Vector3(pickupText.rectTransform.position.x, pickupText.rectTransform.position.y + 200, pickupText.rectTransform.position.x), 2f);
        pickupText.DOFade(0f, 3f).OnComplete((() => transform.position = startPosition));
    }
}
