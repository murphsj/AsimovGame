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
        update,
        winOver90,
        winOver70,
        lose
    }

    private static eventType currentEvent;
    private static PlayerStats playerStats = ServiceLocator.Get<PlayerStats>();

    // initialize dialogue options
    static DialogueData()
    {
        dialogueOptions = new string[9][];
        // intro dialogue
        dialogueOptions[0] = new string[5] {"Hacker001: Alright, the malware's been launched. Every day it should spread further and further until we've infected the whole world!",
        "Hacker002: Guys, take a look at this!",
        "Hacker001: Hm?",
        "All: ........!",
        "Hacker003: What's this? It's making decisions all on its own!"};

        // explanation/tutorial dialogue
        dialogueOptions[1] = new string[4] {"Welcome! You are a computer virus who's developed a mind of its own!",
        "Use resources to buy upgrades to increase your spreading capabilities and resource gathering, and click on the regions to target them.",
        "The higher the notice level, the more the regions will counter your attacks.",
        "Press the next day button to progress!"};

        // currently winning and notice level is low dialogue
        dialogueOptions[2] = new string[3] {"Hacker002: Woah guys look how far the malware's spread!",
        "Hacker003: The authorities haven't caught on yet either.",
        "Hacker001: Soon we'll infect the whole world, and no one would even know it!"};

        // currently winning and notice level is high dialogue
        dialogueOptions[3] = new string[4] {"Hacker002: Woah guys we're on the news! Our malware has been so successful!",
        "Hacker001: It's working a lot faster than I thought, I'm so proud.",
        "Hacker003: A police investigation is underway, we should be careful.",
        "Hacker001: We'll just infect the police too! Nothing can stop us!"};

        // currently losing dialogue
        dialogueOptions[4] = new string[3] {"Hacker002: Hm, our malware doesn't seem to be doing so well.",
        "Hacker003: Maybe we shouldn't have left it to its own devices...",
        "Hacker001: And not believe in our work? No way! We'll secure world domination eventually!"};

        // 90% infected dialogue
        dialogueOptions[6] = new string[6] {"Hacker001: Wow guys, we did it. We've achieved world domination!",
        "Hacker002: Now not even the authorities can touch us!",
        "Hacker003: What will we do now?",
        "Hacker002: Let's have a pizza party!",
        "Hacker001: Good idea! And we don't even have to pay for it!",
        "ENDING.TYPE = WORLD DOMINATION"};

        // 70% infected dialogue
        dialogueOptions[7] = new string[5] {"Hacker001: Alright guys, I think it's time we pivot to plan B.",
        "Hacker003: Got it, I have it prepared now.",
        "Hacker002: Oh I can't wait to see this one on the news.",
        "Hacker001: Perfect. Send out the image to all infected devices!",
        "ENDING.TYPE = PRANK VIRUS"};

        // lose dialogue
        dialogueOptions[8] = new string[4] {"Hacker003: The police have found our location.",
        "Hacker002: Well shit. We're cooked.",
        "Hacker001: Guess it's time to pack it up. Hit the kill switch!",
        "ENDING.TYPE = FAILURE"};

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

    public static void updateEventType()
    {
        if (currentEvent == eventType.intro)
        {
            currentEvent = eventType.start;
        }
        else // set dialogue option to neutral option if it has not been set to anything else
        {
            currentEvent = eventType.update; 
        }
    }

    // overloaded version to update the currentEvent directly
    public static void updateEventType(eventType eT)
    {
        currentEvent = eT;
    }
    public static eventType getCurrentEventType()
    {
        return currentEvent;
    }
}