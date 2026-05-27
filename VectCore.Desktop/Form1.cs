using System.Drawing;
using VectCore.Core.Shapes;
using VectCore.Core.Transformations;
using VectCore.Core.Rendering;
using VectCore.Core.Scene;
using VectCore.Desktop.Rendering;
using VectCore.Core.Selection;
using VectCore.Core.Geometry;

namespace VectCore.Desktop;

public partial class Form1 : Form
{
    private SceneGraph scene;
    private System.Windows.Forms.Timer timer;

    private float angle = 0;
    private Camera2D camera;
    private bool isPanning = false;

    private Point lastMousePosition;

    private SelectionManager
        selectionManager;

    private bool
        isDraggingObject = false;

    private Point2D
        lastWorldMousePosition =
            new Point2D(0, 0);
      

    public Form1()
    {
        InitializeComponent();
        
        selectionManager =
            new SelectionManager();

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

        scene = new SceneGraph();

        RectangleShape rect =
            new RectangleShape(200, 100);

        SceneObject obj =
            new SceneObject(rect);

        scene.Add(obj);

        timer = new System.Windows.Forms.Timer();

        //timer.Interval = 16;

        //timer.Tick += UpdateScene;

        //timer.Start();
        MouseWheel += OnMouseWheelZoom;
    }

    private void UpdateScene(
        object? sender,
        EventArgs e)
    {
        angle += 1;

        SceneObject obj =
            scene.Objects[0];

        obj.Transform =
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
        Point2D worldPoint =
            camera.ScreenToWorld(
                e.X,
                e.Y
            );

        if (e.Button == MouseButtons.Left)
        {
            selectionManager.SelectAt(
                scene,
                worldPoint
            );

            lastWorldMousePosition =
                worldPoint;

            if (selectionManager.SelectedObject != null)
            {
                isDraggingObject = true;
            }

            Invalidate();
        }

        if (e.Button == MouseButtons.Middle)
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
            isDraggingObject = false;
        }

        if (e.Button == MouseButtons.Middle)
        {
            isPanning = false;
        }
    }

    private void OnMouseMovePan(
        object? sender,
        MouseEventArgs e)
    {
        Point2D worldPoint =
            camera.ScreenToWorld(
                e.X,
                e.Y
            );

        if (isDraggingObject &&
            e.Button == MouseButtons.Left &&
            selectionManager.SelectedObject != null)
        {
            double dx =
                worldPoint.X -
                lastWorldMousePosition.X;

            double dy =
                worldPoint.Y -
                lastWorldMousePosition.Y;

            var translation =
                TransformationMatrix
                    .CreateTranslation(
                        dx,
                        dy
                    );

            selectionManager
                .SelectedObject
                .Transform =
                    translation.Multiply(
                        selectionManager
                            .SelectedObject
                            .Transform
                    );

            lastWorldMousePosition =
                worldPoint;

            Invalidate();

            return;
        }

        if (!isPanning)
            return;

        int panDx =
            e.X - lastMousePosition.X;

        int panDy =
            e.Y - lastMousePosition.Y;

        camera.X -= panDx / camera.Zoom;

        camera.Y -= panDy / camera.Zoom;

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

        SceneRenderer.Render(
            g,
            scene,
            camera
        );
    }
}