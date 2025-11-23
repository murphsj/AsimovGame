using UnityEngine;

namespace ShopUpgrades
{
    public class ViralInfection : Upgrade, IUpgradeDayListener
    {
        public ViralInfection()
        {
            Name = "Viral Infection";
            Description = @"Each day, territories deal 1 infection per 10% of infection in each damage type to all neighbors";
            Cost = 200;
        }

        public void OnDayEnd(PlayerStats playerStats)
        {
            foreach (Territory t in Territory.AllTerritories) 
            {
                foreach (Territory neighbor in t.Neighbors)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        int newLevel = neighbor.Infection[i] + Mathf.RoundToInt(t.GetInfectedPercent((MachineType)i) * 10);
                        neighbor.SetInfectionLevel(
                            (MachineType)i, newLevel
                        );
                    }

                    neighbor.button.UpdateVisuals();
                }
            }
        }

        public void OnDayStart(PlayerStats playerStats)
        {
            
        }
    }
}
