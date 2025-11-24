using System;

public interface IUpgradeActionListener 
{   
    public void OnActionStart(PlayerStats playerStats, ITurnAction action);

    public void OnActionEnd(PlayerStats playerStats, ITurnAction action);
}