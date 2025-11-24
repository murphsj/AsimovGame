using System.Collections.Generic;
using UnityEngine;

namespace ShopUpgrades
{
    public class ZeroDay : InfectChangeUpgrade
    {
        private HashSet<Territory> alreadyTriggered = new HashSet<Territory>();

        public ZeroDay()
        {
            Name = "Zero-Day";
            Description = @"Once for each territory on the map, the next direct
attack will also deal 20% CIV and COM infection,
but will accrue +3 Notice";
            Cost = 230;
        }

        public override void OnAction(PlayerStats playerStats, InfectChangeAction action)
        {
            if (!action.isPlayer) return;
            if (alreadyTriggered.Contains(action.territory)) return;

            for (int i = 0; i < 3; i++)
            {
                action.changeLevel[i] += Mathf.RoundToInt((action.territory.Population/3) * 0.2f);
            }

            playerStats.AddNoticeProgress(3);

            action.territory.MakeMessage("+ZeroDay", Territory.MESSAGE_COLOR_GOV);
        }
    }
}
