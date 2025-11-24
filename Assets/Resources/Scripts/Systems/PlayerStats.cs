using System.Collections.Generic;
using Services;
using ShopUpgrades;
using UnityEngine.Events;

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
    public int[] AttackPower = new int[] { 20, 20, 7 };

    /// <summary>
    /// The number of territories an enemy will target per turn.
    /// </summary>
    public int EnemyAttackTargetCount = 5;

    /// <summary>
    /// The infection threshold a territory must meet to be selectable.
    /// </summary>
    public float TargetInfectedThreshhold = 0.15f;

    public int AttacksMadeThisTurn = 0;

    public int NoticeLevel = 1;
    
    public int ToNextNoticeLevel = 15;

    public int NoticeProgress = 0;

    /// <summary>
    /// The player's currency count.
    /// </summary>
    public int Resources
    {
        get
        {
            return _resources;
        }
        set
        {
            _resources = value;
            BarStatsChanged.Invoke();
        }
    }

    /// <summary>
    /// The current day.
    /// </summary>
    public int Day
    {
        get
        {
            return _day;
        }
        set
        {
            _day = value;
            BarStatsChanged.Invoke();
        }
    }

    public UnityEvent BarStatsChanged = new UnityEvent();
    public HashSet<Upgrade> Upgrades { get; private set; }

    private int _resources = 8000;
    private int _day = 49;

    public PlayerStats()
    {
        Upgrades = new HashSet<Upgrade>();
    } 

    public void AddNoticeLevel()
    {
        NoticeLevel++;
        ToNextNoticeLevel += 5;
        NoticeProgress = 0;
        EnemyAttackTargetCount += 1;
    }

    public void AddNoticeProgress(int amount)
    {
        NoticeProgress += amount;
        if (NoticeProgress > ToNextNoticeLevel)
        {
            AddNoticeLevel();
        }

        BarStatsChanged.Invoke();
    }
}