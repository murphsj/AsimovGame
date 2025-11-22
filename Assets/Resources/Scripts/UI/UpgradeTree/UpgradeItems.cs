using System.Collections.Generic;
using ShopUpgrades;

public static class UpgradeItems
{
    public static readonly List<List<Upgrade>> items = new List<List<Upgrade>>();

    static UpgradeItems()
    {
        items[0] = new List<Upgrade>()
        {
            new SuspiciousAdvertisement()
        };
    }
}