using UnityEngine;

namespace ShopUpgrades
{
    public class EmailWorm : Upgrade, IUpgradeDayListener
    {
        public EmailWorm()
        {
            Name = "Email Worm";
            Description = @"Territories with over 25% total infection gain 45 CIV infections per day
""LOVELETTERFORYOU.VBS""";
            Cost = 30;
        }

        public void OnDayEnd(PlayerStats playerStats)
        {
            foreach (Territory t in Territory.AllTerritories) 
            {
                if (t.GetInfectedPercent(MachineType.ALL) > 0.25)
                {
                    t.SetInfectionLevel(MachineType.Civ, t.Infection[(int)MachineType.Civ] + 45);
                    t.button.UpdateVisuals();
                }
            }
        }

        public void OnDayStart(PlayerStats playerStats)
        {
            
        }
    }
}
