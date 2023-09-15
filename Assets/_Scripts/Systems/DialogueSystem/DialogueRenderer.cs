using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private float writingDelay;
    [SerializeField] private Dialogue dialogue;



    private int index;

    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        //this has to be swapped to work with the Input system,
        //for now this is implemented for testing purposes

        if (Input.GetMouseButtonDown(0)){
            if(textComponent.text == dialogue.nodes[index].text){
                NextLine();
            }
            else{
                StopAllCoroutines();
                textComponent.text = dialogue.nodes[index].text;
            }
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
        foreach(char c in dialogue.nodes[index].text.ToCharArray()){
            textComponent.text += c;
            yield return new WaitForSeconds(writingDelay);
        }
    }

    /// <summary>
    /// Tries to move to the next line, or ends the dialogue
    /// </summary>
    void NextLine(){
        if (index < dialogue.nodes.Length - 1)
            {
                index++;
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        Debug.Log("End of dialogue (this would be a good time to dissapear)");
    }
}
