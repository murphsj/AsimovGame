using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
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

    void Start()
    {
        loader = GetComponent<MapLoader>();
    }
}