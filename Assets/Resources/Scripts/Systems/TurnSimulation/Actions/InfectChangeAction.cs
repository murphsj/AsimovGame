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
    private PlayerStats playerStats;
    public int[] changeLevel;
    public bool isPlayer;

    /// <summary>
    /// Generates an InfectChangeAction for a territory's enemy attack
    /// (a turn event for that territory "defending" and lowering infection level)
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static InfectChangeAction MakeEnemyAttackAction(Territory t)
    {
        return new InfectChangeAction(t, new int[] {
            (int)(t.BaseResistance[0] * 0.01 * t.Population/3),
            (int)(t.BaseResistance[1] * 0.01 * t.Population/3),
            (int)(t.BaseResistance[2] * 0.01 * t.Population/3)
        }, false);
    }

    public InfectChangeAction(Territory territory, int[] changeLevel, bool isPlayer)
    {
        this.territory = territory;
        this.changeLevel = new int[3];
        this.isPlayer = isPlayer;
        // To avoid changing the original if changeLevel is modified
        Array.Copy(changeLevel, this.changeLevel, 3);
    }

    public IEnumerator ChangeInfectionLevelsAnimated(int[] changeLevel, float animationTime)
    {
        attackBars.UpdateBarProgress(territory);
        attackBars.MoveToTerritory(territory);

        float timePassed = 0;
        float[] start = new float[3] { territory.Infection[0], territory.Infection[1], territory.Infection[2] };
        float[] current = new float[3] { territory.Infection[0], territory.Infection[1], territory.Infection[2] };
        float[] target = new float[3] {
            territory.Infection[0] + changeLevel[0],
            territory.Infection[1] + changeLevel[1],
            territory.Infection[2] + changeLevel[2]
        };

        bool allSame = true;
        for (int i = 0; i < 3; i++)
        {
            if (Math.Clamp(target[i], 0, territory.Population/3) != current[i])
            {
                allSame = false;
                break;
            }
        }

        if (!allSame)
        {
            yield return attackBars.Open();

            while (timePassed < animationTime)
            {
                timePassed += Time.deltaTime;

                float lerpFactor = Mathf.SmoothStep(0f, 1f, timePassed / animationTime);
                
                for (int i = 0; i < 3; i++)
                {
                    current[i] = Mathf.Lerp(start[i], target[i], lerpFactor);
                    territory.SetInfectionLevel((MachineType)i, Mathf.RoundToInt(current[i]));
                }

                territory.button.UpdateVisuals();
                attackBars.UpdateBarProgress(territory);

                yield return null;
            }

            yield return attackBars.Close();
        }
    }

    public void Start(TurnManager turnManager)
    {
        attackBars = ServiceLocator.Get<AttackBars>();
        playerStats = ServiceLocator.Get<PlayerStats>();
    }

    public IEnumerator Run(TurnManager turnManager)
    {
        yield return ChangeInfectionLevelsAnimated(
            changeLevel, ANIMATION_TIME_SECONDS
        );

        if (!territory.ResourceGainTriggered && territory.GetInfectedPercent(MachineType.ALL) > 0.5)
        {
            territory.ResourceGainTriggered = true;
            playerStats.Resources += 20;
        }

        yield return new WaitForSeconds(PAUSE_BETWEEN_SECONDS);
    }

    public void End(TurnManager turnManager)
    {
        
    }
}