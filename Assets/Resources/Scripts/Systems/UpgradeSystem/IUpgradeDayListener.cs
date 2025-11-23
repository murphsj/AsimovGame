using System;

public interface IUpgradeDayListener 
{   
    public void OnDayStart(PlayerStats playerStats);

    public void OnDayEnd(PlayerStats playerStats);
}