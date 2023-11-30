using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCustomEntry : MonoBehaviour
{
    public string questName;
    public int questIndex;

    public void UnlockEntryAt()
    {
        JournalManager.instance.UnlockEntryAt(questName, questIndex);
    }

    public void UnlockNextEntry()
    {
        JournalManager.instance.UnlockNextEntry(questName);
    }
}
