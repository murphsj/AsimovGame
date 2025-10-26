using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A graphic used to draw a user-defined polygonal shape.
/// </summary>
public class PolygonRenderer : ShapeRenderer, ICanvasRaycastFilter
{
    [SerializeField]
    public List<Vector2> verts;

    protected override List<Vector2> GetVertices()
    {
        return verts;
    }
}
