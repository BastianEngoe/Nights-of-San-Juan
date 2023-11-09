using DG.Tweening;
using TMPro;
using UnityEngine;

public class LocationText : MonoBehaviour
{
    [SerializeField] private TMP_Text locationText;
    
    public void CallLocation(string location)
    {
        locationText.text = location;
        
        locationText.DOFade(1f, 1.5f).OnComplete(() => 
            locationText.DOColor(Color.yellow, 1.5f).OnComplete(() => 
                locationText.DOFade(0f, 1.5f)));
    }
}
