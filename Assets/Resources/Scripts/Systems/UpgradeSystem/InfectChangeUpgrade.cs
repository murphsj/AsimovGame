using System;

public abstract class InfectChangeUpgrade : Upgrade, IUpgradeActionListener
{
    public abstract void OnAction(PlayerStats playerStats, InfectChangeAction action);
    
    public void OnAction(PlayerStats playerStats, ITurnAction action)
    {
        if (action is InfectChangeAction qualifiedAction)
        {
            OnAction(playerStats, qualifiedAction);
        } else
        {
            throw new ArgumentException();
        }
    }
}