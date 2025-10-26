using System.Collections.Generic;
using UnityEngine;

public class PolygonRenderer : ShapeRenderer, ICanvasRaycastFilter
{
    [SerializeField]
    protected List<Vector2> verts;

    protected override List<Vector2> GetVertices()
    {
        return verts;
    }
}
