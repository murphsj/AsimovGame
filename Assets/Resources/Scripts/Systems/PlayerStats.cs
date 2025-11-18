using Services;

[RegisterService]
public class PlayerStats
{
    public int MaxTargetedTerritories = 3;

    public int[] AttackPower = new int[] { 20, 20, 20 };

    public int EnemyAttackTargetCount = 3;

    public PlayerStats()
    {
        
    } 
}