using Services;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeCategoryButton : Selectable
{
    [SerializeField]
    int categoryId = 0;

    private TreeManager treeManager;

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        treeManager.SelectCategory(categoryId);
    }

    protected override void Start()
    {
        base.Start();
        treeManager = ServiceLocator.Get<TreeManager>();
    }
}