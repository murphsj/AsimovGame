using System;

public abstract class InfectChangeUpgrade : Upgrade, IUpgradeActionListener
{
    public virtual void OnAction(PlayerStats playerStats, InfectChangeAction action)
    {
        
    }

    public virtual void AfterAction(PlayerStats playerStats, InfectChangeAction action)
    {
        
    }
    
    public void OnActionStart(PlayerStats playerStats, ITurnAction action)
    {
        if (action is InfectChangeAction qualifiedAction)
        {
            OnAction(playerStats, qualifiedAction);
        } else
        {
            throw new ArgumentException();
        }
    }

    public void OnActionEnd(PlayerStats playerStats, ITurnAction action)
    {
        if (action is InfectChangeAction qualifiedAction)
        {
            AfterAction(playerStats, qualifiedAction);
        } else
        {
            throw new ArgumentException();
        }
    }
}