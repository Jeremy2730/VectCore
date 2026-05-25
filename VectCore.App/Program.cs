using VectCore.Core.Shapes;
using VectCore.Core.Transformations;

RectangleShape rect =
    new RectangleShape(100, 50);

var transform =
    TransformationMatrix
        .CreateTranslation(200, 100)
        .Multiply(
            TransformationMatrix
                .CreateRotation(45)
        );

rect.Transform = transform;

Console.WriteLine(
    "Rectangle Vertices:"
);

foreach (var point in
         rect.GetTransformedVertices())
{
    Console.WriteLine(point);
}