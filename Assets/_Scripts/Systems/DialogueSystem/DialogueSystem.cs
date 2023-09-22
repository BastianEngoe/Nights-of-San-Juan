using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] TextAsset eventTextData; //This variable should be removed and sent to the dialogue system
                                              //through events
    [SerializeField] private GameObject answerBox, answer;
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private float writingDelay;
    public Dialogue dialogue;
    private DialoguesData dialogueTest = new DialoguesData();

    public int GetCurrentIndex() {return index;}

    private int index;

    void Start()
    {
        ProcessJSON();
        textComponent.text = string.Empty;
        StartDialogue();
    }
    
    public bool nextLine(){
        if(textComponent.text == dialogue.lines[index].text){
                NextLine();
                return true;
        }
        else{
                StopAllCoroutines();
                textComponent.text = dialogue.lines[index].text;
                return false;
        }
    }

    /// <summary>
    /// Sets the index to 0 and starts typing the next line
    /// </summary>
    void StartDialogue(){

        index = 0;
        StartCoroutine(TypeLine());
    }

    /// <summary>
    /// Types each character of the string in an interval, determined by the Writing Delay property
    /// </summary>
    /// <returns></returns>
    IEnumerator TypeLine(){
        foreach(char c in dialogue.lines[index].text.ToCharArray()){
            textComponent.text += c;
            yield return new WaitForSeconds(writingDelay);
        }
    }

    /// <summary>
    /// Tries to move to the next line, or ends the dialogue
    /// </summary>
    void NextLine(){
        //this check should not be neccesary anymore but hey im scared
        if (index < dialogue.lines.Length - 1)
        
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
    }

    public void EndDialogue()
    {
        //If can respond activate check box and assign parameters to each answer
        if (dialogue.canRespond)
        {
            answerBox.SetActive(true);
            for(int i = 0; i < dialogue.responses.Length; i++)
            {
                GameObject currAns = Instantiate(answer, answerBox.transform);
                currAns.GetComponentInChildren<TextMeshProUGUI>().SetText(dialogue.responses[i].responseText);
                //dialogue.responses[i].response.Invoke(getNextDialogue());
            }
        }
        Debug.Log("End of dialogue (this would be a good time to dissapear)");
    }

    private void ProcessJSON()
    {
        dialogue=JsonUtility.FromJson<Dialogue>(eventTextData.text);
        dialogueTest=JsonUtility.FromJson<DialoguesData>(eventTextData.text);
    }

    private Dialogue getNextDialogue()
    {
        return dialogue;
    }
}

[System.Serializable]
public class DialoguesData
{
    public Dialogue[] dialoguesTest;
}