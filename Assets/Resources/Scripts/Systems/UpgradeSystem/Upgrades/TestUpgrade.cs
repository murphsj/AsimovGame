using UnityEngine;
using System;

public class TestUpgrade : InfectChangeUpgrade
{
    public override void OnAction(PlayerStats playerStats, InfectChangeAction action)
    {
        action.changeLevel[0] *= 3;
    }
}