using UnityEngine;

namespace ShopUpgrades
{
    public class InputRedirection : InfectChangeUpgrade
    {
        public InputRedirection()
        {
            Name = "Input Redirection";
            Description = @"All counterattacks spread their damage between neighbors
""> dev/null""";
            Cost = 150;
        }

        public override void OnAction(PlayerStats playerStats, InfectChangeAction action)
        {
            if (action.isPlayer) return;
            if (action.territory.GetInfectedPercent(MachineType.ALL) <= 0f) return;

            int factor = action.territory.Neighbors.Count + 1;
            for (int i = 0; i < 3; i++)
            {
                action.changeLevel[i] /= factor;
            }
        }

        public override void AfterAction(PlayerStats playerStats, InfectChangeAction action)
        {
            if (action.isPlayer) return;
            if (action.territory.GetInfectedPercent(MachineType.ALL) <= 0f) return;

            foreach (Territory neighbor in action.territory.Neighbors)
            {
                for (int i = 0; i < 3; i++)
                {
                    neighbor.SetInfectionLevel((MachineType)i, neighbor.Infection[i] + action.changeLevel[i]);
                }

                neighbor.button.UpdateVisuals();
            }
        }
    }
}