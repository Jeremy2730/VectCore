using VectCore.Core.Geometry;

namespace VectCore.Core.Shapes;

public class RectangleShape : Shape
{
    public double Width { get; set; }

    public double Height { get; set; }

    public RectangleShape(
        double width,
        double height)
    {
        Width = width;
        Height = height;
    }

    public override List<Point2D>
        GetVertices()
    {
        return new List<Point2D>
        {
            new Point2D(0,0),

            new Point2D(Width,0),

            new Point2D(Width,Height),

            new Point2D(0,Height)
        };
    }
}