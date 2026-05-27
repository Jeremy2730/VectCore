namespace VectCore.Core.Scene;

public class SceneGraph
{
    public List<SceneObject> Objects
        { get; } = new();

    public void Add(
        SceneObject obj)
    {
        Objects.Add(obj);
    }
}
