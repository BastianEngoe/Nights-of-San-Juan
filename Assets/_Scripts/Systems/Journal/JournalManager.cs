using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.TextCore.Text;
using Unity.VisualScripting;

[ExecuteInEditMode]
public class JournalManager : MonoBehaviour
{
    [SerializeField] private GameObject leftPage;
    [SerializeField] private GameObject rightPage;
    [SerializeField] private GameObject journalObject;
    [SerializeField] private UnityEngine.TextAsset journalData;

    [SerializeField] private GameObject imagePrefab;
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private JournalQuestsData journalQuestsData;
    private bool isActive;
    private string saveFile;
    private int currentDisplayedQuest = 0;

    public bool IsActive{
        get{
            return isActive;
        }
    }
    private void Awake()
    {
        saveFile = Application.persistentDataPath + "/QuestsData.json";
        Debug.Log(saveFile);

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
        UpdatePages();
    }

    public void QuitJournal()
    {
        journalObject.SetActive(false);
        isActive = false;
    }

    private void ProccesJournal()
    {

        if (!File.Exists(saveFile))
        {
            journalQuestsData = JsonUtility.FromJson<JournalQuestsData>(journalData.text);
            File.WriteAllText(saveFile, journalData.text);
        }
        else
        {
            journalQuestsData = JsonUtility.FromJson<JournalQuestsData>(File.ReadAllText(saveFile));
        }
    }
    private void UpdateJournal() //Still has to be tested
    {
        string jsonString = JsonUtility.ToJson(journalQuestsData);
        File.WriteAllText(saveFile, jsonString);
        File.WriteAllText(AssetDatabase.GetAssetPath(journalData), jsonString);
        EditorUtility.SetDirty(journalData);

    }

    public void TurnLeftPage() {
        if(currentDisplayedQuest > 0){
            currentDisplayedQuest -= 2;
            UpdatePages();
            Debug.Log("<---");
        } 
        else {
            Debug.Log("You are on the first page");
        }
    }

    public void TurnRightPage() {
        if(currentDisplayedQuest < journalQuestsData.quests.Length-1){
            currentDisplayedQuest += 2;
            UpdatePages();
            Debug.Log("<---");
        } 
        else {
            Debug.Log("You are on the last page");
        }      
        Debug.Log("--->");
    }
    private void UpdatePages()
    {
        foreach (Transform child in leftPage.transform)
            Destroy(child.gameObject);
        
        foreach (Transform child in rightPage.transform)
            Destroy(child.gameObject);
        
        //Update left page with currentDisplayedQuest
        PopulatePage(journalQuestsData.quests[currentDisplayedQuest], leftPage);
        //Update right page
        if(currentDisplayedQuest + 1 < journalQuestsData.quests.Length)
            PopulatePage(journalQuestsData.quests[currentDisplayedQuest + 1], rightPage);
    }

    private void PopulatePage(Quest quest, GameObject page)
    {
        foreach (Entry entry in quest.entries)
        {
            if (!entry.unlocked)
                continue;
            if (entry.text != "")
            {
                Debug.Log("Trying to print a text");
                AddTextSlot(page, entry.text);
            }
            if (entry.image != "")
            {
                Debug.Log("Trying to print an image");
                AddImageSlot(page, entry.image, 1f);
            }
            else Debug.Log("Hey, the entry you tried to print didn't work, at" + entry);
        }
    }



    ///<summary>
    ///<para>
    ///Adds an Image slot to the "page" Page.
    ///The filepath provided must be from a sprite in the Resources folder.
    ///</para>
    ///<para>
    ///If your sprite is located in
    ///</para>
    ///<para>
    ///Assets/Resources/Sprites/yourSprite.png
    ///</para>
    ///<para>
    ///the filepath value should look like
    ///</para>
    ///<para>
    ///"Sprites/yourSprite.png"
    ///</para>
    ///</summary>
    private void AddImageSlot(GameObject page, string filepath, float scale){


        GameObject newImageSlot = Instantiate(imagePrefab, page.transform);

        var imgObject = newImageSlot.GetComponent<Image>();
        imgObject.sprite = Resources.Load<Sprite>(filepath);
        


        newImageSlot.transform.localScale *= scale;
        
    
    }

    private void AddTextSlot(GameObject page, string text){
        GameObject newTextSlot = Instantiate(textPrefab, page.transform);
        newTextSlot.GetComponent<TMP_Text>().text = text;
    }




}
