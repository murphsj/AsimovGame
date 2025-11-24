using UnityEngine;

namespace ShopUpgrades
{
    public class MacroVirus : Upgrade, IUpgradeDayListener
    {
        public MacroVirus()
        {
            Name = "Macro Virus";
            Description = @"Territories with over 25% total infection gain 30 COM infections per day";
            Cost = 40;
        }

        public void OnDayEnd(PlayerStats playerStats)
        {
            foreach (Territory t in Territory.AllTerritories) 
            {
                if (t.GetInfectedPercent(MachineType.ALL) > 0.25)
                {
                    t.SetInfectionLevel(MachineType.Com, t.Infection[(int)MachineType.Com] + 45);
                    t.button.UpdateVisuals();
                }
            }
        }

        public void OnDayStart(PlayerStats playerStats)
        {
            
        }
    }
}
