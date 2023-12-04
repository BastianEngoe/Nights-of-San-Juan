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

//[ExecuteInEditMode]
public class JournalManager : MonoBehaviour
{
    [SerializeField] private TraceManager traceManager;
    
    [SerializeField] private GameObject leftPage;
    [SerializeField] private GameObject rightPage;
    [SerializeField] private GameObject journalObject;
    [SerializeField] private GameObject journalTutText;
    [SerializeField] private UnityEngine.TextAsset journalData;

    [SerializeField] private GameObject imagePrefab;
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private GameObject titlePrefab;
    [SerializeField] private GameObject indexItemPrefab;
    [SerializeField] private JournalQuestsData journalQuestsData;
    [SerializeField] private Animator animator;
    private bool isActive;
    private string saveFile;
    private int currentDisplayedQuest = 0;

    public static JournalManager instance;

    public bool IsActive{
        get{
            return isActive;
        }
    }
    private void Awake()
    {
        traceManager = FindObjectOfType<TraceManager>();
        saveFile = Application.persistentDataPath + "/QuestsData.json";
        //Uncomment to find your current file route
        saveFile = Application.persistentDataPath + "/QuestsData.json";
        //Debug.Log(saveFile);
        animator = journalObject.GetComponent<Animator>();
        if(instance==null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ProccesJournal();
    }

    //When you exit the game it saves the data changed of the journal
    //MOVE TO SAVE SYSTEM
    private void OnApplicationQuit()
    {
        UpdateJournal();
    }

    //Activates the pages object and updates the info
    public void ShowJournal()
    {
        traceManager.AccessedJournal();
        journalObject.SetActive(true);
        isActive = true;
        UpdatePages();
        Invoke("EnablePages", 1f);
    }

    //Activates the gameobject of both pages
    public void EnablePages()
    {
        leftPage.SetActive(true);
        rightPage.SetActive(true);
        journalTutText.SetActive(true);
    }

    //Disables the gameobject of both pages
    public void DisablePages()
    {
        leftPage.SetActive(false);
        rightPage.SetActive(false);
        journalTutText.SetActive(false);
    }

    //Closes the journal
    public void QuitJournal()
    {
        animator.SetTrigger("closeJournal");
        DisablePages();
        Invoke("DisableJournal", 1f);
    }

    //Disables the journal object with a delay so it has time to perform the animation
    public void DisableJournal()
    {
        journalObject.SetActive(false);
        isActive = false;
    }

    //Manages the journal info
    private void ProccesJournal()
    {
        //If there is not a save file
        if (!File.Exists(saveFile))
        {
            journalQuestsData = JsonUtility.FromJson<JournalQuestsData>(journalData.text);
            File.WriteAllText(saveFile, journalData.text);
        }
        //If there is a save file but is not the same as the text asset (Edited by designers) Text asset takes priority
        else if(saveFile!=journalData.text)
        {
            File.WriteAllText(saveFile,journalData.text);
            journalQuestsData = JsonUtility.FromJson<JournalQuestsData>(journalData.text);
        }
        //If both match
        else
        {
            journalQuestsData = JsonUtility.FromJson<JournalQuestsData>(File.ReadAllText(saveFile));
        }
    }

    //Updates the journal current data and safe data
    public void UpdateJournal()
    {
        string jsonString = JsonUtility.ToJson(journalQuestsData);
        File.WriteAllText(saveFile, jsonString);
        //File.WriteAllText(AssetDatabase.GetAssetPath(journalData), jsonString);
        //EditorUtility.SetDirty(journalData);
    }

    //Toggles between pages, as there are two pages showcased on the journal it progresses +-2
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
        
        //Update left page with currentDisplayedQuest. If on first page, populate with the Index
        if(currentDisplayedQuest == 0){
            Debug.Log("index!");
            PopulateIndexPage();
        } else{
            Debug.Log("not index!");
            PopulatePage(journalQuestsData.quests[currentDisplayedQuest-1], leftPage);
        }
        //Update right page
        if(currentDisplayedQuest + 1 < journalQuestsData.quests.Length)
            PopulatePage(journalQuestsData.quests[currentDisplayedQuest], rightPage);
    }

