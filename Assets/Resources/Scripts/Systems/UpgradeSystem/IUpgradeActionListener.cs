

using System;

public interface IUpgradeActionListener 
{   
    public void OnAction(PlayerStats playerStats, ITurnAction action);
}