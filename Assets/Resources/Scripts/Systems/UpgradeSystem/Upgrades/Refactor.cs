using UnityEngine;

namespace ShopUpgrades
{
    public class Refactor : Upgrade
    {
        public Refactor()
        {
            Name = "Refactor";
            Description = @"Base infection caused by direct attacks increased by 20
""Just one more refactor""";
            Cost = 50;
        }

        public override void OnPurchase(PlayerStats playerStats)
        {
            base.OnPurchase(playerStats);
            for (int i = 0; i < 3; i++)
            {
                playerStats.AttackPower[i] += 20;
            }
        }
    }
}
