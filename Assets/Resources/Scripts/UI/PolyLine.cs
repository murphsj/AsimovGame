using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PolyLine : ShapeRenderer
{
    [SerializeField]
    Vector2 start;

    [SerializeField]
    Vector2 end;

    [SerializeField]
    float thickness;

    [SerializeField]
    int circleEdgeCount;

    protected override List<Vector2> GetVertices()
    {
        List<Vector2> shape = new List<Vector2>();
        Vector2 localDist = end - start;

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



        return shape;
    }
}