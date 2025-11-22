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
    public int Day { get; private set; } = 0;

    private MapSelection mapSelection;
    private PlayerStats playerStats;
    private MapLoader loader;
    private Queue<ITurnAction> turnActions;

    private void GenerateTurnActions()
    {
        turnActions.Clear();

        if (Day == 1)
        {
            turnActions.Enqueue(new InfectChangeAction(Territory.AllTerritories[0], playerStats.AttackPower, true));
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

        // Now we can add the enemy attack actions that are remaining
        foreach (Territory target in toAttack)
        {
            turnActions.Enqueue(InfectChangeAction.MakeEnemyAttackAction(target));
        }
    }

    private IEnumerator RunTurnAction(ITurnAction action)
    {
        foreach (Upgrade upgrade in playerStats.Upgrades)
        {
            if (upgrade is IUpgradeActionListener listener)
            {
                listener.OnAction(playerStats, action);
            }
        }
        action.Start(this);
        yield return action.Run(this);
        action.End(this);
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
        Day++;
        GenerateTurnActions();
        StartCoroutine(ProcessTurnActionQueue());
        mapSelection.ClearSelection();
        mapSelection.SelectionEnabled = false;
    }

    private void EndDayAdvance()
    {
        mapSelection.SelectionEnabled = true;
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

        loader.LoadMap();
    }
}