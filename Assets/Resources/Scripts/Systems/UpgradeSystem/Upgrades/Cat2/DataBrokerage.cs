using Services;
using UnityEngine;

namespace ShopUpgrades
{
    public class DataBrokerage : Upgrade, IUpgradeDayListener
    {
        public DataBrokerage()
        {
            Name = "Data Brokerage";
            Description = @"At the end of each day, all territories with over 50%
infection generate 1 Resources per 500
infected population";
            Cost = 80;
        }

        public void OnDayEnd(PlayerStats playerStats)
        {
            foreach (Territory t in Territory.AllTerritories) 
            {
                if (t.GetInfectedPercent(MachineType.ALL) > 0.5)
                {
                    playerStats.Resources += Mathf.RoundToInt((float)t.Population / 500);
                    t.MakeMessage("+R", Territory.MESSAGE_COLOR_RESOURCES);
                }
            }
        }

        public void OnDayStart(PlayerStats playerStats)
        {
            
        }
    }
}
