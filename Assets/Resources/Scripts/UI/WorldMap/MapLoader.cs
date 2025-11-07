using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

/// <summary>
/// Loads the world map from an SVG file when started.
/// </summary>
public class MapLoader : MonoBehaviour
{
    /// <summary>
    /// The max distance between two points for them to be considered adjacent
    /// </summary>
    const float NEIGHBOR_POLYGON_MAX_RADIUS = 2;

    /// <summary>
    /// The min amount of points two polygons must share to be considered neighbors
    /// </summary>
    const int NEIGHBOR_POLYGON_ADJ_COUNT = 2;

    [SerializeField]
    Canvas canvas;

    [SerializeField]
    Color32 polyColor;

    [SerializeField]
    float lineThickness;

    [SerializeField]
    TerritoryButton territory;

    [SerializeField]
    TextAsset mapData;

    List<TerritoryButton> allMapTerritories;

    private static void ToWorldCoords(ref Vector2 pos)
    {
        pos.y *= -1;
        // Screen dimensions divided by 2
        pos.x -= 140;
        pos.y += 90;
    }

    private static bool IsPointNeighboring(Vector2 point, List<Vector2> polygon)
    {
        foreach (Vector2 vert in polygon)
        {
            if (Vector2.Distance(point, vert) <= NEIGHBOR_POLYGON_MAX_RADIUS)
            {
                return true;
            }
        }

        return false;
    }

    private void SetNeighbors(List<Vector2> verts, Territory thisTerritory)
    {
        // For each point in this polygon, iterate through all other points
        // comparing distance to each. If distance is low enough, they're
        // neighbors. (This is not a perfect solution, but it should be fine here)
        foreach (TerritoryButton button in allMapTerritories)
        {
            int neighborCount = 0;
            List<Vector2> otherPolygon = button.GetComponent<PolygonRenderer>().GetVerticesWorldSpace();

            foreach (Vector2 vert in verts)
            {
                if (IsPointNeighboring(vert, otherPolygon))
                {
                    neighborCount++;
                    if (neighborCount >= NEIGHBOR_POLYGON_ADJ_COUNT)
                    {
                        // These items are both neighbors of each other
                        button.territory.neighbors.Add(thisTerritory);
                        thisTerritory.neighbors.Add(button.territory);
                        break;
                    }

                }
            }
        }
    }

    private void AddTerritory(List<Vector2> verts, byte territoryId)
    {
        // Make the Territory for this area
        Territory tData = Territory.FromId(territoryId);

        // Setting neighbors as we build the map prevents redundant checks
        SetNeighbors(verts, tData);

        // Make the TerritoryButton object
        TerritoryButton obj = Instantiate(territory);
        obj.transform.SetParent(transform);

        obj.GetComponent<PolygonRenderer>().ApplyVerticesWorldSpace(verts);
        obj.name = tData.name;
        obj.lineThickness = lineThickness;
        obj.territory = tData;

        tData.button = obj;

        allMapTerritories.Add(obj);
    }

    private List<Vector2> ParsePointList(string pointList)
    {
        List<Vector2> points = new List<Vector2>();
        string[] coords = pointList.Split(" ");

        for (int i = 0; i < coords.Length - 2; i += 2)
        {
            float x = float.Parse(coords[i]);
            float y = float.Parse(coords[i + 1]);
            Vector2 pos = new Vector2(x, y);
            ToWorldCoords(ref pos);
            points.Add(pos);
        }

        return points;
    }

    private void LoadSVG(Stream fs)
    {
        using (XmlReader reader = XmlReader.Create(fs))
        {
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name.Equals("polygon"))
                        {
                            Color polyColor;
                            ColorUtility.TryParseHtmlString(reader.GetAttribute("fill"), out polyColor);
                            byte territoryId = (byte)(polyColor.r * 255);

                            List<Vector2> verts = ParsePointList(reader.GetAttribute("points"));
                            AddTerritory(verts, territoryId);
                        }
                        break;
                }
            }
        }
    }

    public void LoadMap()
    {
        Territory.SetMapData(mapData);

        using (FileStream fs = File.OpenRead(Application.dataPath + @"/Resources/worldMap.svg"))
        {
            LoadSVG(fs);
        }
    }

    void Awake()
    {
        allMapTerritories = new List<TerritoryButton>();
    }
}