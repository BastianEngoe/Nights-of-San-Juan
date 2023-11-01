using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.Events;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private GameObject answerBox, answer, dialogueCanvas;
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private float writingDelay;
    public DialoguesData dialogue;
    [SerializeField] private GameManager gameManager;
    public int GetLineCurrentIndex() {return lineIndex;}
    public int GetConvCurrentIndex() {return convIndex;}

    private int convIndex;
    private int lineIndex;

    //Cleans previous conversation data and starts the new one
    public void StartConversation()
    {
        textComponent.text = string.Empty;
        dialogueCanvas.SetActive(true);
        StartDialogue();
    }

    //If the text has already been written in the dialogue box it moves to the next conversation
    //else it writes the whole text on the text box
    public bool ProccessLine(){
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

    //Ends the current conversation branch and showcases the next answers,
    //if there are no answers then finsihes the whole conversation
    public void EndDialogue()
    {
        int numAnswers = dialogue.conversations[convIndex].responses.Length;
        //If there are answers move to next dialogue 
        if (numAnswers > 0)
        {
            answerBox.SetActive(true);
            for (int i = 0; i < numAnswers; i++)
            {
                //This variable seems useless but is necessary for lambda expressions memory
                //DONT REMOVE
                int temp = i;
                GameObject currAns = Instantiate(answer, answerBox.transform);
                currAns.GetComponentInChildren<TextMeshProUGUI>().SetText(dialogue.conversations[convIndex].responses[temp].responseText);
                currAns.GetComponent<Button>().onClick.AddListener(() => { AnswerClick(dialogue.conversations[convIndex].responses[temp].nextConvIndex); });
            }
        }
        else
        {
            convIndex = 0;
            lineIndex = 0;
            gameManager.SetCameraState(CameraState.MoveState);
            dialogueCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Debug.Log("End of dialogue (this would be a good time to dissapear)");
        }
    }

    //Event method assigned to every button, which correlates it to next a conversation branch
    private void AnswerClick(int nextDialogue)
    {
        //Set next dialogue and reset text box
        convIndex = nextDialogue;
        textComponent.text = string.Empty;
        gameManager.inputManager.AddDialogueInput();
        //Destroy all answers
        foreach (Transform child in answerBox.transform)
        {
            Destroy(child.gameObject);
        }
        StartDialogue();
    }


    public void setDialogue(TextAsset newDialogue)
    {
        dialogue = JsonUtility.FromJson<DialoguesData>(newDialogue.text);
    }

    public Dialogue getDialogue()
    {
        return dialogue.conversations[convIndex];
    }
}

