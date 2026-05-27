using System.Drawing;
using VectCore.Core.Geometry;

namespace VectCore.Desktop.Rendering;

public static class ShapeRenderer
{
    public static void DrawPolygon(
        Graphics g,
        List<Point2D> vertices)
    {
        PointF[] points =
            vertices
                .Select(v =>
                    new PointF(
                        (float)v.X,
                        (float)v.Y
                    )
                )
                .ToArray();

        g.DrawPolygon(
            Pens.Black,
            points
        );
    }
}