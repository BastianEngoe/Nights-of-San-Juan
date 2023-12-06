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

    public void PlayCredits()
    {
        MenusManager.instance.transform.GetChild(MenusManager.instance.transform.childCount-1).gameObject.SetActive(true);
    }
}
