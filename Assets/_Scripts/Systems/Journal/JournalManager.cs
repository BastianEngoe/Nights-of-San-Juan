using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalManager : MonoBehaviour
{
    [SerializeField] private GameObject journalObject;
    [SerializeField] private TextAsset journalData;
    private JournalQuestsData journalQuestsData;
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        ProccesJournal();
    }

    private void OnApplicationQuit()
    {
        //UpdateJournal();
    }

    public void ShowJournal()
    {
        journalObject.SetActive(true);
        isActive = true;
    }

    public void QuitJournal()
    {
        journalObject.SetActive(false);
        isActive = false;
    }

    private void ProccesJournal()
    {
        journalQuestsData = JsonUtility.FromJson<JournalQuestsData>(journalData.text);
    }
    private void UpdateJournal() //Still has to be tested
    {
        string saveFilePath = Application.persistentDataPath + "Assets/JSONs/QuestsData/QuestsData.json";
        string saveJournalData = JsonUtility.ToJson(journalQuestsData);
        File.WriteAllText(saveFilePath, saveJournalData);
    }

}
