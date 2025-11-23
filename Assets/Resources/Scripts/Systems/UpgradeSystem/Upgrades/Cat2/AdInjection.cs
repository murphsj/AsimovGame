using UnityEngine;

namespace ShopUpgrades
{
    public class AdInjection : InfectChangeUpgrade
    {
        public AdInjection()
        {
            Name = "Ad Injection";
            Description = @"Directly attacking a territory generates 1 Resources
per 100 population infected
""What you get for installing toolbars""";
            Cost = 60;
        }

        public override void AfterAction(PlayerStats playerStats, InfectChangeAction action)
        {
            if (action.isPlayer)
            {
                int totalChange = 0;
                foreach (int i in action.changeLevel) {
                    totalChange += i;
                }
                Debug.Log(totalChange);
                playerStats.Resources += Mathf.RoundToInt((float)totalChange / 100);
            }
        }
    }
}
