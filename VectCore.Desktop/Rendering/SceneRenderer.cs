using VectCore.Core.Scene;
using VectCore.Core.Rendering;
using VectCore.Core.Geometry;

namespace VectCore.Desktop.Rendering;

public static class SceneRenderer
{
    public static void Render(
        Graphics g,
        SceneGraph scene,
        Camera2D camera)
    {
        var cameraMatrix =
            camera.GetViewMatrix();

        foreach (var obj in scene.Objects)
        {
            obj.Shape.Transform =
                obj.Transform;

            var worldVertices =
                obj.Shape
                    .GetTransformedVertices();

            List<Point2D>
                screenVertices = new();

            foreach (var v in worldVertices)
            {
                screenVertices.Add(
                    v.Transform(cameraMatrix)
                );
            }

            ShapeRenderer
                .DrawPolygon(
                    g,
                    screenVertices
                );
        }
    }
}