using DG.Tweening;
using TMPro;
using UnityEngine;

public class PickupText : MonoBehaviour
{
    [SerializeField] private TMP_Text pickupText;
    
    public void ItemText(string item)
    {
        pickupText.text = "Picked up " + item;

        pickupText.DOFade(1f, 0f);
        pickupText.rectTransform.DOMove(new Vector3(pickupText.rectTransform.position.x, pickupText.rectTransform.position.y + 200, pickupText.rectTransform.position.x), 2f);
        pickupText.DOFade(0f, 3f);
    }
}
