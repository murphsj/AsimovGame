using UnityEngine;

/// <summary>
/// A singleton MonoBehavior responsible for handling selection of map tiles.
/// </summary>
public class MapSelection : MonoBehaviour
{
    public static MapSelection instance { get; private set; }

    Territory hoveredTerritory;

    public void OnHoverTerritory(Territory t)
    {
        hoveredTerritory?.button.Unhover();
        hoveredTerritory = t;
    }

    public void OnUnhoverTerritory(Territory t)
    {
        hoveredTerritory?.button.Unhover();
        foreach (Territory neighbor in hoveredTerritory.neighbors)
        {
            neighbor.button.Unhover();
        }
        hoveredTerritory = null;
    }

    public void OnClickTerritory(Territory t)
    {
        t.button.Selected = !t.button.Selected;
    }

    void Update()
    {
        if (hoveredTerritory != null)
        {
            hoveredTerritory.button.UpdateHoverFlash(0.35f, 0.3f);
            foreach (Territory neighbor in hoveredTerritory.neighbors)
            {
                neighbor.button.UpdateHoverFlash(0.15f, 0.15f);
            }
        }
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
    }
}