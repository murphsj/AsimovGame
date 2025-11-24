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
        char[] dialogueChars = DialogueData.currentEventDialogue().ToCharArray();
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
        if(index < DialogueData.currentEventDialogue().Length - 1)
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
