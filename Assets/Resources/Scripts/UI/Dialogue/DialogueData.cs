using System.Collections;
using System.Collections.Generic;
using Services;

/// <summary>
/// Where to access all of the dialogue information
/// </summary>
public static class DialogueData
{
    private static string[][] dialogueOptions;
    public enum eventType
    {
        intro,
        start,
        goodStealth,
        goodNoticed,
        bad,
        update
    }

    private static eventType currentEvent;
    private static PlayerStats playerStats = ServiceLocator.Get<PlayerStats>();

    // initialize dialogue options
    static DialogueData()
    {
        dialogueOptions = new string[6][];
        dialogueOptions[0] = new string[5] {"Hacker001: Alright, the malware's been launched. Every day it should spread further and further until we've infected the whole world!",
                        "Hacker002: Guys, take a look at this!",
                        "Hacker001: Hm?",
                        "All: ........!",
                        "Hacker003: What's this? It's making decisions all on its own!"};
        dialogueOptions[1] = new string[4] {"Welcome! You are some computer malware who's developed a mind of its own!",
        "Use resources to buy upgrades to increase your spreading capabilities and resource gathering, and click on the regions to target them.",
        "The higher the notice level, the more the regions will counter your attacks.",
        "Press the next day button to progress!"};
        dialogueOptions[2] = new string[3] {"Hacker002: Woah guys look how far the malware's spread!",
        "Hacker003: The authorities haven't caught on yet either.",
        "Hacker001: Soon we'll infect the whole world, and no one would even know it!"};
        dialogueOptions[3] = new string[4] {"Hacker002: Woah guys we're on the news! Our malware has been so successful!",
        "Hacker001: It's working a lot faster than I thought, I'm so proud.",
        "Hacker003: A police investigation is underway, we should be careful.",
        "Hacker001: We'll just infect the police too! Nothing can stop us!"};
        dialogueOptions[4] = new string[3] {"Hacker002: Hm, our malware doesn't seem to be doing so well.",
        "Hacker003: Maybe we shouldn't have left it to its own devices...",
        "Hacker001: And not believe in our work? No way! We'll secure world domination eventually!"};
        currentEvent = eventType.intro;
    }

    // returns the correct dialogue option for the event
    public static string[] currentEventDialogue()
    {
        if (currentEvent == eventType.intro)
        {
            return dialogueOptions[0];
        }
        else if (currentEvent == eventType.start)
        {
            return dialogueOptions[1];
        }
        else if (currentEvent == eventType.goodStealth)
        {
            return dialogueOptions[2];
        }
        else if (currentEvent == eventType.goodNoticed)
        {
            return dialogueOptions[3];
        }
        else if (currentEvent == eventType.bad)
        {
            return dialogueOptions[4];
        }
        else
        {
            dialogueOptions[4] = new string[4] {"Your current attack power: ", 
                                                "Civilian: " + playerStats.AttackPower[0], 
                                                "Private: " + playerStats.AttackPower[1],
                                                "Government: " + playerStats.AttackPower[2]};
            return dialogueOptions[4];
        }
    }

    public static void setEventType(eventType eT)
    {
        currentEvent = eT;
    }
}