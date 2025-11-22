public abstract class Upgrade {
    public readonly int Cost = 0;

    public readonly string Name = "MISSING UPGRADE";

    public readonly string Description = "No Description";

    public void OnPurchase(PlayerStats playerStats)
    {
        playerStats.Upgrades.Add(this);
    }
}