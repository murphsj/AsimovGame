using System;
using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;

public class InfectChangeAction : ITurnAction
{
    private const float ANIMATION_TIME_SECONDS = 0.5f;
    private const float PAUSE_BETWEEN_SECONDS = 0.1f;
    private HashSet<Territory> territories;
    private AttackBars attackBars;
    int[] changeLevel;

    public InfectChangeAction(HashSet<Territory> territories, int[] changeLevel)
    {
        this.territories = territories;
        this.changeLevel = changeLevel;
    }

    public IEnumerator ChangeInfectionLevelsAnimated(Territory t, int[] changeLevel, float animationTime)
    {
        attackBars.UpdateBarProgress(t);
        yield return attackBars.Open();

        float timePassed = 0;
        float[] velocity = new float[3];
        float[] current = new float[3] { t.Infection[0], t.Infection[1], t.Infection[2] };

        while (timePassed < animationTime)
        {
            timePassed += Time.deltaTime;
            for (int i = 0; i < 3; i++)
            {
                current[i] = Mathf.SmoothDamp(
                    current[i],
                    t.Infection[i] + changeLevel[i],
                    ref velocity[i],
                    animationTime
                );

                t.Infection[i] = Mathf.RoundToInt(current[i]);
            }

            t.button.UpdateVisuals();
            attackBars.UpdateBarProgress(t);

            yield return null;
        }

        yield return attackBars.Close();
    }

    public void Start(TurnManager turnManager)
    {
        attackBars = ServiceLocator.Get<AttackBars>();
    }

    public IEnumerator Run(TurnManager turnManager)
    {
        foreach (Territory territory in territories)
        {
            yield return ChangeInfectionLevelsAnimated(
                territory, changeLevel, ANIMATION_TIME_SECONDS
            );

            yield return new WaitForSeconds(PAUSE_BETWEEN_SECONDS);
        }
    }

    public void End(TurnManager turnManager)
    {
        
    }
}