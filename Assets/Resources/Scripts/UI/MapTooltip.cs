using TMPro;
using UnityEngine;
using Services;
using Unity.VisualScripting;
using System;

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

    private static string InfectPercentString(Territory t, MachineType mType)
    {
        return string.Format("{0:0}%", t.GetInfectedPercent(MachineType.Com) * 100);
    }

    void Update()
    {
        rect.anchoredPosition = GuiUtils.GetGuiMousePosition();
        tooltipTextLabel.text = activeTerritory.Name
            + "\n<color=#713567>"
            + InfectPercentString(activeTerritory, MachineType.Civ) + "</color>"
            + " - <color=#98974B>"
            + InfectPercentString(activeTerritory, MachineType.Com) + "</color>"
            + " - <color=#357156>" + InfectPercentString(activeTerritory, MachineType.Gov) + "</color>"
            + "\nPopulation: " + activeTerritory.Population;
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
