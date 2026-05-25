using VectCore.Core.Geometry;
using VectCore.Core.Mathematics;

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
}