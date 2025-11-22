namespace ShopUpgrades
{
    public class SuspiciousAdvertisement : InfectChangeUpgrade
    {
        public SuspiciousAdvertisement()
        {
            Name = "Suspicious Advertisement";
            Description = @"Direct attacks deal +0.7x CIV damage
“Download now!”";
            Cost = 50;
        }

        public override void OnAction(PlayerStats playerStats, InfectChangeAction action)
        {
            if (action.isPlayer)
            {
                action.changeLevel[0] *= 3;
            }
        }
    }
}
