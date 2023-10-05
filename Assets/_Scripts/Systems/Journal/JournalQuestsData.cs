using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

//public class JournalSaveData
//{
//    public JournalQuestsData journalQuests = new JournalQuestsData();
//    public void saveJSON()
//    {
//        string savePath = Application.persistentDataPath + "/QuestsData.json";

//        Debug.Log(savePath);
//        string saveJournalData = JsonUtility.ToJson(journalQuests);
//        File.WriteAllText(savePath, saveJournalData);
//    }

//    public JournalQuestsData loadJSON()
//    {
//        string loadPath = Application.persistentDataPath + "/QuestsData.json";
//        string data = File.ReadAllText(loadPath);

//        journalQuests = JsonUtility.FromJson<JournalQuestsData>(data);

//        return journalQuests;
//    }
//} 

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
    public bool editable;
    public string text;
    public string image;
}
