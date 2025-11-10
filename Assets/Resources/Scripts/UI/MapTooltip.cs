using TMPro;
using UnityEngine;
using Services;

/// <summary>
/// Manages the tooltip shown when hovering over territories.
/// </summary>
[RegisterService]
public class MapTooltip : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI tooltipTextLabel;

    private RectTransform rect;

    void Update()
    {
        rect.anchoredPosition = GuiUtils.GetGuiMousePosition();
    }

    public void ShowTooltip(Territory t)
    {
        gameObject.SetActive(true);
        tooltipTextLabel.text = t.Name
            + "\nCiv: " + t.getInfectedPercent(MachineType.Civ)
            + "\nCom: " + t.getInfectedPercent(MachineType.Com)
            + "\nGov: " + t.getInfectedPercent(MachineType.Gov);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
}
