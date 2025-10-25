using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Haze;

public class PolygonRenderer : MaskableGraphic
{
    [SerializeField]
    List<Vector2> verts;

    protected override void OnRectTransformDimensionsChange()
    {
        base.OnRectTransformDimensionsChange();
        SetVerticesDirty();
        SetMaterialDirty();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        List<Triangulator.Triangle> tris = Triangulator.Triangulate(verts);

        // We don't set UV because this will only be used as a mask
        UIVertex vert = new UIVertex();
        vert.color = base.color;

        int index = 0;
        foreach (Triangulator.Triangle tri in tris)
        {
            vert.position = tri.a + rectTransform.pivot;
            vh.AddVert(vert);
            vert.position = tri.b + rectTransform.pivot;
            vh.AddVert(vert);
            vert.position = tri.c + rectTransform.pivot;
            vh.AddVert(vert);

            vh.AddTriangle(index, index + 1, index + 2);
            index += 3;
        }
    }
}