    private void PopulateIndexPage()
    {
        GameObject newTextSlot = Instantiate(titlePrefab, leftPage.transform);
        newTextSlot.GetComponent<TMP_Text>().text = "Index";
        //Always populates the left page with the same object, so no need to have them passed to the function
        for (int i = 0; i < journalQuestsData.quests.Length; i++)
        {
            var indexItem = Instantiate(indexItemPrefab, leftPage.transform);
            //if the quest is unlocked, show the title text
            var indexText = indexItem.transform.GetChild(1).GetComponent<TMP_Text>();
            var indexCheckmark = indexItem.transform.GetChild(0).GetComponent<Toggle>();

            if(journalQuestsData.quests[i].unlocked){
                indexCheckmark.enabled = true;
                indexText.text = journalQuestsData.quests[i].name;
            }
            else{
                indexCheckmark.enabled = false;
                for (int j = 0; j < journalQuestsData.quests[i].name.Length; j++)
                {
                    indexText.text += "?";
                }
            }


        }
    }

    //Adds the text and images to page
    private void PopulatePage(Quest quest, GameObject page)
    {
        if(quest.unlocked)
        AddTitleSlot(page, quest.name);

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
    ///"Sprites/yourSprite"
    ///</para>
    ///</summary>
    private void AddImageSlot(GameObject page, string filepath, float scale){
        GameObject newImageSlot = Instantiate(imagePrefab, page.transform);

        Image imgObject = newImageSlot.GetComponent<Image>();
        imgObject.sprite = Resources.Load<Sprite>(filepath);
    }



    private void AddTextSlot(GameObject page, string text){
        GameObject newTextSlot = Instantiate(textPrefab, page.transform);
        newTextSlot.GetComponent<TMP_Text>().text = text;
    }
    private void AddTitleSlot(GameObject page, string text){
        GameObject newTextSlot = Instantiate(titlePrefab, page.transform);
        newTextSlot.GetComponent<TMP_Text>().text = text;
    }

    //Unlocks the quest with the asked name
    public void UnlockQuest(string questName)
    {
        int questIndex= SearchForQuest(questName);
        journalQuestsData.quests[questIndex].unlocked = true;
        if (journalQuestsData.quests[questIndex].entries.Length > 0)
        {
            journalQuestsData.quests[questIndex].entries[0].unlocked = true;
        }

        currentDisplayedQuest = questIndex - (questIndex % 2);
    }

    public void UnlockEntryAt(string questName, int index) {
        int questIndex = SearchForQuest(questName);
        journalQuestsData.quests[questIndex].unlocked = true;
        journalQuestsData.quests[questIndex].entries[index].unlocked = true;
        GameManager.instance.ShowQuill();

    }

    public void UnlockNextEntry(string questName)
    {
        int questIndex = SearchForQuest(questName);
        int entryNum= journalQuestsData.quests[questIndex].entries.Length;
        int currEnt = 0;
        bool foundNext = false;
        journalQuestsData.quests[questIndex].unlocked = true;
        if (currEnt<entryNum&&!foundNext&&journalQuestsData.quests[questIndex].unlocked)
        {
            if (!journalQuestsData.quests[questIndex].entries[currEnt].unlocked)
            {
                journalQuestsData.quests[questIndex].entries[currEnt].unlocked = true;
                foundNext = true;
                GameManager.instance.ShowQuill();
            }
            else
            {
                currEnt++;
            }
        }
    }

    //Tries to find the quest with a certain name and returns the index of its location
    private int SearchForQuest(string questName)
    {
        int index = -1;
        bool found = false;
        int i = 0;

        while(!found && i < journalQuestsData.quests.Length){
            if(journalQuestsData.quests[i].name == questName){
                found = true;
                index = i;
            }
            i++;
        }

        return index;
    }
}
