using UnityEngine;
using Services;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using Unity.VisualScripting;

/// <summary>
/// Handles simulation of the map.
/// </summary>
[RegisterService]
public class TurnManager : MonoBehaviour
{
    public UnityEvent AdvanceDay;

    private MapSelection mapSelection;
    private PlayerStats playerStats;
    private DialogueManager dialogueManager;
    private GameOverMessage gameOverMessage;
    private MapLoader loader;
    private Queue<ITurnAction> turnActions;
    private bool inDayCutscene = false;

    private void GenerateTurnActions()
    {
        turnActions.Clear();

        if (playerStats.Day == 1)
        {
            turnActions.Enqueue(new InfectChangeAction(Territory.AllTerritories[0], new int[3] {30, 30, 30}, true));
        }

        HashSet<Territory> toAttack = new HashSet<Territory>();

        // Pick enemy attack targets
        for (int i = 0; i < playerStats.EnemyAttackTargetCount; i++)
        {
            Territory target;
            do
            {
                target = Territory.AllTerritories[Random.Range(0, Territory.AllTerritories.Count)];
            } while (toAttack.Contains(target));
            toAttack.Add(target);
        }

        // Generate the player's attack action
        foreach (Territory target in mapSelection.SelectedTerritories)
        {
            turnActions.Enqueue(new InfectChangeAction(target, playerStats.AttackPower, true));
            if (toAttack.Contains(target))
            {
                // Enqueue the counterattack action early so it happens right after
                turnActions.Enqueue(InfectChangeAction.MakeEnemyAttackAction(target));
                toAttack.Remove(target);
            }
        }

        playerStats.AttacksMadeThisTurn = mapSelection.SelectedTerritories.Count;

        // Now we can add the enemy attack actions that are remaining
        foreach (Territory target in toAttack)
        {
            turnActions.Enqueue(InfectChangeAction.MakeEnemyAttackAction(target));
        }

        playerStats.AddNoticeProgress(1);
    }

    private IEnumerator RunTurnAction(ITurnAction action)
    {
        foreach (Upgrade upgrade in playerStats.Upgrades)
        {
            if (upgrade is IUpgradeActionListener listener)
            {
                listener.OnActionStart(playerStats, action);
            }
        }
        action.Start(this);
        yield return action.Run(this);
        action.End(this);
        foreach (Upgrade upgrade in playerStats.Upgrades)
        {
            if (upgrade is IUpgradeActionListener listener)
            {
                listener.OnActionEnd(playerStats, action);
            }
        }
    }

    private IEnumerator ProcessTurnActionQueue()
    {
        foreach (ITurnAction action in turnActions)
        {
            yield return RunTurnAction(action);
        }

        EndDayAdvance();
    }

    private void HandleOnDayAdvance()
    {
        if (inDayCutscene) return;

        foreach (Upgrade upgrade in playerStats.Upgrades)
        {
            if (upgrade is IUpgradeDayListener listener)
            {
                listener.OnDayStart(playerStats);
            }
        }

        playerStats.Day++;
        GenerateTurnActions();
        StartCoroutine(ProcessTurnActionQueue());
        mapSelection.ClearSelection();
        mapSelection.SelectionEnabled = false;
        inDayCutscene = true;
    }

    private void EndDayAdvance()
    {
        playerStats.Resources += 10;
        mapSelection.SelectionEnabled = true;
        inDayCutscene = false;

        foreach (Upgrade upgrade in playerStats.Upgrades)
        {
            if (upgrade is IUpgradeDayListener listener)
            {
                listener.OnDayEnd(playerStats);
            }
        }

        if (playerStats.Day == 50)
        {
            float perc = Territory.GetTotalInfected();

            if (perc > 0.9f)
            {
                DialogueData.currentEvent = DialogueData.eventType.winOver90;
            } else if (perc > 0.7f)
            {
                DialogueData.currentEvent = DialogueData.eventType.winOver70;
            } else
            {
                DialogueData.currentEvent = DialogueData.eventType.lose;
            }
            dialogueManager.StartDialogue();
            mapSelection.SelectionEnabled = false;
            inDayCutscene = true;
            gameOverMessage.ShowGameOver();
        }
    }

    void Awake()
    {
        loader = GetComponent<MapLoader>();
        AdvanceDay.AddListener(HandleOnDayAdvance);
        turnActions = new Queue<ITurnAction>();
    }

    void Start()
    {
        mapSelection = ServiceLocator.Get<MapSelection>();
        playerStats = ServiceLocator.Get<PlayerStats>();
        gameOverMessage = ServiceLocator.Get<GameOverMessage>();
        dialogueManager = ServiceLocator.Get<DialogueManager>();

        loader.LoadMap();
    }
}