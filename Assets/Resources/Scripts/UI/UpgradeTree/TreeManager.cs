using UnityEngine;
using UnityEngine.UI;
using Services;
using TMPro;
using System;

[RegisterService]
public class TreeManager : MonoBehaviour
{
    [SerializeField]
    private Button buyButton;

    [SerializeField]
    private TextMeshProUGUI nameText;

    [SerializeField]
    private TextMeshProUGUI descriptionText;

    [SerializeField]
    private UpgradeCategoryButton[] categoryButtons;

    private PlayerStats playerStats;
    private int selectedCategory = -1;
    
    void Start()
    {
        playerStats = ServiceLocator.Get<PlayerStats>();
        buyButton.onClick.AddListener(TryPurchaseUpgrade);
        gameObject.SetActive(false);
    }

    private static string AddKeywordColors(string input)
    {
        input = input.Replace("CIV", "<color=#357156>CIV</color>");
        input = input.Replace("COM", "<color=#98974B>COM</color>");
        input = input.Replace("GOV", "<color=#713567>GOV</color>");
        return input;
    }

    public void SelectCategory(int categoryId)
    {
        selectedCategory = categoryId;
        Upgrade upgrade = ShopCategoryItems.GetNextUpgradeForCategory(categoryId);
        if (upgrade == null)
        {
            nameText.text = "NONE";
            descriptionText.text = "No more upgrades in this category";
        } else {
            nameText.text = upgrade.Name + " - " + upgrade.Cost + " Resources";
            descriptionText.text = AddKeywordColors(upgrade.Description);
        }
    }

    public void TryPurchaseUpgrade()
    {
        if (selectedCategory == -1) return;
        Upgrade upgrade = ShopCategoryItems.GetNextUpgradeForCategory(selectedCategory);
        if (upgrade == null) return;
        if (playerStats.Resources < upgrade.Cost) return;

        playerStats.Resources -= upgrade.Cost;

        upgrade.OnPurchase(playerStats);
        ShopCategoryItems.AdvancePurchaseProgress(selectedCategory);
        // Reselect to show the next upgrade
        SelectCategory(selectedCategory);
    }
}
