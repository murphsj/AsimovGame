using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Haze;

/// <summary>
/// A graphic used to draw a single closed polygonal shape based on
/// a list of vertices. Does not support a texture.
/// </summary>
public abstract class ShapeRenderer : MaskableGraphic, ICanvasRaycastFilter
{
    protected List<Triangulator.Triangle> tris;

    /// <summary>
    /// Returns the list of vertices making up the shape
    /// </summary>
    /// <returns></returns>
    protected abstract List<Vector2> GetVertices();

    protected override void OnRectTransformDimensionsChange()
    {
        base.OnRectTransformDimensionsChange();
        SetVerticesDirty();
        SetMaterialDirty();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        tris = Triangulator.TriangulateConvexPolygon(GetVertices());

        // We don't set UV because this will only be used as a mask
        // or a solid colored shape
        // Also because calculating this is really hard
        UIVertex vert = new UIVertex();
        vert.color = base.color;

        Vector2 toScreen = rectTransform.pivot + Vector2.up * rectTransform.rect.height;

        int index = 0;
        foreach (Triangulator.Triangle tri in tris)
        {
            vert.position = tri.a + toScreen;
            vh.AddVert(vert);
            vert.position = tri.b + toScreen;
            vh.AddVert(vert);
            vert.position = tri.c + toScreen;
            vh.AddVert(vert);

            vh.AddTriangle(index, index + 1, index + 2);
            index += 3;
        }
    }

    public bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform, screenPoint, eventCamera, out var local))
            return false;

        local -= Vector2.up * rectTransform.rect.height;

        // Debug
        foreach (Triangulator.Triangle tri in tris)
        {
            Debug.DrawLine(tri.a, tri.b);
            Debug.DrawLine(tri.b, tri.c);
            Debug.DrawLine(tri.c, tri.a);
        }

        Vector2 xoffset = new Vector2(1f, 0);
        Vector2 yoffset = new Vector2(0, 1f);
        Debug.DrawLine(local - xoffset, local + xoffset, Color.cyan);
        Debug.DrawLine(local - yoffset, local + yoffset, Color.cyan);

        foreach (Triangulator.Triangle tri in tris)
        {
            if (local.PointInTriangle2D(tri.a, tri.b, tri.c))
            {
                return true;
            }
        }

        return false;
    }
}
