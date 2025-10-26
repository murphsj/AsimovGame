using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class TestWorldMapLoader : MonoBehaviour
{
    [SerializeField]
    Canvas canvas;

    [SerializeField]
    Color32 polyColor;

    [SerializeField]
    Color32 lineColor;

    private static (Vector2, Vector2) GetExtents(List<Vector2> verts)
    {
        Vector2 topLeft = verts[0];
        Vector2 bottomRight = verts[0];

        foreach (Vector2 point in verts)
        {
            // Find the highest point's y and leftmost point's x
            topLeft.x = Mathf.Min(point.x, topLeft.x);
            topLeft.y = Mathf.Max(point.y, topLeft.y);

            // Find lowest point's y and rightmost point's x
            bottomRight.x = Mathf.Max(point.x, bottomRight.x);
            bottomRight.y = Mathf.Min(point.y, bottomRight.y);
        }

        return (topLeft, bottomRight);
    }

    private void AddPolygon(List<Vector2> verts)
    {
        // The math is a little weird here because our Polygon class draws from top left
        // while Unity's coordinate system has bottom left as the "origin"
        (Vector2 topLeft, Vector2 bottomRight) = GetExtents(verts);
        Vector2 size = new Vector2(bottomRight.x - topLeft.x, topLeft.y - bottomRight.y);
        
        GameObject obj = new GameObject("Polygon");
        RectTransform rect = obj.AddComponent<RectTransform>();
        rect.sizeDelta = size;

        PolygonRenderer polygon = obj.AddComponent<PolygonRenderer>();
        TerritoryButton button = obj.AddComponent<TerritoryButton>();
        obj.AddComponent<CanvasRenderer>();
        obj.transform.SetParent(canvas.transform);

        rect.localScale = new Vector2(1f, 1f);
        rect.pivot = new Vector2(0f, 0f);

        button.polyColor = polyColor;
        button.borderColor = lineColor;

        

        rect.anchoredPosition = new Vector2(topLeft.x, topLeft.y - size.y);

        for (int i = 0; i < verts.Count; i++)
        {
            verts[i] -= topLeft;
        }
        polygon.verts = verts;
    }
    
    private List<Vector2> ParsePointList(string pointList)
    {
        List<Vector2> points = new List<Vector2>();
        string[] coords = pointList.Split(" ");

        for (int i = 0; i < coords.Length-2; i += 2)
        {
            float x = float.Parse(coords[i]);
            float y = -float.Parse(coords[i + 1]);
            Debug.Log(x + ", " + y);
            points.Add(new Vector2(x, y));
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
                            List<Vector2> verts = ParsePointList(reader.GetAttribute("points"));
                            AddPolygon(verts);
                        }
                        break;
                }
            }
        }
    }
    
    void Start()
    {
        using (FileStream fs = File.OpenRead(Application.dataPath + @"/Resources/worldMap.svg"))
        {
            LoadSVG(fs);
        }
    }
}