using System.Collections.Generic;
using UnityEngine;
using Services;

/// <summary>
/// Handles selection of map tiles.
/// </summary>
[RegisterService]
public class MapSelection : MonoBehaviour
{
    Territory hoveredTerritory;
    HashSet<Territory> selectedTerritories;

    private PlayerStats playerStats;

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
        } else if (selectedTerritories.Count < playerStats.MaxTargetedTerritories) {
            selectedTerritories.Add(t);
            t.button.Selected = true;
        }
    }

    void Update()
    {
        playerStats = ServiceLocator.Get<PlayerStats>();
        
        if (hoveredTerritory != null)
        {
            hoveredTerritory.button.UpdateHoverFlash(0.35f, 0.3f);
            foreach (Territory neighbor in hoveredTerritory.Neighbors)
            {
                neighbor.button.UpdateHoverFlash(0.15f, 0.15f);
            }
        }
    }

    void Awake()
    {
        selectedTerritories = new HashSet<Territory>();
    }
}