using System.Collections.Generic;
using UnityEngine;
using ShopUpgrades;

public static class ShopCategoryItems
{
    public static readonly List<Upgrade>[] items = new List<Upgrade>[8];
    public static readonly int[] purchaseProgress = new int[8];

    static ShopCategoryItems()
    {
        items[0] = new List<Upgrade>()
        {
            {new SuspiciousAdvertisement()},
            {new Refactor()}
        };

        Debug.Log(items[0][0].Name);
    }

    public static Upgrade GetNextUpgradeForCategory(int categoryId)
    {
        if (items[categoryId].Count > purchaseProgress[categoryId])
        {
            return items[categoryId][purchaseProgress[categoryId]];
        } else {
            return null;
        }
    }

    public static void AdvancePurchaseProgress(int categoryId)
    {
        purchaseProgress[categoryId]++;
    }
}