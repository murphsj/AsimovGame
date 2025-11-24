using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Services;

/// <summary>
/// Displays the dialogue in the popup dialogue window after an event occurs
/// </summary>
public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private GameObject DialogueBox;
    [SerializeField]
    private TextMeshProUGUI dialogueText;

    // variables to change progression speed
    [SerializeField]
    private float textSpeed;
    private float nextLineSpeed;
    private int index;

    private bool messageSent;
    private bool lineSent;

    private bool messageDismissed; //to indicate when the next message can pop up

    private PlayerStats playerStats;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerStats = ServiceLocator.Get<PlayerStats>();
        dialogueText.text = string.Empty;
        messageSent = false;
        lineSent = false;
        messageDismissed = false;
        nextLineSpeed = 1f;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (!messageSent)
        {
            if (lineSent) //after each line completes being written out, then it starts writing the next line
            {
                lineSent = false;
                Debug.Log("line sent");
                NextLine();
            }
        }

        if (messageDismissed && playerStats.Day == 0 && DialogueData.getCurrentEventType() == DialogueData.eventType.start)
        {
            StartDialogue();
        }

        if (messageDismissed && playerStats.Day == 50)
        {
            // change the current event type to the correct one based on infection level here
            StartDialogue();
        }

        if (!DialogueBox.activeSelf)
        {
            messageDismissed = true;
        }
    }

    public void StartDialogue()
    {
        Debug.Log("Pop up!");
        index = 0;
        dialogueText.text = string.Empty;
        DialogueBox.SetActive(true);
        messageDismissed = false;
        messageSent = false;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        string[] currentDialogue = DialogueData.currentEventDialogue();
        char[] dialogueChars = currentDialogue[index].ToCharArray();
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

        lineSent = true;
    }

    private void NextLine()
    {
        if(index < DialogueData.currentEventDialogue().Length - 1)
        {
            Debug.Log("next line");
            index++;
            dialogueText.text += "\n";
            StartCoroutine(TypeLine());
        }
        else
        {
            messageSent = true;
            DialogueData.updateEventType();
        }
    }
}
