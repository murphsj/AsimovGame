namespace ShopUpgrades
{
    public class InitiationPhase : InfectChangeUpgrade
    {
        public InitiationPhase()
        {
            Name = "Initiation Phase";
            Description = @"Direct attacks deal +1.5x CIV damage if the targetted territory has less than 30% total infection";
            Cost = 30;
        }

        public override void OnAction(PlayerStats playerStats, InfectChangeAction action)
        {
            if (action.territory.GetInfectedPercent(MachineType.ALL) > 0.3) return;

            action.changeLevel[2] = (int)(action.changeLevel[0] * 1.5f);
        }
    }
}
