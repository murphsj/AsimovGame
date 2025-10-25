using UnityEngine;

public static class Vector2Extensions {
    /// <summary>
    /// Return whether a point is inside a triangle in 2D
    /// </summary>
    /// <param name="p">Point to test</param>
    /// <param name="t0">Vertex of the triangle</param>
    /// <param name="t1">Vertex of the triangle</param>
    /// <param name="t3">Vertex of the triangle</param>
    /// <returns></returns>
    public static bool PointInTriangle2D(this Vector2 p, Vector2 t0, Vector2 t1, Vector2 t3) {
        var s = t0.y * t3.x - t0.x * t3.y + (t3.y - t0.y) * p.x + (t0.x - t3.x) * p.y;
        var t = t0.x * t1.y - t0.y * t1.x + (t0.y - t1.y) * p.x + (t1.x - t0.x) * p.y;

        if (s < 0 != t < 0)
            return false;

        var A = -t1.y * t3.x + t0.y * (t3.x - t1.x) + t0.x * (t1.y - t3.y) + t1.x * t3.y;

        return A < 0 ?
            s <= 0 && s + t >= A :
            s >= 0 && s + t <= A;
    }    
}