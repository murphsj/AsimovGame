using UnityEngine;

namespace ShopUpgrades
{
    public class AttackVector : Upgrade, IUpgradeDayListener
    {
        public AttackVector()
        {
            Name = "Attack Vector";
            Description = @"Each day, every territory with >90% infection deals 7% of its infection to neighboring territories";
            Cost = 250;
        }

        public void OnDayEnd(PlayerStats playerStats)
        {
            foreach (Territory t in Territory.AllTerritories) 
            {
                if (t.GetInfectedPercent(MachineType.ALL) > 0.9f)
                {
                    foreach (Territory neighbor in t.Neighbors)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            neighbor.SetInfectionLevel((MachineType)i, neighbor.Infection[i] + Mathf.RoundToInt(t.Infection[i]*0.07f));
                        }
                    }
                }
            }
        }

        public void OnDayStart(PlayerStats playerStats)
        {
            
        }
    }
}
