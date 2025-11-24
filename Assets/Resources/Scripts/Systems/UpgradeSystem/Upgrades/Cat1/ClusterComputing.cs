using UnityEngine;

namespace ShopUpgrades
{
    public class ClusterComputing : Upgrade
    {
        public ClusterComputing()
        {
            Name = "Cluster Computing";
            Description = @"+1 to the amount of territories you can attack each turn";
            Cost = 150;
        }

        public override void OnPurchase(PlayerStats playerStats)
        {
            base.OnPurchase(playerStats);
            playerStats.MaxTargetedTerritories += 1;
        }
    }
}
