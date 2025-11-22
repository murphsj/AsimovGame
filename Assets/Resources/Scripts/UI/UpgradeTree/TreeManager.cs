using UnityEngine;
using UnityEngine.UI;
using Services;

[RegisterService]
public class TreeManager : MonoBehaviour
{
    [SerializeField]
    private Button buyButton;

    [SerializeField]
    private UpgradeCategoryButton[] categoryButtons;
    
    void Start()
    {
        buyButton.interactable = false;
    }

    public void SelectCategory(int categoryId)
    {
        
    }
}
