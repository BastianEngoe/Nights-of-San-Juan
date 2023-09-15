using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueRenderer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private float writingDelay;
    public Dialogue dialogue {get; set;}



    private int index;

    void Start()
    {
        DebugSomeStuff();
        textComponent.text = string.Empty;
        StartDialogue();
    }

    void Update()
    {
        //this has to be swapped to work with the Input system,
        //for now this is implemented for testing purposes

        if (Input.GetMouseButtonDown(0)){
            if(textComponent.text == dialogue.strings[index]){
                NextLine();
            }
            else{
                StopAllCoroutines();
                textComponent.text = dialogue.strings[index];
            }
        }
    }
    void DebugSomeStuff(){

        string[] strings = {
            "Hey! Did you know that Final Fantasy XIV has a free trial?",
            "It lets you play through the base game A Realm Reborn",
            "AND the award-winning expansion Heavensward",
            "AND the award-winning expansion Stormblood",
            "until lvl 70 with no restrictions on playtime!",
            "Create an account and enjoy Eorzea TODAY!"
            };
        dialogue = new Dialogue(strings);
    } 

    void StartDialogue(){
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine(){
        foreach(char c in dialogue.strings[index].ToCharArray()){
            textComponent.text += c;
            yield return new WaitForSeconds(writingDelay);
        }
    }

    void NextLine(){
        if (index < dialogue.strings.Length - 1)
            {
                index++;
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else {
                Debug.Log("End of dialogue (this would be a good time to dissapear)");
            }
    }
}
