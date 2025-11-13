using System.Collections.Generic;
using UnityEngine;
using Services;

/// <summary>
/// Handles selection of map tiles.
/// </summary>
[RegisterService]
public class MapSelection : MonoBehaviour
{
    public Territory HoveredTerritory { get; private set; }
    public HashSet<Territory> SelectedTerritories { get; private set; }
    public bool SelectionEnabled = true;

    private PlayerStats playerStats;

    public void OnHoverTerritory(Territory t)
    {
        HoveredTerritory?.button.Unhover();
        HoveredTerritory = t;
    }

    public void OnUnhoverTerritory(Territory t)
    {
        HoveredTerritory?.button.Unhover();
        foreach (Territory neighbor in HoveredTerritory.Neighbors)
        {
            neighbor.button.Unhover();
        }
        HoveredTerritory = null;
    }

    public void OnClickTerritory(Territory t)
    {
        if (SelectedTerritories.Contains(t))
        {
            SelectedTerritories.Remove(t);
            t.button.Selected = false;
        }
        else if (SelectedTerritories.Count < playerStats.MaxTargetedTerritories)
        {
            SelectedTerritories.Add(t);
            t.button.Selected = true;
        }
    }
    
    public void ClearSelection()
    {
        foreach (Territory t in SelectedTerritories)
        {
            t.button.Selected = false;
        }

        SelectedTerritories.Clear();
    }

    void Update()
    {
        playerStats = ServiceLocator.Get<PlayerStats>();
        
        if (SelectionEnabled && HoveredTerritory != null)
        {
            HoveredTerritory.button.UpdateHoverFlash(0.35f, 0.3f);
            foreach (Territory neighbor in HoveredTerritory.Neighbors)
            {
                neighbor.button.UpdateHoverFlash(0.15f, 0.15f);
            }
        }
    }

    void Awake()
    {
        SelectedTerritories = new HashSet<Territory>();
    }
}