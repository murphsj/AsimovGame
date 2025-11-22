using Services;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCategoryButton : MonoBehaviour
{
    [SerializeField]
    int categoryId = 0;

    private TreeManager treeManager;
    private Button button;

    void OnCategorySelect()
    {
        treeManager.Select(categoryId);
    }

    void Start()
    {
        treeManager = ServiceLocator.Get<TreeManager>();
    }

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnCategorySelect);
    }
}