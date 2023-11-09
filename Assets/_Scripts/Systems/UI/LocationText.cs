using TMPro;
using UnityEngine;

public class LocationText : MonoBehaviour
{
    [SerializeField] private TMP_Text locationText;
    
    void CallLocation(string location)
    {
        locationText.text = location;
        
        
    }
}
