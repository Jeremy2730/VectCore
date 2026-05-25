using VectCore.Core.Mathematics;

namespace VectCore.Core.Geometry;

public class Point2D
{
    public double X { get; set; }

    public double Y { get; set; }

    public Point2D(double x, double y)
    {
        X = x;
        Y = y;
    }

    public Point2D Transform(Matrix3x3 matrix)
    {
        double newX =
            X * matrix.Values[0, 0] +
            Y * matrix.Values[0, 1] +
                matrix.Values[0, 2];

        double newY =
            X * matrix.Values[1, 0] +
            Y * matrix.Values[1, 1] +
                matrix.Values[1, 2];

        return new Point2D(newX, newY);
    }

    public override string ToString()
    {
        return $"({X:F2}, {Y:F2})";
    }
}