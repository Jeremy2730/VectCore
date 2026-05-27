using VectCore.Core.Shapes;
using VectCore.Core.Mathematics;

namespace VectCore.Core.Scene;

public class SceneObject
{
    public Guid Id { get; }
        = Guid.NewGuid();

    public string Name { get; set; }
        = "Object";

    public Shape Shape { get; set; }

    public Matrix3x3 Transform { get; set; }
        = Matrix3x3.Identity();

    public bool IsSelected
        { get; set; }

    public SceneObject(
        Shape shape)
    {
        Shape = shape;
    }
}