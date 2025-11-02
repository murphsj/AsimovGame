using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A graphic used to draw a user-defined polygonal shape.
/// </summary>
public class PolygonRenderer : ShapeRenderer, ICanvasRaycastFilter
{
    [SerializeField]
    public List<Vector2> verts;

    private RectTransform rect;

    protected override void Start()
    {
        base.Start();

        rect = GetComponent<RectTransform>();
    }

    public override List<Vector2> GetVertices()
    {
        return verts;
    }

    /// <summary>
    /// Sets this renderer's mesh to the provided points in world space.
    /// Adjusts the object's transform so it is positioned correctly.
    /// </summary>
    /// <param name="worldVerts"></param>
    public void ApplyVerticesWorldSpace(List<Vector2> worldVerts)
    {
        if (rect == null) rect = GetComponent<RectTransform>();

        (Vector2 topLeft, Vector2 bottomRight) = Vector2Extensions.GetExtents(worldVerts);

        for (int i = 0; i < worldVerts.Count; i++)
        {
            verts.Add(worldVerts[i] - topLeft);
        }

        Vector3 size = new Vector3(bottomRight.x - topLeft.x, topLeft.y - bottomRight.y, 1);
        rect.sizeDelta = size;
        rect.localScale = new Vector3(1f, 1f, 1f);
        rect.pivot = new Vector2(0f, 0f);
        rect.anchoredPosition = new Vector2(topLeft.x, topLeft.y - size.y);
    }

    public List<Vector2> GetVerticesWorldSpace()
    {
        if (rect == null) rect = GetComponent<RectTransform>();

        List<Vector2> answer = new List<Vector2>();
        List<Vector2> verts = GetVertices();
        (Vector2 topLeft, Vector2 bottomRight) = Vector2Extensions.GetExtents(verts);
        float ySize = topLeft.y - bottomRight.y;
        

        foreach (Vector2 vert in verts)
        {
            answer.Add(vert + rect.anchoredPosition + Vector2.up * ySize);
        }

        return answer;
    }
}
