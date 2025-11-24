using System;
using UnityEngine;

namespace ShopUpgrades
{
    public class RKit : InfectChangeUpgrade
    {
        public RKit()
        {
            Name = "Rootkit";
            Description = @"Any damage a direct attack deals over 100% infection reduces that territory's counterattack
abilities";
            Cost = 400;
        }

        public override void OnAction(PlayerStats playerStats, InfectChangeAction action)
        {
            bool procced = false;
            if (!action.isPlayer) return;
            for (int i = 0; i < 3; i++)
            {
                Debug.Log(action.changeLevel[i]);
                if (action.territory.Infection[i] + action.changeLevel[i] > action.territory.Population/3)
                {
                    
                    action.territory.BaseResistance[i] = Math.Clamp(
                        action.territory.BaseResistance[i] - action.territory.Infection[i]/20, 0, 100);
                    procced = true;
                }
            }

            if (procced) action.territory.MakeMessage("+RKit", Territory.MESSAGE_COLOR_CIV);
        }
    }
}