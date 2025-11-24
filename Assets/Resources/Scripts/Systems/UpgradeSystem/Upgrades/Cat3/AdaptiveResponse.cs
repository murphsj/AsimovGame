using UnityEngine;

namespace ShopUpgrades
{
    public class AdaptiveResponse : InfectChangeUpgrade, IUpgradeDayListener
    {
        private int attacksBlocked = 0;


        public AdaptiveResponse()
        {
            Name = "Adaptive Response";
            Description = @"The first 4 counterattacks in a day cause all neighbors of the target to gain 35% of the target's infection";
            Cost = 650;
        }

        public override void AfterAction(PlayerStats playerStats, InfectChangeAction action)
        {
            if (action.isPlayer) return;
            if (action.territory.GetInfectedPercent(MachineType.ALL) <= 0f) return;
            if (attacksBlocked >= 4) return;
            attacksBlocked++;

            foreach (Territory neighbor in action.territory.Neighbors)
            {
                for (int i = 0; i < 3; i++)
                {
                    neighbor.SetInfectionLevel((MachineType)i, neighbor.Infection[i] + Mathf.RoundToInt(action.territory.Infection[i] * 0.35f));
                }

                neighbor.button.UpdateVisuals();
            }
        }

        public void OnDayEnd(PlayerStats playerStats)
        {
            attacksBlocked = 0;
        }

        public void OnDayStart(PlayerStats playerStats)
        {
            
        }
    }
}
