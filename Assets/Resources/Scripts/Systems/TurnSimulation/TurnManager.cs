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
        turnActions.Enqueue(new InfectChangeAction(
            new HashSet<Territory>(mapSelection.SelectedTerritories), playerStats.AttackPower
        ));
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
    }

    private void HandleOnDayAdvance()
    {
        Day++;
        GenerateTurnActions();
        StartCoroutine(ProcessTurnActionQueue());
        mapSelection.ClearSelection();
        mapSelection.SelectionEnabled = false;
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