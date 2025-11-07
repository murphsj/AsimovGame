using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

/// <summary>
/// A singleton behavior for controlling the tooltip shown when hovering over territories.
/// </summary>
public class MapTooltip : MonoBehaviour
{
    public static MapTooltip instance { get; private set; }

    [SerializeField]
    TextMeshProUGUI tooltipTextLabel;

    private RectTransform rect;
    private Mouse mouse;

    void Update()
    {
        Debug.Log("Original transform: " + transform.position + " New mouse position: " + mouse.position.ReadValue());
        rect.anchoredPosition = mouse.position.ReadValue() - new Vector2(Screen.width/2, Screen.height/2);
    }

    public void ShowTooltip(Territory t)
    {
        gameObject.SetActive(true);
        tooltipTextLabel.text = t.name;
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
        mouse = Mouse.current;
        rect = GetComponent<RectTransform>();
    }

    void Start()
    {
        //HideTooltip();

    }
}
