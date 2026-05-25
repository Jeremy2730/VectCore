using VectCore.Core.Mathematics;
using VectCore.Core.Transformations;

namespace VectCore.Core.Rendering;

public class Camera2D
{
    public double X { get; set; }

    public double Y { get; set; }

    public double Zoom { get; set; } = 1.0;

    public Matrix3x3 GetViewMatrix()
    {
        var translation =
            TransformationMatrix
                .CreateTranslation(
                    -X,
                    -Y
                );

        var scale =
            TransformationMatrix
                .CreateScale(
                    Zoom,
                    Zoom
                );

        return scale.Multiply(translation);
    }
}