using System.Drawing;
using VectCore.Core.Shapes;
using VectCore.Core.Transformations;
using VectCore.Core.Rendering;

namespace VectCore.Desktop;

public partial class Form1 : Form
{
    private RectangleShape rectangle;

    private System.Windows.Forms.Timer timer;

    private float angle = 0;
    private Camera2D camera;
    private bool isPanning = false;

    private Point lastMousePosition;

    public Form1()
    {
        InitializeComponent();

        camera = new Camera2D();

        camera.X = -200;
        camera.Y = -100;

        camera.Zoom = 1.0;
        MouseDown += OnMouseDown;
        MouseUp += OnMouseUp;
        MouseMove += OnMouseMovePan;

        DoubleBuffered = true;

        Width = 1000;
        Height = 700;

        rectangle = new RectangleShape(
            200,
            100
        );

        timer = new System.Windows.Forms.Timer();

        timer.Interval = 16;

        timer.Tick += UpdateScene;

        timer.Start();
        MouseWheel += OnMouseWheelZoom;
    }

    private void UpdateScene(
        object? sender,
        EventArgs e)
    {
        angle += 1;

        rectangle.Transform =
            TransformationMatrix
                .CreateTranslation(400, 300)
                .Multiply(
                    TransformationMatrix
                        .CreateRotation(angle)
                );

        Invalidate();
    }

    private void OnMouseWheelZoom(
        object? sender,
        MouseEventArgs e)
    {
        if (e.Delta > 0)
        {
            camera.Zoom *= 1.1;
        }
        else
        {
            camera.Zoom *= 0.9;
        }

        Invalidate();
    }

    private void OnMouseDown(
        object? sender,
        MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isPanning = true;

            lastMousePosition = e.Location;
        }
    }

    private void OnMouseUp(
        object? sender,
        MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isPanning = false;
        }
    }

    private void OnMouseMovePan(
        object? sender,
        MouseEventArgs e)
    {
        if (!isPanning)
            return;

        int dx =
            e.X - lastMousePosition.X;

        int dy =
            e.Y - lastMousePosition.Y;

        camera.X -= dx / camera.Zoom;

        camera.Y -= dy / camera.Zoom;

        lastMousePosition = e.Location;

        Invalidate();
    }

    protected override void OnPaint(
        PaintEventArgs e)
    {
        base.OnPaint(e);

        Graphics g = e.Graphics;

        g.Clear(Color.White);

        int gridSize = 50;

        Pen gridPen = new Pen(
            Color.LightGray,
            1
        );

        for (int x = 0; x < Width; x += gridSize)
        {
            g.DrawLine(
                gridPen,
                x,
                0,
                x,
                Height
            );
        }

        for (int y = 0; y < Height; y += gridSize)
        {
            g.DrawLine(
                gridPen,
                0,
                y,
                Width,
                y
            );
        }

        var worldVertices =
            rectangle
                .GetTransformedVertices();

        var cameraMatrix =
            camera.GetViewMatrix();

        List<VectCore.Core.Geometry.Point2D>
            vertices = new();

        foreach (var v in worldVertices)
        {
            vertices.Add(
                v.Transform(cameraMatrix)
            );
        }

        PointF[] points =
        [
            new PointF(
                (float)vertices[0].X,
                (float)vertices[0].Y),

            new PointF(
                (float)vertices[1].X,
                (float)vertices[1].Y),

            new PointF(
                (float)vertices[2].X,
                (float)vertices[2].Y),

            new PointF(
                (float)vertices[3].X,
                (float)vertices[3].Y)
        ];

        g.DrawPolygon(
            Pens.Black,
            points
        );
    }
}