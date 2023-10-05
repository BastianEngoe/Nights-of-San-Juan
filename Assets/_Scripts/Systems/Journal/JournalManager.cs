using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs.LowLevel.Unsafe;
using System;
using UnityEditor;
using UnityEngine.TextCore.Text;
using Unity.VisualScripting;

public class JournalManager : MonoBehaviour
{
    [SerializeField] private GameObject journalObject;
    [SerializeField] private UnityEngine.TextAsset journalJSON;
    [SerializeField] private JournalQuestsData journalData;
    private bool isActive;

    public bool IsActive{
        get{
            return isActive;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //journalQuestsData = new JournalSaveData();
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
        //try
        //{
        //    journalData = journalQuestsData.loadJSON();
        //}
        //catch
        //{
            journalData = JsonUtility.FromJson<JournalQuestsData>(journalJSON.text);
        //}

    }
    private void UpdateJournal() //Still has to be tested
    {
    //    journalQuestsData.saveJSON();
        File.WriteAllText(AssetDatabase.GetAssetPath(journalJSON), JsonUtility.ToJson(journalData).Prettify());
    //    EditorUtility.SetDirty(journalJSON);
    }

    public void TurnLeftPage() {Debug.Log("<---");}
    public void TurnRightPage() {Debug.Log("--->");}

}
