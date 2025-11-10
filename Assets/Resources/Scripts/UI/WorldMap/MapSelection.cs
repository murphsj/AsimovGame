using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A singleton MonoBehavior responsible for handling selection of map tiles.
/// </summary>
public class MapSelection : MonoBehaviour
{
    public static MapSelection instance { get; private set; }

    Territory hoveredTerritory;
    HashSet<Territory> selectedTerritories;

    public void OnHoverTerritory(Territory t)
    {
        hoveredTerritory?.button.Unhover();
        hoveredTerritory = t;
    }

    public void OnUnhoverTerritory(Territory t)
    {
        hoveredTerritory?.button.Unhover();
        foreach (Territory neighbor in hoveredTerritory.Neighbors)
        {
            neighbor.button.Unhover();
        }
        hoveredTerritory = null;
    }

    public void OnClickTerritory(Territory t)
    {
        if (selectedTerritories.Contains(t))
        {
            selectedTerritories.Remove(t);
            t.button.Selected = false;
        // TODO: check for max territories per turn stat
        } else if (selectedTerritories.Count < 3) {
            selectedTerritories.Add(t);
            t.button.Selected = true;
        }
    }

    void Update()
    {
        if (hoveredTerritory != null)
        {
            hoveredTerritory.button.UpdateHoverFlash(0.35f, 0.3f);
            foreach (Territory neighbor in hoveredTerritory.Neighbors)
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
        selectedTerritories = new HashSet<Territory>();
    }
}