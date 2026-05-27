using VectCore.Core.Geometry;

namespace VectCore.Core.Shapes;

public class CircleShape : Shape
{
    public double Radius { get; set; }

    public int Segments { get; set; }
        = 32;

    public CircleShape(
        double radius)
    {
        Radius = radius;
    }

    public override List<Point2D>
        GetVertices()
    {
        List<Point2D> vertices
            = new();

        double step =
            (System.Math.PI * 2)
            / Segments;

        for (int i = 0; i < Segments; i++)
        {
            double angle =
                i * step;

            double x =
                Radius *
                System.Math.Cos(angle);

            double y =
                Radius *
                System.Math.Sin(angle);

            vertices.Add(
                new Point2D(x, y)
            );
        }

        return vertices;
    }
}