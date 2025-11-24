using UnityEngine;

namespace ShopUpgrades
{
    public class Fractionalization : InfectChangeUpgrade
    {
        private int[] startInfectionLevel;
        
        public Fractionalization()
        {
            Name = "Fractionalization";
            Description = @"Direct attacks also deal 25% of their damage to neighboring territories";
            Cost = 250;
        }

        public override void OnAction(PlayerStats playerStats, InfectChangeAction action)
        {
            startInfectionLevel = new int[3];
            action.territory.Infection.CopyTo(startInfectionLevel, 0);
        }

        public override void AfterAction(PlayerStats playerStats, InfectChangeAction action)
        {
            if (!action.isPlayer) return;
            int[] deltaInfectionLevel = new int[3];

            for (int i = 0; i < 3; i++)
            {
                deltaInfectionLevel[i] = Mathf.RoundToInt((action.territory.Infection[i] - startInfectionLevel[i])*0.25f);
            }

            foreach (Territory neighbor in action.territory.Neighbors)
            {
                for (int i = 0; i < 3; i++)
                {
                    neighbor.SetInfectionLevel(
                        (MachineType)i, neighbor.Infection[i] + deltaInfectionLevel[i]
                    );
                }
                neighbor.button.UpdateVisuals();
            }
        }
    }
}
