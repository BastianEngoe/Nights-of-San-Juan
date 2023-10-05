using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using UnityEngine.TextCore.Text;

public class JournalManager : MonoBehaviour
{
    [SerializeField] private GameObject journalObject;
    [SerializeField] private UnityEngine.TextAsset journalData;
    [SerializeField] private JournalQuestsData journalQuestsData;
    private bool isActive;

    public bool IsActive{
        get{
            return isActive;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ProccesJournal();
    }

    private void OnApplicationQuit()
    {
        UpdateJournal();
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
        File.WriteAllText(AssetDatabase.GetAssetPath(journalData), JsonUtility.ToJson(journalQuestsData));
        EditorUtility.SetDirty(journalData);
    }

    public void TurnLeftPage() {Debug.Log("<---");}
    public void TurnRightPage() {Debug.Log("--->");}

}
