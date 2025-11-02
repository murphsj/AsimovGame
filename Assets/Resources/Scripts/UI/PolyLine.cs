using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A graphic used to draw a polygon-based line.
/// </summary>
public class PolyLine : ShapeRenderer
{
    [SerializeField]
    public Vector2 localDist;

    [SerializeField]
    public float thickness;

    [SerializeField]
    public int circleEdgeCount;

    public override List<Vector2> GetVertices()
    {
        List<Vector2> shape = new List<Vector2>();

        float lookAtAngle = Mathf.Atan2(localDist.x, localDist.y) + Mathf.PI;

        for (int i = 0; i <= circleEdgeCount; i++)
        {
            float theta = (float)i / circleEdgeCount * Mathf.PI - lookAtAngle;
            Vector2 point = new Vector2(Mathf.Cos(theta) * thickness, Mathf.Sin(theta) * thickness);
            shape.Add(point);
        }

        for (int i = 0; i <= circleEdgeCount; i++)
        {
            float theta = (float)i / circleEdgeCount * Mathf.PI - lookAtAngle;
            Vector2 point = localDist
                + new Vector2(-Mathf.Cos(theta) * thickness, -Mathf.Sin(theta) * thickness);
            shape.Add(point);
        }

        shape.Add(shape[0]);

        return shape;
    }
}