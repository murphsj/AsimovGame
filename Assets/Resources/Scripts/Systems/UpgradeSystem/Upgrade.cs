public abstract class Upgrade {
    public int Cost { get; protected set; } = 0;
    public string Name { get; protected set; } = "NAME MISSING";
    public string Description { get; protected set; } = "DESCRIPTION MISSING";

    public void OnPurchase(PlayerStats playerStats)
    {
        playerStats.Upgrades.Add(this);
    }
}