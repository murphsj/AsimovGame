using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private Image DialogueBox;
    [SerializeField]
    private TextMeshProUGUI dialogueText;

    // the dialogue
    private string[] introDialogue;
    [SerializeField]
    private string[] firstEvent;

    // variables to change progression speed
    [SerializeField]
    private float textSpeed;
    private float nextLineSpeed;
    private int index;

    private bool messageSent;
    private bool lineSent;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialogueText.text = string.Empty;
        messageSent = false;
        lineSent = false;
        nextLineSpeed = 1f;

        introDialogue = new string[5] {"Hacker001: Alright, the malware's been launched. Every day it should spread further and further until we've infected the whole world!",
                        "Hacker002: Guys, take a look at this!",
                        "Hacker001: Hm?",
                        "........!",
                        "Hacker003: What's this? It's making decisions all on its own!"};
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (!messageSent)
        {
            if (lineSent) //after each line completes being written out
            {
                lineSent = false;
                NextLine();
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        dialogueText.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        char[] dialogueChars = introDialogue[index].ToCharArray();
        for (int i = 0; i < dialogueChars.Length; i++)
        {
            dialogueText.text += dialogueChars[i];
            if (i == dialogueChars.Length - 1)
            {
                yield return new WaitForSeconds(nextLineSpeed);
            }
            else
            {
                yield return new WaitForSeconds(textSpeed);
            }
        }
        // foreach (char c in introDialogue[index].ToCharArray())
        // {
        //     dialogueText.text += c;
        //     yield return new WaitForSeconds(textSpeed);
        // }
        lineSent = true;
    }

    private void NextLine()
    {
        if(index < introDialogue.Length - 1)
        {
            index++;
            dialogueText.text += "\n";
            StartCoroutine(TypeLine());
        }
        else
        {
            messageSent = true;
        }
    }
}
