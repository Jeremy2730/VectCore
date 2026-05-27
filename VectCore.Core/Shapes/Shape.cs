using VectCore.Core.Geometry;
using VectCore.Core.Mathematics;
using VectCore.Core.HitTesting;

namespace VectCore.Core.Shapes;

public abstract class Shape
{
    public Matrix3x3 Transform { get; set; }
        = Matrix3x3.Identity();

    public abstract List<Point2D>
        GetVertices();

    public List<Point2D>
        GetTransformedVertices()
    {
        List<Point2D> result = new();

        foreach (Point2D vertex in GetVertices())
        {
            result.Add(
                vertex.Transform(Transform)
            );
        }

        return result;
    }

    public virtual BoundingBox
        GetBoundingBox()
    {
        var vertices =
            GetTransformedVertices();

        double minX =
            vertices.Min(v => v.X);

        double minY =
            vertices.Min(v => v.Y);

        double maxX =
            vertices.Max(v => v.X);

        double maxY =
            vertices.Max(v => v.Y);

        return new BoundingBox
        {
            MinX = minX,
            MinY = minY,
            MaxX = maxX,
            MaxY = maxY
        };
    }
}