using UnityEngine;

namespace ShopUpgrades
{
    public class Fractionalization : InfectChangeUpgrade
    {
        private int[] startInfectionLevel;
        
        public Fractionalization()
        {
            Name = "Fractionalization";
            Description = @"Direct attacks also deal 10% of their damage to neighboring territories";
            Cost = 300;
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
                deltaInfectionLevel[i] = Mathf.RoundToInt((action.territory.Infection[i] - startInfectionLevel[i])*0.1f);
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
