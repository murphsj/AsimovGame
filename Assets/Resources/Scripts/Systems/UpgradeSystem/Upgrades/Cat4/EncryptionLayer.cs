using UnityEngine;

namespace ShopUpgrades
{
    public class EncryptionLayer : InfectChangeUpgrade, IUpgradeDayListener
    {
        private int attacksBlocked = 0;

        public EncryptionLayer()
        {
            Name = "Encryption Layer";
            Description = @"The first 4 counterattacks in a day have a 50% chance for their damage to be halved";
            Cost = 70;
        }

        public override void OnAction(PlayerStats playerStats, InfectChangeAction action)
        {
            if (action.isPlayer) return;
            if (action.territory.GetInfectedPercent(MachineType.ALL) <= 0f) return;
            if (attacksBlocked >= 4) return;
            attacksBlocked++;

            if (Random.Range(0f, 1f) < 0.5) return;

            action.territory.MakeMessage("+EncryptionLayer", Territory.MESSAGE_COLOR_CIV);

            for (int i = 0; i < 3; i++)
            {
                action.changeLevel[i] /= 2;
            }
        }

        public void OnDayEnd(PlayerStats playerStats)
        {
        }

        public void OnDayStart(PlayerStats playerStats)
        {
            attacksBlocked = 0;
        }
    }
}