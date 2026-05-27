using VectCore.Core.Geometry;

namespace VectCore.Core.HitTesting;

public class BoundingBox
{
    public double MinX { get; set; }

    public double MinY { get; set; }

    public double MaxX { get; set; }

    public double MaxY { get; set; }

    public bool Contains(
        Point2D point)
    {
        return point.X >= MinX &&
               point.X <= MaxX &&
               point.Y >= MinY &&
               point.Y <= MaxY;
    }
}