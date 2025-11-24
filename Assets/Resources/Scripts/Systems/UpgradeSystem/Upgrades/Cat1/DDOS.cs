using UnityEngine;

namespace ShopUpgrades
{
    public class DDOS : Upgrade
    {
        public DDOS()
        {
            Name = "DDOS";
            Description = "+2 to the amount of territories you can attack per turn";
            Cost = 800;
        }

        public override void OnPurchase(PlayerStats playerStats)
        {
            base.OnPurchase(playerStats);
            playerStats.MaxTargetedTerritories += 2;
        }
    }
}
