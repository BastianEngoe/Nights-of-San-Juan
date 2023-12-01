using UnityEngine;
using UnityEngine.Events;

public class CuelebereQuestController : MonoBehaviour
{
    public UnityEvent eventToTrigger;
    public void CheckInventory()
    {
        if (GameManager.instance.XanaArtifact >= 1)
        {
            eventToTrigger.Invoke();
        }
    }
}
