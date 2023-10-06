using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;
using System.Security.Cryptography.X509Certificates;

public class DialogueSystem : MonoBehaviour
{
    TextAsset dialoguesTextData; 
    [SerializeField] private GameObject answerBox, answer, dialogueCanvas;
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private float writingDelay;
    public DialoguesData dialogue;
    [SerializeField] private GameManager gameManager;
    public int GetLineCurrentIndex() {return lineIndex;}
    public int GetConvCurrentIndex() {return convIndex;}

    private int convIndex;
    private int lineIndex;

    void Start()
    {
    }

    public void StartConversation()
    {
        ProcessJSON();
        textComponent.text = string.Empty;
        dialogueCanvas.SetActive(true);
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
                currAns.GetComponent<Button>().onClick.AddListener(() => { answerClick(dialogue.conversations[convIndex].responses[temp].nextConvIndex); });
            }
        }
        else
        {
            convIndex = 0;
            lineIndex = 0;
            gameManager.setCameraState(CameraState.MoveState);
            dialogueCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Debug.Log("End of dialogue (this would be a good time to dissapear)");
        }
    }

    private void answerClick(int nextDialogue)
    {
        //Set next dialogue and reset text box
        convIndex = nextDialogue;
        textComponent.text = string.Empty;
        gameManager.inputManager.addDialogueInput();
        //Destroy all answers
        foreach (Transform child in answerBox.transform)
        {
            Destroy(child.gameObject);
        }
        StartDialogue();
    }

    private void ProcessJSON()
    {
        dialogue=JsonUtility.FromJson<DialoguesData>(dialoguesTextData.text);
    }

    public void setDialogue(TextAsset newDialogue)
    {
        dialoguesTextData = newDialogue;
    }
    public Dialogue getDialogue()
    {
        return dialogue.conversations[convIndex];
    }
}

