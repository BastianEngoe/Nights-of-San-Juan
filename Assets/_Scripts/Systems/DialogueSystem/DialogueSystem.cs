using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Newtonsoft.Json;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] TextAsset dialoguesTextData; //This variable should be removed and sent to the dialogue system
                                                  //through events
    [SerializeField] private GameObject answerBox, answer;
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private float writingDelay;
    public DialoguesData dialogue;

    public int GetLineCurrentIndex() {return lineIndex;}
    public int GetConvCurrentIndex() {return convIndex;}

    private int convIndex;
    private int lineIndex;

    void Start()
    {
        ProcessJSON();
        textComponent.text = string.Empty;
        StartDialogue();
    }
    
    public bool nextLine(){
        if(textComponent.text == dialogue.conversations[convIndex].lines[lineIndex].text){
                NextLine();
                return true;
        }
        else{
                StopAllCoroutines();
                textComponent.text = dialogue.conversations[convIndex].lines[lineIndex].text;
                return false;
        }
    }

    /// <summary>
    /// Sets the index to 0 and starts typing the next line
    /// </summary>
    public void StartDialogue(){

        lineIndex = 0;
        StartCoroutine(TypeLine());
    }

    /// <summary>
    /// Types each character of the string in an interval, determined by the Writing Delay property
    /// </summary>
    /// <returns></returns>
    IEnumerator TypeLine(){
        foreach(char c in dialogue.conversations[convIndex].lines[lineIndex].text.ToCharArray()){
            textComponent.text += c;
            yield return new WaitForSeconds(writingDelay);
        }
    }

    /// <summary>
    /// Tries to move to the next line, or ends the dialogue
    /// </summary>
    void NextLine(){
        lineIndex++;
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    public void EndDialogue()
    {
        int numAnswers = dialogue.conversations[convIndex].responses.Length;
        //If can respond activate check box and assign parameters to each answer
        if (numAnswers>0)
        {
            answerBox.SetActive(true);
            for (int i = 0; i < numAnswers; i++)
            {
                GameObject currAns = Instantiate(answer, answerBox.transform);
                currAns.GetComponentInChildren<TextMeshProUGUI>().SetText(dialogue.conversations[convIndex].responses[i].responseText);
                currAns.GetComponent<Button>().onClick.AddListener(() => { answerClick(dialogue.conversations[convIndex].responses[i].nextConvIndex); });
            }
        }
        Debug.Log("End of dialogue (this would be a good time to dissapear)");
    }

    private void answerClick(int nextDialogue)
    {
        convIndex = nextDialogue;
        Debug.Log(nextDialogue);
    }

    private void ProcessJSON()
    {
        dialogue=JsonUtility.FromJson<DialoguesData>(dialoguesTextData.text);
    }

    public Dialogue getDialogue()
    {
        return dialogue.conversations[convIndex];
    }

    public void click()
    {
        Debug.Log("clicked");
    }
}

