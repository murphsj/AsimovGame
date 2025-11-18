using System;
using System.Collections;
using System.Collections.Generic;
using Services;
using UnityEngine;

public class InfectChangeAction : ITurnAction
{
    private const float ANIMATION_TIME_SECONDS = 0.6f;
    private const float PAUSE_BETWEEN_SECONDS = 0.1f;
    private Territory territory;
    private AttackBars attackBars;
    int[] changeLevel;

    public InfectChangeAction(Territory territory, int[] changeLevel)
    {
        this.territory = territory;
        this.changeLevel = changeLevel;
    }

    public IEnumerator ChangeInfectionLevelsAnimated(Territory t, int[] changeLevel, float animationTime)
    {
        attackBars.UpdateBarProgress(t);
        attackBars.MoveToTerritory(t);
        yield return attackBars.Open();

        float timePassed = 0;
        float[] start = new float[3] { t.Infection[0], t.Infection[1], t.Infection[2] };
        float[] current = new float[3] { t.Infection[0], t.Infection[1], t.Infection[2] };
        float[] target = new float[3] {
            t.Infection[0] + changeLevel[0],
            t.Infection[1] + changeLevel[1],
            t.Infection[2] + changeLevel[2]
        };

        while (timePassed < animationTime)
        {
            timePassed += Time.deltaTime;

            float lerpFactor = Mathf.SmoothStep(0f, 1f, timePassed / animationTime);
            
            for (int i = 0; i < 3; i++)
            {
                current[i] = Mathf.Lerp(start[i], target[i], lerpFactor);
                t.SetInfectionLevel((MachineType)i, Mathf.RoundToInt(current[i]));
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
        yield return ChangeInfectionLevelsAnimated(
            territory, changeLevel, ANIMATION_TIME_SECONDS
        );

        yield return new WaitForSeconds(PAUSE_BETWEEN_SECONDS);
    }

    public void End(TurnManager turnManager)
    {
        
    }
}