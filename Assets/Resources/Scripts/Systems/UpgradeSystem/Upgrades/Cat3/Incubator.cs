using UnityEngine;

namespace ShopUpgrades
{
    public class Incubator : Upgrade, IUpgradeDayListener
    {
        public Incubator()
        {
            Name = "Incubator";
            Description = @"If you end a day making no direct attacks, all territories with >0%
infection gain 15% CIV and COM infection";
            Cost = 150;
        }

        public void OnDayEnd(PlayerStats playerStats)
        {
            if (playerStats.AttacksMadeThisTurn > 0) return;
            
            foreach (Territory t in Territory.AllTerritories)
            {
                if (t.GetInfectedPercent(MachineType.ALL) > 0)
                {
                    t.SetInfectionLevel(MachineType.Com, t.Infection[1] + Mathf.RoundToInt((t.Population/3)*0.15f));
                    t.SetInfectionLevel(MachineType.Civ, t.Infection[0] + Mathf.RoundToInt((t.Population/3)*0.15f));
                }

                t.button.UpdateVisuals();
            }
        }

        public void OnDayStart(PlayerStats playerStats)
        {
            
        }
    }
}
