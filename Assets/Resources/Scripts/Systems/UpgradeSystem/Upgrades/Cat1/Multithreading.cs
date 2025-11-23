using UnityEngine;

namespace ShopUpgrades
{
    public class Multithreading : Upgrade
    {
        public Multithreading()
        {
            Name = "Multithreading";
            Description = @"Base infection caused by direct attacks increased by 30
""Hottest computing technology of the 1960s""";
            Cost = 100;
        }

        public override void OnPurchase(PlayerStats playerStats)
        {
            base.OnPurchase(playerStats);
            for (int i = 0; i < 3; i++)
            {
                playerStats.AttackPower[i] += 30;
            }
        }
    }
}
