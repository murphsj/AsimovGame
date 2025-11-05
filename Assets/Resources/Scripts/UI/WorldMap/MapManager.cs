using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A singleton MonoBehavior responsible for handling interaction and simulation of the world map.
/// </summary>
public class MapManager : MonoBehaviour
{
    public static MapManager instance { get; private set; }

    MapLoader loader;
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
        loader = GetComponent<MapLoader>();
    }

    void Start()
    {
        loader.LoadMap();
    }
}