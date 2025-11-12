using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectChangeAction : ITurnAction
{
    private const float ANIMATION_TIME_SECONDS = 0.5f;
    private const float PAUSE_BETWEEN_SECONDS = 0.1f;
    private HashSet<Territory> territories;
    int[] changeLevel;

    public InfectChangeAction(HashSet<Territory> territories, int[] changeLevel)
    {
        this.territories = territories;
        this.changeLevel = changeLevel;
    }

    public void Start(TurnManager turnManager)
    {

    }

    public IEnumerator Run(TurnManager turnManager)
    {
        foreach (Territory territory in territories)
        {
            Debug.Log(territory.Name);
            yield return territory.ChangeInfectionLevelsAnimated(
                changeLevel, ANIMATION_TIME_SECONDS
            );

            yield return new WaitForSeconds(PAUSE_BETWEEN_SECONDS);
        }
    }

    public void End(TurnManager turnManager)
    {
        
    }
}