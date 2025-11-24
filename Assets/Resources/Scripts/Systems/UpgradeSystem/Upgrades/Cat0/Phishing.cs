namespace ShopUpgrades
{
    public class Phishing : InfectChangeUpgrade
    {
        public Phishing()
        {
            Name = "Phishing";
            Description = @"Direct attacks deal +0.7x COM damage
""Click here to get a free promotion!""";
            Cost = 50;
        }

        public override void OnAction(PlayerStats playerStats, InfectChangeAction action)
        {
            if (!action.isPlayer) return;
            action.changeLevel[1] = (int)(action.changeLevel[1] * 1.7f);
        }
    }
}
