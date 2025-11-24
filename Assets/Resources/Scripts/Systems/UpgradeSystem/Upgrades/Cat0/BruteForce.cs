namespace ShopUpgrades
{
    public class BruteForce : InfectChangeUpgrade
    {
        public BruteForce()
        {
            Name = "Brute Forcing";
            Description = @"When a territory is direct attacked, if all neighbors have >90% infection, that attack gets +4.0x to all damage";
            Cost = 650;
        }

        public override void OnAction(PlayerStats playerStats, InfectChangeAction action)
        {
            if (!action.isPlayer) return;
            foreach (Territory neighbor in action.territory.Neighbors)
            {
                if (!action.isPlayer) return;
                if (neighbor.GetInfectedPercent(MachineType.ALL) < 0.9f) return;
            }

            for (int i = 0; i < 3; i++)
            {
                action.changeLevel[i] *= 4;
            }

            action.territory.MakeMessage("+BruteForce", Territory.MESSAGE_COLOR_GOV);
        }
    }
}
