using VectCore.Core.Geometry;
using VectCore.Core.Scene;

namespace VectCore.Core.Selection;

public class SelectionManager
{
    public SceneObject?
        SelectedObject { get; private set; }

    public void SelectAt(
        SceneGraph scene,
        Point2D point)
    {
        SelectedObject = null;

        foreach (var obj in scene.Objects)
        {
            obj.Shape.Transform =
                obj.Transform;

            var bounds =
                obj.Shape
                    .GetBoundingBox();

            if (bounds.Contains(point))
            {
                SelectedObject = obj;

                obj.IsSelected = true;
            }
            else
            {
                obj.IsSelected = false;
            }
        }
    }
}