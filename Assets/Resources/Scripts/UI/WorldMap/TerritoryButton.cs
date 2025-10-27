using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TerritoryButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    public Color32 borderColor;

    private Color32 polyColor;
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

        Debug.Log(transform.position);

        Vector2 pos = new Vector2(
            a.x,
            a.y + rect.sizeDelta.y
        ) + rect.anchoredPosition;

        borderRect.anchoredPosition = pos;
        line.localDist = b - a;
        line.thickness = 2.3f;
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
        graphic.color = new Color32(255, 0, 0, 255);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        graphic.color = polyColor;
    }
}
