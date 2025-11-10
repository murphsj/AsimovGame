using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

/// <summary>
/// A singleton behavior for controlling the tooltip shown when hovering over territories.
/// </summary>
public class MapTooltip : MonoBehaviour
{
    public static MapTooltip instance { get; private set; }

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

    private void EnforceSingleton()
    {
        // Enforce singleton behavior; delete this instance if one already exists
        if (instance != null && instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            instance = this; 
        } 
    }

    void Awake()
    {
        EnforceSingleton();
        rect = GetComponent<RectTransform>();
    }
}
