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
    private Territory activeTerritory;

    void Update()
    {
        rect.anchoredPosition = GuiUtils.GetGuiMousePosition();
        tooltipTextLabel.text = activeTerritory.Name
            + "\nCiv: " + activeTerritory.GetInfectedPercent(MachineType.Civ) * 100 + "%"
            + "\nCom: " + activeTerritory.GetInfectedPercent(MachineType.Com) * 100 + "%"
            + "\nGov: " + activeTerritory.GetInfectedPercent(MachineType.Gov) * 100 + "%";
    }

    public void ShowTooltip(Territory t)
    {
        activeTerritory = t;
        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        HideTooltip();
    }

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
}
