using Services;

[RegisterService]
public class PlayerStats
{
    /// <summary>
    /// The max number of territories the player can target in a turn.
    /// </summary>
    public int MaxTargetedTerritories = 3;

    /// <summary>
    /// The base infection change that the player inflicts on targeted territories during their turn.
    /// </summary>
    public int[] AttackPower = new int[] { 20, 20, 20 };

    /// <summary>
    /// The number of territories an enemy will target per turn.
    /// </summary>
    public int EnemyAttackTargetCount = 3;

    /// <summary>
    /// The infection threshold a territory must meet to be selectable.
    /// </summary>
    public float TargetInfectedThreshhold = 0.15f;

    public PlayerStats()
    {
        
    } 
}