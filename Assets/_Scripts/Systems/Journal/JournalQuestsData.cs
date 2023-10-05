using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JournalQuestsData
{
    public Quest[] quests;
}
[System.Serializable]
public class Quest
{
    public string name;
    public bool unlocked;
    public Entry[] entries;
}
[System.Serializable]
public class Entry
{
    public bool unlocked;
    public string text;
    public string image;
}
