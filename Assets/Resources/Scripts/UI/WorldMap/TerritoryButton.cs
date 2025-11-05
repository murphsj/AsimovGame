using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TerritoryButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    public Color32 borderColor;

    [SerializeField]
    public float lineThickness;

    [SerializeField]
    public Image stripeImage;

    public Territory territory;

    private Color polyColor;
    private PolygonRenderer graphic;
    private RectTransform rect;
    private List<PolyLine> borders;

    void Start()
    {
        graphic = GetComponent<PolygonRenderer>();
        polyColor = graphic.color;
        rect = GetComponent<RectTransform>();
        borders = new List<PolyLine>();
        MakeBorders();
    }

    public void UpdateHoverFlash(float flashBrightness, float baseBrightness)
    {
        float brightness = Mathf.Abs(Mathf.Sin(Time.time * 3)) * flashBrightness + baseBrightness;
        graphic.color = Color.Lerp(polyColor, Color.white, brightness);
        SetOutlineColor(Color.Lerp(borderColor, Color.white, brightness));
    }

    public void Unhover()
    {
        graphic.color = polyColor;
        SetOutlineColor(borderColor);
    }
    
    private void SetOutlineColor(Color color)
    {
        foreach (PolyLine line in borders)
        {
            line.color = color;
        }

        stripeImage.color = color;
    }

    private void MakeBorderLine(Vector2 a, Vector2 b)
    {
        GameObject border = new GameObject("Line");
        border.AddComponent<CanvasRenderer>();

        RectTransform borderRect = border.AddComponent<RectTransform>();
        PolyLine line = border.AddComponent<PolyLine>();
        border.transform.SetParent(transform.parent);

        borderRect.localScale = new Vector2(1f, 1f);
        borderRect.pivot = new Vector2(0, 0);
        borderRect.sizeDelta = new Vector2(0, 0);

        Vector2 pos = new Vector2(
            a.x,
            a.y + rect.sizeDelta.y
        ) + rect.anchoredPosition;

        borderRect.anchoredPosition = pos;
        line.localDist = b - a;
        line.thickness = lineThickness;
        line.circleEdgeCount = 5;

        line.color = borderColor;

        borders.Add(line);
    }

    private void MakeBorders()
    {
        List<Vector2> verts = graphic.verts;
        for (int i = 0; i < verts.Count - 1; i++)
        {
            Vector2 a = verts[i];
            Vector2 b = verts[i + 1];

            MakeBorderLine(a, b);
        }

        MakeBorderLine(verts[0], verts[verts.Count - 1]);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MapManager.instance.OnHoverTerritory(territory);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MapManager.instance.OnUnhoverTerritory(territory);
    }
}
