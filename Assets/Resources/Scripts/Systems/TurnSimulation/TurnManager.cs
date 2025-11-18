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
        // Generate the player's attack action
        foreach (Territory t in mapSelection.SelectedTerritories)
        {
            turnActions.Enqueue(new InfectChangeAction(t, playerStats.AttackPower));
        }

        // Generate counterattack
        for (int i = 0; i < playerStats.EnemyAttackTargetCount; i++)
        {
            Territory target = Territory.AllTerritories[Random.Range(0, Territory.AllTerritories.Count)];
            turnActions.Enqueue(new InfectChangeAction(target, target.BaseResistance));
        }
    }

    private IEnumerator RunTurnAction(ITurnAction action)
    {
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