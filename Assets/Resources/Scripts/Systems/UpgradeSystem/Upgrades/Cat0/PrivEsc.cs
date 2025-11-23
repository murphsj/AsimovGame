namespace ShopUpgrades
{
    public class PrivEsc : InfectChangeUpgrade
    {
        public PrivEsc()
        {
            Name = "Privilege Escalation";
            Description = @"A direct attack on a territory does +1.5x GOV damage if
all neighboring territories have over 40%
overall infection. +20 Notice";
            Cost = 70;
        }

        public override void OnPurchase(PlayerStats playerStats)
        {
            base.OnPurchase(playerStats);
            playerStats.AddNoticeProgress(20);
        }

        public override void OnAction(PlayerStats playerStats, InfectChangeAction action)
        {
            if (!action.isPlayer) return;
            foreach (Territory neighbor in action.territory.Neighbors)
            {
                if (neighbor.GetInfectedPercent(MachineType.ALL) < 0.4f) return;
            }

            action.changeLevel[2] = (int)(action.changeLevel[2] * 2.5);
            action.territory.MakeMessage("+PrivEsc", Territory.MESSAGE_COLOR_GOV);
        }
    }
}
