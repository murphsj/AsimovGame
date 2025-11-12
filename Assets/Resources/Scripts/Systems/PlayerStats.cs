using Services;

[RegisterService]
public class PlayerStats
{
    public int MaxTargetedTerritories = 3;

    public int[] AttackPower = new int[] { 10, 10, 10 };

    public PlayerStats()
    {
        
    }
}